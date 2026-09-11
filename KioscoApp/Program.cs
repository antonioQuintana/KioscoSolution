using System;
using System.Windows.Forms;

namespace KioscoApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            // Iniciamos en Login para habilitar la navegación
            Application.Run(new FrmLogin());
        }
    }
}
