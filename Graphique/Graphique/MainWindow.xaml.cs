using Graphique.Classes;
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
using System.Xml.Linq;

namespace Graphique
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ListData list = new ListData();

        int maxYear = 0;
        int minYear = 0;


        public MainWindow()
        {
            InitializeComponent();

            list.Init("Chess.csv");

            list.NameOfPlayer.ForEach(x =>
            {
                CheckBox checkBox = new CheckBox { Content = x };
                checkBox.Checked += CheckBoxPlayer_Checked;
                checkBox.Unchecked += CheckBoxPlayer_Unchecked;
                ListBoxPlayer.Items.Add(checkBox);
                GrapheData.Plot.Axes.AutoScale();
            });

            GrapheData.Plot.Legend.Orientation = ScottPlot.Orientation.Horizontal;
            GrapheData.Plot.ShowLegend(Edge.Bottom);
            GrapheData.Refresh();

        }


        // Lorsque l'utilisateur coche un joueur
        private void CheckBoxPlayer_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox name = sender as CheckBox;
            LineData dataxy = new LineData();

            dataxy = list.GetLineData(list.GetIndex(name.Content.ToString()));

            // va ajouter le joueur sur le graphique
            GrapheData.Plot.Add.Scatter(dataxy.X, dataxy.Y);
            GrapheData.Plot.Add.Scatter(dataxy.X, dataxy.Y).LegendText = name.Content.ToString();
            GrapheData.Plot.Axes.AutoScale();
            GrapheData.Refresh();

            // ajoute le joueur dans la liste des joueurs afficher
            list.AddPlayer(list.GetIndex(name.Content.ToString()));
        }

        // Lorsque l'utilisateur décoche un joueur
        private void CheckBoxPlayer_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox name = sender as CheckBox;
            GrapheData.Plot.Clear();
            list.RemovePlayer(list.GetIndex(name.Content.ToString()));
            list.ListIdPlayer.ForEach(x =>
            {
                LineData dataxy = new LineData();

                dataxy = list.GetLineData(x);

                // va ajouter le joueur sur le graphique
                GrapheData.Plot.Add.Scatter(dataxy.X, dataxy.Y);
                GrapheData.Plot.Add.Scatter(dataxy.X, dataxy.Y).LegendText = name.Content.ToString();
            });
            GrapheData.Plot.Axes.AutoScale();
            GrapheData.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            minYear = Convert.ToInt32(start_date.Text);
            maxYear = Convert.ToInt32(finish_date.Text);
            GrapheData.Plot.Clear();

            list.ListIdPlayer.ForEach(x =>
            {
                LineData dataxy = new LineData();

                dataxy = list.GetLineData(x, minYear, maxYear);

                // va ajouter le joueur sur le graphique
                GrapheData.Plot.Add.Scatter(dataxy.X, dataxy.Y);
                GrapheData.Plot.Add.Scatter(dataxy.X, dataxy.Y).LegendText = list.GetName(x);
            });

            GrapheData.Plot.Axes.AutoScale();
            GrapheData.Refresh();
        }
    }
}