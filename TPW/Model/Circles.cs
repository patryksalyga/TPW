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
            Boolean flag;

            for (int i = 0; i < n; i++)
            {
                flag = false;
                for(int j = 0; j < CirclesList.Count; j++)
                {
                    Circle NewCircle = new Circle(height, width);
                    if (NewCircle.isCollidingWith(CirclesList[j]))
                    {
                        flag = true;
                    }

                }
                if(!flag)
                {
                    CirclesList.Add(new Circle(height, width));
                }
                else
                {
                    i--;
                }
            }
        }

        public void updateCircles()
        {
            foreach (var circle in CirclesList)
            {
                circle.update();
            }
        }

    }
}
