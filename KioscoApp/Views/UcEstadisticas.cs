using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using KioscoApp.Utils;
using static KioscoApp.Data.EstadisticasRepository;

namespace KioscoApp
{
    public partial class UcEstadisticas : UserControl
    {
        private Chart chart1;
        private Chart chart2;
        private Chart chart3;
        private Chart chart4;
        private Chart chart5;
        private Chart chart6;

        public UcEstadisticas()
        {
            InitializeComponent();
            EnlazarEventosBotones();
            ConfigurarAyuda();
        }

        private void ConfigurarAyuda()
        {
            AgregarAyudaFlujo(
                flowLayoutPanel1,
                "Gráfico 1: Productos Más Vendidos por Categoría",
                "Muestra el artículo líder en unidades vendidas dentro de cada categoría comercial.\n" +
                "Seleccione el rango de fechas 'Desde' y 'Hasta' y pulse 'Filtrar' para recalcular el ranking.");

            AgregarAyudaFlujo(
                flowLayoutPanel2,
                "Gráfico 2: Ingresos por Día de la Semana",
                "Compara el monto total facturado ($) según el día de la semana (Lunes a Domingo) en el período seleccionado, ideal para identificar los días de mayor movimiento comercial.");

            AgregarAyudaFlujo(
                flowLayoutPanel3,
                "Gráfico 3: Cantidad de Ventas por Intervalo de Horarios",
                "Analiza la cantidad de tickets emitidos en cada franja horaria del día para detectar horas pico y optimizar la asignación de turnos de cajeros.");

            AgregarAyudaFlujo(
                flowLayoutPanel4,
                "Gráfico 4: Ingresos por Categoría",
                "Muestra la facturación acumulada en pesos ($) aportada por cada familia o rubro de productos en el rango de fechas indicado.");

            AgregarAyudaFlujo(
                flowLayoutPanel5,
                "Gráfico 5: Ingresos por Método de Pago",
                "Compara el volumen monetario total ($) recaudado a través de cada medio de cobro (Efectivo, MercadoPago/QR, Tarjeta, Cuenta Corriente).");

            AgregarAyudaFlujo(
                flowLayoutPanel6,
                "Gráfico 6: Cantidad de Ventas por Método de Pago",
                "Muestra el número total de transacciones (cantidad de tickets) cobradas con cada medio de pago para conocer las preferencias de los clientes.");
        }

        private void AgregarAyudaFlujo(FlowLayoutPanel panel, string titulo, string descripcion)
        {
            if (panel == null) return;
            HelpTooltipHelper.AttachHelp(panel, titulo, descripcion);
        }

        private void EnlazarEventosBotones()
        {
            btnFiltrar1.Click += BtnFiltrar1_Click;
            btnFiltrar2.Click += BtnFiltrar2_Click;
            btnFiltrar3.Click += BtnFiltrar3_Click;
            btnFiltrar4.Click += BtnFiltrar4_Click;
            btnFiltrar5.Click += BtnFiltrar5_Click;
            btnFiltrar6.Click += BtnFiltrar6_Click;
        }

        private void UcEstadisticas_Load(object sender, EventArgs e)
        {
            ConfigurarFiltrosDefault();
            InicializarGrafico();
        }

        private void ConfigurarFiltrosDefault()
        {
            DateTime desdeDefault = DateTime.Today.AddDays(-30);
            DateTime hastaDefault = DateTime.Today;

            ConfigurarParFechas(dtpDesdeProd1, dtpHastaProd1, desdeDefault, hastaDefault);
            ConfigurarParFechas(dtpDesdeProd2, dtpHastaProd2, desdeDefault, hastaDefault);
            ConfigurarParFechas(dtpDesdeProd3, dtpHastaProd3, desdeDefault, hastaDefault);
            ConfigurarParFechas(dtpDesdeProd4, dtpHastaProd4, desdeDefault, hastaDefault);
            ConfigurarParFechas(dtpDesdeProd5, dtpHastaProd5, desdeDefault, hastaDefault);
            ConfigurarParFechas(dtpDesdeProd6, dtpHastaProd6, desdeDefault, hastaDefault);
        }

