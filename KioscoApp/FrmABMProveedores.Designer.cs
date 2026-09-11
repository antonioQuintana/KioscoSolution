namespace KioscoApp
{
    partial class FrmABMProveedores
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) { components.Dispose(); } base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.dgvProveedores = new System.Windows.Forms.DataGridView();
            this.lblRazonSocial = new System.Windows.Forms.Label();
            this.txtRazonSocial = new System.Windows.Forms.TextBox();
            this.lblCuit = new System.Windows.Forms.Label();
            this.txtCuit = new System.Windows.Forms.TextBox();
            this.lblTel = new System.Windows.Forms.Label();
            this.txtTel = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProveedores)).BeginInit();
            this.SuspendLayout();
            
            this.dgvProveedores.Location = new System.Drawing.Point(20, 20);
            this.dgvProveedores.Size = new System.Drawing.Size(400, 300);
            
            this.lblRazonSocial.Location = new System.Drawing.Point(450, 20);
            this.lblRazonSocial.Text = "Razón Social:";
            this.txtRazonSocial.Location = new System.Drawing.Point(450, 40);
            this.txtRazonSocial.Size = new System.Drawing.Size(200, 23);
            
            this.lblCuit.Location = new System.Drawing.Point(450, 70);
            this.lblCuit.Text = "CUIT/RUT:";
            this.txtCuit.Location = new System.Drawing.Point(450, 90);
            this.txtCuit.Size = new System.Drawing.Size(200, 23);
            
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
            this.Controls.Add(this.txtCuit);
            this.Controls.Add(this.lblCuit);
            this.Controls.Add(this.txtRazonSocial);
            this.Controls.Add(this.lblRazonSocial);
            this.Controls.Add(this.dgvProveedores);
            this.Text = "Gestión de Proveedores";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.dgvProveedores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private System.Windows.Forms.DataGridView dgvProveedores;
        private System.Windows.Forms.Label lblRazonSocial;
        private System.Windows.Forms.TextBox txtRazonSocial;
        private System.Windows.Forms.Label lblCuit;
        private System.Windows.Forms.TextBox txtCuit;
        private System.Windows.Forms.Label lblTel;
        private System.Windows.Forms.TextBox txtTel;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnEliminar;
    }
}
