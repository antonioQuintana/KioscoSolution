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
            string modulo = btn != null ? btn.Text : "Esta función";
            MessageBox.Show($"Módulo en desarrollo:\n\n[{modulo}]\n\nEstará disponible próximamente para el perfil Supervisor.", "Función No Disponible", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
