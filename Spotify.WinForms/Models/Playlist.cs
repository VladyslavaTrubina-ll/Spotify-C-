using System;
using System.Collections.Generic;

namespace Spotify.WinForms.Models
{
    /// <summary>
    /// Представляє плейлист користувача.
    /// </summary>
    public class Playlist
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string FechaCreacion { get; set; }
        public int IdCliente { get; set; }
        public List<Audio> Audios { get; set; }

        public Playlist()
        {
            Audios = new List<Audio>();
        }

        public Playlist(int id, string titulo, string fechaCreacion, int idCliente)
        {
            Id = id;
            Titulo = titulo;
            FechaCreacion = fechaCreacion;
            IdCliente = idCliente;
            Audios = new List<Audio>();
        }

        public override string ToString()
        {
            return $"Playlist[id={Id}, titulo={Titulo}, audios={Audios.Count}]";
        }
    }
}
