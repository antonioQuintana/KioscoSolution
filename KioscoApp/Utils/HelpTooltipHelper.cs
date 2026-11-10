using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace KioscoApp.Utils
{
    /// <summary>
    /// Provee íconos interactivos de ayuda (?) y carteles emergentes (ToolTips)
    /// para guiar al usuario en cada formulario, sección y funcionalidad del sistema.
    /// </summary>
    public static class HelpTooltipHelper
    {
        private static readonly Color BadgeNormalBack = Color.FromArgb(0, 122, 204);
        private static readonly Color BadgeHoverBack = Color.FromArgb(241, 196, 15);
        private static readonly Color BadgeNormalFore = Color.White;
        private static readonly Color BadgeHoverFore = Color.FromArgb(30, 30, 30);

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

        /// <summary>
        /// Crea el control visual con el signo de pregunta (?) circular interactivo.
        /// </summary>
        public static Label CreateHelpBadge(string titulo, string descripcion, int size = 20)
        {
            string formattedText = FormatTooltipText(descripcion, 68);
            var tip = CreateToolTip(titulo);

            var badge = new Label
            {
                Text = "?",
                Tag = "HelpBadge",
                AutoSize = false,
                Size = new Size(size, size),
                Font = new Font("Segoe UI", size >= 22 ? 10.5F : 9F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
                ForeColor = BadgeNormalFore,
                Cursor = Cursors.Help
            };

            bool isHovered = false;

            badge.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                var rect = new Rectangle(1, 1, badge.Width - 3, badge.Height - 3);
                using (var brush = new SolidBrush(isHovered ? BadgeHoverBack : BadgeNormalBack))
                {
                    e.Graphics.FillEllipse(brush, rect);
                }

                using (var pen = new Pen(isHovered ? Color.White : Color.FromArgb(120, 200, 255), 1.5f))
                {
                    e.Graphics.DrawEllipse(pen, rect);
                }

                TextRenderer.DrawText(
                    e.Graphics,
                    "?",
                    badge.Font,
                    new Rectangle(0, 0, badge.Width, badge.Height),
                    isHovered ? BadgeHoverFore : BadgeNormalFore,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
            };

            badge.MouseEnter += (s, e) =>
            {
                isHovered = true;
                badge.Invalidate();
            };

            badge.MouseLeave += (s, e) =>
            {
                isHovered = false;
                badge.Invalidate();
            };

            badge.Click += (s, e) =>
            {
                MessageBox.Show(
                    descripcion,
                    $"ℹ️ Ayuda: {titulo}",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            };

            tip.SetToolTip(badge, formattedText);
            return badge;
        }

        /// <summary>
        /// Asocia un cartel emergente a un control existente y coloca el signo de pregunta (?) a su derecha.
        /// Si shrinkControl es true, reduce ligeramente el ancho del control para que el signo (?) ocupe
        /// exactamente el mismo espacio original sin superponerse con controles vecinos.
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
            if (target == null || target.Parent == null)
                return CreateHelpBadge(titulo, descripcion, badgeSize);

            string formattedText = FormatTooltipText(descripcion, 68);
            var tip = CreateToolTip(titulo);
            tip.SetToolTip(target, formattedText);

            if (shrinkControl && target.Width > badgeSize + offsetX + 40)
            {
                target.Width -= (badgeSize + offsetX);
            }

            var badge = CreateHelpBadge(titulo, descripcion, badgeSize);
            int x = target.Right + offsetX;
            int y = target.Top + (target.Height - badgeSize) / 2 + offsetY;

            badge.Location = new Point(x, y);
            badge.Anchor = target.Anchor;

            target.Parent.Controls.Add(badge);
            badge.BringToFront();

            // Sincronizar visibilidad si el control cambia su estado Visible
            badge.Visible = target.Visible;
            target.VisibleChanged += (s, e) =>
            {
                badge.Visible = target.Visible;
            };

            return badge;
        }

        /// <summary>
        /// Coloca un ícono (?) en la esquina superior derecha de un GroupBox y asocia el cartel emergente.
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
            tip.SetToolTip(groupBox, formattedText);

            var badge = CreateHelpBadge(titulo, descripcion, 20);
            badge.Location = new Point(groupBox.Width - 20 - rightMargin, topOffset);
            badge.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            groupBox.Controls.Add(badge);
            badge.BringToFront();
            return badge;
        }

        /// <summary>
        /// Coloca un ícono (?) general de formulario en una posición específica del contenedor.
        /// </summary>
        public static Label AttachFormHelp(
            Control container,
            string tituloFormulario,
            string descripcionGeneral,
            Point location,
            AnchorStyles anchor = AnchorStyles.Top | AnchorStyles.Left)
        {
            var badge = CreateHelpBadge(tituloFormulario, descripcionGeneral, 24);
            badge.Location = location;
            badge.Anchor = anchor;
            container.Controls.Add(badge);
            badge.BringToFront();
            return badge;
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
