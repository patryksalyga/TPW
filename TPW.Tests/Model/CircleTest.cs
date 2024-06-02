using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPW.Dane;
using TPW.Logika;

namespace TPW.Tests.Model
{
    public class CircleTest
    {
       private readonly Circle _circle;
       private readonly Circle _circle2;
        private readonly Circles _circles;
        private int n = 5;
        private double X = 500;
        private double Y = 500;

       public CircleTest()
        {
            _circle = new Circle(X, Y);
            _circles = new Circles(n, X, Y);
        }

        [Fact]
        public void CircleExistsTest()
        {
            Assert.True(_circle != null);
        }

        [Fact]
        public void CircleRandTest() 
        {
            
            Assert.True(_circle.getx() >= 0 && _circle.getx() <= X);
            Assert.True(_circle.gety() >= 0 && _circle.gety() <= Y);
        }

        [Fact]
        public void CirclesEqualTest()
        {
            Assert.NotEqual(_circle, _circle2);
        }

        [Fact]
        public void CirclesisCollidingWith()
        {
            Assert.True(_circle.isCollidingWith(_circle));
        }

        [Fact]
        public void reverseVelocity()
        {
            double speed = _circle.getSpeedX();
            _circle.reverseXVelocity(X);
            Assert.Equal(speed * -1, _circle.getSpeedX());
            speed = _circle.getSpeedY();
            _circle.reverseYVelocity(Y);
            Assert.Equal(speed * -1, _circle.getSpeedY());
        }

        [Fact]
        public void CircleUpdateTest()
        {
            double updatedX = _circle.getx() + _circle.getSpeedX();
            double updatedY = _circle.gety() + _circle.getSpeedY();
            _circle.update();
            Assert.Equal(updatedX, _circle.getx());
            Assert.Equal(updatedY, _circle.gety());
        }

        [Fact]
        public void CirclesCountTest()
        {
            Assert.Equal(_circles.CirclesList.Count, n);
        }

        [Fact]
        public void CirclesConstructorTest()
        {
            for(int i = 0; i < n; i++)
            {
                for(int j =0; j < n; j++)
                {
                    if (i != j)
                    {
                        Assert.False(_circles.CirclesList[i].isCollidingWith(_circles.CirclesList[j]));
                    }
                }
            }
        }
    }
}
