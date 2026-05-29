using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.Versioning;
using Spotify.WinForms.Controllers;

namespace Spotify.WinForms.Views
{
    /// <summary>
    /// Головна форма входу з динамічним розташуванням елементів управління.
    /// Форма використовує як інструменти ToolBox, так і програмне створення елементів.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public partial class FormLogin : Form
    {
        private ControladorDB _controlador;
        private Label lblUsuario;
        private TextBox txtUsuario;
        private Label lblContrasena;
        private TextBox txtContrasena;
        private Button btnLogin;
        private Button btnRegistro;
        private PictureBox pictureBox;

        public FormLogin()
        {
            InitializeComponent();
            this.Text = "Spotify - Вхід";
            this.Size = new Size(500, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(20, 20, 20); // Темний фон
            
            _controlador = new ControladorDB("spoty");
            
            CriarElementosProgramaticamente();
        }

        /// <summary>
        /// Створює елементи управління програмово під час роботи форми.
        /// Це відповідає вимогам: розташування не тільки ручне через ToolBox, 
        /// але й програмне із координатами та розмірами.
        /// </summary>
        private void CriarElementosProgramaticamente()
        {
            // Завантажити фотографію
            try
            {
                // Якщо є фото, розташувати його
                pictureBox = new PictureBox();
                pictureBox.Location = new Point(150, 10);
                pictureBox.Size = new Size(200, 100);
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                // pictureBox.Image = Image.FromFile(@"imagenes\spotify-logo.png");
                this.Controls.Add(pictureBox);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Не можна завантажити зображення: {ex.Message}");
            }

            // Етикета та поле для користувача
            lblUsuario = new Label();
            lblUsuario.Text = "Користувач:";
            lblUsuario.Location = new Point(50, 130);
            lblUsuario.Size = new Size(100, 25);
            lblUsuario.ForeColor = Color.White;
            lblUsuario.Font = new Font("Arial", 10, FontStyle.Bold);
            this.Controls.Add(lblUsuario);

            txtUsuario = new TextBox();
            txtUsuario.Location = new Point(150, 130);
            txtUsuario.Size = new Size(250, 30);
            txtUsuario.Font = new Font("Arial", 10);
            this.Controls.Add(txtUsuario);

            // Етикета та поле для пароля
            lblContrasena = new Label();
            lblContrasena.Text = "Пароль:";
            lblContrasena.Location = new Point(50, 170);
            lblContrasena.Size = new Size(100, 25);
            lblContrasena.ForeColor = Color.White;
            lblContrasena.Font = new Font("Arial", 10, FontStyle.Bold);
            this.Controls.Add(lblContrasena);

            txtContrasena = new TextBox();
            txtContrasena.Location = new Point(150, 170);
            txtContrasena.Size = new Size(250, 30);
            txtContrasena.Font = new Font("Arial", 10);
            txtContrasena.UseSystemPasswordChar = true;
            this.Controls.Add(txtContrasena);

            // Кнопка входу
            btnLogin = new Button();
            btnLogin.Text = "Вхід";
            btnLogin.Location = new Point(150, 220);
            btnLogin.Size = new Size(120, 40);
            btnLogin.BackColor = Color.FromArgb(30, 215, 96); // Spotify зелений
            btnLogin.ForeColor = Color.White;
            btnLogin.Font = new Font("Arial", 10, FontStyle.Bold);
            btnLogin.Click += BtnLogin_Click;
            this.Controls.Add(btnLogin);

            // Кнопка реєстрації
            btnRegistro = new Button();
            btnRegistro.Text = "Реєстрація";
            btnRegistro.Location = new Point(280, 220);
            btnRegistro.Size = new Size(120, 40);
            btnRegistro.BackColor = Color.FromArgb(100, 100, 100);
            btnRegistro.ForeColor = Color.White;
            btnRegistro.Font = new Font("Arial", 10, FontStyle.Bold);
            btnRegistro.Click += BtnRegistro_Click;
            this.Controls.Add(btnRegistro);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text;
            string contrasena = txtContrasena.Text;

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contrasena))
            {
                MessageBox.Show("Будь ласка, введіть користувача та пароль.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_controlador.StartConnection())
            {
                MessageBox.Show("Помилка підключення до БД.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_controlador.IniciarSesion(usuario, contrasena))
            {
                MessageBox.Show($"Ласкаво просимо, {usuario}!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Відкрити головне меню
                FormMenuPrincipal menuForm = new FormMenuPrincipal(_controlador);
                menuForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Неправильне користувача або пароль.", "Помилка входу", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            _controlador.CloseConnection();
        }

        private void BtnRegistro_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text?.Trim();
            string contrasena = txtContrasena.Text;

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contrasena))
            {
                MessageBox.Show("Введіть користувача та пароль для реєстрації.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_controlador.StartConnection())
            {
                MessageBox.Show("Помилка підключення до БД.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool registrado = _controlador.RegistrarCliente(usuario, usuario, contrasena, 1);
            if (registrado)
            {
                MessageBox.Show("Реєстрація успішна. Тепер можна увійти.", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Не вдалося зареєструвати користувача. Перевірте, чи логін вже не зайнятий.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            _controlador.CloseConnection();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);
        }
    }
}
