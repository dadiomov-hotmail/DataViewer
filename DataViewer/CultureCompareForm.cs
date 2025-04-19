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
    public partial class CultureCompareForm : System.Windows.Forms.Form
    {
        private string Location;
        public static double[,] DistancesMatrix;
        public static string[] CountryNames1;
        public static string[] CountryNames2;

        public CultureCompareForm(string location)
        {
            InitializeComponent();
            Location = location;

            this.Resize += CultureCompareForm_Resize;

            var distancesFile = Path.Combine(Location, "CultureDistances.csv");

            var data = Parsing.ParseCsvFile(distancesFile, false);
            int nCountries = data.Length - 1;
            DistancesMatrix = new double[nCountries, nCountries];
            CountryNames1 = new string[nCountries];
            CountryNames2 = new string[nCountries];
            for (int i = 0; i < nCountries; i++)
            {
                CountryNames1[i] = data[i + 1][0];
                CountryNames2[i] = data[i + 1][0];
                for (int j = 0; j < nCountries; j++)
                    DistancesMatrix[i, j] = double.Parse(data[i + 1][j + 1]);
            }

            Center1.DataSource = CountryNames1;
            Center2.DataSource = CountryNames2;

            // Set Center1 to US ands Center2 to China
            for (int i = 0; i < CountryNames1.Count(); i++)
            {
                if (CountryNames1[i] == "United States")
                    Center1.SelectedIndex = i;
            }

            for (int i = 0; i < CountryNames2.Count(); i++)
            {
                if (CountryNames1[i] == "China")
                    Center2.SelectedIndex = i;
            }
        }

        private void CultureCompareForm_Resize(object sender, EventArgs e)
        {
            this.Invalidate(); // Force the form to repaint
        }

        private void canvasPanel_Paint(object sender, PaintEventArgs e)
        {
            ShowDistances();
        }

        private void ShowDistances()
        {
            if (Center1.SelectedItem == null || Center2.SelectedItem == null)
                return;

            var g = canvasPanel.CreateGraphics();
            g.Clear(canvasPanel.BackColor);
            var f = new Font(this.Font.FontFamily, 10, FontStyle.Bold);
            var fheight = f.GetHeight(g);
            var visualColumnHeight = fheight + 2.0;
            var visualRows = (int)(canvasPanel.Height / fheight);
            var visualColumnWidth = 10.0;
            var visualColumns = (int)(canvasPanel.Width / visualColumnWidth);
            var cells = new bool[visualRows, visualColumns];

            var center1 = Center1.SelectedItem.ToString();
            var center2 = Center2.SelectedItem.ToString();

            var index1 = Array.IndexOf(CountryNames1, center1);
            var index2 = Array.IndexOf(CountryNames2, center2);
            if (index1 == -1 || index2 == -1)
            {
                MessageBox.Show("Invalid selection");
                return;
            }

            var nCountries = CountryNames1.Length;
            for (int i = 0; i < nCountries; i++)
            {
                if (i == index1 || i == index2)
                    continue;

                var country = CountryNames1[i];
                var fsize = g.MeasureString(country, f);
                var d1 = DistancesMatrix[index1, i];
                var d2 = DistancesMatrix[index2, i];
                var r = d1 / (d1 + d2);

                var x = (int)(canvasPanel.Width * r);

                var visColumn1 = (int)(x / visualColumnWidth);
                var visColumn2 = (int)((x + fsize.Width + 6) / visualColumnWidth);

                var visRow = 0;
                while (visRow < visualRows - 3)
                {
                    bool found = false;
                    for (int j = visColumn1; j < visColumn2; j++)
                    {
                        if (cells[visRow, j])
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                        break;
                    visRow++;
                }

                for (int j = visColumn1; j <= visColumn2; j++)
                    cells[visRow, j] = true;

                var y = (int)(visualColumnHeight * visRow);

                var b = new SolidBrush(Color.FromArgb(255, (int)(255 * (1.0 - r)), 0, (int)(255 * r)));
                g.DrawString(country, f, b, new PointF(x, y));

                b.Dispose();

            }
            g.Dispose();
            f.Dispose();
        }

        private void Center1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowDistances();
        }

        private void Center2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowDistances();
        }
    }
}
