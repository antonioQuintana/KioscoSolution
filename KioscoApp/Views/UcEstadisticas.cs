using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace KioscoApp
{
    public partial class UcEstadisticas : UserControl
    {
        private Chart chartVentas;
        private Chart chartDias;
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
            // Grafico de Ventas

            chartVentas = new Chart
            {

                Dock = DockStyle.Fill,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White,
            };


            // definimos y le damos estilo al área
            ChartArea area = new ChartArea("AreaEstadisticas")
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

            chartVentas.ChartAreas.Add(area);

            // le damos estilo a las barras
            Series serie = new Series("Productos Más Vendidos")
            {
                ChartType = SeriesChartType.Column, // barras verticales
                Color = Color.LightCyan,
                IsValueShownAsLabel = true, // muestra el número sobre la barra
                LabelForeColor = Color.White,
                Font = new Font("Arial", 15, FontStyle.Bold),
            };

            // cargamos los datos
            serie.Points.AddXY("Coca Cola 2.25L", 120);
            serie.Points.AddXY("Galletitas Oreos", 85);
            serie.Points.AddXY("Cigarrillos Marlboro", 210);
            serie.Points.AddXY("Alfajor Jorjito", 160);

            // agregamos la serie al chart y el chart al UserControl
            chartVentas.Series.Add(serie);
            this.Controls.Add(chartVentas);

            // Grafico dia con más ventas
            chartDias = new Chart
            {
                Dock = DockStyle.Fill,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White,
            };

            ChartArea areaD = new ChartArea("AreaEstadisticas")
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

            chartDias.ChartAreas.Add(areaD);

            Series serieD = new Series("Productos Más Vendidos")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.Turquoise,
                IsValueShownAsLabel = true,
                LabelForeColor = Color.White,
                LabelFormat = "$#,##0",
                Font = new Font("Arial", 15, FontStyle.Bold),
            };

            // cargamos los datos
            serieD.Points.AddXY("Lunes", 240000);
            serieD.Points.AddXY("Martes", 260000);
            serieD.Points.AddXY("Miércoles", 290000);
            serieD.Points.AddXY("Jueves", 350000);
            serieD.Points.AddXY("Viernes", 580000);
            serieD.Points.AddXY("Sabado", 650000);
            serieD.Points.AddXY("Domingo", 420000);

            // agregamos la serie al chart y el chart al UserControl
            chartDias.Series.Add(serieD);
            this.Controls.Add(chartDias);

            tableLayoutPanel1.Controls.Add(lblProdMasVendido, 0, 0);
            tableLayoutPanel1.Controls.Add(chartVentas, 0, 1); // agregamos los graficos y las labels a la tableLayout
            tableLayoutPanel1.Controls.Add(lblDiaMasVentas, 0, 2);
            tableLayoutPanel1.Controls.Add(chartDias, 0, 3);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {}

    }
}

