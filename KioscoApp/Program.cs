using System;
using System.Windows.Forms;

namespace KioscoApp
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (args != null && args.Length > 0 && args[0] == "--test-qa")
            {
                Tests.EdgeCaseRunner.RunAll();
                return;
            }

            ApplicationConfiguration.Initialize();
            // Iniciamos en Login para habilitar la navegacion
            Application.Run(new FrmLogin());
        }
    }
}
