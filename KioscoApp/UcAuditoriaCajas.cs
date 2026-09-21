using System;
using System.Windows.Forms;

namespace KioscoApp
{
    public partial class UcAuditoriaCajas : UserControl
    {
        public UcAuditoriaCajas()
        {
            InitializeComponent();
        }

        private void MostrarProximamente(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string modulo = btn != null ? btn.Text : "Esta funciÃ³n";
            MessageBox.Show($"MÃ³dulo en desarrollo:\n\n[{modulo}]\n\nEstarÃ¡ disponible prÃ³ximamente para el perfil Supervisor.", "FunciÃ³n No Disponible", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
