using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Graphique
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            List<DataChess> field = ReadCSV();
            field.GroupBy(x => x.Name).ToList().ForEach(x =>
            {
                double[] dataX = new double[x.Count()];
                double[] dataY = new double[x.Count()];

                for (int i = 0; i < x.Count(); i++)
                {
                    dataX[i] = x.ElementAt(i).Year;
                    dataY[i] = x.ElementAt(i).Elo;
                }
                GrapheData.Plot.Add.Scatter(dataX, dataY);
                GrapheData.Plot.Axes.AutoScale();
            });
            GrapheData.Refresh();   
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