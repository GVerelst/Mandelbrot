using Mandelbrot.Calculations;
using Mandelbrot.UWP.Extensions;
using Mandelbrot.UWP.ViewModels;
using System.Collections.Generic;
using System.Drawing;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Mandelbrot.UWP
{
    /// <summary>An empty page that can be used on its own or navigated to within a Frame.</summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            DataContext = new MandelbrotParameters(this);
        }

        public void DrawImage(IEnumerable<FractalPoint> pts)
        {
            int width = (int)ImgBorder.ActualWidth;
            int height = (int)ImgBorder.ActualHeight;
            WriteableBitmap wbmap = new WriteableBitmap(width, height);

            wbmap.SetPixels(pts);
            img.Source = wbmap;
        }

        public RectangleF GetImageViewport()
        {
            RectangleF viewport = new RectangleF(0f, 0f, (float)ImgBorder.ActualWidth, (float)ImgBorder.ActualHeight);
            return viewport;
        }
    }
}