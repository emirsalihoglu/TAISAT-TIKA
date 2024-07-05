using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TAISAT
{
    public class CustomGroupBox : GroupBox
    {
        public Color BorderColor { get; set; } = Color.Black;

        protected override void OnPaint(PaintEventArgs e)
        {
            // Kenarlıkları görünmez yapmak için Graphics nesnesini kullanarak GroupBox çizimini iptal edin
            e.Graphics.Clear(this.BackColor);

            // Text'i manuel olarak çiz
            TextRenderer.DrawText(e.Graphics, this.Text, this.Font, new Point(6, 0), this.ForeColor);
        }
    }
}
