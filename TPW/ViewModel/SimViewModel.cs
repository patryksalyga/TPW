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

            new Thread(() =>
            {
                while (true)
                {
                    circles.updateCircles();

                    simWindow.Dispatcher.Invoke(() =>
                    {
                        foreach (var pair in ellipseCircleDict)
                        {
                            var ellipse = pair.Key;
                            var circle = pair.Value;
                            Canvas.SetLeft(ellipse, circle.getx() - circle.getRadius());
                            Canvas.SetTop(ellipse, circle.gety() - circle.getRadius());
                        }
                    });

                    Thread.Sleep(20);
                }
            }).Start();
        }

    }
}
