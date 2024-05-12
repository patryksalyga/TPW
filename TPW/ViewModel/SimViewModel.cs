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
            simWindow.Loaded += (s, e) =>
            {

                Circles circles = new Circles(n, simWindow.ActualHeight - SystemParameters.WindowCaptionHeight, simWindow.ActualWidth);

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

                // Create a barrier with a participant count of the number of circles
                Barrier barrier = new Barrier(ellipseCircleDict.Count);

                // Create a lock object
                object lockObject = new object();

                foreach (var pair in ellipseCircleDict)
                {
                    Task.Run(() =>
                    {
                        while (true)
                        {
                            var ellipse = pair.Key;
                            var circle = pair.Value;

                            circle.update();

                            lock (lockObject) //ellipseCircleDict nie chcemy aby dwa watki jednoczenie modyfikowały pozycje elpse w liscie
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
                            }
                                // Check for collisions with walls
                                if (circle.getx() - circle.getRadius() < 0 || circle.getx() + circle.getRadius() > simWindow.ActualWidth)
                                {
                                    circle.reverseXVelocity(simWindow.ActualWidth);
                                }
                                if (circle.gety() - circle.getRadius() < 0 || circle.gety() + circle.getRadius() > simWindow.ActualHeight - SystemParameters.WindowCaptionHeight)
                                {
                                    circle.reverseYVelocity(simWindow.ActualHeight - SystemParameters.WindowCaptionHeight);
                                }
                            

                            // Signal the barrier that this thread is done
                            barrier.SignalAndWait(); //nierowne rozpoczecie

                            simWindow.Dispatcher.Invoke(() =>
                            {
                                Canvas.SetLeft(ellipse, circle.getx() - circle.getRadius());
                                Canvas.SetTop(ellipse, circle.gety() - circle.getRadius());
                            });

                            Task.Delay(20).Wait();
                        }
                    });
                }
            };
            simWindow.Show();

        }
    }


}

