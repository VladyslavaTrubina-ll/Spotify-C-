using System;
using System.Windows.Forms;
using System.Runtime.Versioning;
using Spotify.WinForms.Views;

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
            Application.Run(new FormLogin());
        }
    }
}
