using System;
using System.Windows.Forms;
using System.Runtime.Versioning;
using Spotify.WinForms.Views;
using Spotify.WinForms.Controllers;
using System.IO;
using System.Linq;

namespace Spotify.WinForms
{
    /// <summary>
    /// Точка входу програми Spotify.
    /// Запускає головну форму входу.
    /// </summary>
    [SupportedOSPlatform("windows")]
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            EnsureCancionesDirectory();
            CopyMusicFromWorkspace();
            EnsureTestWav();

            // If caller passed "musica" or "calc" as argument, allow launching alternative forms for testing,
            // otherwise start the normal login flow.
            string[] args = Environment.GetCommandLineArgs();
            bool launchMusic = false;
            bool launchCalc = false;
            foreach (var a in args)
            {
                if (string.Equals(a, "musica", StringComparison.OrdinalIgnoreCase)) { launchMusic = true; }
                if (string.Equals(a, "calc", StringComparison.OrdinalIgnoreCase)) { launchCalc = true; }
            }

            if (launchCalc)
                Application.Run(new Views.FormFunctionCalc());
            else if (launchMusic)
                Application.Run(new FormMusica(new ControladorDB()));
            else
                Application.Run(new FormLogin());
        }

        private static void CopyMusicFromWorkspace()
        {
            try
            {
                string dir = Directory.GetCurrentDirectory();
                string musicFolder = null;
                for (int i = 0; i < 8; i++)
                {
                    string candidate = Path.Combine(dir, "Music");
                    if (Directory.Exists(candidate)) { musicFolder = candidate; break; }
                    var parent = Directory.GetParent(dir);
                    if (parent == null) break;
                    dir = parent.FullName;
                }

                if (musicFolder == null) return;

                string dest = Path.Combine(Directory.GetCurrentDirectory(), "canciones");
                var files = Directory.EnumerateFiles(musicFolder, "*.*", SearchOption.TopDirectoryOnly)
                    .Where(f => f.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase) || f.EndsWith(".wav", StringComparison.OrdinalIgnoreCase));

                int copied = 0;
                foreach (var f in files)
                {
                    string name = Path.GetFileName(f);
                    string destPath = Path.Combine(dest, name);
                    if (!File.Exists(destPath))
                    {
                        File.Copy(f, destPath);
                        copied++;
                    }
                }

                if (copied > 0)
                    Console.WriteLine($"Copied {copied} music file(s) from {musicFolder} to {dest}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error copying music files: {ex.Message}");
            }
        }

        private static void EnsureCancionesDirectory()
        {
            string dir = Path.Combine(Directory.GetCurrentDirectory(), "canciones");
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        }

        // Creates a short (1s) silent 16-bit 44.1kHz mono WAV for quick testing if not present
        private static void EnsureTestWav()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "canciones", "test_silence.wav");
            if (File.Exists(path)) return;

            int sampleRate = 44100;
            short bitsPerSample = 16;
            short channels = 1;
            int seconds = 1;

            int byteRate = sampleRate * channels * (bitsPerSample / 8);
            short blockAlign = (short)(channels * (bitsPerSample / 8));
            int dataSize = sampleRate * channels * (bitsPerSample / 8) * seconds;

            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            using (var bw = new BinaryWriter(fs))
            {
                // RIFF header
                bw.Write(System.Text.Encoding.ASCII.GetBytes("RIFF"));
                bw.Write(36 + dataSize);
                bw.Write(System.Text.Encoding.ASCII.GetBytes("WAVE"));

                // fmt chunk
                bw.Write(System.Text.Encoding.ASCII.GetBytes("fmt "));
                bw.Write(16); // Subchunk1Size for PCM
                bw.Write((short)1); // AudioFormat PCM
                bw.Write(channels);
                bw.Write(sampleRate);
                bw.Write(byteRate);
                bw.Write(blockAlign);
                bw.Write(bitsPerSample);

                // data chunk
                bw.Write(System.Text.Encoding.ASCII.GetBytes("data"));
                bw.Write(dataSize);

                // write silence
                for (int i = 0; i < dataSize; i++) bw.Write((byte)0);
            }
        }
    }
}
