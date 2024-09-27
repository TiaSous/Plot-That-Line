using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphique.Classes
{
    internal class DataChess
    {
        private int id_player;
        private string name;
        private int elo;
        private int year;
        private int age;

        public string Name { get => name; set => name = value; }
        public int Elo { get => elo; set => elo = value; }
        public int Year { get => year; set => year = value; }
        public int Age { get => age; set => age = value; }
        public int Id_player { get => id_player; set => id_player = value; }
    }
}
