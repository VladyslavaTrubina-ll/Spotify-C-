using System;

namespace Spotify.WinForms.Models
{
    /// <summary>
    /// Представляє подкастера.
    /// </summary>
    public class Podcaster : Artista
    {
        public Podcaster() : base() { }

        public Podcaster(int id, string nomeArt, string genero, string descripcion, string foto)
            : base(id, nomeArt, genero, descripcion, foto)
        {
        }

        public override string ToString()
        {
            return $"Podcaster[id={Id}, nome={NomeArt}]";
        }
    }
}
