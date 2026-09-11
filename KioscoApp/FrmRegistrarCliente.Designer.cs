namespace KioscoApp
{
    partial class FrmRegistrarCliente
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblEnConstruccion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblEnConstruccion
            // 
            this.lblEnConstruccion.AutoSize = true;
            this.lblEnConstruccion.Location = new System.Drawing.Point(50, 50);
            this.lblEnConstruccion.Name = "lblEnConstruccion";
            this.lblEnConstruccion.Size = new System.Drawing.Size(185, 15);
            this.lblEnConstruccion.TabIndex = 0;
            this.lblEnConstruccion.Text = "Formulario cliente (Próximamente)";
            // 
            // FrmRegistrarCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.lblEnConstruccion);
            this.Name = "FrmRegistrarCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Registrar Cliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblEnConstruccion;
    }
}
