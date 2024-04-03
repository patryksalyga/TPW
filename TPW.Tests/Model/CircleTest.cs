using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPW.Model;

namespace TPW.Tests.Model
{
    public class CircleTest
    {
       private readonly Circle _circle;
       private readonly Circle _circle2;
        private double X = 100;
        private double Y = 100;

       public CircleTest()
        {
            _circle = new Circle(X, Y);
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
    }
}
