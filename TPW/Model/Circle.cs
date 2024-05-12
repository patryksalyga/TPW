using Accord.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPW.Model
{
    public class Circle : BinaryNode<Circle>
    {
        double x;
        double y;
        double Radius;
        double speedX;
        double speedY;

        public Circle(double height, double width)
        {
            Random rnd = new Random();
            Radius = rnd.NextDouble() * 30 + 20; //<20;50>
            x = rnd.NextDouble() * ((width - Radius) - Radius) + Radius;
            y = rnd.NextDouble() * ((height - Radius) - Radius) + Radius;
            speedX = rnd.NextDouble() * 10 - 5; //<-5;5>
            speedY = rnd.NextDouble() * 10 - 5; 
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

        public void setSpeedX(double speedX) 
        {
            this.speedX = speedX;
        }

        public void setSpeedY(double speedY)
        {
            this.speedY = speedY;
        }

        public void setX(double x)
        {
            this.x = x;
        }

        public void setY(double y)
        {
            this.y = y;
        }

        public void HandleCollision(Circle other)
        {
            // Calculate the distance between the circles
            double dx = other.getx() - this.getx();
            double dy = other.gety() - this.gety();
            double distance = Math.Sqrt(dx * dx + dy * dy);

            // Calculate the angle of collision
            double angle = Math.Atan2(dy, dx);

            // Calculate the velocities in the x and y directions for each circle
            double v1x = this.speedX * Math.Cos(angle) + this.speedY * Math.Sin(angle);
            double v1y = this.speedY * Math.Cos(angle) - this.speedX * Math.Sin(angle);
            double v2x = other.getSpeedX() * Math.Cos(angle) + other.getSpeedY() * Math.Sin(angle);
            double v2y = other.getSpeedY() * Math.Cos(angle) - other.getSpeedX() * Math.Sin(angle);

            // Calculate the mass of the circles
            double m1 = this.getRadius() / 10; // 10 radius = 1kg
            double m2 = other.getRadius() / 10; // 10 radius = 1kg

            // Calculate the final velocities after the collision
            double final_v1x = ((m1 - m2) * v1x + 2 * m2 * v2x) / (m1 + m2);
            double final_v2x = ((m2 - m1) * v2x + 2 * m1 * v1x) / (m1 + m2);

            // Update the velocities of the circles
            this.speedX = Math.Cos(angle) * final_v1x - Math.Sin(angle) * v1y;
            this.speedY = Math.Sin(angle) * final_v1x + Math.Cos(angle) * v1y;
            other.setSpeedX(Math.Cos(angle) * final_v2x - Math.Sin(angle) * v2y);
            other.setSpeedY(Math.Sin(angle) * final_v2x + Math.Cos(angle) * v2y);

            // Calculate the overlap between the circles (how much one circle
            // has moved into the other)
            double overlap = 0.5 * (distance - this.getRadius() - other.getRadius());

            // Adjust the positions of the circles so they are no longer overlapping
            this.setX(this.getx() - overlap * (this.getx() - other.getx()) / distance);
            this.setY(this.gety() - overlap * (this.gety() - other.gety()) / distance);
            other.setX(other.getx() + overlap * (this.getx() - other.getx()) / distance);
            other.setY(other.gety() + overlap * (this.gety() - other.gety()) / distance);
        }

        internal bool isCollidingWith(Circle otherCircle)
        {
            double dx = this.x - otherCircle.getx();
            double dy = this.y - otherCircle.gety();
            double distance = Math.Sqrt(dx * dx + dy * dy);

            return distance <= (this.Radius + otherCircle.getRadius());
        }

        internal void reverseXVelocity()
        {
            this.speedX =  - this.speedX;
        }

        internal void reverseYVelocity()
        {
            this.speedY = - this.speedY;
        }
    }
}
