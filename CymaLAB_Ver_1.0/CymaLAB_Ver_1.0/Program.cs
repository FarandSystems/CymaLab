using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Drawing;
using Splash_Screen;

namespace CymaLAB_Ver_1._0
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (var stream = typeof(Program).Assembly.GetManifestResourceStream("CymaLab.PreciTestSplash.png"))
            using (var artwork = Image.FromStream(stream))
            using (var splash = SplashScreen.Show(artwork))
            {
                RunMainApplication(splash);
            }
        }

        // Keep main-form type resolution and JIT work behind the painted splash.
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void RunMainApplication(SplashScreen splash)
        {
            Form1 mainWindow = null;
            try
            {
                splash.Report("Building application interface");
                mainWindow = new Form1();
                // Create the UI handle before services can post callbacks to it.
                var handle = mainWindow.Handle;
                mainWindow.PrepareForStartup(splash);
                splash.Report("Ready — opening CymaLab");
            }
            catch (Exception ex)
            {
                splash.Dispose();
                mainWindow?.Dispose();
                MessageBox.Show("CymaLab could not start." + Environment.NewLine + ex.Message,
                    "CymaLab startup", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (mainWindow)
            {
                mainWindow.Shown += (sender, args) => splash.Complete(mainWindow);
                Application.Run(mainWindow);
            }
        }
    }
}
