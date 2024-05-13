using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPW.Model
{
    public class Circles
    {
        public List<Circle> CirclesList { get; private set; }

        
        public Circles(int n, double height, double width)
        {
            CirclesList = new List<Circle>();
            bool flag;

            for (int i = 0; i < n; i++)
            {
                Circle NewCircle = new Circle(height, width);

                flag = false;
                for(int j = 0; j < CirclesList.Count; j++)
                {
                    if (NewCircle.isCollidingWith(CirclesList[j]))
                    {
                        flag = true;
                    }
                }

                if(!flag)
                {
                    CirclesList.Add(NewCircle);
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
