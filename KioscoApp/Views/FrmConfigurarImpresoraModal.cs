using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using KioscoApp.Business;
using KioscoApp.Models;
using KioscoApp.Utils;

namespace KioscoApp.Views
{
    public class FrmConfigurarImpresoraModal : Form
    {
        private readonly ConfiguracionService _configService;
        private ConfiguracionEmpresa _config;

        private ComboBox cmbImpresoras = null!;
        private RadioButton rb80mm = null!;
        private RadioButton rb58mm = null!;
        private CheckBox chkAutoImprimir = null!;
        private CheckBox chkCortarPapel = null!;
        private Button btnTestPrint = null!;
        private Button btnGuardar = null!;
        private Button btnCancelar = null!;

        public FrmConfigurarImpresoraModal()
        {
            _configService = new ConfiguracionService();
            _config = _configService.Obtener();

            InitializeCustomComponents();
            CargarImpresoras();
        }

        private void InitializeCustomComponents()
        {
            Text = "Configuración de Impresora Térmica de Tickets";
            Size = new Size(580, 480);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.FromArgb(30, 30, 30);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 9.5F);

            var lblTitulo = new Label
            {
                Text = "🖨️ Configuración de Impresora de Comprobantes",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 168, 255),
                Location = new Point(20, 15),
                AutoSize = true
            };

