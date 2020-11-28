using System;
using System.Collections.Generic;
using System.Drawing;

namespace Mandelbrot.Calculations
{
    public static class MandelbrotFractal
    {
        public static IEnumerable<FractalPoint> Calculate(RectangleF viewport, RectangleF drawing, int maxIterations)
        {
            RectangleF normalized = Normalize(viewport, drawing);

            float stepX = normalized.Width / viewport.Width;
            float stepY = normalized.Height / viewport.Height;
            PointF currentPoint = new PointF(drawing.X, drawing.Y);

            List<FractalPoint> pts = new List<FractalPoint>((int)(viewport.Width * viewport.Height));

            for (int y = 0; y < viewport.Height; y++)
            {
                for (int x = 0; x < viewport.Width; x++)
                {
                    int n = calcIterations(currentPoint, maxIterations);
                    Color clr = MapColor(n, maxIterations);
                    pts.Add(new FractalPoint(x, y, clr));

                    currentPoint = new PointF(currentPoint.X + stepX, currentPoint.Y);
                }
                currentPoint = new PointF(drawing.X, currentPoint.Y + stepY);
            }

            return pts;
        }

        private static Color MapColor(int n, int maxIterations)
        {
            if (n >= maxIterations)
                return Color.Black;

            double colourIndex = ((double)n) / maxIterations;
            double hue = Math.Pow(colourIndex, 0.25);
            return colorFromHSLA(hue, 0.9, 0.6);
        }

        /// <summary>Convert HSL colour value to Color object.</summary>
        /// <param name="H">Hue</param>
        /// <param name="S">Saturation</param>
        /// <param name="L">Lightness</param>
        /// <returns>Color object</returns>
        private static Color colorFromHSLA(double H, double S, double L)
        {
            double v;
            double r, g, b;

            r = L;   // Set RGB all equal to L, defaulting to grey.
            g = L;
            b = L;

            // Standard HSL to RGB conversion. This is described in detail at: http://www.niwa.nu/2013/05/math-behind-colorspace-conversions-rgb-hsl/
            v = (L <= 0.5) ? (L * (1.0 + S)) : (L + S - L * S);

            if (v > 0)
            {
                double m;
                double sv;
                int sextant;
                double fract, vsf, mid1, mid2;

                m = L + L - v;
                sv = (v - m) / v;
                H *= 6.0;
                sextant = (int)H;
                fract = H - sextant;
                vsf = v * sv * fract;
                mid1 = m + vsf;
                mid2 = v - vsf;

                switch (sextant)
                {
                    case 0:
                        r = v;
                        g = mid1;
                        b = m;
                        break;

                    case 1:
                        r = mid2;
                        g = v;
                        b = m;
                        break;

                    case 2:
                        r = m;
                        g = v;
                        b = mid1;
                        break;

                    case 3:
                        r = m;
                        g = mid2;
                        b = v;
                        break;

                    case 4:
                        r = mid1;
                        g = m;
                        b = v;
                        break;

                    case 5:
                        r = v;
                        g = m;
                        b = mid2;
                        break;
                }
            }

            // Create Color object from RGB values.
            Color color = Color.FromArgb((int)(r * 255), (int)(g * 255), (int)(b * 255));
            return color;
        }

        private static int calcIterations(PointF pt, int maxIterations)
        {
            double distance = 0.0;
            int k = 0;
            PointF zk = pt;
            for (k = 0; k < maxIterations && distance < 4.0; k++)
            {
                zk = CalcNextPoint(pt, zk);
                distance = (zk.X * zk.X + zk.Y * zk.Y);
            };

            return k;
        }

        /// <summary>Make the drawing rectangle fit the viewport rectangle.</summary>
        /// <param name="viewport"></param>
        /// <param name="drawing"> </param>
        /// <returns></returns>
        private static RectangleF Normalize(RectangleF viewport, RectangleF drawing)
        {
            float widthRatio = viewport.Width / drawing.Width;
            float heightRatio = viewport.Height / drawing.Height;

            float width;
            float height;

            if (widthRatio > heightRatio)
            {
                width = drawing.Width * widthRatio / heightRatio;
                height = drawing.Height;
            }
            else
            {
                width = drawing.Width;
                height = drawing.Height * heightRatio / widthRatio;
            }

            return new RectangleF(drawing.X, drawing.Y, width, height);
        }

        private static PointF CalcNextPoint(PointF pt, PointF zk)
        {
            PointF result = new PointF
            (
                zk.X * zk.X - zk.Y * zk.Y + pt.X,
                2.0f * zk.X * zk.Y + pt.Y
            );

            return result;
        }
    }
}