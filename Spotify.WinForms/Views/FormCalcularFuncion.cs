using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace Spotify.WinForms.Views
{
    /// <summary>
    /// Форма для виконання завдання №14 з обчисленням y(x) за двома формулами.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class FormCalcularFuncion : Form
    {
        private Label lblInfo;
        private Label lblXmin;
        private Label lblXmax;
        private Label lblDx;
        private Label lblA;
        private TextBox txtXmin;
        private TextBox txtXmax;
        private TextBox txtDx;
        private TextBox txtA;
        private Button btnRun;
        private Button btnFormulas;

        public FormCalcularFuncion()
        {
            Text = "№14 — Обчислення y(x)";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(780, 460);
            BackColor = Color.WhiteSmoke;

            InitializeUi();
        }

        private void InitializeUi()
        {
            lblInfo = new Label
            {
                Text = "Введіть xmin, xmax, dx та a. Для кожного x випадково обирається формула за q.",
                Location = new Point(20, 15),
                Size = new Size(720, 35),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            Controls.Add(lblInfo);

            lblXmin = new Label { Text = "xmin:", Location = new Point(20, 70), Size = new Size(80, 24) };
            txtXmin = new TextBox { Location = new Point(110, 68), Size = new Size(120, 24), Text = "0" };

            lblXmax = new Label { Text = "xmax:", Location = new Point(260, 70), Size = new Size(80, 24) };
            txtXmax = new TextBox { Location = new Point(350, 68), Size = new Size(120, 24), Text = "10" };

            lblDx = new Label { Text = "dx:", Location = new Point(20, 110), Size = new Size(80, 24) };
            txtDx = new TextBox { Location = new Point(110, 108), Size = new Size(120, 24), Text = "1" };

            lblA = new Label { Text = "a:", Location = new Point(260, 110), Size = new Size(80, 24) };
            txtA = new TextBox { Location = new Point(350, 108), Size = new Size(120, 24), Text = "1" };

            btnRun = new Button
            {
                Text = "Обчислити",
                Location = new Point(20, 155),
                Size = new Size(180, 35),
                BackColor = Color.FromArgb(30, 215, 96),
                ForeColor = Color.White
            };
            btnRun.Click += BtnRun_Click;

            btnFormulas = new Button
            {
                Text = "Показати формули",
                Location = new Point(220, 155),
                Size = new Size(180, 35),
                BackColor = Color.FromArgb(80, 80, 80),
                ForeColor = Color.White
            };
            btnFormulas.Click += (s, e) => new FormFormulas().Show();

            Controls.AddRange(new Control[]
            {
                lblInfo, lblXmin, txtXmin, lblXmax, txtXmax, lblDx, txtDx, lblA, txtA,
                btnRun, btnFormulas
            });
        }

        private void BtnRun_Click(object sender, EventArgs e)
        {
            var culture = CultureInfo.CurrentCulture;
            
            if (!double.TryParse(txtXmin.Text, NumberStyles.Any, culture, out double xmin) ||
                !double.TryParse(txtXmax.Text, NumberStyles.Any, culture, out double xmax) ||
                !double.TryParse(txtDx.Text, NumberStyles.Any, culture, out double dx) ||
                !double.TryParse(txtA.Text, NumberStyles.Any, culture, out double a))
            {
                MessageBox.Show("Введіть коректні числові значення.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dx <= 0 || xmin > xmax)
            {
                MessageBox.Show("Перевірте, що dx > 0 і xmin <= xmax.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var rnd = new Random();
            var resultsF1 = new List<string>();
            var resultsF2 = new List<string>();
            var errors = new List<string>();
            int countF1 = 0;
            int countF2 = 0;

            for (double x = xmin; x <= xmax + 1e-9; x += dx)
            {
                double q = rnd.NextDouble();
                if (q <= 0) q = double.Epsilon;
                
                if (q <= 0.25)
                {
                    try
                    {
                        double numeratorArg = q * Math.Sin(a - x);
                        if (numeratorArg <= 0)
                            throw new InvalidOperationException($"log не визначений, бо q*sin(a-x)={numeratorArg:F6}");

                        if (Math.Abs(q + x) < 1e-12)
                            throw new DivideByZeroException("Ділення на нуль у знаменнику q + x");

                        double y = Math.Log(numeratorArg) / (q + x);
                        resultsF1.Add($"x={x:F4}, q={q:F6} => y={y:F6}");
                        countF1++;
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"x={x:F4}, q={q:F6}: f1 — {ex.Message}");
                    }
                }
                else
                {
                    try
                    {
                        double baseValue = q - a * x;
                        if (baseValue < 0)
                            throw new InvalidOperationException($"Підкореневий вираз < 0: {baseValue:F6}");

                        double y = Math.Pow(baseValue, 0.25);
                        resultsF2.Add($"x={x:F4}, q={q:F6} => y={y:F6}");
                        countF2++;
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"x={x:F4}, q={q:F6}: f2 — {ex.Message}");
                    }
                }
            }

            if (countF1 > 0)
                new FormResults("Результати f1(x)", resultsF1, countF1).Show();
            
            if (countF2 > 0)
                new FormResults("Результати f2(x)", resultsF2, countF2).Show();

            if (errors.Count > 0)
                MessageBox.Show(string.Join(Environment.NewLine, errors.Take(20)), "Помилки обчислення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
