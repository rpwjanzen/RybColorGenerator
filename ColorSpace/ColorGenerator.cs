using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Media;
using System.Diagnostics;

namespace ColorSpace
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Port from: https://github.com/afriggeri/RYB
    /// 
    /// Original License
    /// 
    /// Copyright (C) 2012 Adrien Friggeri
    /// 
    /// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation
    /// files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify,
    /// merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is
    /// furnished to do so, subject to the following conditions:
    /// 
    /// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
    /// 
    /// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT
    /// LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
    /// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
    /// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
    /// SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
    /// </remarks>
    public sealed class ColorGenerator
    {
        #region Fields

        // not the last picked color, but something else
        private RybColor m_picked;

        private readonly List<RybColor> m_colorPalette = new List<RybColor>();
        private int m_pickedColorCount = 0;

        #endregion

        #region Constructors
        /// <param name="paletteCount">Min 3.</param>
        public ColorGenerator(int paletteCount)
        {
            if (paletteCount < 3)
                return;

            // generate at least as many colors that we need
            const double exponent = 3;
            
            double a = Math.Pow(paletteCount, 1 / exponent);
            double @base = Math.Ceiling(a);
            double max = Math.Pow(@base, exponent);

            for (int i = 0; i < max; i++)
            {
                double red = Math.Floor(i / (@base * @base))
                    / (@base - 1);
                double yellow = Math.Floor((i / @base) % @base)
                    / (@base - 1);
                double blue = Math.Floor(i % @base)
                    / (@base - 1);

                RybColor color = new RybColor(red, yellow, blue);
                m_colorPalette.Add(color);
            }

            m_picked = null;
        }

        #endregion

        #region Public Methods

        public RybColor PickNextColor()
        {
            if (m_picked == null)
            {
                m_picked = m_colorPalette.FirstOrDefault();
                if (m_picked == null)
                    return null;

                m_colorPalette.RemoveAt(0);
                Debug.WriteLine("Initial color is {0}", m_picked);
                m_pickedColorCount = 1;
                return m_picked;
            }

            
            // pick (close to) the color that is farthest away from the last picked color
            var result = m_colorPalette.Aggregate<RybColor,Tuple<RybColor, double>>(
                Tuple.Create(m_colorPalette[0], DistanceFromLastPickedColor(m_colorPalette[0])),
                (acc, color) =>
                {
                    var currentDistance = DistanceFromLastPickedColor(color);
                    if (acc.Item2 < currentDistance)
                        return Tuple.Create(color, currentDistance);
                    else
                        return acc;
                });

            m_colorPalette.Remove(result.Item1);

            var pick = result.Item1;
            m_picked = new RybColor(
                (m_pickedColorCount * m_picked.Red + pick.Red)
                    / (m_pickedColorCount + 1),
                (m_pickedColorCount * m_picked.Yellow + pick.Yellow)
                    / (m_pickedColorCount + 1),
                (m_pickedColorCount * m_picked.Blue + pick.Blue)
                    / (m_pickedColorCount + 1));

            m_pickedColorCount++;

            System.Diagnostics.Debug.WriteLine("Farthest color is " + pick.ToString());
            return pick;
        }

        #endregion

        #region Private Methods

        private double DistanceFromLastPickedColor(RybColor color)
        {
            return RybColor.DistanceSquared(color, m_picked);
        }

        #endregion
    }
}
