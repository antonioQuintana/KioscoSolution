using System;
using System.IO;
using System.Windows.Forms;
using KioscoApp.Business;
using KioscoApp.Views;

namespace KioscoApp
{
    public partial class UcConfiguracion : UserControl
    {
        private readonly BaseDatosAdminService _dbAdminService;

        public UcConfiguracion()
        {
            InitializeComponent();
            _dbAdminService = new BaseDatosAdminService();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                string defaultFolder = @"C:\KioscoBackups";
                if (!Directory.Exists(defaultFolder))
                {
                    try { Directory.CreateDirectory(defaultFolder); } catch { }
                }

                using var sfd = new SaveFileDialog
                {
                    Title = "Guardar Copia de Seguridad de la Base de Datos",
                    Filter = "Archivo de Copia de Seguridad SQL Server (*.bak)|*.bak",
                    FileName = $"KioscoDB_Backup_{DateTime.Now:yyyyMMdd_HHmmss}.bak",
                    InitialDirectory = Directory.Exists(defaultFolder) ? defaultFolder : Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    var res = _dbAdminService.CrearBackup(sfd.FileName);

                    Cursor.Current = Cursors.Default;

                    if (res.Exitoso)
                    {
                        MessageBox.Show(
                            $"¡Copia de seguridad generada con éxito!\n\n" +
                            $"• Archivo: {Path.GetFileName(res.RutaArchivo)}\n" +
                            $"• Ubicación: {res.RutaArchivo}\n" +
                            $"• Tamaño: {res.TamanoFormateado}\n" +
                            $"• Duración: {res.Duracion.TotalSeconds:N1} seg.\n" +
                            $"• Integridad: Verificada correctamente en SQL Server.",
                            "Copia de Seguridad Exitosa",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(
                            $"No se pudo completar la copia de seguridad:\n\n{res.Mensaje}",
                            "Error de Copia de Seguridad",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show($"Error al iniciar la copia de seguridad: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                string defaultFolder = @"C:\KioscoBackups";

                using var ofd = new OpenFileDialog
                {
                    Title = "Seleccionar Copia de Seguridad para Restaurar",
                    Filter = "Archivo de Copia de Seguridad SQL Server (*.bak)|*.bak",
                    InitialDirectory = Directory.Exists(defaultFolder) ? defaultFolder : Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                };

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    var confirm = MessageBox.Show(
                        "¡ADVERTENCIA DE SEGURIDAD CRÍTICA!\n\n" +
                        $"Está a punto de restaurar la base de datos desde el archivo:\n[{Path.GetFileName(ofd.FileName)}]\n\n" +
                        "Esta acción SOBRESCRIBIRÁ POR COMPLETO todas las tablas y transacciones actuales de 'KioscoDB'.\n\n" +
                        "¿Está absolutamente seguro de que desea continuar con la restauración?",
                        "Confirmar Restauración de Base de Datos",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button2);

                    if (confirm == DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        try
                        {
                            _dbAdminService.RestaurarBackup(ofd.FileName);

                            Cursor.Current = Cursors.Default;

                            MessageBox.Show(
                                $"¡Base de datos restaurada correctamente desde:\n[{ofd.FileName}]!\n\n" +
                                "Todas las tablas, ventas y usuarios se han sincronizado con la copia de respaldo.\n" +
                                "Se sugiere reiniciar la aplicación para actualizar la memoria de los módulos.",
                                "Restauración Finalizada",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show(
                                $"Error crítico durante la restauración de la base de datos:\n\n{ex.Message}\n\n" +
                                "Asegúrese de que ningún otro proceso mantenga la base de datos bloqueada.",
                                "Error de Restauración",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show($"Error al seleccionar archivo de restauración: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                var confirm = MessageBox.Show(
                    "Mantenimiento y Compactación de Base de Datos:\n\n" +
                    "Esta tarea realizará las siguientes acciones en el motor SQL Server:\n" +
                    "• Reorganización y reconstrucción de índices (sp_updatestats).\n" +
                    "• Liberación de espacio en disco no asignado (DBCC SHRINKDATABASE).\n" +
                    "• Optimización de tiempos de respuesta del catálogo.\n\n" +
                    "¿Desea iniciar la optimización ahora?",
                    "Mantenimiento del Sistema",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    string resultado = _dbAdminService.OptimizarBaseDatos();

                    Cursor.Current = Cursors.Default;

                    MessageBox.Show(resultado, "Mantenimiento Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show($"Error durante el mantenimiento: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDatosEmpresa_Click(object sender, EventArgs e)
        {
            using var frm = new FrmDatosEmpresaModal();
            frm.ShowDialog();
        }

        private void btnImpresora_Click(object sender, EventArgs e)
        {
            using var frm = new FrmConfigurarImpresoraModal();
            frm.ShowDialog();
        }

        private void btnLicencia_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "=========================================\n" +
                "       KIOSCO EXPRESS POS SYSTEM         \n" +
                "=========================================\n\n" +
                "• Versión del Sistema: 2.1.0 (Build 2026.11)\n" +
                "• Plataforma: .NET 8.0 Windows Desktop (x64)\n" +
                "• Motor de Datos: Microsoft SQL Server 2022 Express\n" +
                "• Tipo de Licencia: Licencia Comercial de Uso Continuo\n" +
                "• Estado: ACTIVA Y REGISTRADA\n" +
                "• Perfil de Puesto: Administrador de Sistemas (IT/SysAdmin)\n\n" +
                "© 2026 KioscoApp Team. Todos los derechos reservados.",
                "Licencia y Acerca de KioscoApp",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void btnTrazabilidad_Click(object sender, EventArgs e)
        {
            using var frm = new FrmTrazabilidadModal();
            frm.ShowDialog();
        }

        private void btnLogs_Click(object sender, EventArgs e)
        {
            using var frm = new FrmLogsModal();
            frm.ShowDialog();
        }

        private void MostrarProximamente(object sender, EventArgs e)
        {
            Button? btn = sender as Button;
            string modulo = btn != null ? btn.Text : "Esta función";
            MessageBox.Show($"Módulo en desarrollo:\n\n[{modulo}]\n\nEstará disponible próximamente.", "Función No Disponible", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
