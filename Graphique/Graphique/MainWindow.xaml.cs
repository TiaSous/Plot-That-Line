using Microsoft.VisualBasic;
using ScottPlot;
using System.IO;
using System.Numerics;
using System.Reflection.Emit;
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
        
        List<DataChess> dataChess;                          // liste de données
        int minYear = 0;                                    // année minimum par défaut
        int maxYear = 9999;                                 // année maximum par défaut
        List<string> playerShow = new List<string>();       // les joueurs qui sont acctuellement affichées

        public MainWindow()
        {
            InitializeComponent();

            List<DataChess> field = ReadCSV("Chess.csv");
            dataChess = field;

            field.GroupBy(x => x.Name).ToList().ForEach(x =>
            {
                CheckBox checkBox = new CheckBox { Content = x.Key };
                checkBox.Checked += CheckBoxPlayer_Checked;
                checkBox.Unchecked += CheckBoxPlayer_Unchecked;
                ListBoxPlayer.Items.Add(checkBox);
                GrapheData.Plot.Axes.AutoScale();
            });

            GrapheData.Plot.Legend.Orientation = ScottPlot.Orientation.Horizontal;
            GrapheData.Plot.ShowLegend(Edge.Bottom);
            GrapheData.Refresh();

        }

        private List<DataChess> ReadCSV(string file)
        {
            List<string> csv = File.ReadAllLines(file).Skip(1).ToList();

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

        private void CheckBoxPlayer_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox name = sender as CheckBox;
            var(x, y) = GetDataForAPlayer(name.Content.ToString(), dataChess);
            GrapheData.Plot.Add.Scatter(x, y);
            GrapheData.Plot.Add.Scatter(x, y).LegendText = name.Content.ToString();
            GrapheData.Plot.Axes.AutoScale();
            GrapheData.Refresh();
            playerShow.Add(name.Content.ToString());
        }
        private void CheckBoxPlayer_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox name = sender as CheckBox;
            RemoveScatter(name.Content.ToString(), GrapheData);
            playerShow.Remove(name.Content.ToString());
        }

        private (List<double> dataX, List<double> dataY) GetDataForAPlayer(string name, List<DataChess> dataChesses)
        {
            var player = dataChesses.Where(x => x.Name == name).GroupBy(x => x.Name).ToList();

            List<double> dataX = new List<double>();
            List<double> dataY = new List<double>();

            for (int i = 0; i < player[0].Count(); i++)
            {
                if(player[0].ElementAt(i).Year >= minYear && player[0].ElementAt(i).Year <= maxYear)
                {
                    dataX.Add(player[0].ElementAt(i).Year);
                    dataY.Add(player[0].ElementAt(i).Elo);
                }
            }

            return (dataX, dataY);
        }

        private void RemoveScatter(string name, ScottPlot.WPF.WpfPlot data)
        {
            var plottables = data.Plot.GetPlottables().ToList();

            // aidé par chatgpt
            int index = plottables.FindIndex(x => x.LegendItems.FirstOrDefault().Label == name);

            data.Plot.Remove(plottables[index - 1]);
            data.Plot.Remove(plottables[index]);
            data.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            minYear = Convert.ToInt32(start_date.Text);
            maxYear = Convert.ToInt32(finish_date.Text);
            GrapheData.Plot.Clear();
            playerShow.ForEach(name => {
                var(x, y) = GetDataForAPlayer(name, dataChess);

                GrapheData.Plot.Add.Scatter(x, y);
                GrapheData.Plot.Add.Scatter(x, y).LegendText = name;
            });

            
            GrapheData.Plot.Axes.AutoScale();
            GrapheData.Refresh();
        }
    }
}