using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphique.Classes
{
    // représente une ligne dans le csv
    internal class DataLine
    {
        private int id_player; // va être générer 
        private string name;
        private int elo;
        private int year;

        public string Name { get => name; set => name = value; }
        public int Elo { get => elo; set => elo = value; }
        public int Year { get => year; set => year = value; }
        public int Id_player { get => id_player; set => id_player = value; }
    }
}