        private void ConfigurarParFechas(DateTimePicker dtpDesde, DateTimePicker dtpHasta, DateTime desde, DateTime hasta)
        {
            dtpDesde.MaxDate = DateTimePicker.MaximumDateTime;
            dtpHasta.MaxDate = DateTimePicker.MaximumDateTime;

            dtpDesde.Value = desde;
            dtpHasta.Value = hasta;

            dtpDesde.MaxDate = DateTime.Today;
            dtpHasta.MaxDate = DateTime.Today;
        }

        private bool ValidarFechas(DateTimePicker dtpDesde, DateTimePicker dtpHasta)
        {
            if (dtpDesde.Value.Date > dtpHasta.Value.Date)
            {
                MessageBox.Show(
                    "La fecha 'Desde' debe ser menor o igual a la fecha 'Hasta'.",
                    "Rango de fechas inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void InicializarGrafico()
        {
            // Grafico 1: Producto más vendido por categoría
            chart1 = new Chart
            {
                Dock = DockStyle.Fill,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White,
            };

            ChartArea area1 = new ChartArea("AreaEstadisticas")
            {
                BackColor = Color.FromArgb(30, 30, 30),
                AxisX = {
                    LineColor = Color.White,
                    LabelStyle = { ForeColor = Color.White },
                    MajorGrid = { LineColor = Color.Gray }
                },
                AxisY = {
                    LineColor = Color.White,
                    LabelStyle = { ForeColor = Color.White },
                    MajorGrid = { LineColor = Color.Gray }
                }
            };

            chart1.ChartAreas.Add(area1);

            Series serie1 = new Series("Productos Más Vendidos Por Categoria")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.LightSkyBlue,
                IsValueShownAsLabel = true,
                LabelForeColor = Color.White,
                Font = new Font("Arial", 15, FontStyle.Bold),
            };

            chart1.Series.Add(serie1);
            CargarDatosGrafico1();

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            // Grafico 2: Ventas por día
            chart2 = new Chart
            {
                Dock = DockStyle.Fill,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White,
            };

            ChartArea area2 = new ChartArea("AreaEstadisticas")
            {
                BackColor = Color.FromArgb(30, 30, 30),
                AxisX = {
                    LineColor = Color.White,
                    LabelStyle = { ForeColor = Color.White },
                    MajorGrid = { LineColor = Color.Gray }
                },
                AxisY = {
                    LineColor = Color.White,
                    LabelStyle = { ForeColor = Color.White, Format = "$#,##0" },
                    MajorGrid = { LineColor = Color.Gray }
                }
            };

            chart2.ChartAreas.Add(area2);

            Series serie2 = new Series("Productos Más Vendidos")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.Turquoise,
                IsValueShownAsLabel = true,
                LabelForeColor = Color.White,
                LabelFormat = "$#,##0",
                Font = new Font("Arial", 15, FontStyle.Bold),
            };

            chart2.Series.Add(serie2);
            CargarDatosGrafico2();

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            // Grafico 3: Horario con más ventas
            chart3 = new Chart
            {
                Dock = DockStyle.Fill,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White,
            };

            ChartArea area3 = new ChartArea("AreaEstadisticas")
            {
                BackColor = Color.FromArgb(30, 30, 30),
                AxisX = {
                    LineColor = Color.White,
                    LabelStyle = { ForeColor = Color.White },
                    MajorGrid = { LineColor = Color.Gray }
                },
                AxisY = {
                    LineColor = Color.White,
                    LabelStyle = { ForeColor = Color.White },
                    MajorGrid = { LineColor = Color.Gray }
                }
            };

            chart3.ChartAreas.Add(area3);

            Series serie3 = new Series("Ventas por Intervalos de Horario")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.DarkSeaGreen,
                IsValueShownAsLabel = true,
                LabelForeColor = Color.White,
                Font = new Font("Arial", 15, FontStyle.Bold),
            };

            chart3.Series.Add(serie3);
            CargarDatosGrafico3();

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            // Grafico 4: Ingreso por categoría
            chart4 = new Chart
            {
                Dock = DockStyle.Fill,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White,
            };

            ChartArea area4 = new ChartArea("AreaEstadisticas")
            {
                BackColor = Color.FromArgb(30, 30, 30),
                AxisX = {
                    LineColor = Color.White,
                    LabelStyle = { ForeColor = Color.White },
                    MajorGrid = { LineColor = Color.Gray }
                },
                AxisY = {
                    LineColor = Color.White,
                    LabelStyle = { ForeColor = Color.White, Format = "$#,##0" },
                    MajorGrid = { LineColor = Color.Gray }
                }
            };

            chart4.ChartAreas.Add(area4);

            Series serie4 = new Series("Ingresos por Categoria")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.MediumPurple,
                IsValueShownAsLabel = true,
                LabelForeColor = Color.White,
                Font = new Font("Arial", 15, FontStyle.Bold),
            };

