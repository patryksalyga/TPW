using Accord.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using TPW.Model;
using TPW.View;

namespace TPW.ViewModel
{
    public class SimViewModel
    {
        public SimViewModel(int n, SimWindow simWindow)
        {
            simWindow.Show();
            Circles circles = new Circles(n, simWindow.ActualHeight, simWindow.ActualWidth);

            Dictionary<Ellipse, Circle> ellipseCircleDict = new Dictionary<Ellipse, Circle>();

            foreach (var circle in circles.CirclesList)
            {
                var ellipse = new Ellipse
                {
                    Width = circle.getRadius() * 2,
                    Height = circle.getRadius() * 2,
                    Fill = Brushes.Blue,
                };
                Canvas.SetLeft(ellipse, circle.getx() - circle.getRadius());
                Canvas.SetTop(ellipse, circle.gety() - circle.getRadius());
                simWindow.MyCanvas.Children.Add(ellipse);

                ellipseCircleDict[ellipse] = circle;
            }

            foreach (var pair in ellipseCircleDict)
            {
                Task.Run(() =>
                {
                    while (true)
                    {
                        var ellipse = pair.Key;
                        var circle = pair.Value;

                        circle.update();

                        // Create a lock object
                        object lockObject = new object();

                        lock (lockObject) //ellipseCircleDict jest współdzielonym zasobem, który wymaga synchronizacji (warunki wyścigu, gdzie dwa wątki próbują jednocześnie modyfikować ten sam okrąg)
                        {
                            // Check for collisions with other circles
                            foreach (var otherPair in ellipseCircleDict)
                            {
                                if (otherPair.Key != ellipse)
                                {
                                    var otherCircle = otherPair.Value;
                                    if (circle.isCollidingWith(otherCircle))
                                    {
                                        circle.HandleCollision(otherCircle);
                                    }
                                }
                            }

                            // Check for collisions with walls
                            if (circle.getx() - circle.getRadius() < 0 || circle.getx() + circle.getRadius() > simWindow.ActualWidth)
                            {
                                circle.reverseXVelocity();
                            }
                            if (circle.gety() - circle.getRadius() < 0 || circle.gety() + circle.getRadius() > simWindow.ActualHeight - SystemParameters.WindowCaptionHeight)
                            {
                                circle.reverseYVelocity();
                            }
                        }

                        simWindow.Dispatcher.Invoke(() =>
                        {
                            Canvas.SetLeft(ellipse, circle.getx() - circle.getRadius());
                            Canvas.SetTop(ellipse, circle.gety() - circle.getRadius());
                        });

                        Task.Delay(20).Wait();
                    }
                });

            }
        }
    }


}

