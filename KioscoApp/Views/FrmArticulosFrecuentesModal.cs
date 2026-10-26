using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using KioscoApp.Business;
using KioscoApp.Models;

namespace KioscoApp.Views
{
    public class FrmArticulosFrecuentesModal : Form
    {
        public Producto? ProductoSeleccionado { get; private set; }

        private readonly ProductoService _productoService;

        public FrmArticulosFrecuentesModal()
        {
            _productoService = new ProductoService();

            this.Text = "Artículos Frecuentes / Rápidos";
            this.Size = new Size(650, 420);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(45, 45, 48);
            this.ForeColor = Color.White;

            InicializarComponentes();
        }

        private void InicializarComponentes()
        {
            var lblTitulo = new Label
            {
                Text = "⚡ Artículos de Alta Rotación (1 Clic para agregar)",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(20, 15),
                AutoSize = true
            };

            var flowPanel = new FlowLayoutPanel
            {
                Location = new Point(20, 50),
                Size = new Size(600, 310),
                AutoScroll = true
            };

            var productos = _productoService.ObtenerTodos()?.Take(12).ToList() ?? new List<Producto>();

            foreach (var prod in productos)
            {
                var btn = new Button
                {
                    Size = new Size(185, 90),
                    BackColor = Color.FromArgb(63, 63, 70),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand,
                    Margin = new Padding(6),
                    Text = $"{prod.Nombre}\n\n$ {prod.PrecioVenta:N2}",
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                };
                btn.FlatAppearance.BorderColor = Color.SeaGreen;
                btn.FlatAppearance.BorderSize = 1;

                btn.Click += (s, e) =>
                {
                    ProductoSeleccionado = prod;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                };

                flowPanel.Controls.Add(btn);
            }

            this.Controls.Add(lblTitulo);
            this.Controls.Add(flowPanel);
        }
    }
}
