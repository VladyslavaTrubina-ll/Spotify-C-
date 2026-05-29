using System;

namespace Spotify.WinForms.Models
{
    /// <summary>
    /// Представляє подкаст.
    /// </summary>
    public class Podcast : Audio
    {
        public string Colaboradores { get; set; }
        public int IdPodcaster { get; set; }

        /// <summary>
        /// Конструктор подкаста.
        /// </summary>
        public Podcast(int id, string nomeAudio, string arquivo, int reproducoes, int duracaoSegundos,
            string colaboradores, int idPodcaster, string tipo)
            : base(id, nomeAudio, arquivo, reproducoes, duracaoSegundos, tipo)
        {
            Colaboradores = colaboradores;
            IdPodcaster = idPodcaster;
        }

        public override string ToString()
        {
            return $"Podcast[id={Id}, nome={NomeAudio}, podcaster={IdPodcaster}, duracao={DuracaoConvertida()}]";
        }
    }
}
