using Mandelbrot.Calculations;
using Mandelbrot.WPF.Extensions;
using Mandelbrot.WPF.ViewModels;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Mandelbrot.WPF
{
    /// <summary>Interaction logic for MainWindow.xaml</summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MandelbrotParameters(this);
        }

        public void DrawImage(IEnumerable<FractalPoint> pts)
        {
            int width = (int)ImgBorder.ActualWidth;
            int height = (int)ImgBorder.ActualHeight;
            WriteableBitmap wbmap = new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgra32, null);

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