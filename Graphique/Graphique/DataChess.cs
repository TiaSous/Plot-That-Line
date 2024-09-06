using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphique
{
    internal class DataChess
    {
        private int position;
        private string name;
        private int elo;
        private int year;
        private int age;

        public int Position { get => position; set => position = value; }
        public string Name { get => name; set => name = value; }
        public int Elo { get => elo; set => elo = value; }
        public int Year { get => year; set => year = value; }
        public int Age { get => age; set => age = value; }
    }
}
