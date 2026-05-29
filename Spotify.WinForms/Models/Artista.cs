using System;

namespace Spotify.WinForms.Models
{
    /// <summary>
    /// Абстрактний клас для художника (музиканта або подкастера).
    /// </summary>
    public abstract class Artista
    {
        public int Id { get; set; }
        public string NomeArt { get; set; }
        public string Genero { get; set; }
        public string Descripcion { get; set; }
        public string Foto { get; set; }

        public Artista() { }

        public Artista(int id, string nomeArt, string genero, string descripcion, string foto)
        {
            Id = id;
            NomeArt = nomeArt;
            Genero = genero;
            Descripcion = descripcion;
            Foto = foto;
        }

        public int IdArtista => Id;

        public override string ToString()
        {
            return $"Artista[id={Id}, nomeArt={NomeArt}, genero={Genero}, descripcion={Descripcion}, foto={Foto}]";
        }
    }
}