            var gbDispositivo = new GroupBox
            {
                Text = "Dispositivo de Impresión",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 55),
                Size = new Size(520, 110)
            };

            var lblImpresora = new Label
            {
                Text = "Seleccione la impresora térmica instalada:",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(200, 200, 200),
                Location = new Point(15, 28),
                AutoSize = true
            };
            gbDispositivo.Controls.Add(lblImpresora);

            cmbImpresoras = new ComboBox
            {
                Location = new Point(15, 52),
                Size = new Size(485, 28),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.FromArgb(45, 45, 48),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F)
            };
            gbDispositivo.Controls.Add(cmbImpresoras);

            var gbOpciones = new GroupBox
            {
                Text = "Formato y Comportamiento",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 180),
                Size = new Size(520, 175)
            };

            var lblAncho = new Label
            {
                Text = "Ancho del papel térmico:",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(220, 220, 220),
                Location = new Point(15, 30),
                AutoSize = true
            };
            gbOpciones.Controls.Add(lblAncho);

            rb80mm = new RadioButton
            {
                Text = "80 mm (Estándar Punto de Venta / 40-48 columnas)",
                Location = new Point(25, 55),
                Size = new Size(420, 24),
                ForeColor = Color.White,
                Checked = true
            };
            gbOpciones.Controls.Add(rb80mm);

            rb58mm = new RadioButton
            {
                Text = "58 mm (Mini térmica / portátil / 32 columnas)",
                Location = new Point(25, 82),
                Size = new Size(420, 24),
                ForeColor = Color.White
            };
            gbOpciones.Controls.Add(rb58mm);

            chkAutoImprimir = new CheckBox
            {
                Text = "Imprimir ticket automáticamente tras confirmar cada venta [F5]",
                Location = new Point(15, 115),
                Size = new Size(480, 24),
                ForeColor = Color.FromArgb(46, 204, 113),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold)
            };
            gbOpciones.Controls.Add(chkAutoImprimir);

            chkCortarPapel = new CheckBox
            {
                Text = "Enviar comando de corte de papel al finalizar el ticket",
                Location = new Point(15, 142),
                Size = new Size(480, 24),
                ForeColor = Color.White
            };
            gbOpciones.Controls.Add(chkCortarPapel);

            btnTestPrint = new Button
            {
                Text = "🖨️ Imprimir Ticket de Prueba",
                Location = new Point(20, 375),
                Size = new Size(220, 42),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnTestPrint.FlatAppearance.BorderSize = 0;
            btnTestPrint.Click += BtnTestPrint_Click;

            btnGuardar = new Button
            {
                Text = "Guardar Configuración",
                Location = new Point(255, 375),
                Size = new Size(160, 42),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.Click += BtnGuardar_Click;

            btnCancelar = new Button
            {
                Text = "Cancelar",
                Location = new Point(425, 375),
                Size = new Size(115, 42),
                BackColor = Color.FromArgb(127, 140, 141),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.Click += (s, e) => Close();

            Controls.Add(lblTitulo);
            Controls.Add(gbDispositivo);
            Controls.Add(gbOpciones);
            Controls.Add(btnTestPrint);
            Controls.Add(btnGuardar);
            Controls.Add(btnCancelar);

            HelpTooltipHelper.AttachHelp(lblTitulo, "Configuración de Impresora Térmica",
                "Seleccione la impresora de tickets instalada en Windows y defina el ancho del rollo de papel y el comportamiento de impresión.");
            HelpTooltipHelper.AttachGroupBoxHelp(gbDispositivo, "Selección de Dispositivo",
                "Elija de la lista desplegable la impresora térmica conectada por USB o Red que utilizará el Punto de Venta.");
            HelpTooltipHelper.AttachGroupBoxHelp(gbOpciones, "Formato y Automatización",
                "Seleccione el ancho del papel (80mm estándar o 58mm compacto), active la impresión automática al cobrar con [F5] y el corte automático de papel.");
            HelpTooltipHelper.AttachHelp(btnTestPrint, "Ticket de Prueba",
                "Envía una página de prueba a la impresora seleccionada para verificar que esté encendida, con papel y correctamente conectada.", shrinkControl: true);
            HelpTooltipHelper.AttachHelp(btnGuardar, "Guardar Impresora",
                "Guarda las preferencias de impresión para todas las ventas del sistema.", shrinkControl: true);
        }

        private void CargarImpresoras()
        {
            cmbImpresoras.Items.Clear();
            string predeterminadaOS = string.Empty;

            try
            {
                var doc = new PrintDocument();
                predeterminadaOS = doc.PrinterSettings.PrinterName;

                foreach (string printer in PrinterSettings.InstalledPrinters)
                {
                    cmbImpresoras.Items.Add(printer);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Advertencia al consultar impresoras del sistema: {ex.Message}", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (cmbImpresoras.Items.Count == 0)
            {
                cmbImpresoras.Items.Add("Impresora por Defecto de Windows");
            }

            // Seleccionar guardada o predeterminada
            if (!string.IsNullOrEmpty(_config.ImpresoraPredeterminada) && cmbImpresoras.Items.Contains(_config.ImpresoraPredeterminada))
            {
                cmbImpresoras.SelectedItem = _config.ImpresoraPredeterminada;
            }
            else if (!string.IsNullOrEmpty(predeterminadaOS) && cmbImpresoras.Items.Contains(predeterminadaOS))
            {
                cmbImpresoras.SelectedItem = predeterminadaOS;
            }
            else
            {
                cmbImpresoras.SelectedIndex = 0;
            }

            if (_config.AnchoPapelMm == 58)
                rb58mm.Checked = true;
            else
                rb80mm.Checked = true;

            chkAutoImprimir.Checked = _config.ImpresionAutomatica;
            chkCortarPapel.Checked = _config.CortarPapel;
        }

        private void BtnTestPrint_Click(object sender, EventArgs e)
        {
            string impresora = cmbImpresoras.SelectedItem?.ToString() ?? string.Empty;
            int ancho = rb58mm.Checked ? 58 : 80;

            try
            {
                var pd = new PrintDocument();
                pd.PrinterSettings.PrinterName = impresora;

                pd.PrintPage += (s, ev) =>
                {
                    var fontTitulo = new Font("Courier New", 10, FontStyle.Bold);
                    var fontCuerpo = new Font("Courier New", 8, FontStyle.Regular);
                    var brush = Brushes.Black;
                    float y = 10;
                    float lineHeight = fontCuerpo.GetHeight(ev.Graphics!);

                    ev.Graphics!.DrawString("================================", fontTitulo, brush, 5, y);
                    y += lineHeight;
                    ev.Graphics.DrawString("       TEST DE IMPRESIÓN        ", fontTitulo, brush, 5, y);
                    y += lineHeight;
                    ev.Graphics.DrawString($" Negocio: {_config.NombreFantasia} ", fontCuerpo, brush, 5, y);
                    y += lineHeight;
                    ev.Graphics.DrawString($" Fecha:   {DateTime.Now:dd/MM/yyyy HH:mm} ", fontCuerpo, brush, 5, y);
                    y += lineHeight;
                    ev.Graphics.DrawString($" Ancho:   {ancho} mm              ", fontCuerpo, brush, 5, y);
                    y += lineHeight;
                    ev.Graphics.DrawString(" Estado:  CONEXIÓN CORRECTA OK  ", fontTitulo, brush, 5, y);
                    y += lineHeight;
                    ev.Graphics.DrawString("================================", fontTitulo, brush, 5, y);
                };

                pd.Print();
                MessageBox.Show($"Se ha enviado la página de prueba a la impresora:\n[{impresora}]\n\nVerifique la salida física de papel en el equipo.", "Prueba de Impresión Enviada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo enviar la impresión a '{impresora}':\n\n{ex.Message}\n\nVerifique que el dispositivo esté encendido y conectado por USB / Red.", "Error de Impresión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string impresora = cmbImpresoras.SelectedItem?.ToString() ?? string.Empty;
                int ancho = rb58mm.Checked ? 58 : 80;
                bool autoPrint = chkAutoImprimir.Checked;
                bool cortar = chkCortarPapel.Checked;

                _configService.GuardarImpresora(impresora, ancho, autoPrint, cortar);

                MessageBox.Show("Configuración de impresora guardada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar configuración: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
