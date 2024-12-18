﻿using Graphique.Classes;
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
        DataPoints list = new DataPoints();

        int maxYear = 9999;
        int minYear = 0;


        public MainWindow(DataPoints file)
        {
            InitializeComponent();

            list = file;

            // crée les checkbox
            list.NameOfPlayer.ForEach(name =>
            {
                CheckBox checkBox = new CheckBox { Content = name, Tag = list.GetIndex(name) };
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
            Classes.Point dataXY = new Classes.Point();

            // récupère les données XY de la personnes
            dataXY = list.GetLineData(Convert.ToInt32(name.Tag), minYear, maxYear);

            // va ajouter le joueur sur le graphique
            GrapheData.Plot.Add.Scatter(dataXY.X, dataXY.Y);
            GrapheData.Plot.Add.Scatter(dataXY.X, dataXY.Y).LegendText = name.Content.ToString();
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
            list.RemovePlayer(Convert.ToInt32(name.Tag));

            // réaffiche les autres qui sont cochées
            list.ListIdPlayer.ForEach(idPlayer =>
            {
                Classes.Point dataXY = new Classes.Point();

                dataXY = list.GetLineData(idPlayer);

                // va ajouter le joueur sur le graphique
                GrapheData.Plot.Add.Scatter(dataXY.X, dataXY.Y);
                GrapheData.Plot.Add.Scatter(dataXY.X, dataXY.Y).LegendText = list.GetName(idPlayer);
            });
            GrapheData.Plot.Axes.AutoScale();
            GrapheData.Refresh();
        }

        // Lorsqu'il filtre par la date
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try   
            {

                minYear = Convert.ToInt32(start_date.Text);
                maxYear = Convert.ToInt32(finish_date.Text);

                if (minYear >= maxYear)
                {
                    MessageBox.Show("Min year greater than max year", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                GrapheData.Plot.Clear();

                list.ListIdPlayer.ForEach(idPlayer =>
                {
                    Classes.Point dataXY = new Classes.Point();

                    dataXY = list.GetLineData(idPlayer, minYear, maxYear);

                    // va ajouter le joueur sur le graphique
                    GrapheData.Plot.Add.Scatter(dataXY.X, dataXY.Y);
                    GrapheData.Plot.Add.Scatter(dataXY.X, dataXY.Y).LegendText = list.GetName(idPlayer);
                });

                GrapheData.Plot.Axes.AutoScale();
                GrapheData.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid input in box", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}