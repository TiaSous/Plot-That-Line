using Graphique.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphique.Intrerfaces
{
    internal interface IDataPoints
    {
        void Init(string source);
        void AddPlayer(int idPlayer);
        void RemovePlayer(int idPlayer);
        Point GetLineData(int idPlayer, int minYear, int maxYear);
        int GetIndex(string player);
        public string GetName(int playerId);
    }
}
