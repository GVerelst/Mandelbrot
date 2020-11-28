using Mandelbrot.Calculations;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml.Media.Imaging;

namespace Mandelbrot.UWP.Extensions
{
    public static class WriteableBitmapExtensions
    {
        public static void SetPixels(
            this WriteableBitmap wbm,
            IEnumerable<FractalPoint> pts)
        {
            byte[] imageArray = new byte[(int)(wbm.PixelWidth * wbm.PixelHeight * 4)];
            int i = 0;
            foreach (var p in pts)
            {
                imageArray[i] = p.Color.B;
                imageArray[i + 1] = p.Color.G;
                imageArray[i + 2] = p.Color.R;
                imageArray[i + 3] = p.Color.A;

                i += 4;
            }
            using (Stream stream = wbm.PixelBuffer.AsStream())
            {
                //write to bitmap
                stream.Write(imageArray, 0, imageArray.Length);
            }
        }
    }
}