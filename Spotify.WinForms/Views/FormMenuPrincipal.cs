using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.Versioning;
using Spotify.WinForms.Controllers;

namespace Spotify.WinForms.Views
{
    /// <summary>
    /// Головне меню користувача.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public partial class FormMenuPrincipal : Form
    {
        private ControladorDB _controlador;
        private Label lblTitulo;
        private Label lblBienvenida;
        private Button btnMusica;
        private Button btnFuncion;
        private Button btnCerrarSesion;

        public FormMenuPrincipal(ControladorDB controlador)
        {
            InitializeComponent();
            _controlador = controlador;
            this.Text = "Spotify - Головне меню";

            CrearInterfaz();
        }

        private void InitializeComponent()
        {
            this.Size = new System.Drawing.Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Spotify - Головне меню";
            this.BackColor = Color.FromArgb(20, 20, 20);
        }

        private void CrearInterfaz()
        {
            lblTitulo = new Label();
            lblTitulo.Text = "Spotify";
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Arial", 24, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(30, 215, 96);
            lblTitulo.Location = new Point(40, 30);
            this.Controls.Add(lblTitulo);

            lblBienvenida = new Label();
            lblBienvenida.Text = "Вітаємо в Spotify! Оберіть розділ:";
            lblBienvenida.AutoSize = true;
            lblBienvenida.Font = new Font("Arial", 12, FontStyle.Regular);
            lblBienvenida.ForeColor = Color.White;
            lblBienvenida.Location = new Point(40, 80);
            this.Controls.Add(lblBienvenida);

            btnMusica = new Button();
            btnMusica.Text = "Музика";
            btnMusica.Size = new Size(220, 60);
            btnMusica.Location = new Point(40, 140);
            btnMusica.BackColor = Color.FromArgb(30, 215, 96);
            btnMusica.ForeColor = Color.White;
            btnMusica.Font = new Font("Arial", 12, FontStyle.Bold);
            btnMusica.Click += BtnMusica_Click;
            this.Controls.Add(btnMusica);

            btnFuncion = new Button();
            btnFuncion.Text = "Обчислення функції";
            btnFuncion.Size = new Size(220, 60);
            btnFuncion.Location = new Point(40, 220);
            btnFuncion.BackColor = Color.FromArgb(70, 70, 70);
            btnFuncion.ForeColor = Color.White;
            btnFuncion.Font = new Font("Arial", 12, FontStyle.Bold);
            btnFuncion.Click += BtnFuncion_Click;
            this.Controls.Add(btnFuncion);

            btnCerrarSesion = new Button();
            btnCerrarSesion.Text = "Вийти";
            btnCerrarSesion.Size = new Size(220, 60);
            btnCerrarSesion.Location = new Point(40, 300);
            btnCerrarSesion.BackColor = Color.FromArgb(200, 50, 50);
            btnCerrarSesion.ForeColor = Color.White;
            btnCerrarSesion.Font = new Font("Arial", 12, FontStyle.Bold);
            btnCerrarSesion.Click += BtnCerrarSesion_Click;
            this.Controls.Add(btnCerrarSesion);

            Label lblInfo = new Label();
            lblInfo.Text = "Проект містить ще форми для логіну, музики та обчислень.";
            lblInfo.AutoSize = true;
            lblInfo.ForeColor = Color.Gainsboro;
            lblInfo.Location = new Point(40, 390);
            this.Controls.Add(lblInfo);
        }

        private void BtnMusica_Click(object sender, EventArgs e)
        {
            FormMusica form = new FormMusica(_controlador);
            form.Show();
        }

        private void BtnFuncion_Click(object sender, EventArgs e)
        {
            FormCalcularFuncion form = new FormCalcularFuncion();
            form.Show();
        }

        private void BtnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Close();
            FormLogin login = new FormLogin();
            login.Show();
        }
    }
}
