using System;
using System.Windows.Forms;
using System.Runtime.Versioning;
using Spotify.WinForms.Views;
using System.IO;

namespace Spotify.WinForms
{
    [SupportedOSPlatform("windows")]
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            EnsureCancionesDirectory();

            Application.Run(new FormLogin());
        }

        private static void EnsureCancionesDirectory()
        {
            string dir = Path.Combine(Directory.GetCurrentDirectory(), "canciones");
            if (!Directory.Exists(dir)) 
                Directory.CreateDirectory(dir);
        }
    }
}
