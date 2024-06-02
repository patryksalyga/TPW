using TPW.Logika;

namespace TPW.Tests.Model
{
    public class MathematicsTests
    {
        private readonly Mathematics _mathematics;

        public MathematicsTests()
        {
            _mathematics = new Mathematics();
        }

        [Theory]
        [InlineData(3, 2, 5)]
        [InlineData(-3, -2, -5)]
        [InlineData(3, -2, 1)]
        public void Add_ReturnsCorrectResult(double a, double b, double expectedResult)
        {
            var result = _mathematics.add(a, b);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(3, 2, 1)]
        [InlineData(-3, -2, -1)]
        [InlineData(3, -2, 5)]
        public void Subtract_ReturnsCorrectResult(double a, double b, double expectedResult)
        {
            var result = _mathematics.subtract(a, b);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(3, 2, 6)]
        [InlineData(-3, -2, 6)]
        [InlineData(3, -2, -6)]
        public void Multiply_ReturnsCorrectResult(double a, double b, double expectedResult)
        {
            var result = _mathematics.multiply(a, b);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(3, 2, 1.5)]
        [InlineData(-3, -2, 1.5)]
        [InlineData(3, -2, -1.5)]
        public void Divide_ReturnsCorrectResult(double a, double b, double expectedResult)
        {
            var result = _mathematics.divide(a, b);
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Divide_ByZero_ThrowsException()
        {
            Assert.Throws<DivideByZeroException>(() => _mathematics.divide(3, 0));
        }
    }
}