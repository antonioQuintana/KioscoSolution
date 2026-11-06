using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using KioscoApp.Business;
using KioscoApp.Models;

namespace KioscoApp.Views
{
    public class FrmTrazabilidadModal : Form
    {
        private readonly AuditoriaService _auditoriaService;
        private DataGridView dgvAuditoria = null!;
        private DateTimePicker dtpDesde = null!;
        private DateTimePicker dtpHasta = null!;
        private ComboBox cmbModulo = null!;
        private TextBox txtBuscar = null!;
        private Label lblTotalRegistros = null!;
        private List<RegistroAuditoria> _registrosActuales = new List<RegistroAuditoria>();

        public FrmTrazabilidadModal()
        {
            _auditoriaService = new AuditoriaService();
            InitializeCustomComponents();
            ConfigurarGrilla();
            CargarDatos();
        }

        private void InitializeCustomComponents()
        {
            Text = "Trazabilidad de Seguridad y Registro de Auditoría del Sistema";
            Size = new Size(1050, 680);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.FromArgb(30, 30, 30);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 9.5F);

            var lblTitulo = new Label
            {
                Text = "🛡️ Trazabilidad de Auditoría y Acciones Sensibles del Sistema",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 168, 255),
                Location = new Point(20, 15),
                AutoSize = true
            };

            var pnlFiltros = new Panel
            {
                Location = new Point(20, 50),
                Size = new Size(995, 55),
                BackColor = Color.FromArgb(38, 38, 42)
            };

            var lblDesde = new Label { Text = "Desde:", Location = new Point(10, 18), AutoSize = true, ForeColor = Color.White };
            dtpDesde = new DateTimePicker
            {
                Location = new Point(60, 15),
                Size = new Size(110, 25),
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Today.AddDays(-30)
            };

            var lblHasta = new Label { Text = "Hasta:", Location = new Point(180, 18), AutoSize = true, ForeColor = Color.White };
            dtpHasta = new DateTimePicker
            {
                Location = new Point(225, 15),
                Size = new Size(110, 25),
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Today.AddDays(1).AddSeconds(-1)
            };

