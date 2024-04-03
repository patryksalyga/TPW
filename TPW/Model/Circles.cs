using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPW.Model
{
    class Circles
    {
        public List<Circle> CirclesList { get; private set; }

        public Circles(int n, double height, double width)
        {
            CirclesList = new List<Circle>();

            for (int i = 0; i < n; i++)
            {
                CirclesList.Add(new Circle(height, width));
            }
        }

    }
}