            chart4.Series.Add(serie4);
            CargarDatosGrafico4();

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            // Grafico 5: Método de pago con mayor ingreso
            chart5 = new Chart
            {
                Dock = DockStyle.Fill,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White,
            };

            ChartArea area5 = new ChartArea("AreaEstadisticas")
            {
                BackColor = Color.FromArgb(30, 30, 30),
                AxisX = {
                    LineColor = Color.White,
                    LabelStyle = { ForeColor = Color.White },
                    MajorGrid = { LineColor = Color.Gray }
                },
                AxisY = {
                    LineColor = Color.White,
                    LabelStyle = { ForeColor = Color.White, Format = "$#,##0" },
                    MajorGrid = { LineColor = Color.Gray }
                }
            };

            chart5.ChartAreas.Add(area5);

            Series serie5 = new Series("Método de Pago con Mayor Ingreso")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.Plum,
                IsValueShownAsLabel = true,
                LabelForeColor = Color.White,
                Font = new Font("Arial", 15, FontStyle.Bold),
            };

            chart5.Series.Add(serie5);
            CargarDatosGrafico5();

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            // Grafico 6: Método de pago más utilizado
            chart6 = new Chart
            {
                Dock = DockStyle.Fill,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White,
            };

            ChartArea area6 = new ChartArea("AreaEstadisticas")
            {
                BackColor = Color.FromArgb(30, 30, 30),
                AxisX = {
                    LineColor = Color.White,
                    LabelStyle = { ForeColor = Color.White },
                    MajorGrid = { LineColor = Color.Gray }
                },
                AxisY = {
                    LineColor = Color.White,
                    LabelStyle = { ForeColor = Color.White },
                    MajorGrid = { LineColor = Color.Gray }
                }
            };

            chart6.ChartAreas.Add(area6);

