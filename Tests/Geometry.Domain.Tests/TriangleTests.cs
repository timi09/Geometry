using Geometry.Domain;
using Geometry.Domain.Exceptions;
using System;

namespace Geometry.Tests
{
    public class TriangleTests
    {
        [Theory]
        [InlineData(0, 1, 1)]
        [InlineData(1, 0, 1)]
        [InlineData(1, 1, 0)]
        [InlineData(0, 0, 1)]
        [InlineData(1, 0, 0)]
        [InlineData(0, 1, 0)]
        [InlineData(0, 0, 0)]
        [InlineData(0, 1.1, 1.2)]
        [InlineData(1.2, 0, 1.3)]
        [InlineData(1.3, 1.4, 0)]
        [InlineData(0, 0, 1.5)]
        [InlineData(1.6, 0, 0)]
        [InlineData(0, 1.7, 0)]
        public void ZeroSideFail(float side1, float side2, float side3)
        {
            Assert.Throws<ArgumentZeroException>(() => new Triangle(side1, side2, side3));
        }

        [Theory]
        [InlineData(-0.1, 1, 1)]
        [InlineData(1, -0.3, 1)]
        [InlineData(1, 1, -0.7)]
        [InlineData(-0.9, -1.1, 1)]
        [InlineData(1, -1.2, -1.3)]
        [InlineData(-1, -1, -1)]
        [InlineData(-float.Epsilon, 1.1, 1.2)]
        [InlineData(1.2, -float.Epsilon, 1.3)]
        [InlineData(1.3, 1.4, -float.Epsilon)]
        [InlineData(-float.MaxValue, 1, 1.5)]
        [InlineData(1.6, -float.MaxValue, 1)]
        [InlineData(1, 1.7, -float.MaxValue)]
        public void NegativeSideFail(float side1, float side2, float side3)
        {
            Assert.Throws<ArgumentNegativeException>(() => new Triangle(side1, side2, side3));
        }

        [Theory]
        [InlineData(float.NegativeInfinity, 1, 1)]
        [InlineData(1, float.NegativeInfinity, 1)]
        [InlineData(1, 1, float.NegativeInfinity)]
        [InlineData(float.PositiveInfinity, 1, 1)]
        [InlineData(1, float.PositiveInfinity, 1)]
        [InlineData(1, 1, float.PositiveInfinity)]
        public void InfinitySideFail(float side1, float side2, float side3)
        {
            Assert.Throws<ArgumentInfinityException>(() => new Triangle(side1, side2, side3));
        }

        [Theory]
        [InlineData(3, 1, 1)]
        [InlineData(2, 5, 3)]
        [InlineData(1.1, 1.01, 2.12)]
        [InlineData(200.53, 100, 99.99)]
        public void IncorrectSidesSizesFail(float side1, float side2, float side3)
        {
            Assert.Throws<ArgumentException>(() => new Triangle(side1, side2, side3));
        }

        [Theory]
        [InlineData(1.3, 1.1, 1.2)]
        [InlineData(2.6, 3.2, 2.01)]
        [InlineData(9873.43, 8754.11, 15123.35)]
        public void PropertyMaxSideTest(float side1, float side2, float side3)
        {
            var triangle = new Triangle(side1, side2, side3);

            float expected = Math.Max(side1, Math.Max(side2, side3));

            float actual = triangle.MaxSide;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1.001, 1.1, 1.5)]
        [InlineData(4.6, 3.2, 3.21)]
        [InlineData(6463.81, 8138.32, 5433.33)]
        public void PropertyMinSideTest(float side1, float side2, float side3)
        {
            var triangle = new Triangle(side1, side2, side3);

            float expected = Math.Min(side1, Math.Min(side2, side3));

            float actual = triangle.MinSide;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1.001, 1.1, 1.5)]
        [InlineData(4.6, 3.2, 3.21)]
        [InlineData(6463.81, 8138.32, 5433.33)]
        public void PropertyMidSideTest(float side1, float side2, float side3)
        {
            var triangle = new Triangle(side1, side2, side3);
            
            var sides = new float[3] { side1, side2,  side3 };
            Array.Sort(sides);

            float expected = sides[1];

            float actual = triangle.MidSide;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1.001, 1.1, 1.5)]
        [InlineData(4.6, 3.2, 3.21)]
        [InlineData(646.81, 813.32, 543.33)]
        public void GetAreaSuccess(float side1, float side2, float side3)
        {
            var triangle = new Triangle(side1, side2, side3);

            float perimeter = side1 + side2 + side3;
            float half = perimeter / 2f;

            float expected = (float)Math.Sqrt(half*(half - side1)*(half - side2)*(half - side3));
            
            float actual = triangle.GetArea();

            Assert.Equal(expected, actual, Constants.Tolerance);
        }

        [Theory]
        [InlineData(0.2E-24, 0.3E-24, 0.4E-24)]
        [InlineData(float.Epsilon*3, float.Epsilon*2, float.Epsilon*2)]
        public void GetAreaOverflowToZero(float side1, float side2, float side3)
        {
            var triangle = new Triangle(side1, side2, side3);

            float expected = 0;

            float actual = triangle.GetArea();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(float.MaxValue-3, float.MaxValue-1, float.MaxValue-1)]
        [InlineData(1234567890123456789020f, 1234567890123456789021f, 1234567890123456789021f)]
        public void GetAreaOverflowToInfinity(float side1, float side2, float side3)
        {
            var triangle = new Triangle(side1, side2, side3);

            float expected = float.PositiveInfinity;

            float actual = triangle.GetArea();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(3.5, 2.1, 2.8)]
        [InlineData(35, 21, 28)]
        [InlineData(0.16, 0.12, 0.20)]
        [InlineData(160, 120, 200)]
        [InlineData(1, 1, 1.4142135)]
        public void HasRightAngleTrue(float side1, float side2, float side3)
        {
            var triangle = new Triangle(side1, side2, side3);

            Assert.True(triangle.HasRightAngle());
        }

        [Theory]
        [InlineData(1.001, 2, 1.5)]
        [InlineData(4.6, 3.2, 3.21)]
        [InlineData(646.81, 813.32, 543.33)]
        public void HasRightAngleFalse(float side1, float side2, float side3)
        {
            var triangle = new Triangle(side1, side2, side3);

            Assert.False(triangle.HasRightAngle());
        }
    }
}