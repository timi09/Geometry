namespace Geometry.Domain
{
    public interface IPolygon : IFigure
    {
        /// <summary>Check that polygon has right angle</summary>
        /// <returns>
        /// <see langword="true"/> if polygon has right angle, otherwise <see langword="false"/>
        /// </returns>
        bool HasRightAngle();
    }
}
