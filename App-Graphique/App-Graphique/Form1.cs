using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace App_Graphique
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Graphique.Series.Add("test");
            Graphique.Series["test"].ChartType = SeriesChartType.Line;
            Graphique.Series["test"].Points.AddXY(2000, 1500);
            Graphique.Series["test"].Points.AddXY(2001, 1450);
            Graphique.Series["Magnus Carlsen"].Points.AddXY(2000, 2500);
            Graphique.Series["Magnus Carlsen"].Points.AddXY(2001, 2600);
            Graphique.Series["Magnus Carlsen"].Points.AddXY(2002, 2650);
        }
    }
}
