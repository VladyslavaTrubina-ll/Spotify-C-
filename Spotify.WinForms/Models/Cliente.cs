using System;
using System.Collections.Generic;

namespace Spotify.WinForms.Models
{
    /// <summary>
    /// Представляє клієнта/користувача Spotify.
    /// </summary>
    public class Cliente
    {
        public int Id { get; set; }
        public int LimitesPlaylists { get; set; }
        public string Nome { get; set; }
        public string Apellido { get; set; }
        public string Idioma { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
        public string FecNac { get; set; }
        public string FecReg { get; set; }
        public bool EsPremium { get; set; }
        public List<Playlist> PlaylistCliente { get; set; }

        /// <summary>
        /// Конструктор порожнього клієнта.
        /// </summary>
        public Cliente()
        {
            PlaylistCliente = new List<Playlist>();
        }

        /// <summary>
        /// Конструктор з основними даними.
        /// </summary>
        public Cliente(int id, string nome, string apellido, string idioma, string usuario, 
            string contrasena, string fecNac, string fecReg, bool esPremium)
        {
            Id = id;
            Nome = nome;
            Apellido = apellido;
            Idioma = idioma;
            Usuario = usuario;
            Contrasena = contrasena;
            FecNac = fecNac;
            FecReg = fecReg;
            EsPremium = esPremium;
            PlaylistCliente = new List<Playlist>();
        }

        public override string ToString()
        {
            return $"Cliente[id={Id}, nome={Nome}, usuario={Usuario}, premium={EsPremium}]";
        }
    }
}
