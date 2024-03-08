using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TAISAT
{
    public partial class Form1 : Form
    {
        private bool isGreen = true;
        private bool isWhite = true;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //KONTROL SİSTEMİ GROUPBOX
        private void groupBoxKontrolSistemi_Paint(object sender, PaintEventArgs e)
        {
            // GroupBox kenarlığını gizlemek için:
            e.Graphics.Clear(Color.FromArgb(95, 95, 95));

            // GroupBox arka planını gri renkte doldurmak için:
            using (var brush = new SolidBrush(Color.FromArgb(95, 95, 95)))
            {
                e.Graphics.FillRectangle(brush, e.ClipRectangle);
            }

            // GroupBox başlığını çizmek için:
            var textRect = new Rectangle(3, 3, groupBoxKontrolSistemi.Width - 6, groupBoxKontrolSistemi.Font.Height);
            var sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;
            using (var font = new Font("Bahnschrift", 17))
            {
                e.Graphics.DrawString(groupBoxKontrolSistemi.Text, font, Brushes.White, textRect, sf);
            }
        }
        //KONTROL SİSTEMİ GROUPBOX

    }
}
