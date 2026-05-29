using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Spotify.WinForms.Controllers
{
    /// <summary>
    /// Контролер для роботи з файлами аудіо 
    /// </summary>
    public class ControladorFicheros
    {
        private string _rutaMusica;

        public ControladorFicheros(string rutaMusica)
        {
            _rutaMusica = rutaMusica;
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
