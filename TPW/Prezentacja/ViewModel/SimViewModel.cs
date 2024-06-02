using Accord.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Shapes;
using TPW.Dane;
using TPW.Logika;
using TPW.Prezentacja.Model;
using TPW.View;

namespace TPW.ViewModel
{
    public class SimViewModel
    {
        private System.Timers.Timer timer; // Pełna kwalifikacja dla Timer
        private CircleDrawer circleDrawer;
        private readonly object lockObject = new object();
        private string filePath = "circles_info.txt";

        public SimViewModel(int n, SimWindow simWindow)
        {
            simWindow.Loaded += (s, e) =>
            {
                Circles circles = new Circles(n, simWindow.ActualHeight - SystemParameters.WindowCaptionHeight, simWindow.ActualWidth);

                circleDrawer = new CircleDrawer(simWindow.MyCanvas, simWindow);
                circleDrawer.DrawCircles(circles);

                Dictionary<Ellipse, Circle> ellipseCircleDict = circleDrawer.GetEllipseCircleDict();
                object lockObject = ellipseCircleDict;

                // Create a barrier with a participant count of the number of circles
                Barrier barrier = new Barrier(ellipseCircleDict.Count);

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
                            if (circle.getx() - circle.getRadius() < 0 || circle.getx() + circle.getRadius() > circleDrawer.GetCanvasWidth())
                            {
                                circle.reverseXVelocity(circleDrawer.GetCanvasWidth());
                            }
                            if (circle.gety() - circle.getRadius() < 0 || circle.gety() + circle.getRadius() > circleDrawer.GetCanvasHeight())
                            {
                                circle.reverseYVelocity(circleDrawer.GetCanvasHeight());
                            }

                            // Signal the barrier that this thread is done
                            barrier.SignalAndWait(); //nierowne rozpoczecie

                            circleDrawer.InvokeOnUIThread(() =>
                            {
                                circleDrawer.UpdateEllipsePosition(ellipse, circle);
                            });

                            Task.Delay(20).Wait();
                        }
                    });
                }

                // Initialize and start the timer for saving to file every 2 seconds
                timer = new System.Timers.Timer(2000); // Pełna kwalifikacja dla Timer
                timer.Elapsed += SaveCirclesInfoToFile;
                timer.AutoReset = true;
                timer.Enabled = true;
            };
            simWindow.Show();
        }

        private void SaveCirclesInfoToFile(object sender, ElapsedEventArgs e)
        {
            lock (lockObject)
            {
                using (StreamWriter writer = new StreamWriter(filePath, false))
                {
                    foreach (var pair in circleDrawer.GetEllipseCircleDict())
                    {
                        var circle = pair.Value;
                        writer.WriteLine($"Circle at ({circle.getx()}, {circle.gety()}) with radius {circle.getRadius()}");
                    }
                    writer.WriteLine("-----");
                }
            }
        }
    }
}
