using System;
using System.Windows.Media;

namespace ColorSpace
{
    public static class RybColorConverter
    {
        private static double GetRed(RybColor rybColor)
        {
            var iR = rybColor.Red;
            var iY = rybColor.Yellow;
            var iB = rybColor.Blue;

            var x0 = CubicInterpolate(iB, 1.0, 0.163);
            var x1 = CubicInterpolate(iB, 1.0, 0.0);
            var x2 = CubicInterpolate(iB, 1.0, 0.5);
            var x3 = CubicInterpolate(iB, 1.0, 0.2);
            var y0 = CubicInterpolate(iY, x0, x1);
            var y1 = CubicInterpolate(iY, x2, x3);
            return Math.Ceiling(255 * CubicInterpolate(iR, y0, y1));
        }

        private static double GetGreen(RybColor rybColor)
        {
            var iR = rybColor.Red;
            var iY = rybColor.Yellow;
            var iB = rybColor.Blue;

            var x0 = CubicInterpolate(iB, 1.0, 0.373);
            var x1 = CubicInterpolate(iB, 1.0, 0.66);
            var x2 = CubicInterpolate(iB, 0.0, 0.0);
            var x3 = CubicInterpolate(iB, 0.5, 0.094);
            var y0 = CubicInterpolate(iY, x0, x1);
            var y1 = CubicInterpolate(iY, x2, x3);

            return Math.Ceiling(255 * CubicInterpolate(iR, y0, y1)); 
        }

        private static double GetBlue(RybColor rybColor)
        {
            var iR = rybColor.Red;
            var iY = rybColor.Yellow;
            var iB = rybColor.Blue;

            var x0 = CubicInterpolate(iB, 1.0, 0.6);
            var x1 = CubicInterpolate(iB, 0.0, 0.2);
            var x2 = CubicInterpolate(iB, 0.0, 0.5);
            var x3 = CubicInterpolate(iB, 0.0, 0.0);
            var y0 = CubicInterpolate(iY, x0, x1);
            var y1 = CubicInterpolate(iY, x2, x3);

            return Math.Ceiling(255 * CubicInterpolate(iR, y0, y1));
        }

        public static Color ToColor(RybColor rybColor)
        {
            Color color = new Color()
            {
                A = 255,
                R = (byte)GetRed(rybColor),
                G = (byte)GetGreen(rybColor),
                B = (byte)GetBlue(rybColor),
            };

            return color;
        }

        private static double CubicInterpolate(double t, double A, double B)
        {
            var weight = t*t*(3-2*t);
            return A + weight*(B-A);
        }
    }
}
