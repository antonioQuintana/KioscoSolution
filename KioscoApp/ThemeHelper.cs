using System.Drawing;
using System.Windows.Forms;

namespace KioscoApp
{
    public static class ThemeHelper
    {
        public static Color BackgroundColor = Color.FromArgb(30, 30, 30);
        public static Color ForegroundColor = Color.White;
        public static Color TextBoxColor = Color.FromArgb(45, 45, 48);
        public static Color ButtonColor = Color.FromArgb(63, 63, 70);
        public static Color ButtonAccentColor = Color.SeaGreen;

        public static void ApplyTheme(Form form)
        {
            form.BackColor = BackgroundColor;
            form.ForeColor = ForegroundColor;
            ApplyThemeToControls(form.Controls);
        }
        
        public static void ApplyTheme(UserControl uc)
        {
            uc.BackColor = BackgroundColor;
            uc.ForeColor = ForegroundColor;
            ApplyThemeToControls(uc.Controls);
        }

        public static void ApplyThemeToControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is TextBox txt)
                {
                    txt.BackColor = TextBoxColor;
                    txt.ForeColor = ForegroundColor;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                }
                else if (control is ComboBox cmb)
                {
                    cmb.BackColor = TextBoxColor;
                    cmb.ForeColor = ForegroundColor;
                    cmb.FlatStyle = FlatStyle.Flat;
                }
                else if (control is Button btn)
                {
                    btn.BackColor = ButtonColor;
                    btn.ForeColor = ForegroundColor;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Cursor = Cursors.Hand;
                    
                    if (btn.Text.ToLower().Contains("guardar") || btn.Text.ToLower().Contains("cobrar") || btn.Text.ToLower().Contains("agregar"))
                    {
                        btn.BackColor = ButtonAccentColor;
                    }
                    else if (btn.Text.ToLower().Contains("eliminar"))
                    {
                        btn.BackColor = Color.IndianRed;
                    }
                }
                else if (control is DataGridView dgv)
                {
                    dgv.BackgroundColor = TextBoxColor;
                    dgv.ForeColor = Color.Black;
                    dgv.DefaultCellStyle.BackColor = Color.White;
                    dgv.DefaultCellStyle.ForeColor = Color.Black;
                    dgv.ColumnHeadersDefaultCellStyle.BackColor = ButtonColor;
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                    dgv.EnableHeadersVisualStyles = false;
                }
                else if (control is Label lbl)
                {
                    lbl.ForeColor = ForegroundColor;
                }

                if (control.HasChildren)
                {
                    ApplyThemeToControls(control.Controls);
                }
            }
        }
    }
}
