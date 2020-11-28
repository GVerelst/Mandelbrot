using Mandelbrot.Calculations;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Mandelbrot.WPF.Extensions
{
    public static class WriteableBitmapExtensions
    {
        public static void SetPixel(
                  this WriteableBitmap wbm,
                  int x, int y, System.Drawing.Color c)
        {
            if (y > wbm.PixelHeight - 1 ||
               x > wbm.PixelWidth - 1) return;
            if (y < 0 || x < 0) return;
            if (!wbm.Format.Equals(
                   PixelFormats.Bgra32)) return;

            wbm.Lock();
            IntPtr buff = wbm.BackBuffer;
            int Stride = wbm.BackBufferStride;

            unsafe
            {
                byte* pbuff = (byte*)buff.ToPointer();
                int loc = y * Stride + x * 4;
                pbuff[loc] = c.B;
                pbuff[loc + 1] = c.G;
                pbuff[loc + 2] = c.R;
                pbuff[loc + 3] = c.A;
            }

            wbm.AddDirtyRect(new Int32Rect(x, y, 1, 1));
            wbm.Unlock();
        }

        public static void SetPixels(
            this WriteableBitmap wbm,
            IEnumerable<FractalPoint> pts)
        {
            wbm.Lock();
            IntPtr buff = wbm.BackBuffer;
            int Stride = wbm.BackBufferStride;

            unsafe
            {
                byte* pbuff = (byte*)buff.ToPointer();

                foreach (FractalPoint pt in pts)
                {
                    System.Drawing.Color c = pt.Color;
                    int loc = pt.Point.Y * Stride + pt.Point.X * 4;
                    pbuff[loc] = c.B;
                    pbuff[loc + 1] = c.G;
                    pbuff[loc + 2] = c.R;
                    pbuff[loc + 3] = c.A;
                }
            }

            wbm.AddDirtyRect(new Int32Rect(0, 0, (int)wbm.Width, (int)wbm.Height));
            wbm.Unlock();
        }
    }
}