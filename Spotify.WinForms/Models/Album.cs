using System;

namespace Spotify.WinForms.Models
{
    /// <summary>
    /// Представляє альбом музики.
    /// </summary>
    public class Album
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Ano { get; set; }
        public string Genero { get; set; }
        public string Imagen { get; set; }
        public int IdMusico { get; set; }

        /// <summary>
        /// Конструктор порожнього альбому.
        /// </summary>
        public Album() { }

        /// <summary>
        /// Конструктор з усіма параметрами.
        /// </summary>
        public Album(int id, string titulo, string ano, string genero, string imagen, int idMusico)
        {
            Id = id;
            Titulo = titulo;
            Ano = ano;
            Genero = genero;
            Imagen = imagen;
            IdMusico = idMusico;
        }

        public string FechaPub => Ano;
        public string Foto => Imagen;

        public override string ToString()
        {
            return $"Album[id={Id}, titulo={Titulo}, ano={Ano}, genero={Genero}]";
        }
    }
}
