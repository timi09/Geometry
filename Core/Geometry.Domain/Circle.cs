using Geometry.Domain.Exceptions;

namespace Geometry.Domain
{
    public class Circle : IFigure
    {
        /// <summary>Cached value of area of this circle</summary>
        private float _area = float.NaN;

        /// <summary>Radius of this circle</summary>
        public readonly float Radius;

        /// <param name="radius">Radius of this circle</param>
        /// <exception cref="ArgumentZeroException">If radius is zero</exception>
        /// <exception cref="ArgumentInfinityException">If radius is infinity</exception>
        /// <exception cref="ArgumentNegativeException">If radius is negative</exception>
        public Circle(float radius)
        {
            if (radius == 0)
                throw new ArgumentZeroException("Radius must not be zero");

            if (float.IsInfinity(radius))
                throw new ArgumentInfinityException("Radius must not be infinity");

            if (radius < 0)
                throw new ArgumentNegativeException("Radius must not be negative");

            Radius = radius;
        }

        /// <inheritdoc/>
        public float GetArea()
        {
            if(float.IsNaN(_area))
                _area = Constants.PI * Radius * Radius;
            return _area;
        }
    }
}
