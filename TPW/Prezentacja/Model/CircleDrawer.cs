using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using TPW.Dane;
using TPW.Logika;
using TPW.View;

namespace TPW.Prezentacja.Model
{
    public class CircleDrawer
    {
        private Dictionary<Ellipse, Circle> ellipseCircleDict;
        private readonly object lockObject = new object();
        private Canvas canvas;
        private SimWindow simWindow;

        public CircleDrawer(Canvas canvas, SimWindow simWindow)
        {
            this.canvas = canvas;
            this.simWindow = simWindow;
            ellipseCircleDict = new Dictionary<Ellipse, Circle>();
        }

        public void DrawCircles(Circles circles)
        {
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
                canvas.Children.Add(ellipse);

                ellipseCircleDict[ellipse] = circle;
            }
        }

        public Dictionary<Ellipse, Circle> GetEllipseCircleDict()
        {
            return ellipseCircleDict;
        }

        public object GetLockObject()
        {
            return lockObject;
        }

        public void UpdateEllipsePosition(Ellipse ellipse, Circle circle)
        {
            Canvas.SetLeft(ellipse, circle.getx() - circle.getRadius());
            Canvas.SetTop(ellipse, circle.gety() - circle.getRadius());
        }

        public double GetCanvasWidth()
        {
            return canvas.ActualWidth;
        }

        public double GetCanvasHeight()
        {
            return canvas.ActualHeight - SystemParameters.WindowCaptionHeight;
        }

        public void InvokeOnUIThread(Action action)
        {
            simWindow.Dispatcher.Invoke(action);
        }
    }
}
