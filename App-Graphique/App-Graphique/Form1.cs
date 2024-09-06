using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            List<DataChess> field = ReadCSV();
            field.GroupBy(x => x.Name).ToList().ForEach(x =>
            {
                Graphique.Series.Add(x.Key);
                Graphique.Series[x.Key].ChartType = SeriesChartType.Line;
                foreach (var item in x)
                {
                    Graphique.Series[item.Name].Points.AddXY(item.Year, item.Elo);
                }
            });
        }

        private List<DataChess> ReadCSV()
        {
            List<string> csv = File.ReadAllLines("Chess.csv").Skip(1).ToList();

            List<DataChess> data_chess = new List<DataChess>();

            csv.ForEach(c =>
            {
                string[] values = c.Split(',');
                DataChess chess_player = new DataChess();
                chess_player.Position = Convert.ToInt32(values[0]);
                chess_player.Name = values[1];
                chess_player.Elo = Convert.ToInt32(values[2]);
                chess_player.Year = Convert.ToInt32(values[3]);
                chess_player.Age = Convert.ToInt32(values[4]);

                data_chess.Add(chess_player);
            });

            return data_chess;
        }
    }
}