            Series serie6 = new Series("Método de Pago con Mayor Ingreso")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.PaleVioletRed,
                IsValueShownAsLabel = true,
                LabelForeColor = Color.White,
                Font = new Font("Arial", 15, FontStyle.Bold),
            };

            chart6.Series.Add(serie6);
            CargarDatosGrafico6();

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            // Organizamos los graficos en la tableLayoutPanel
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 0, 0);
            tableLayoutPanel1.Controls.Add(chart1, 0, 1);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel2, 0, 2);
            tableLayoutPanel1.Controls.Add(chart2, 0, 3);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel3, 0, 4);
            tableLayoutPanel1.Controls.Add(chart3, 0, 5);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel4, 0, 6);
            tableLayoutPanel1.Controls.Add(chart4, 0, 7);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel5, 0, 8);
            tableLayoutPanel1.Controls.Add(chart5, 0, 9);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel6, 0, 10);
            tableLayoutPanel1.Controls.Add(chart6, 0, 11);
        }

        private void CargarDatosGrafico1()
        {
            if (!ValidarFechas(dtpDesdeProd1, dtpHastaProd1)) return;

            var serie = chart1.Series["Productos Más Vendidos Por Categoria"];
            serie.Points.Clear();

            List<ProductoMasVendido> listaTopVentas = ListarMasVendidos(dtpDesdeProd1.Value, dtpHastaProd1.Value);
            foreach (var item in listaTopVentas)
            {
                serie.Points.AddXY(item.NombreCategoria + " - " + item.NombreProducto, item.CantidadTotalVendida);
            }
        }

        private void CargarDatosGrafico2()
        {
            if (!ValidarFechas(dtpDesdeProd2, dtpHastaProd2)) return;

            var serie = chart2.Series["Productos Más Vendidos"];
            serie.Points.Clear();

            List<DiaMasVendido> diasMasVentas = ListarVentasPorDias(dtpDesdeProd2.Value, dtpHastaProd2.Value);
            foreach (var item in diasMasVentas)
            {
                serie.Points.AddXY(item.NombreDia, item.TotalVendido);
            }
        }

        private void CargarDatosGrafico3()
        {
            if (!ValidarFechas(dtpDesdeProd3, dtpHastaProd3)) return;

            var serie = chart3.Series["Ventas por Intervalos de Horario"];
            serie.Points.Clear();

            List<HorarioMasVendido> listaHorariosMasVendidos = ListarVentasPorHora(dtpDesdeProd3.Value, dtpHastaProd3.Value);
            foreach (var item in listaHorariosMasVendidos)
            {
                serie.Points.AddXY(item.Hora, item.CantidadVentas);
            }
        }

        private void CargarDatosGrafico4()
        {
            if (!ValidarFechas(dtpDesdeProd4, dtpHastaProd4)) return;

            var serie = chart4.Series["Ingresos por Categoria"];
            serie.Points.Clear();

            List<CategoriaMasVendida> listaCategorias = ListarVentasPorCategoria(dtpDesdeProd4.Value, dtpHastaProd4.Value);
            foreach (var item in listaCategorias)
            {
                serie.Points.AddXY(item.Categoria, item.TotalFacturado);
            }
        }

        private void CargarDatosGrafico5()
        {
            if (!ValidarFechas(dtpDesdeProd5, dtpHastaProd5)) return;

            var serie = chart5.Series["Método de Pago con Mayor Ingreso"];
            serie.Points.Clear();

            List<MetodoPagoMasUsado> listaMetodosDePago = listarMetodosMasUsados(dtpDesdeProd5.Value, dtpHastaProd5.Value);
            foreach (var item in listaMetodosDePago)
            {
                serie.Points.AddXY(item.Nombre, item.TotalFacturado);
            }
        }

        private void CargarDatosGrafico6()
        {
            if (!ValidarFechas(dtpDesdeProd6, dtpHastaProd6)) return;

            var serie = chart6.Series["Método de Pago con Mayor Ingreso"];
            serie.Points.Clear();

            List<MetodoPagoMasUsado> listaMetodosDePago = listarMetodosMasUsados(dtpDesdeProd6.Value, dtpHastaProd6.Value);
            foreach (var item in listaMetodosDePago)
            {
                serie.Points.AddXY(item.Nombre, item.CantidadVentas);
            }
        }

        private void BtnFiltrar1_Click(object sender, EventArgs e) => CargarDatosGrafico1();
        private void BtnFiltrar2_Click(object sender, EventArgs e) => CargarDatosGrafico2();
        private void BtnFiltrar3_Click(object sender, EventArgs e) => CargarDatosGrafico3();
        private void BtnFiltrar4_Click(object sender, EventArgs e) => CargarDatosGrafico4();
        private void BtnFiltrar5_Click(object sender, EventArgs e) => CargarDatosGrafico5();
        private void BtnFiltrar6_Click(object sender, EventArgs e) => CargarDatosGrafico6();

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        { }

        private void lblDiaMasVentas_Click(object sender, EventArgs e)
        { }

        private void label5_Click(object sender, EventArgs e)
        { }
    }
}
