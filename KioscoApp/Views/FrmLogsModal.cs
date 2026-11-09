using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using KioscoApp.Utils;

namespace KioscoApp.Views
{
    public class FrmLogsModal : Form
    {
        private class LogEntry
        {
            public DateTime Fecha { get; set; } = DateTime.MinValue;
            public string Nivel { get; set; } = "INFO";
            public string Modulo { get; set; } = "General";
            public string Usuario { get; set; } = "";
            public string Mensaje { get; set; } = "";
            public string TextoCompleto { get; set; } = "";
        }

        private TextBox txtLogs = null!;
        private TextBox txtFiltroTexto = null!;
        private ComboBox cmbNivel = null!;
        private ComboBox cmbModulo = null!;
        private CheckBox chkFiltrarFecha = null!;
        private DateTimePicker dtpDesde = null!;
        private DateTimePicker dtpHasta = null!;
        private Label lblResumenLogs = null!;

        private List<LogEntry> _todasLasEntradas = new List<LogEntry>();

        public FrmLogsModal()
        {
            InitializeCustomComponents();
            CargarLogs();
        }

        private void InitializeCustomComponents()
        {
            Text = "Visor de Registros de Seguridad y Diagnóstico del Sistema (Logs)";
            ClientSize = new Size(1000, 700);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.FromArgb(30, 30, 30);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 9.5F);

            var lblTitulo = new Label
            {
                Text = "📋 Registro de Errores y Diagnóstico de la Aplicación",
                Font = new Font("Segoe UI", 12.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 168, 255),
                Location = new Point(20, 12),
                AutoSize = true
            };

            // Panel de Filtros Multicriterio (Fechas, Nivel, Módulo y Búsqueda)
            var pnlFiltros = new Panel
            {
                Location = new Point(20, 45),
                Size = new Size(960, 80),
                BackColor = Color.FromArgb(38, 38, 42),
                BorderStyle = BorderStyle.FixedSingle
            };

            // --- FILA 1: FILTROS DE FECHA Y ACCIONES DE ARCHIVO ---
            chkFiltrarFecha = new CheckBox
            {
                Text = "Filtrar Fechas:",
                Location = new Point(12, 10),
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.White,
                Checked = true
            };
            chkFiltrarFecha.CheckedChanged += (s, e) =>
            {
                dtpDesde.Enabled = chkFiltrarFecha.Checked;
                dtpHasta.Enabled = chkFiltrarFecha.Checked;
                AplicarFiltros();
            };

            dtpDesde = new DateTimePicker
            {
                Location = new Point(125, 7),
                Size = new Size(105, 25),
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Today.AddDays(-7)
            };
            dtpDesde.ValueChanged += (s, e) => AplicarFiltros();

            var lblHasta = new Label
            {
                Text = "Hasta:",
                Location = new Point(238, 11),
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.White
            };

            dtpHasta = new DateTimePicker
            {
                Location = new Point(285, 7),
                Size = new Size(105, 25),
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Today
            };
            dtpHasta.ValueChanged += (s, e) => AplicarFiltros();

            var btnHoy = CrearBotonRapido("Hoy", 400, 6, 50, () =>
            {
                dtpDesde.Value = DateTime.Today;
                dtpHasta.Value = DateTime.Today;
                chkFiltrarFecha.Checked = true;
                AplicarFiltros();
            });

            var btn7Dias = CrearBotonRapido("7 Días", 455, 6, 60, () =>
            {
                dtpDesde.Value = DateTime.Today.AddDays(-7);
                dtpHasta.Value = DateTime.Today;
                chkFiltrarFecha.Checked = true;
                AplicarFiltros();
            });

            var btnVerTodo = CrearBotonRapido("Ver Todo", 520, 6, 75, () =>
            {
                chkFiltrarFecha.Checked = false;
                cmbNivel.SelectedIndex = 0;
                cmbModulo.SelectedIndex = 0;
                txtFiltroTexto.Clear();
                AplicarFiltros();
            });

