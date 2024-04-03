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

        public Circle(double height, double width)
        {
            Random rnd = new Random();
            x = rnd.NextDouble() * width;
            y = rnd.NextDouble() * height;
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
    }
}
