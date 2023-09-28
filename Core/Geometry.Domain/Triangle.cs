using Geometry.Domain.Exceptions;

namespace Geometry.Domain
{
    public class Triangle : IPolygon
    {
        /// <summary>Cached value of area of this triangle</summary>
        private float _area = float.NaN;

        /// <summary>Cached value of perimeter of this triangle</summary>
        private float _perimeter = float.NaN;

        /// <summary>Cached flag that this triangle has right angle</summary>
        private bool? _hasRightAngle;

        /// <summary>Size of min side of this triangle</summary>
        public readonly float MinSide;

        /// <summary>Size of mid side of this triangle</summary>
        public readonly float MidSide;

        /// <summary>Size of max side of this triangle</summary>
        public readonly float MaxSide;

        /// <param name="side1">Size of first side</param>
        /// <param name="side2">Size of second side</param>
        /// <param name="side3">Size of third side</param>
        /// <exception cref="ArgumentException">Incorrect sides sizes</exception>
        /// <exception cref="ArgumentZeroException">If side is zero</exception>
        /// <exception cref="ArgumentInfinityException">If side is infinity</exception>
        /// <exception cref="ArgumentNegativeException">If side is negative</exception>
        public Triangle(float side1, float side2, float side3)
        {
            var sides = new float[3] { side1, side2, side3 };

            foreach (var side in sides) 
            {
                if (side == 0)
                    throw new ArgumentZeroException("Side must not be zero");

                if (float.IsInfinity(side))
                    throw new ArgumentInfinityException("Side must not be infinity");

                if (side < 0)
                    throw new ArgumentNegativeException("Side must not be negative");
            }

            Array.Sort(sides);

            MinSide = sides[0];
            MidSide = sides[1];
            MaxSide = sides[2];

            _perimeter = MaxSide + MidSide + MinSide;

            if (MaxSide >= _perimeter - MaxSide)
                throw new ArgumentException("Max side of triangle must be less than other sides");
        }

        /// <inheritdoc/>
        public float GetArea()
        {
            if (float.IsNaN(_area))
            {
                float half = _perimeter / 2f;
                _area = (float)Math.Sqrt(half*(half - MaxSide)*(half - MidSide)*(half - MinSide));
            }
            return _area;
        }

        /// <inheritdoc/>
        public bool HasRightAngle()
        {
            if (!_hasRightAngle.HasValue)
                _hasRightAngle = Math.Abs(MaxSide*MaxSide - MidSide*MidSide - MinSide*MinSide) < Constants.Tolerance;

            return _hasRightAngle.Value;
        }
    }
}
