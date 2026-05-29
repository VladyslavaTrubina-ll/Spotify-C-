using System;

namespace Spotify.WinForms.Models
{
    /// <summary>
    /// Представляє пісню з інформацією про альбом та співавторів.
    /// </summary>
    public class Cancion : Audio
    {
        public int IdAlbum { get; set; }
        public string NombresColaboradores { get; set; }
        public string Foto { get; set; }

        /// <summary>
        /// Конструктор із основними параметрами.
        /// </summary>
        public Cancion(int id, string nomeAudio, string arquivo, int duracaoSegundos, int reproducoes, 
            int idAlbum, string nombresColaboradores, string tipo)
            : base(id, nomeAudio, arquivo, reproducoes, duracaoSegundos, tipo)
        {
            IdAlbum = idAlbum;
            NombresColaboradores = nombresColaboradores;
            Foto = null;
        }

        /// <summary>
        /// Конструктор з фото.
        /// </summary>
        public Cancion(int id, string nomeAudio, string arquivo, int duracaoSegundos, int reproducoes,
            int idAlbum, string nombresColaboradores, string tipo, string foto)
            : base(id, nomeAudio, arquivo, reproducoes, duracaoSegundos, tipo)
        {
            IdAlbum = idAlbum;
            NombresColaboradores = nombresColaboradores;
            Foto = foto;
        }

        public override string ToString()
        {
            return $"Cancion[id={Id}, nome={NomeAudio}, album={IdAlbum}, duracao={DuracaoConvertida()}]";
        }
    }
}
