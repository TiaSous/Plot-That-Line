using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphique.Classes
{
    internal class Point
    {
        private List<double> _x = new List<double>();
        private List<double> _y = new List<double>();

        public List<double> X { get => _x; set => _x = value; }
        public List<double> Y { get => _y; set => _y = value; }
    }
}
