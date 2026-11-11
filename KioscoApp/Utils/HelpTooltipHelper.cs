using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace KioscoApp.Utils
{
    /// <summary>
    /// Provee carteles emergentes (ToolTips) al pasar el ratón sobre etiquetas (Labels),
    /// botones y controles para guiar al usuario en cada formulario y sección del sistema.
    /// </summary>
    public static class HelpTooltipHelper
    {
        private sealed class TooltipEntry
        {
            public required ToolTip Tip { get; init; }
            public required bool IsSpecific { get; init; }
        }

        private static readonly ConditionalWeakTable<Control, TooltipEntry> AssignedToolTips = new();

        /// <summary>
        /// Crea una instancia de ToolTip configurada para respuesta rápida y lectura cómoda.
        /// </summary>
        public static ToolTip CreateToolTip(string titulo = "Guía de Uso - KioscoApp")
        {
            return new ToolTip
            {
                ToolTipTitle = $"ℹ️ {titulo}",
                ToolTipIcon = ToolTipIcon.Info,
                InitialDelay = 120,
                ReshowDelay = 80,
                AutoPopDelay = 30000,
                ShowAlways = true,
                IsBalloon = false
            };
        }

        private static void SetControlToolTip(Control ctrl, ToolTip tip, string formattedText, bool isSpecific, bool overwriteSpecific = true)
        {
            if (ctrl == null) return;

            if (AssignedToolTips.TryGetValue(ctrl, out var existing))
            {
                // Un tooltip general de contenedor nunca pisa uno específico de control/etiqueta
                if (existing.IsSpecific && (!isSpecific || !overwriteSpecific))
                    return;

                existing.Tip.SetToolTip(ctrl, null);
                AssignedToolTips.Remove(ctrl);
            }

            tip.SetToolTip(ctrl, formattedText);
            AssignedToolTips.Add(ctrl, new TooltipEntry { Tip = tip, IsSpecific = isSpecific });
        }

        /// <summary>
        /// Mantiene compatibilidad de firma sin mostrar el ícono (?) circular.
        /// </summary>
        public static Label CreateHelpBadge(string titulo, string descripcion, int size = 20)
        {
            return new Label
            {
                Visible = false,
                Size = Size.Empty,
                Tag = "HelpBadge"
            };
        }

        /// <summary>
        /// Asocia un cartel emergente al pasar el ratón sobre el control indicado (botón, etiqueta o campo)
        /// y sobre las etiquetas (Labels) descriptivas asociadas, sin mostrar el botón (?) ni achicar el control.
        /// </summary>
        public static Label AttachHelp(
            Control target,
            string titulo,
            string descripcion,
            bool shrinkControl = false,
            int offsetX = 4,
            int offsetY = 0,
            int badgeSize = 20)
        {
            if (target == null)
                return CreateHelpBadge(titulo, descripcion, badgeSize);

            string formattedText = FormatTooltipText(descripcion, 68);
            var tip = CreateToolTip(titulo);

            SetControlToolTip(target, tip, formattedText, isSpecific: true, overwriteSpecific: true);

            // Si el target es un contenedor (ej. FlowLayoutPanel o Panel), asociar a sus Labels y Buttons hijos
            foreach (Control child in target.Controls)
            {
                if (child is Label or Button)
                {
                    SetControlToolTip(child, tip, formattedText, isSpecific: true, overwriteSpecific: false);
                }
            }

            // Asociar automáticamente a etiquetas (Labels) o controles compañeros en el mismo contenedor
            if (target.Parent != null)
            {
                foreach (Control sibling in target.Parent.Controls)
                {
                    if (sibling == target) continue;

                    if (target is not Label && target is not Button && sibling is Label lbl)
                    {
                        // Etiqueta ubicada inmediatamente arriba o a la izquierda del campo
                        bool isAbove = lbl.Bottom <= target.Top + 8 && lbl.Top >= target.Top - 36 && Math.Abs(lbl.Left - target.Left) <= 45;
                        bool isLeft = Math.Abs(lbl.Top - target.Top) <= 14 && lbl.Right <= target.Left + 20 && target.Left - lbl.Left <= 220;
                        if (isAbove || isLeft)
                        {
                            SetControlToolTip(lbl, tip, formattedText, isSpecific: true, overwriteSpecific: false);
                        }
                    }
                    else if (target is Label targetLbl && sibling is Label otherLbl)
                    {
                        // Etiquetas en la misma fila horizontal (ej. formularios de 2 columnas como Nombre y Apellido)
                        if (Math.Abs(otherLbl.Top - targetLbl.Top) <= 8)
                        {
                            SetControlToolTip(otherLbl, tip, formattedText, isSpecific: true, overwriteSpecific: false);
                        }
                    }
                }
            }

            return CreateHelpBadge(titulo, descripcion, badgeSize);
        }

        /// <summary>
        /// Asocia el cartel emergente al GroupBox y a sus etiquetas (Labels) internas sin agregar el ícono (?).
        /// </summary>
        public static Label AttachGroupBoxHelp(
            GroupBox groupBox,
            string titulo,
            string descripcion,
            int rightMargin = 8,
            int topOffset = 14)
        {
            if (groupBox == null)
                return CreateHelpBadge(titulo, descripcion, 20);

            string formattedText = FormatTooltipText(descripcion, 68);
            var tip = CreateToolTip(titulo);

            SetControlToolTip(groupBox, tip, formattedText, isSpecific: false);

            foreach (Control child in groupBox.Controls)
            {
                if (child is Label)
                {
                    SetControlToolTip(child, tip, formattedText, isSpecific: false);
                }
            }

            return CreateHelpBadge(titulo, descripcion, 20);
        }

        /// <summary>
        /// Asocia el cartel emergente general al contenedor y a sus etiquetas (Labels) de encabezado/totales sin mostrar el ícono (?).
        /// </summary>
        public static Label AttachFormHelp(
            Control container,
            string tituloFormulario,
            string descripcionGeneral,
            Point location,
            AnchorStyles anchor = AnchorStyles.Top | AnchorStyles.Left)
        {
            if (container == null)
                return CreateHelpBadge(tituloFormulario, descripcionGeneral, 24);

            string formattedText = FormatTooltipText(descripcionGeneral, 68);
            var tip = CreateToolTip(tituloFormulario);

            SetControlToolTip(container, tip, formattedText, isSpecific: false);

            foreach (Control child in container.Controls)
            {
                if (child is Label)
                {
                    SetControlToolTip(child, tip, formattedText, isSpecific: false);
                }
                else if (child is Panel pnl)
                {
                    foreach (Control pnlChild in pnl.Controls)
                    {
                        if (pnlChild is Label)
                        {
                            SetControlToolTip(pnlChild, tip, formattedText, isSpecific: false);
                        }
                    }
                }
            }

            return CreateHelpBadge(tituloFormulario, descripcionGeneral, 24);
        }

        /// <summary>
        /// Envuelve líneas largas para que el cartel emergente sea fácil y cómodo de leer.
        /// </summary>
        private static string FormatTooltipText(string text, int maxLineLength)
        {
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;

            var paragraphs = text.Replace("\r\n", "\n").Split('\n');
            var result = new System.Text.StringBuilder();

            for (int p = 0; p < paragraphs.Length; p++)
            {
                string paragraph = paragraphs[p].Trim();
                if (string.IsNullOrEmpty(paragraph))
                {
                    result.AppendLine();
                    continue;
                }

                var words = paragraph.Split(' ');
                var currentLine = new System.Text.StringBuilder();

                foreach (var word in words)
                {
                    if (currentLine.Length > 0 && currentLine.Length + 1 + word.Length > maxLineLength)
                    {
                        result.AppendLine(currentLine.ToString());
                        currentLine.Clear();
                    }
                    if (currentLine.Length > 0) currentLine.Append(' ');
                    currentLine.Append(word);
                }

                if (currentLine.Length > 0)
                {
                    if (p < paragraphs.Length - 1)
                        result.AppendLine(currentLine.ToString());
                    else
                        result.Append(currentLine.ToString());
                }
            }

            return result.ToString();
        }
    }
}
