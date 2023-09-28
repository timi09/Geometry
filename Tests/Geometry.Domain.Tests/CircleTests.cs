using Geometry.Domain;
using Geometry.Domain.Exceptions;

namespace Geometry.Tests
{
    public class CircleTests
    {
        [Fact]
        public void ZeroRadiusFail()
        {
            Assert.Throws<ArgumentZeroException>(() => new Circle(0));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-0.1)]
        [InlineData(-1.1)]
        [InlineData(-0.001)]
        [InlineData(-123.123)]
        [InlineData(-float.Epsilon)]
        [InlineData(-float.MaxValue)]
        public void NegativeRadiusFail(float radius)
        {
            Assert.Throws<ArgumentNegativeException>(() => new Circle(radius));
        }

        [Theory]
        [InlineData(float.NegativeInfinity)]
        [InlineData(float.PositiveInfinity)]
        public void InfinityRadiusFail(float radius)
        {
            Assert.Throws<ArgumentInfinityException>(() => new Circle(radius));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(0.1)]
        [InlineData(1.1)]
        [InlineData(0.001)]
        [InlineData(123.123)]
        [InlineData(float.Epsilon)]
        [InlineData(float.MaxValue)]
        public void PropertyRadiusTest(float radius)
        {
            var circle = new Circle(radius);

            float expected = radius;

            float actual = circle.Radius;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(0.1)]
        [InlineData(1.1)]
        [InlineData(0.001)]
        [InlineData(123.123)]
        public void GetAreaSuccess(float radius)
        {
            var circle = new Circle(radius);

            float expected = Constants.PI * radius * radius;

            float actual = circle.GetArea();

            Assert.Equal(expected, actual, 0.000001);
        }

        [Theory]
        [InlineData(0.1E-23)]
        [InlineData(float.Epsilon)]
        public void GetAreaOverflowToZero(float radius)
        {
            var circle = new Circle(radius);

            float expected = 0;

            float actual = circle.GetArea();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1234567890123456789024f)]
        [InlineData(float.MaxValue)]
        public void GetAreaOverflowToInfinity(float radius)
        {
            var circle = new Circle(radius);

            float expected = float.PositiveInfinity;

            float actual = circle.GetArea();

            Assert.Equal(expected, actual);
        }
    }
}