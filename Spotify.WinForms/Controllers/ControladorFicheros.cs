using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Spotify.WinForms.Controllers
{
    /// <summary>
    /// Контролер для роботи з файлами аудіо та зображень.
    /// </summary>
    public class ControladorFicheros
    {
        private string _rutaMusica;
        private string _rutaImagenes;
        private string _rutaPodcasts;

        public ControladorFicheros(string rutaMusica, string rutaImagenes, string rutaPodcasts)
        {
            _rutaMusica = rutaMusica;
            _rutaImagenes = rutaImagenes;
            _rutaPodcasts = rutaPodcasts;
        }

        /// <summary>
        /// Отримує список всіх музичних файлів в папці.
        /// </summary>
        public List<string> ObtenerArchivoMusica()
        {
            List<string> archivos = new List<string>();
            try
            {
                if (Directory.Exists(_rutaMusica))
                {
                    archivos = Directory.GetFiles(_rutaMusica, "*.mp3")
                        .Concat(Directory.GetFiles(_rutaMusica, "*.wav"))
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при зчитуванні музичних файлів: {ex.Message}");
            }
            return archivos;
        }

        /// <summary>
        /// Отримує список всіх зображень в папці.
        /// </summary>
        public List<string> ObtenerArchivoImagenes()
        {
            List<string> archivos = new List<string>();
            try
            {
                if (Directory.Exists(_rutaImagenes))
                {
                    archivos = Directory.GetFiles(_rutaImagenes, "*.jpg")
                        .Concat(Directory.GetFiles(_rutaImagenes, "*.png"))
                        .Concat(Directory.GetFiles(_rutaImagenes, "*.gif"))
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при зчитуванні зображень: {ex.Message}");
            }
            return archivos;
        }

        /// <summary>
        /// Отримує список всіх подкастів в папці.
        /// </summary>
        public List<string> ObtenerArchivoPodcasts()
        {
            List<string> archivos = new List<string>();
            try
            {
                if (Directory.Exists(_rutaPodcasts))
                {
                    archivos = Directory.GetFiles(_rutaPodcasts, "*.mp3")
                        .Concat(Directory.GetFiles(_rutaPodcasts, "*.wav"))
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при зчитуванні подкастів: {ex.Message}");
            }
            return archivos;
        }

        /// <summary>
        /// Перевіряє, чи файл існує.
        /// </summary>
        public bool ExisteArchivo(string ruta)
        {
            return File.Exists(ruta);
        }

        /// <summary>
        /// Отримує назву файлу без розширення.
        /// </summary>
        public string ObtenerNombreArchivo(string ruta)
        {
            return Path.GetFileNameWithoutExtension(ruta);
        }
    }
}
