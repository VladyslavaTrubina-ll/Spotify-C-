using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Spotify.WinForms.Views
{
    public class FormFunctionCalc : Form
    {
        private Label lblXmin, lblXmax, lblDx, lblA, lblInfo, lblPhoto;
        private TextBox txtXmin, txtXmax, txtDx, txtA;
        private Button btnRun, btnFormulas;
        private PictureBox picStudent;

        public FormFunctionCalc()
        {
            this.Text = "Обчислення функції y(x)";
            this.Size = new Size(700, 420);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 240, 240);

            InitializeComponents();
        }

        private void InitializeComponents()
        {
            lblInfo = new Label() { Text = "Введіть вхідні дані (xmin, xmax, dx, a).", Location = new Point(20, 10), Size = new Size(540, 20) };
            this.Controls.Add(lblInfo);

            lblXmin = new Label() { Text = "xmin:", Location = new Point(20, 40), Size = new Size(80, 20) };
            txtXmin = new TextBox() { Location = new Point(100, 40), Size = new Size(120, 24), Text = "0" };
            lblXmax = new Label() { Text = "xmax:", Location = new Point(240, 40), Size = new Size(80, 20) };
            txtXmax = new TextBox() { Location = new Point(320, 40), Size = new Size(120, 24), Text = "10" };

            lblDx = new Label() { Text = "dx:", Location = new Point(20, 80), Size = new Size(80, 20) };
            txtDx = new TextBox() { Location = new Point(100, 80), Size = new Size(120, 24), Text = "1" };
            lblA = new Label() { Text = "a (const):", Location = new Point(240, 80), Size = new Size(80, 20) };
            txtA = new TextBox() { Location = new Point(320, 80), Size = new Size(120, 24), Text = "1" };

            btnRun = new Button() { Text = "Запустити обчислення", Location = new Point(20, 120), Size = new Size(200, 36) };
            btnRun.Click += BtnRun_Click;

            btnFormulas = new Button() { Text = "Показати формули", Location = new Point(240, 120), Size = new Size(200, 36) };
            btnFormulas.Click += (s, e) => { new FormFormulas().Show(); };

            // Photo area
            picStudent = new PictureBox() { Location = new Point(480, 40), Size = new Size(160, 160), BorderStyle = BorderStyle.FixedSingle, SizeMode = PictureBoxSizeMode.Zoom };
            lblPhoto = new Label() { Text = "Прізвище Ім'я\nГрупа 123", Location = new Point(480, 205), Size = new Size(160, 40), TextAlign = ContentAlignment.MiddleCenter };

            // Try load student photo from imagenes/student.jpg
            try
            {
                var path = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "imagenes", "student.jpg");
                if (System.IO.File.Exists(path)) picStudent.Image = Image.FromFile(path);
            }
            catch { }

            this.Controls.AddRange(new Control[] { lblXmin, txtXmin, lblXmax, txtXmax, lblDx, txtDx, lblA, txtA, btnRun, btnFormulas, picStudent, lblPhoto });
        }

        private void BtnRun_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(txtXmin.Text, out double xmin) || !double.TryParse(txtXmax.Text, out double xmax) || !double.TryParse(txtDx.Text, out double dx) || !double.TryParse(txtA.Text, out double a))
            {
                MessageBox.Show("Будь ласка введіть коректні числові значення для xmin, xmax, dx, a.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dx <= 0)
            {
                MessageBox.Show("dx має бути додатнім.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (xmin > xmax)
            {
                MessageBox.Show("xmin має бути <= xmax.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var resultsF1 = new List<string>();
            var resultsF2 = new List<string>();
            var errors = new List<string>();

            var rnd = new Random();
            int countF1 = 0, countF2 = 0;

            for (double x = xmin; x <= xmax + 1e-9; x += dx)
            {
                double q = rnd.NextDouble(); // 0 <= q < 1
                if (q <= 0) q = Double.Epsilon;

                if (q > 0 && q <= 0.25)
                {
                    // f1(x) = log( q * sin(a - x) ) / (q + x)
                    try
                    {
                        double arg = q * Math.Sin(a - x);
                        if (arg <= 0) throw new InvalidOperationException($"Неможливо взяти log для q*sin(a-x)={arg:F6}");
                        if (Math.Abs(q + x) < 1e-12) throw new DivideByZeroException("Ділення на нуль у знаменнику (q + x)");
                        double y = Math.Log(arg) / (q + x);
                        resultsF1.Add($"x={x:F4}, q={q:F6} => y={y:F6}");
                        countF1++;
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"x={x:F4}, q={q:F6} : f1 помилка -> {ex.Message}");
                    }
                }
                else if (q > 0.25 && q <= 1.0)
                {
                    // f2(x) = (q - a*x)^(1/4)
                    try
                    {
                        double baseVal = q - a * x;
                        if (baseVal < 0) throw new InvalidOperationException($"Підкореневе значення < 0: {baseVal:F6}");
                        double y = Math.Pow(baseVal, 0.25);
                        resultsF2.Add($"x={x:F4}, q={q:F6} => y={y:F6}");
                        countF2++;
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"x={x:F4}, q={q:F6} : f2 помилка -> {ex.Message}");
                    }
                }
            }

            var frm1 = new FormResults("Результати f1(x)", resultsF1, countF1);
            var frm2 = new FormResults("Результати f2(x)", resultsF2, countF2);
            frm1.Show();
            frm2.Show();

            if (errors.Any())
            {
                var msg = string.Join("\n", errors.Take(20));
                MessageBox.Show("Деякі обчислення не вдалиcь:\n" + msg, "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