            var btnActualizar = new Button
            {
                Text = "🔄 Refrescar",
                Location = new Point(690, 5),
                Size = new Size(115, 28),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnActualizar.FlatAppearance.BorderSize = 0;
            btnActualizar.Click += (s, e) => CargarLogs();

            var btnAbrirNotepad = new Button
            {
                Text = "📂 Abrir en Bloc",
                Location = new Point(815, 5),
                Size = new Size(130, 28),
                BackColor = Color.FromArgb(108, 92, 231),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnAbrirNotepad.FlatAppearance.BorderSize = 0;
            btnAbrirNotepad.Click += BtnAbrirNotepad_Click;

            pnlFiltros.Controls.Add(chkFiltrarFecha);
            pnlFiltros.Controls.Add(dtpDesde);
            pnlFiltros.Controls.Add(lblHasta);
            pnlFiltros.Controls.Add(dtpHasta);
            pnlFiltros.Controls.Add(btnHoy);
            pnlFiltros.Controls.Add(btn7Dias);
            pnlFiltros.Controls.Add(btnVerTodo);
            pnlFiltros.Controls.Add(btnActualizar);
            pnlFiltros.Controls.Add(btnAbrirNotepad);

            // --- FILA 2: NIVEL, MÓDULO Y BÚSQUEDA LIBRE ---
            var lblNivel = new Label
            {
                Text = "Nivel:",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(12, 45),
                AutoSize = true
            };
            pnlFiltros.Controls.Add(lblNivel);

            cmbNivel = new ComboBox
            {
                Location = new Point(58, 42),
                Size = new Size(115, 26),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.FromArgb(50, 50, 54),
                ForeColor = Color.White
            };
            cmbNivel.Items.AddRange(new object[] { "Todos", "ERROR", "WARN", "INFO" });
            cmbNivel.SelectedIndex = 0;
            cmbNivel.SelectedIndexChanged += (s, e) => AplicarFiltros();
            pnlFiltros.Controls.Add(cmbNivel);

            var lblModulo = new Label
            {
                Text = "Módulo:",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(185, 45),
                AutoSize = true
            };
            pnlFiltros.Controls.Add(lblModulo);

            cmbModulo = new ComboBox
            {
                Location = new Point(250, 42),
                Size = new Size(155, 26),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.FromArgb(50, 50, 54),
                ForeColor = Color.White
            };
            cmbModulo.Items.AddRange(new object[] { "Todos los Módulos", "Seguridad", "BaseDeDatos", "Configuracion", "Caja", "Clientes", "Stock", "Ventas" });
            cmbModulo.SelectedIndex = 0;
            cmbModulo.SelectedIndexChanged += (s, e) => AplicarFiltros();
            pnlFiltros.Controls.Add(cmbModulo);

            var lblBuscar = new Label
            {
                Text = "Buscar:",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(418, 45),
                AutoSize = true
            };
            pnlFiltros.Controls.Add(lblBuscar);

            txtFiltroTexto = new TextBox
            {
                Location = new Point(475, 42),
                Size = new Size(330, 25),
                BackColor = Color.FromArgb(50, 50, 54),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            txtFiltroTexto.TextChanged += (s, e) => AplicarFiltros();
            pnlFiltros.Controls.Add(txtFiltroTexto);

            var btnLimpiarFiltro = new Button
            {
                Text = "✖ Limpiar",
                Location = new Point(815, 41),
                Size = new Size(130, 27),
                BackColor = Color.FromArgb(127, 140, 141),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnLimpiarFiltro.FlatAppearance.BorderSize = 0;
            btnLimpiarFiltro.Click += (s, e) =>
            {
                cmbNivel.SelectedIndex = 0;
                cmbModulo.SelectedIndex = 0;
                txtFiltroTexto.Clear();
                AplicarFiltros();
            };
            pnlFiltros.Controls.Add(btnLimpiarFiltro);

            // Resumen de Registros y Estadísticas
            lblResumenLogs = new Label
            {
                Location = new Point(22, 133),
                Size = new Size(955, 22),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(200, 214, 229),
                Text = "Cargando registros..."
            };

            // Visor de Registros
            txtLogs = new TextBox
            {
                Location = new Point(20, 160),
                Size = new Size(960, 455),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Both,
                WordWrap = false,
                BackColor = Color.FromArgb(18, 18, 18),
                ForeColor = Color.FromArgb(230, 230, 230),
                Font = new Font("Consolas", 9.5F)
            };

            // Botones inferiores con amplio margen
            var btnCopiar = new Button
            {
                Text = "📋 Copiar Logs al Portapapeles",
                Location = new Point(20, 630),
                Size = new Size(250, 42),
                BackColor = Color.FromArgb(52, 73, 94),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCopiar.FlatAppearance.BorderSize = 0;
            btnCopiar.Click += (s, e) =>
            {
                if (!string.IsNullOrEmpty(txtLogs.Text))
                {
                    Clipboard.SetText(txtLogs.Text);
                    MessageBox.Show("¡Registros copiados al portapapeles!", "Copiado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };

            var btnLimpiar = new Button
            {
                Text = "🗑️ Limpiar Archivo de Logs",
                Location = new Point(285, 630),
                Size = new Size(220, 42),
                BackColor = Color.FromArgb(192, 57, 43),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnLimpiar.FlatAppearance.BorderSize = 0;
            btnLimpiar.Click += BtnLimpiar_Click;

            var btnCerrar = new Button
            {
                Text = "Cerrar [Esc]",
                Location = new Point(830, 630),
                Size = new Size(150, 42),
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
            Controls.Add(lblResumenLogs);
            Controls.Add(txtLogs);
            Controls.Add(btnCopiar);
            Controls.Add(btnLimpiar);
            Controls.Add(btnCerrar);
        }

        private Button CrearBotonRapido(string texto, int x, int y, int width, Action onClick)
        {
            var btn = new Button
            {
                Text = texto,
                Location = new Point(x, y),
                Size = new Size(width, 27),
                BackColor = Color.FromArgb(60, 60, 66),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += (s, e) => onClick();
            return btn;
        }

        private void CargarLogs()
        {
            string contenido = Logger.LeerLogs(5000);
            var lineas = contenido.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            _todasLasEntradas = ParsearLogEntries(lineas);
            AplicarFiltros();
        }

        private List<LogEntry> ParsearLogEntries(string[] lineas)
        {
            var lista = new List<LogEntry>();
            LogEntry? actual = null;

            foreach (var linea in lineas)
            {
                if (string.IsNullOrWhiteSpace(linea)) continue;

                // Formato de Logger: [yyyy-MM-dd HH:mm:ss] [NIVEL] [MODULO] [USUARIO] Mensaje
                if (linea.StartsWith("[") && linea.Length > 21 && DateTime.TryParse(linea.Substring(1, 19), out DateTime dt))
                {
                    actual = new LogEntry
                    {
                        Fecha = dt,
                        TextoCompleto = linea
                    };

                    int idxNivelStart = linea.IndexOf('[', 21);
                    if (idxNivelStart > 0)
                    {
                        int idxNivelEnd = linea.IndexOf(']', idxNivelStart);
                        if (idxNivelEnd > 0)
                        {
                            actual.Nivel = linea.Substring(idxNivelStart + 1, idxNivelEnd - idxNivelStart - 1).Trim();

                            int idxModStart = linea.IndexOf('[', idxNivelEnd);
                            if (idxModStart > 0)
                            {
                                int idxModEnd = linea.IndexOf(']', idxModStart);
                                if (idxModEnd > 0)
                                {
                                    actual.Modulo = linea.Substring(idxModStart + 1, idxModEnd - idxModStart - 1).Trim();

                                    int idxUserStart = linea.IndexOf('[', idxModEnd);
                                    if (idxUserStart > 0)
                                    {
                                        int idxUserEnd = linea.IndexOf(']', idxUserStart);
                                        if (idxUserEnd > 0)
                                        {
                                            actual.Usuario = linea.Substring(idxUserStart + 1, idxUserEnd - idxUserStart - 1).Trim();
                                            actual.Mensaje = linea.Substring(idxUserEnd + 1).Trim();
                                        }
                                    }
                                }
                            }
                        }
                    }

                    lista.Add(actual);
                }
                else if (actual != null)
                {
                    // Línea continua (ej. StackTrace)
                    actual.TextoCompleto += Environment.NewLine + linea;
                    actual.Mensaje += " " + linea;
                }
                else
                {
                    lista.Add(new LogEntry { Fecha = DateTime.MinValue, TextoCompleto = linea, Mensaje = linea });
                }
            }

            return lista;
        }

        private void AplicarFiltros()
        {
            if (_todasLasEntradas == null || _todasLasEntradas.Count == 0)
            {
                txtLogs.Text = "No hay registros disponibles en el archivo de log.";
                lblResumenLogs.Text = "No hay registros disponibles.";
                return;
            }

            bool filtrarFecha = chkFiltrarFecha != null && chkFiltrarFecha.Checked;
            DateTime fDesde = dtpDesde?.Value.Date ?? DateTime.MinValue;
            DateTime fHasta = dtpHasta?.Value.Date.AddDays(1).AddSeconds(-1) ?? DateTime.MaxValue;

            string nivel = cmbNivel?.SelectedItem?.ToString() ?? "Todos";
            string modulo = cmbModulo?.SelectedItem?.ToString() ?? "Todos los Módulos";
            string busqueda = txtFiltroTexto?.Text.Trim() ?? "";

            var filtrados = _todasLasEntradas.Where(entry =>
            {
                // Filtro Fecha
                if (filtrarFecha && entry.Fecha != DateTime.MinValue)
                {
                    if (entry.Fecha < fDesde || entry.Fecha > fHasta) return false;
                }

                // Filtro Nivel
                if (nivel != "Todos" && !entry.Nivel.Equals(nivel, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }

                // Filtro Módulo
                if (modulo != "Todos los Módulos" && !entry.Modulo.Equals(modulo, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }

                // Filtro Búsqueda
                if (!string.IsNullOrEmpty(busqueda) && !entry.TextoCompleto.Contains(busqueda, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }

                return true;
            }).ToList();

            var sb = new System.Text.StringBuilder();
            foreach (var f in filtrados)
            {
                sb.AppendLine(f.TextoCompleto);
            }

            txtLogs.Text = sb.Length > 0 ? sb.ToString() : "No se encontraron líneas que coincidan con los filtros aplicados.";
            txtLogs.SelectionStart = txtLogs.Text.Length;
            txtLogs.ScrollToCaret();

            // Estadísticas en tiempo real
            int errores = filtrados.Count(e => e.Nivel.Equals("ERROR", StringComparison.OrdinalIgnoreCase));
            int warnings = filtrados.Count(e => e.Nivel.Equals("WARN", StringComparison.OrdinalIgnoreCase));
            int infos = filtrados.Count(e => e.Nivel.Equals("INFO", StringComparison.OrdinalIgnoreCase));

            lblResumenLogs.Text = $"Mostrando: {filtrados.Count} registros de {_todasLasEntradas.Count}  |  🔴 Errores: {errores}  |  🟡 Advertencias: {warnings}  |  🔵 Informativos: {infos}";
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "¿Está seguro de que desea reiniciar y vaciar el archivo de logs del sistema?\n\nEsta acción no se puede deshacer.",
                "Confirmar Limpieza de Logs",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                Logger.LimpiarLogs();
                CargarLogs();
            }
        }

        private void BtnAbrirNotepad_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(Logger.LogFilePath))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "notepad.exe",
                        Arguments = $"\"{Logger.LogFilePath}\"",
                        UseShellExecute = true
                    });
                }
                else
                {
                    MessageBox.Show("El archivo de logs aún no ha sido creado en el disco.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el archivo de logs: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
