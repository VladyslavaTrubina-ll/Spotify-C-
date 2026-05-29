using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.Versioning;

namespace Spotify.WinForms.Views
{
    /// <summary>
    /// Форма для обчислення функції w = tg(x).
    /// Демонструє програмне розташування елементів управління при роботі програми.
    /// Це відповідає вимогам практичної роботи про динамічне створення об'єктів.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public partial class FormCalcularFuncion : Form
    {
        private Label lblA, lblB, lblC;
        private TextBox txtA, txtB, txtC;
        private Button btnCalcular;
        private PictureBox pictureBox;
        private ListBox listBox;
        private ComboBox comboBoxFuncion;

        public FormCalcularFuncion()
        {
            InitializeComponent();
            this.Text = "Обчислення функції w = tg(x)";
            this.Size = new Size(600, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(30, 30, 30);

            CriarElementosProgramaticamente();
        }

        private void CriarElementosProgramaticamente()
        {
            // Завантажити фотографію (за наявності)
            pictureBox = new PictureBox();
            pictureBox.Location = new Point(150, 10);
            pictureBox.Size = new Size(300, 80);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.BackColor = Color.FromArgb(50, 50, 50);
            this.Controls.Add(pictureBox);

            // Етикета та TextBox для початкового значення (a)
            lblA = new Label();
            lblA.Text = "Початкове значення (a):";
            lblA.Location = new Point(30, 110);
            lblA.Size = new Size(200, 20);
            lblA.ForeColor = Color.White;
            lblA.Font = new Font("Arial", 10, FontStyle.Bold);
            this.Controls.Add(lblA);

            txtA = new TextBox();
            txtA.Location = new Point(240, 110);
            txtA.Size = new Size(300, 25);
            txtA.Text = "0";
            txtA.Font = new Font("Arial", 10);
            this.Controls.Add(txtA);

            // Етикета та TextBox для кінцевого значення (b)
            lblB = new Label();
            lblB.Text = "Кінцеве значення (b):";
            lblB.Location = new Point(30, 150);
            lblB.Size = new Size(200, 20);
            lblB.ForeColor = Color.White;
            lblB.Font = new Font("Arial", 10, FontStyle.Bold);
            this.Controls.Add(lblB);

            txtB = new TextBox();
            txtB.Location = new Point(240, 150);
            txtB.Size = new Size(300, 25);
            txtB.Text = "3.14159";
            txtB.Font = new Font("Arial", 10);
            this.Controls.Add(txtB);

            // Етикета та TextBox для кроку (c)
            lblC = new Label();
            lblC.Text = "Крок (c):";
            lblC.Location = new Point(30, 190);
            lblC.Size = new Size(200, 20);
            lblC.ForeColor = Color.White;
            lblC.Font = new Font("Arial", 10, FontStyle.Bold);
            this.Controls.Add(lblC);

            txtC = new TextBox();
            txtC.Location = new Point(240, 190);
            txtC.Size = new Size(300, 25);
            txtC.Text = "0.5";
            txtC.Font = new Font("Arial", 10);
            this.Controls.Add(txtC);

            // ComboBox для вибору функції
            Label lblFuncion = new Label();
            lblFuncion.Text = "Виберіть функцію:";
            lblFuncion.Location = new Point(30, 230);
            lblFuncion.Size = new Size(200, 20);
            lblFuncion.ForeColor = Color.White;
            lblFuncion.Font = new Font("Arial", 10, FontStyle.Bold);
            this.Controls.Add(lblFuncion);

            comboBoxFuncion = new ComboBox();
            comboBoxFuncion.Location = new Point(240, 230);
            comboBoxFuncion.Size = new Size(300, 25);
            comboBoxFuncion.Items.AddRange(new[] { "w = tg(x)", "w = sin(x)", "w = cos(x)", "w = e^x" });
            comboBoxFuncion.SelectedIndex = 0;
            comboBoxFuncion.Font = new Font("Arial", 10);
            this.Controls.Add(comboBoxFuncion);

            // Кнопка для обчислення
            btnCalcular = new Button();
            btnCalcular.Text = "Обчислити";
            btnCalcular.Location = new Point(240, 270);
            btnCalcular.Size = new Size(300, 35);
            btnCalcular.BackColor = Color.FromArgb(30, 215, 96);
            btnCalcular.ForeColor = Color.White;
            btnCalcular.Font = new Font("Arial", 11, FontStyle.Bold);
            btnCalcular.Click += BtnCalcular_Click;
            this.Controls.Add(btnCalcular);

            // ListBox для результатів
            Label lblResultados = new Label();
            lblResultados.Text = "Результати:";
            lblResultados.Location = new Point(30, 320);
            lblResultados.Size = new Size(200, 20);
            lblResultados.ForeColor = Color.White;
            lblResultados.Font = new Font("Arial", 10, FontStyle.Bold);
            this.Controls.Add(lblResultados);

            listBox = new ListBox();
            listBox.Location = new Point(30, 345);
            listBox.Size = new Size(510, 300);
            listBox.BackColor = Color.FromArgb(40, 40, 40);
            listBox.ForeColor = Color.FromArgb(0, 200, 100);
            listBox.Font = new Font("Courier New", 9);
            this.Controls.Add(listBox);
        }

        private void BtnCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                double a = double.Parse(txtA.Text);
                double b = double.Parse(txtB.Text);
                double c = double.Parse(txtC.Text);
                string funcion = comboBoxFuncion.SelectedItem?.ToString() ?? "w = tg(x)";

                listBox.Items.Clear();
                listBox.Items.Add($"=== Обчислення функції: {funcion} ===");
                listBox.Items.Add($"Від {a} до {b} з кроком {c}");
                listBox.Items.Add("---");

                // Циклічне обчислення функції та додавання результатів до ListBox
                // За аналогією з прикладом з інструкції
                for (double x = a; x <= b; x += c)
                {
                    double w = 0;
                    string resultado = "";

                    switch (funcion)
                    {
                        case "w = tg(x)":
                            w = Math.Tan(x);
                            resultado = $"x = {x:F4}  =>  w = {w:F6}";
                            break;
                        case "w = sin(x)":
                            w = Math.Sin(x);
                            resultado = $"x = {x:F4}  =>  w = {w:F6}";
                            break;
                        case "w = cos(x)":
                            w = Math.Cos(x);
                            resultado = $"x = {x:F4}  =>  w = {w:F6}";
                            break;
                        case "w = e^x":
                            w = Math.Exp(x);
                            resultado = $"x = {x:F4}  =>  w = {w:F6}";
                            break;
                    }

                    listBox.Items.Add(resultado);
                }

                MessageBox.Show("Обчислення завершено! Результати показані у списку.", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FormatException)
            {
                MessageBox.Show("Помилка: Введіть коректні числові значення!", "Помилка вводу", MessageBoxButtons.OK, MessageBoxIcon.Error);
                listBox.Items.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                listBox.Items.Clear();
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);
        }
    }
}
