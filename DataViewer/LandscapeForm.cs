using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataViewer
{
    public struct MdsItem
    {
        // constructor
        public MdsItem(string countryName, double x, double y)
        {
            CountryName = countryName;
            X = x;
            Y = y;
        }
        // copy constructor
        public MdsItem(MdsItem item)
        {
            CountryName = item.CountryName;
            X = item.X;
            Y = item.Y;
        }

        public string CountryName;
        public double X;
        public double Y;
    }

    public partial class LandscapeForm: System.Windows.Forms.Form
    {
        private string Location;
        public static  MdsItem[] MDS;

        public LandscapeForm(string location, string filename)
        {
            InitializeComponent();
            Location = location;

            this.Resize += LandscapeForm_Resize;

            var mdsFile = Path.Combine(Location, filename);
            var data = Parsing.ParseCsvFile(mdsFile, false);

            MDS = new MdsItem[data.Length];
            for(int i=0; i < data.Length; i++)
            {
                MDS[i] = new MdsItem(
                    data[i][0], 
                    double.Parse(data[i][1]), 
                    double.Parse(data[i][2]));
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e); // Call the base class's OnPaint method

            Graphics g = e.Graphics; // Use the Graphics object from PaintEventArgs
            int width = this.ClientSize.Width;
            int height = this.ClientSize.Height;
            g.Clear(this.BackColor);
            var f = new Font(this.Font.FontFamily, 10, FontStyle.Regular);

            double minX = double.MaxValue;
            double minY = double.MaxValue;
            double maxX = double.MinValue;
            double maxY = double.MinValue;

            foreach (var mds in MDS)
            {
                if (mds.X < minX) minX = mds.X;
                if (mds.Y < minY) minY = mds.Y;
                if (mds.X > maxX) maxX = mds.X;
                if (mds.Y > maxY) maxY = mds.Y;
            }

            Brush[] brushes = new Brush[10];
            brushes[0] = Brushes.Red;
            brushes[1] = Brushes.DarkSlateBlue;
            brushes[2] = Brushes.Green;
            brushes[3] = Brushes.DarkViolet;
            brushes[4] = Brushes.DarkMagenta;
            brushes[5] = Brushes.Purple;
            brushes[6] = Brushes.Brown;
            brushes[7] = Brushes.DarkGreen;
            brushes[8] = Brushes.DarkKhaki;
            brushes[9] = Brushes.Black;

            Random rand = new Random();
            foreach (var mds in MDS)
            {
                // Example: Draw country names at random positions
                var p = new Point(
                    (int)( (width -60) * (mds.X - minX) / (maxX - minX)), 
                    (int)( (height-20) * (mds.Y - minY) / (maxY - minY)));
                g.DrawString(mds.CountryName, f, brushes[rand.Next(10)], p);
            }
        }

        private void LandscapeForm_Resize(object sender, EventArgs e)
        {
            this.Invalidate(); // Force the form to repaint
        }
    }
}
