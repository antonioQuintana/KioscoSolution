using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
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

        }

        private void UcEstadisticas_Load(object sender, EventArgs e)
        {
            InicializarGrafico();
        }

        private void InicializarGrafico()
        {
            // Grafico de Producto más vendido por categoría
            chart1 = new Chart
            {

                Dock = DockStyle.Fill,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White,
            };

            // definimos y le damos estilo al área
            ChartArea area1 = new ChartArea("AreaEstadisticas")
            {
                BackColor = Color.FromArgb(30, 30, 30),
                AxisX ={
                    LineColor = Color.White,
                    LabelStyle = { ForeColor = Color.White },
                    MajorGrid = { LineColor = Color.Gray }
                },
                AxisY ={
                    LineColor = Color.White,
                    LabelStyle = { ForeColor = Color.White },
                    MajorGrid = { LineColor = Color.Gray }
                }
            };

            chart1.ChartAreas.Add(area1);

            // le damos estilo a las barras
            Series serie1 = new Series("Productos Más Vendidos Por Categoria")
            {
                ChartType = SeriesChartType.Column, // barras verticales
                Color = Color.LightSkyBlue,
                IsValueShownAsLabel = true, // muestra el número sobre la barra
                LabelForeColor = Color.White,
                Font = new Font("Arial", 15, FontStyle.Bold),
            };

            List<ProductoMasVendido> listaTopVentas = ListarMasVendidos();
            foreach (var item in listaTopVentas)
            {
                serie1.Points.AddXY(item.NombreCategoria + " - " + item.NombreProducto, item.CantidadTotalVendida);
            }

            //agregamos la serie al chart y el chart al UserControl
            chart1.Series.Add(serie1);
            this.Controls.Add(chart1);

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            // Grafico ventas de la última semana por día
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
                AxisX ={
                    LineColor = Color.White,
                    LabelStyle = { ForeColor = Color.White },
                    MajorGrid = { LineColor = Color.Gray }
                },
                AxisY ={
                    LineColor = Color.White,
                    LabelStyle = { ForeColor = Color.White,  Format = "$#,##0"},
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

            List<DiaMasVendido> diasMasVentas = ListarVentasPorDias();
            foreach (var item in diasMasVentas)
            {
                serie2.Points.AddXY(item.NombreDia, item.TotalVendido);
            }

            // agregamos la serie al chart y el chart al UserControl
            chart2.Series.Add(serie2);
            this.Controls.Add(chart2);


            ////////////////////////////////////////////////////////////////////////////////////////////////////
            // Gafico de horario con más ventas
            chart3 = new Chart
            {

                Dock = DockStyle.Fill,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White,
            };

            // definimos y le damos estilo al área
            ChartArea area3 = new ChartArea("AreaEstadisticas")
            {
                BackColor = Color.FromArgb(30, 30, 30),
                AxisX ={
                    LineColor = Color.White,
                    LabelStyle = { ForeColor = Color.White },
                    MajorGrid = { LineColor = Color.Gray }
                },
                AxisY ={
                    LineColor = Color.White,
                    LabelStyle = { ForeColor = Color.White },
                    MajorGrid = { LineColor = Color.Gray }
                }
            };

            chart3.ChartAreas.Add(area3);

            // le damos estilo a las barras
            Series serie3 = new Series("Ventas por Intervalos de Horario")
            {
                ChartType = SeriesChartType.Column, // barras verticales
                Color = Color.DarkSeaGreen,
                IsValueShownAsLabel = true, // muestra el número sobre la barra
                LabelForeColor = Color.White,
                Font = new Font("Arial", 15, FontStyle.Bold),
            };

            List<HorarioMasVendido> listaHorariosMasVendidos = ListarVentasPorHora();
            foreach (var item in listaHorariosMasVendidos)
            {
                serie3.Points.AddXY(item.Hora, item.CantidadVentas);
            }

            //agregamos la serie al chart y el chart al UserControl
            chart3.Series.Add(serie3);
            this.Controls.Add(chart3);

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            // Gafico de ingreso por categoría
            chart4 = new Chart
            {

                Dock = DockStyle.Fill,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White,
            };

            // definimos y le damos estilo al área
            ChartArea area4 = new ChartArea("AreaEstadisticas")
            {
                BackColor = Color.FromArgb(30, 30, 30),
                AxisX ={
                    LineColor = Color.White,
                    LabelStyle = { ForeColor = Color.White },
                    MajorGrid = { LineColor = Color.Gray }
                },
                AxisY ={
                    LineColor = Color.White,
                    LabelStyle = { ForeColor = Color.White, Format = "$#,##0" },
                    MajorGrid = { LineColor = Color.Gray }
                }
            };

            chart4.ChartAreas.Add(area4);

            // le damos estilo a las barras
            Series serie4 = new Series("Ingresos por Categoria")
            {
                ChartType = SeriesChartType.Column, // barras verticales
                Color = Color.MediumPurple,
                IsValueShownAsLabel = true, // muestra el número sobre la barra
                LabelForeColor = Color.White,
                Font = new Font("Arial", 15, FontStyle.Bold),
            };

            List<CategoriaMasVendida> listaCategorias = ListarVentasPorCategoria();
            foreach (var item in listaCategorias)
            {
                serie4.Points.AddXY(item.Categoria, item.TotalFacturado);
            }

            //agregamos la serie al chart y el chart al UserControl
            chart4.Series.Add(serie4);
            this.Controls.Add(chart4);

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            // Gafico de método de pago con mayor ingreso
            chart5 = new Chart
            {

                Dock = DockStyle.Fill,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White,
            };

            // definimos y le damos estilo al área
            ChartArea area5 = new ChartArea("AreaEstadisticas")
            {
                BackColor = Color.FromArgb(30, 30, 30),
                AxisX ={
                    LineColor = Color.White,
                    LabelStyle = { ForeColor = Color.White },
                    MajorGrid = { LineColor = Color.Gray }
                },
                AxisY ={
                    LineColor = Color.White,
                    LabelStyle = { ForeColor = Color.White, Format = "$#,##0" },
                    MajorGrid = { LineColor = Color.Gray }
                }
            };

            chart5.ChartAreas.Add(area5);

            // le damos estilo a las barras
            Series serie5 = new Series("Método de Pago con Mayor Ingreso")
            {
                ChartType = SeriesChartType.Column, // barras verticales
                Color = Color.Plum,
                IsValueShownAsLabel = true, // muestra el número sobre la barra
                LabelForeColor = Color.White,
                Font = new Font("Arial", 15, FontStyle.Bold),
            };

            List<MetodoPagoMasUsado> listaMetodosDePago = listarMetodosMasUsados();
            foreach (var item in listaMetodosDePago)
            {
                serie5.Points.AddXY(item.Nombre, item.TotalFacturado);
            }

            //agregamos la serie al chart y el chart al UserControl
            chart5.Series.Add(serie5);
            this.Controls.Add(chart5);

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            // Gafico de método de pago más utilizado
            chart6 = new Chart
            {

                Dock = DockStyle.Fill,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White,
            };

            // definimos y le damos estilo al área
            ChartArea area6 = new ChartArea("AreaEstadisticas")
            {
                BackColor = Color.FromArgb(30, 30, 30),
                AxisX ={
                    LineColor = Color.White,
                    LabelStyle = { ForeColor = Color.White },
                    MajorGrid = { LineColor = Color.Gray }
                },
                AxisY ={
                    LineColor = Color.White,
                    LabelStyle = { ForeColor = Color.White },
                    MajorGrid = { LineColor = Color.Gray }
                }
            };

            chart6.ChartAreas.Add(area6);

            // le damos estilo a las barras
            Series serie6 = new Series("Método de Pago con Mayor Ingreso")
            {
                ChartType = SeriesChartType.Column, // barras verticales
                Color = Color.PaleVioletRed,
                IsValueShownAsLabel = true, // muestra el número sobre la barra
                LabelForeColor = Color.White,
                Font = new Font("Arial", 15, FontStyle.Bold),
            };

            foreach (var item in listaMetodosDePago)
            {
                serie6.Points.AddXY(item.Nombre, item.CantidadVentas);
            }

            //agregamos la serie al chart y el chart al UserControl
            chart6.Series.Add(serie6);
            this.Controls.Add(chart6);

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            // Organizamos los graficos en la tableLayoutPanel
            tableLayoutPanel1.Controls.Add(lblProdMasVendido, 0, 0);
            tableLayoutPanel1.Controls.Add(chart1, 0, 1); // agregamos los graficos y las labels a la tableLayout
            tableLayoutPanel1.Controls.Add(lblDiaMasVentas, 0, 2);
            tableLayoutPanel1.Controls.Add(chart2, 0, 3);
            tableLayoutPanel1.Controls.Add(label1, 0, 4);
            tableLayoutPanel1.Controls.Add(chart3, 0, 5);
            tableLayoutPanel1.Controls.Add(label2, 0, 6);
            tableLayoutPanel1.Controls.Add(chart4, 0, 7);
            tableLayoutPanel1.Controls.Add(label3, 0, 8);
            tableLayoutPanel1.Controls.Add(chart5, 0, 9);
            tableLayoutPanel1.Controls.Add(label4, 0, 10);
            tableLayoutPanel1.Controls.Add(chart6, 0, 11);
        }

        
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {}

        private void lblDiaMasVentas_Click(object sender, EventArgs e)
        {}
    }
}

