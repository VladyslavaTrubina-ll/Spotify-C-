using System;
using System.IO;
using System.Runtime.Versioning;
using NAudio.Wave;

namespace Spotify.WinForms.Controllers
{
    [SupportedOSPlatform("windows")]
    public class ReproductorAudio
    {
        private WaveOutEvent _mediaPlayer;
        private AudioFileReader _audioFile;
        private string _archivoActual;
        private bool _estaPausado;

        public ReproductorAudio()
        {
            _mediaPlayer = new WaveOutEvent();
            _archivoActual = "";
            _estaPausado = false;
        }

        /// <summary>
        /// Завантажує та запускає відтворення аудіофайлу.
        /// </summary>
        public bool CargarYReproducir(string rutaArchivo)
        {
            try
            {
                if (!File.Exists(rutaArchivo))
                {
                    Console.WriteLine($"Файл не знайдено: {rutaArchivo}");
                    return false;
                }

                DetenerYLiberarRecursos();
                _archivoActual = rutaArchivo;
                _audioFile = new AudioFileReader(rutaArchivo);
                _mediaPlayer.Init(_audioFile);
                _mediaPlayer.Play();
                _estaPausado = false;
                Console.WriteLine($"Відтворення: {Path.GetFileName(rutaArchivo)}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при відтворенні: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Паузує відтворення.
        /// </summary>
        public void Pausar()
        {
            try
            {
                if (_mediaPlayer.PlaybackState == PlaybackState.Playing)
                {
                    _mediaPlayer.Pause();
                    _estaPausado = true;
                    Console.WriteLine("Музика призупинена.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при паузі: {ex.Message}");
            }
        }

        private void DetenerYLiberarRecursos()
        {
            if (_mediaPlayer != null)
            {
                _mediaPlayer.Stop();
                _mediaPlayer.Dispose();
                _mediaPlayer = new WaveOutEvent();
            }

            if (_audioFile != null)
            {
                _audioFile.Dispose();
                _audioFile = null;
            }

            _archivoActual = "";
            _estaPausado = false;
        }
    }
}
 