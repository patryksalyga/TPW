using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPW.Model
{
    public class Circle
    {
        double x;
        double y;
        double Radius = 25;
        double speedX;
        double speedY;

        public Circle(double height, double width)
        {
            Random rnd = new Random();
            x = rnd.NextDouble() * width;
            y = rnd.NextDouble() * height;
            speedX = rnd.NextDouble() * 6 - 3; //<-10;10>
            speedY = rnd.NextDouble() * 6 - 3; 
            Debug.WriteLine(x + " " + y + " " + height + " " + width);
        }

        public double getx()
        {
            return x;
        }

        public double gety()
        {
            return y;
        }

        public double getRadius()
        {
            return Radius;
        }

        public double getSpeedX()
        {
            return speedX;
        }

        public double getSpeedY()
        {
            return speedY;
        }

        public void update()
        {
            x = x + speedX;
            y = y + speedY;
        }
    }
}
