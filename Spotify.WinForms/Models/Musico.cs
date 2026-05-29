using System;

namespace Spotify.WinForms.Models
{
    /// <summary>
    /// Представляє музиканта.
    /// </summary>
    public class Musico : Artista
    {
        public string Caracteristica { get; set; } // Solista або Grupo

        public Musico() : base() { }

        public Musico(int id, string nomeArt, string genero, string descripcion, string foto, string caracteristica)
            : base(id, nomeArt, genero, descripcion, foto)
        {
            Caracteristica = caracteristica;
        }

        public override string ToString()
        {
            return $"Musico[id={Id}, nome={NomeArt}, caracteristica={Caracteristica}]";
        }
    }
}
