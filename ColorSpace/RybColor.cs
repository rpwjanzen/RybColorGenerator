using System;
using System.Windows.Media;

namespace ColorSpace
{
    /// <summary>
    /// Colors are [1-0] => [0-255]
    /// </summary>
    public sealed class RybColor
    {
        public readonly double Red;
        public readonly double Yellow;
        public readonly double Blue;

        public RybColor(double red, double yellow, double blue)
        {
            if (double.IsNaN(red))
                throw new ArgumentException("red");
            if (double.IsNaN(yellow))
                throw new ArgumentException("yellow");
            if (double.IsNaN(blue))
                throw new ArgumentException("blue");

            if (red < 0 || red > 1)
                throw new ArgumentException("red");
            if (yellow < 0 || yellow > 1)
                throw new ArgumentException("yellow");
            if (blue < 0 || blue > 1)
                throw new ArgumentException("blue");

            Red = red;
            Yellow = yellow;
            Blue = blue;
        }

        public Color ToColor()
        {
            return RybColorConverter.ToColor(this);
        }

        public override string ToString()
        {
            var color = ToColor();
            return string.Format("{0},{1},{2} => {3},{4},{5}", Red, Yellow, Blue, color.R, color.G, color.B);
        }

        /// <summary>
        /// Euclidian distance
        /// </summary>
        public static double DistanceSquared(RybColor color, RybColor other)
        {
            double redDistance = Math.Pow(color.Red - other.Red, 2);
            double yellowDistance = Math.Pow(color.Yellow - other.Yellow, 2);
            double blueDistance = Math.Pow(color.Blue - other.Blue, 2);

            return redDistance + yellowDistance + blueDistance;
        }
    }
}
