using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.Versioning;
using Spotify.WinForms.Controllers;
using Spotify.WinForms.Models;

namespace Spotify.WinForms.Views
{
    /// <summary>
    /// Форма для відображення та відтворення музики.
    /// Демонструє програмне створення списків та кнопок.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public partial class FormMusica : Form
    {
        private ControladorDB _controladorDB;
        private ControladorFicheros _controladorFicheros;
        private ReproductorAudio _reproductor;
        private ListBox listBoxCanciones;
        private Button btnPlay, btnPause, btnStop;
        private Label lblActual;
        private ProgressBar progressBar;

        public FormMusica(ControladorDB controladorDB)
        {
            InitializeComponent();
            _controladorDB = controladorDB;
            _controladorFicheros = new ControladorFicheros(
                "canciones", 
                "imagenes", 
                "podcasts"
            );
            _reproductor = new ReproductorAudio();
            
            this.Text = "Spotify - Музика";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(20, 20, 20);
            
            CriarElementosProgramaticamente();
            CargarCanciones();
        }

        private void CriarElementosProgramaticamente()
        {
            // Заголовок
            Label lblTitulo = new Label();
            lblTitulo.Text = "Моя музика";
            lblTitulo.Location = new Point(20, 20);
            lblTitulo.Size = new Size(300, 40);
            lblTitulo.Font = new Font("Arial", 16, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            this.Controls.Add(lblTitulo);

            // Список пісень
            listBoxCanciones = new ListBox();
            listBoxCanciones.Location = new Point(20, 70);
            listBoxCanciones.Size = new Size(750, 300);
            listBoxCanciones.BackColor = Color.FromArgb(40, 40, 40);
            listBoxCanciones.ForeColor = Color.White;
            listBoxCanciones.Font = new Font("Arial", 10);
            listBoxCanciones.DoubleClick += ListBoxCanciones_DoubleClick;
            this.Controls.Add(listBoxCanciones);

            // Мітка поточної пісні
            lblActual = new Label();
            lblActual.Text = "Поточна пісня: Немає";
            lblActual.Location = new Point(20, 380);
            lblActual.Size = new Size(750, 25);
            lblActual.Font = new Font("Arial", 10);
            lblActual.ForeColor = Color.FromArgb(0, 200, 100);
            this.Controls.Add(lblActual);

            // ProgressBar
            progressBar = new ProgressBar();
            progressBar.Location = new Point(20, 415);
            progressBar.Size = new Size(750, 20);
            progressBar.Value = 0;
            this.Controls.Add(progressBar);

            // Кнопка Play
            btnPlay = new Button();
            btnPlay.Text = "▶ Play";
            btnPlay.Location = new Point(50, 450);
            btnPlay.Size = new Size(100, 40);
            btnPlay.BackColor = Color.FromArgb(30, 215, 96);
            btnPlay.ForeColor = Color.White;
            btnPlay.Font = new Font("Arial", 10, FontStyle.Bold);
            btnPlay.Click += BtnPlay_Click;
            this.Controls.Add(btnPlay);

            // Кнопка Pause
            btnPause = new Button();
            btnPause.Text = "⏸ Pause";
            btnPause.Location = new Point(200, 450);
            btnPause.Size = new Size(100, 40);
            btnPause.BackColor = Color.FromArgb(100, 100, 100);
            btnPause.ForeColor = Color.White;
            btnPause.Font = new Font("Arial", 10, FontStyle.Bold);
            btnPause.Click += BtnPause_Click;
            this.Controls.Add(btnPause);

            // Кнопка Stop
            btnStop = new Button();
            btnStop.Text = "⏹ Stop";
            btnStop.Location = new Point(350, 450);
            btnStop.Size = new Size(100, 40);
            btnStop.BackColor = Color.FromArgb(200, 50, 50);
            btnStop.ForeColor = Color.White;
            btnStop.Font = new Font("Arial", 10, FontStyle.Bold);
            btnStop.Click += BtnStop_Click;
            this.Controls.Add(btnStop);

            // Кнопка назад
            Button btnBack = new Button();
            btnBack.Text = "← Назад";
            btnBack.Location = new Point(600, 450);
            btnBack.Size = new Size(150, 40);
            btnBack.BackColor = Color.FromArgb(70, 70, 70);
            btnBack.ForeColor = Color.White;
            btnBack.Font = new Font("Arial", 10, FontStyle.Bold);
            btnBack.Click += BtnBack_Click;
            this.Controls.Add(btnBack);
        }

        private void CargarCanciones()
        {
            List<string> canciones = _controladorFicheros.ObtenerArchivoMusica();
            listBoxCanciones.Items.Clear();
            
            foreach (string cancion in canciones)
            {
                string nombre = System.IO.Path.GetFileNameWithoutExtension(cancion);
                listBoxCanciones.Items.Add(nombre);
            }

            if (canciones.Count == 0)
            {
                listBoxCanciones.Items.Add("Немає піс, завантажте музику в папку 'canciones'");
            }
        }

        private void ListBoxCanciones_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxCanciones.SelectedIndex >= 0)
            {
                List<string> canciones = _controladorFicheros.ObtenerArchivoMusica();
                if (listBoxCanciones.SelectedIndex < canciones.Count)
                {
                    string cancion = canciones[listBoxCanciones.SelectedIndex];
                    if (_reproductor.CargarYReproducir(cancion))
                    {
                        lblActual.Text = $"Поточна пісня: {System.IO.Path.GetFileNameWithoutExtension(cancion)}";
                    }
                }
            }
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            if (listBoxCanciones.SelectedIndex >= 0)
            {
                List<string> canciones = _controladorFicheros.ObtenerArchivoMusica();
                if (listBoxCanciones.SelectedIndex < canciones.Count)
                {
                    _reproductor.CargarYReproducir(canciones[listBoxCanciones.SelectedIndex]);
                }
            }
            else
            {
                MessageBox.Show("Виберіть пісню зі списку!", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnPause_Click(object sender, EventArgs e)
        {
            _reproductor.Pausar();
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            _reproductor.Detener();
            lblActual.Text = "Поточна пісня: Немає";
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);
        }
    }
}
