using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using KioscoApp.Utils;

namespace KioscoApp.Views
{
    public class FrmLogsModal : Form
    {
        private TextBox txtLogs = null!;
        private TextBox txtFiltroTexto = null!;
        private ComboBox cmbNivel = null!;
        private string[] _todasLasLineas = Array.Empty<string>();

        public FrmLogsModal()
        {
            InitializeCustomComponents();
            CargarLogs();
        }

        private void InitializeCustomComponents()
        {
            Text = "Visor de Registros de Seguridad y Diagnóstico del Sistema (Logs)";
            ClientSize = new Size(950, 650);
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
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 168, 255),
                Location = new Point(20, 15),
                AutoSize = true
            };

            var pnlFiltros = new Panel
            {
                Location = new Point(20, 50),
                Size = new Size(910, 40),
                BackColor = Color.FromArgb(38, 38, 42)
            };

            var lblNivel = new Label
            {
                Text = "Nivel:",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(10, 10),
                AutoSize = true
            };
            pnlFiltros.Controls.Add(lblNivel);

            cmbNivel = new ComboBox
            {
                Location = new Point(55, 6),
                Size = new Size(130, 26),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.FromArgb(50, 50, 54),
                ForeColor = Color.White
            };
            cmbNivel.Items.AddRange(new object[] { "Todos", "ERROR", "WARN", "INFO" });
            cmbNivel.SelectedIndex = 0;
            cmbNivel.SelectedIndexChanged += (s, e) => AplicarFiltros();
            pnlFiltros.Controls.Add(cmbNivel);

            var lblBuscar = new Label
            {
                Text = "Buscar texto:",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(205, 10),
                AutoSize = true
            };
            pnlFiltros.Controls.Add(lblBuscar);

            txtFiltroTexto = new TextBox
            {
                Location = new Point(295, 7),
                Size = new Size(260, 25),
                BackColor = Color.FromArgb(50, 50, 54),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            txtFiltroTexto.TextChanged += (s, e) => AplicarFiltros();
            pnlFiltros.Controls.Add(txtFiltroTexto);

            var btnActualizar = new Button
            {
                Text = "🔄 Refrescar",
                Location = new Point(570, 5),
                Size = new Size(110, 30),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnActualizar.FlatAppearance.BorderSize = 0;
            btnActualizar.Click += (s, e) => CargarLogs();
            pnlFiltros.Controls.Add(btnActualizar);

            var btnAbrirNotepad = new Button
            {
                Text = "📂 Abrir Archivo",
                Location = new Point(690, 5),
                Size = new Size(120, 30),
                BackColor = Color.FromArgb(108, 92, 231),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnAbrirNotepad.FlatAppearance.BorderSize = 0;
            btnAbrirNotepad.Click += BtnAbrirNotepad_Click;
            pnlFiltros.Controls.Add(btnAbrirNotepad);

            txtLogs = new TextBox
            {
                Location = new Point(20, 100),
                Size = new Size(910, 455),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Both,
                WordWrap = false,
                BackColor = Color.FromArgb(18, 18, 18),
                ForeColor = Color.FromArgb(230, 230, 230),
                Font = new Font("Consolas", 9.5F)
            };

            // Botones inferiores con amplio espacio inferior y superior
            var btnCopiar = new Button
            {
                Text = "📋 Copiar Logs al Portapapeles",
                Location = new Point(20, 575),
                Size = new Size(240, 42),
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
                Location = new Point(275, 575),
                Size = new Size(210, 42),
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
                Location = new Point(780, 575),
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
            Controls.Add(txtLogs);
            Controls.Add(btnCopiar);
            Controls.Add(btnLimpiar);
            Controls.Add(btnCerrar);
        }

        private void CargarLogs()
        {
            string contenido = Logger.LeerLogs(1000);
            _todasLasLineas = contenido.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            AplicarFiltros();
        }

        private void AplicarFiltros()
        {
            if (_todasLasLineas == null || _todasLasLineas.Length == 0)
            {
                txtLogs.Text = "No hay registros disponibles.";
                return;
            }

            string nivel = cmbNivel.SelectedItem?.ToString() ?? "Todos";
            string busqueda = txtFiltroTexto.Text.Trim();

            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < _todasLasLineas.Length; i++)
            {
                string linea = _todasLasLineas[i];
                if (string.IsNullOrWhiteSpace(linea)) continue;

                if (nivel != "Todos" && !linea.Contains($"[{nivel}]", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                if (!string.IsNullOrEmpty(busqueda) && !linea.Contains(busqueda, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                sb.AppendLine(linea);
            }

            txtLogs.Text = sb.Length > 0 ? sb.ToString() : "No se encontraron líneas que coincidan con el filtro aplicado.";
            txtLogs.SelectionStart = txtLogs.Text.Length;
            txtLogs.ScrollToCaret();
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
