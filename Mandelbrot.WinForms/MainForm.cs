using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mandelbrot.WinForms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            Debug.WriteLine("canvas_Paint");
            Rectangle r = new Rectangle(e.ClipRectangle.X + 5, e.ClipRectangle.Y + 5, e.ClipRectangle.Width - 10, e.ClipRectangle.Height - 10);
            //e.Graphics.FillRectangle(new SolidBrush(Color.Red), r);

            Bitmap b = new Bitmap(canvas.ClientRectangle.Width, canvas.ClientRectangle.Height   );
            for (int i = 1; i < 100; i++)
            {
                b.SetPixel(i, i, Color.White);
                b.SetPixel(i+1, i, Color.White);
                b.SetPixel(i+2, i, Color.White);
                b.SetPixel(i+3, i, Color.White);
            }

            canvas.Image = b;

        }
    }
}
