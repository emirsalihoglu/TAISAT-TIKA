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
            base.OnPaint(e);

            // Kenarlık rengini çizme
            e.Graphics.Clear(this.BackColor);

            using (Pen pen = new Pen(BorderColor))
            {
                Size tSize = TextRenderer.MeasureText(this.Text, this.Font);
                Rectangle borderRect = new Rectangle(0, tSize.Height / 2, this.Width - 1, this.Height - tSize.Height / 2 - 1);

                // Kenarlığı çizme
                e.Graphics.DrawRectangle(pen, borderRect);

                // Başlığı çizme
                e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), new Point(6, 0));
            }
        }
    }
}
