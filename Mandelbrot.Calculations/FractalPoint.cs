using System.Drawing;

namespace Mandelbrot.Calculations
{
    /// <summary>
    ///     Combination of a point and a color. The point is the coordinate on the screen
    /// </summary>
    public class FractalPoint
    {
        public FractalPoint(int x, int y, Color clr)
        {
            Point = new Point(x, y);
            Color = clr;
        }

        public Point Point { get; private set; }
        public Color Color { get; private set; }

        public override string ToString()
        {
            return $"{Point} - {Color}";
        }
    }
}