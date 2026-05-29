using System;
using System.Drawing;
using System.Windows.Forms;

namespace Spotify.WinForms.Views
{
    public class FormFormulas : Form
    {
        public FormFormulas()
        {
            this.Text = "Формули f1 та f2";
            this.Size = new Size(500, 250);
            this.StartPosition = FormStartPosition.CenterScreen;

            var lbl1 = new Label() { Location = new Point(10, 10), Size = new Size(460, 80) };
            lbl1.Text = "f1(x) = log( q * sin(a - x) ) / (q + x)\nОбласть: q*sin(a-x) > 0, q + x != 0\n(виконується при 0 < q <= 0.25)";
            var lbl2 = new Label() { Location = new Point(10, 100), Size = new Size(460, 80) };
            lbl2.Text = "f2(x) = (q - a*x)^(1/4)\nОбласть: q - a*x >= 0\n(виконується при 0.25 < q <= 1)";

            this.Controls.Add(lbl1);
            this.Controls.Add(lbl2);
        }
    }
}
