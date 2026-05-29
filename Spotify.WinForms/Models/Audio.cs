using System;

namespace Spotify.WinForms.Models
{
    /// <summary>
    /// Базовий абстрактний клас для аудіо контенту (пісні та подкасти).
    /// Визначає загальні властивості та поведінку відтворення.
    /// </summary>
    public abstract class Audio
    {
        public int Id { get; set; }
        public string NomeAudio { get; set; }
        public string Arquivo { get; set; }
        public int Reproducoes { get; set; }
        public int DuracaoSegundos { get; set; }
        public string Tipo { get; set; }

        /// <summary>
        /// Конструктор порожнього аудіо.
        /// </summary>
        public Audio() { }

        /// <summary>
        /// Конструктор з основними даними.
        /// </summary>
        public Audio(int id, string nomeAudio, string arquivo, int reproducoes, int duracaoSegundos, string tipo)
        {
            Id = id;
            NomeAudio = nomeAudio;
            Arquivo = arquivo;
            Reproducoes = Math.Max(0, reproducoes);
            DuracaoSegundos = Math.Max(0, duracaoSegundos);
            Tipo = tipo;
        }

        /// <summary>
        /// Перетворює тривалість зі секунд у формат MM:SS.
        /// </summary>
        public string DuracaoConvertida()
        {
            int minutos = DuracaoSegundos / 60;
            int segundos = DuracaoSegundos % 60;
            return $"{minutos}:{segundos:D2}";
        }

        public override string ToString()
        {
            return $"Audio[id={Id}, nome={NomeAudio}, tipo={Tipo}, duracao={DuracaoConvertida()}]";
        }
    }
}
