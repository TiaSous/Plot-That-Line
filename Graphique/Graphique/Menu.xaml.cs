using Graphique.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Logique d'interaction pour Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        string name;
        DataPoints dataPoints = null;

        public Menu()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(dataPoints == null)
            {
                MessageBox.Show("No csv import", "Invalid CSV", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MainWindow graph = new MainWindow(dataPoints);
                graph.Show();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Fais par copilot
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".csv";
            dlg.Filter = "CSV files (*.csv)|*.csv";

            dlg.ShowDialog();
            name = dlg.FileName;

            // va lire le fichier 
            // TODO : régler problème si ";" quelque part dans les données
            try
            {
                DataPoints lineTest = new DataPoints();
                lineTest.Init(name);
                dataPoints = lineTest;
            }
            catch
            {
                MessageBox.Show("Incorrect format for csv read the readme", "Invalid CSV", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
