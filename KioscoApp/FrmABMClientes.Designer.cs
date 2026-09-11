namespace KioscoApp
{
    partial class FrmABMClientes
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) { components.Dispose(); } base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.dgvClientes = new System.Windows.Forms.DataGridView();
            this.lblDNI = new System.Windows.Forms.Label();
            this.txtDNI = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblTel = new System.Windows.Forms.Label();
            this.txtTel = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).BeginInit();
            this.SuspendLayout();
            
            this.dgvClientes.Location = new System.Drawing.Point(20, 20);
            this.dgvClientes.Size = new System.Drawing.Size(400, 300);
            
            this.lblDNI.Location = new System.Drawing.Point(450, 20);
            this.lblDNI.Text = "DNI / Doc:";
            this.txtDNI.Location = new System.Drawing.Point(450, 40);
            this.txtDNI.Size = new System.Drawing.Size(200, 23);
            
            this.lblNombre.Location = new System.Drawing.Point(450, 70);
            this.lblNombre.Text = "Nombre Completo:";
            this.txtNombre.Location = new System.Drawing.Point(450, 90);
            this.txtNombre.Size = new System.Drawing.Size(200, 23);
            
            this.lblTel.Location = new System.Drawing.Point(450, 120);
            this.lblTel.Text = "Teléfono:";
            this.txtTel.Location = new System.Drawing.Point(450, 140);
            this.txtTel.Size = new System.Drawing.Size(200, 23);

            this.btnGuardar.Location = new System.Drawing.Point(450, 200);
            this.btnGuardar.Size = new System.Drawing.Size(95, 30);
            this.btnGuardar.Text = "Guardar";

            this.btnEliminar.Location = new System.Drawing.Point(555, 200);
            this.btnEliminar.Size = new System.Drawing.Size(95, 30);
            this.btnEliminar.Text = "Eliminar";

            this.ClientSize = new System.Drawing.Size(700, 350);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.txtTel);
            this.Controls.Add(this.lblTel);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.txtDNI);
            this.Controls.Add(this.lblDNI);
            this.Controls.Add(this.dgvClientes);
            this.Text = "Gestión de Clientes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private System.Windows.Forms.DataGridView dgvClientes;
        private System.Windows.Forms.Label lblDNI;
        private System.Windows.Forms.TextBox txtDNI;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblTel;
        private System.Windows.Forms.TextBox txtTel;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnEliminar;
    }
}
