using System;
using System.Windows.Forms;

namespace KioscoApp
{
    public partial class UcConfiguracion : UserControl
    {
        public UcConfiguracion()
        {
            InitializeComponent();
        }

        private void MostrarProximamente(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string modulo = btn != null ? btn.Text : "Esta funciÃ³n";
            MessageBox.Show($"MÃ³dulo en desarrollo:\n\n[{modulo}]\n\nEstarÃ¡ disponible prÃ³ximamente en futuras actualizaciones.", "FunciÃ³n No Disponible", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
