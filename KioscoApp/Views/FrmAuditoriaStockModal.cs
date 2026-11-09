using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KioscoApp.Business;
using KioscoApp.Models;

namespace KioscoApp.Views
{
    public class FrmAuditoriaStockModal : Form
    {
        private readonly AuditoriaService _auditoriaService;
        private List<RegistroAuditoria> _registros = new List<RegistroAuditoria>();

        private DateTimePicker dtpDesde = null!;
        private DateTimePicker dtpHasta = null!;
        private TextBox txtFiltro = null!;
        private DataGridView dgvAuditoria = null!;
        private Label lblTotal = null!;

        public FrmAuditoriaStockModal()
        {
            _auditoriaService = new AuditoriaService();
            InitializeCustomComponents();
            CargarAuditoria();
        }

        private void InitializeCustomComponents()
        {
            Text = "Auditoría y Trazabilidad de Ajustes de Stock";
            ClientSize = new Size(1020, 680);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.FromArgb(28, 28, 30);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 9.5F);
            KeyPreview = true;
            KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) Close(); };

            var lblTitulo = new Label
            {
                Text = "📋 Registro Histórico de Modificaciones Manuales de Stock",
                Font = new Font("Segoe UI", 12.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 168, 255),
                Location = new Point(20, 15),
                AutoSize = true
            };

            // Panel filtros
            var pnlFiltros = new Panel
            {
                Location = new Point(20, 50),
                Size = new Size(980, 50),
                BackColor = Color.FromArgb(38, 38, 42),
                BorderStyle = BorderStyle.FixedSingle
            };

            var lblDesde = new Label
            {
                Text = "Desde:",
                Location = new Point(12, 14),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.White
            };

            dtpDesde = new DateTimePicker
            {
                Location = new Point(65, 11),
                Size = new Size(105, 25),
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Today.AddDays(-30)
            };
            dtpDesde.ValueChanged += (s, e) => CargarAuditoria();

            var lblHasta = new Label
            {
                Text = "Hasta:",
                Location = new Point(180, 14),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.White
            };

            dtpHasta = new DateTimePicker
            {
                Location = new Point(228, 11),
                Size = new Size(105, 25),
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Today
            };
            dtpHasta.ValueChanged += (s, e) => CargarAuditoria();

            var lblBuscar = new Label
            {
                Text = "Buscar:",
                Location = new Point(345, 14),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.White
            };

            txtFiltro = new TextBox
            {
                Location = new Point(398, 11),
                Size = new Size(200, 25),
                BackColor = Color.FromArgb(50, 50, 55),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            txtFiltro.TextChanged += (s, e) => AplicarFiltroLocal();

            var btnHoy = new Button
            {
                Text = "Hoy",
                Location = new Point(610, 10),
                Size = new Size(50, 28),
                BackColor = Color.FromArgb(60, 60, 65),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnHoy.Click += (s, e) =>
            {
                dtpDesde.Value = DateTime.Today;
                dtpHasta.Value = DateTime.Today;
            };

            var btnTodo = new Button
            {
                Text = "Ver Todo",
                Location = new Point(668, 10),
                Size = new Size(75, 28),
                BackColor = Color.FromArgb(60, 60, 65),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnTodo.Click += (s, e) =>
            {
                dtpDesde.Value = DateTime.Today.AddYears(-1);
                dtpHasta.Value = DateTime.Today;
            };

            var btnExportarCSV = new Button
            {
                Text = "📥 Exportar CSV",
                Location = new Point(830, 10),
                Size = new Size(135, 28),
                BackColor = Color.FromArgb(39, 174, 96),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnExportarCSV.Click += (s, e) => ExportarACSV();

            pnlFiltros.Controls.AddRange(new Control[] { lblDesde, dtpDesde, lblHasta, dtpHasta, lblBuscar, txtFiltro, btnHoy, btnTodo, btnExportarCSV });

            // DataGridView
            dgvAuditoria = new DataGridView
            {
                Location = new Point(20, 110),
                Size = new Size(980, 490),
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.FromArgb(32, 32, 36),
                BorderStyle = BorderStyle.Fixed3D
            };

            dgvAuditoria.DefaultCellStyle.BackColor = Color.FromArgb(45, 45, 48);
            dgvAuditoria.DefaultCellStyle.ForeColor = Color.White;
            dgvAuditoria.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 122, 204);
            dgvAuditoria.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvAuditoria.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 30, 32);
            dgvAuditoria.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvAuditoria.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvAuditoria.EnableHeadersVisualStyles = false;
            dgvAuditoria.RowHeadersVisible = false;

            // Barra estado inferior
            lblTotal = new Label
            {
                Location = new Point(20, 615),
                Size = new Size(800, 30),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(200, 200, 200),
                Text = "Cargando registros..."
            };

            var btnCerrar = new Button
            {
                Text = "Cerrar [Esc]",
                Location = new Point(850, 615),
                Size = new Size(150, 42),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnCerrar.Click += (s, e) => Close();

            Controls.AddRange(new Control[] { lblTitulo, pnlFiltros, dgvAuditoria, lblTotal, btnCerrar });
        }

        private void CargarAuditoria()
        {
            try
            {
                DateTime desde = dtpDesde.Value.Date;
                DateTime hasta = dtpHasta.Value.Date.AddDays(1).AddSeconds(-1);

                _registros = _auditoriaService.Obtener(desde, hasta, modulo: "Stock", filtro: null);
                AplicarFiltroLocal();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar auditoría de stock: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AplicarFiltroLocal()
        {
            string busqueda = txtFiltro.Text.Trim();

            var filtrados = _registros.Where(r =>
            {
                if (string.IsNullOrEmpty(busqueda)) return true;

                return (r.Usuario ?? "").Contains(busqueda, StringComparison.OrdinalIgnoreCase) ||
                       (r.Detalle ?? "").Contains(busqueda, StringComparison.OrdinalIgnoreCase) ||
                       (r.Accion ?? "").Contains(busqueda, StringComparison.OrdinalIgnoreCase);
            }).ToList();

            var display = filtrados.Select(r => new
            {
                r.Id,
                Fecha = r.Fecha.ToString("dd/MM/yyyy HH:mm:ss"),
                r.Usuario,
                r.Rol,
                Operacion = r.Accion,
                DetalleAjuste = r.Detalle,
                Terminal = r.IpEquipo
            }).ToList();

            dgvAuditoria.DataSource = display;

            lblTotal.Text = $"Registros de stock auditados en período: {filtrados.Count} (de {_registros.Count} totales)";
        }

        private void ExportarACSV()
        {
            if (dgvAuditoria.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var sfd = new SaveFileDialog
                {
                    Filter = "Archivo CSV compatible con Excel (*.csv)|*.csv",
                    FileName = $"Auditoria_Stock_{DateTime.Now:yyyyMMdd_HHmm}.csv",
                    Title = "Exportar Historial de Ajustes de Stock"
                };

                if (sfd.ShowDialog() != DialogResult.OK) return;

                var sb = new StringBuilder();
                var headers = new List<string>();
                foreach (DataGridViewColumn col in dgvAuditoria.Columns)
                {
                    headers.Add(col.HeaderText);
                }
                sb.AppendLine(string.Join(";", headers));

                foreach (DataGridViewRow row in dgvAuditoria.Rows)
                {
                    var cells = new List<string>();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        string val = cell.Value?.ToString() ?? "";
                        cells.Add(val.Replace(";", " ").Replace("\r", " ").Replace("\n", " "));
                    }
                    sb.AppendLine(string.Join(";", cells));
                }

                File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);

                MessageBox.Show(
                    $"Reporte exportado exitosamente a:\n\n{sfd.FileName}\n\nPuede abrirlo en Excel para control de auditoría.",
                    "Exportación Exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