            var lblMod = new Label { Text = "Módulo:", Location = new Point(345, 18), AutoSize = true, ForeColor = Color.White };
            cmbModulo = new ComboBox
            {
                Location = new Point(405, 15),
                Size = new Size(130, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.FromArgb(50, 50, 54),
                ForeColor = Color.White
            };
            cmbModulo.Items.AddRange(new object[] { "Todos", "BaseDeDatos", "Configuracion", "Caja", "Clientes", "Stock", "Ventas", "Seguridad" });
            cmbModulo.SelectedIndex = 0;

            var lblBuscar = new Label { Text = "Filtro:", Location = new Point(545, 18), AutoSize = true, ForeColor = Color.White };
            txtBuscar = new TextBox
            {
                Location = new Point(585, 15),
                Size = new Size(140, 25),
                BackColor = Color.FromArgb(50, 50, 54),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            var btnFiltrar = new Button
            {
                Text = "🔍 Filtrar",
                Location = new Point(735, 12),
                Size = new Size(85, 30),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnFiltrar.FlatAppearance.BorderSize = 0;
            btnFiltrar.Click += (s, e) => CargarDatos();

            var btnReset = new Button
            {
                Text = "Limpiar",
                Location = new Point(825, 12),
                Size = new Size(70, 30),
                BackColor = Color.FromArgb(127, 140, 141),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnReset.FlatAppearance.BorderSize = 0;
            btnReset.Click += (s, e) =>
            {
                dtpDesde.Value = DateTime.Today.AddDays(-30);
                dtpHasta.Value = DateTime.Today.AddDays(1).AddSeconds(-1);
                cmbModulo.SelectedIndex = 0;
                txtBuscar.Text = string.Empty;
                CargarDatos();
            };

            var btnExportar = new Button
            {
                Text = "📥 CSV",
                Location = new Point(900, 12),
                Size = new Size(85, 30),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnExportar.FlatAppearance.BorderSize = 0;
            btnExportar.Click += BtnExportar_Click;

            pnlFiltros.Controls.Add(lblDesde);
            pnlFiltros.Controls.Add(dtpDesde);
            pnlFiltros.Controls.Add(lblHasta);
            pnlFiltros.Controls.Add(dtpHasta);
            pnlFiltros.Controls.Add(lblMod);
            pnlFiltros.Controls.Add(cmbModulo);
            pnlFiltros.Controls.Add(lblBuscar);
            pnlFiltros.Controls.Add(txtBuscar);
            pnlFiltros.Controls.Add(btnFiltrar);
            pnlFiltros.Controls.Add(btnReset);
            pnlFiltros.Controls.Add(btnExportar);

            dgvAuditoria = new DataGridView
            {
                Location = new Point(20, 115),
                Size = new Size(995, 480),
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.FromArgb(45, 45, 48),
                ForeColor = Color.Black
            };

            lblTotalRegistros = new Label
            {
                Text = "Registros cargados: 0",
                Location = new Point(20, 608),
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(200, 200, 200)
            };

            var btnCerrar = new Button
            {
                Text = "Cerrar [Esc]",
                Location = new Point(885, 605),
                Size = new Size(130, 35),
                BackColor = Color.FromArgb(127, 140, 141),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.Click += (s, e) => Close();

            Controls.Add(lblTitulo);
            Controls.Add(pnlFiltros);
            Controls.Add(dgvAuditoria);
            Controls.Add(lblTotalRegistros);
            Controls.Add(btnCerrar);
        }

        private void ConfigurarGrilla()
        {
            dgvAuditoria.Columns.Clear();
            dgvAuditoria.AutoGenerateColumns = false;

            dgvAuditoria.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Fecha", HeaderText = "Fecha y Hora", FillWeight = 16 });
            dgvAuditoria.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Usuario", HeaderText = "Usuario", FillWeight = 12 });
            dgvAuditoria.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Rol", HeaderText = "Rol", FillWeight = 10 });
            dgvAuditoria.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Modulo", HeaderText = "Módulo", FillWeight = 12 });
            dgvAuditoria.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Accion", HeaderText = "Acción Realizada", FillWeight = 22 });
            dgvAuditoria.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Detalle", HeaderText = "Detalles / Parámetros", FillWeight = 28 });
        }

        private void CargarDatos()
        {
            try
            {
                DateTime desde = dtpDesde.Value.Date;
                DateTime hasta = dtpHasta.Value.Date.AddDays(1).AddSeconds(-1);
                string? mod = cmbModulo.SelectedItem?.ToString();
                if (mod == "Todos") mod = null;
                string? filtro = txtBuscar.Text.Trim();
                if (string.IsNullOrEmpty(filtro)) filtro = null;

                _registrosActuales = _auditoriaService.Obtener(desde, hasta, mod, filtro);

                var display = new List<object>();
                foreach (var r in _registrosActuales)
                {
                    display.Add(new
                    {
                        Fecha = r.Fecha.ToString("dd/MM/yyyy HH:mm:ss"),
                        r.Usuario,
                        r.Rol,
                        r.Modulo,
                        r.Accion,
                        r.Detalle
                    });
                }

                dgvAuditoria.DataSource = display;
                lblTotalRegistros.Text = $"Total de eventos de auditoría: {_registrosActuales.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al consultar auditoría: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExportar_Click(object sender, EventArgs e)
        {
            if (_registrosActuales == null || _registrosActuales.Count == 0)
            {
                MessageBox.Show("No hay datos cargados para exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var sfd = new SaveFileDialog
            {
                Filter = "Archivo CSV (*.csv)|*.csv",
                FileName = $"Auditoria_Kiosco_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("Id;Fecha;Usuario;Rol;Modulo;Accion;Detalle;Equipo");

                    foreach (var r in _registrosActuales)
                    {
                        string safeDetalle = (r.Detalle ?? "").Replace(";", ",").Replace("\r", " ").Replace("\n", " ");
                        sb.AppendLine($"{r.Id};{r.Fecha:yyyy-MM-dd HH:mm:ss};{r.Usuario};{r.Rol};{r.Modulo};{r.Accion};{safeDetalle};{r.IpEquipo}");
                    }

                    File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                    MessageBox.Show($"Registros exportados correctamente a:\n{sfd.FileName}", "Exportación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al exportar archivo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
