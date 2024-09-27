﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphique.Intrerfaces;

namespace Graphique.Classes
{
    internal class ListData : PlotData
    {
        private List<int> listIdPlayer = new List<int>();
        private List<DataChess> data = new List<DataChess>();
        private List<string> nameOfPlayer = new List<string>();

        public List<string> NameOfPlayer { get => nameOfPlayer; set => nameOfPlayer = value; }
        public List<int> ListIdPlayer { get => listIdPlayer; set => listIdPlayer = value; }

        public void AddPlayer(int idPlayer)
        {
            ListIdPlayer.Add(idPlayer);
        }

        public LineData GetLineData(int idPlayer, int minYear = 0, int maxYear = 9999)
        {
            LineData lineData = new LineData();
            var player = data.Where(line => line.Id_player == idPlayer).GroupBy(x => x.Name).ToList();


            for (int i = 0; i < player[0].Count(); i++)
            {
                if (player[0].ElementAt(i).Year >= minYear && player[0].ElementAt(i).Year <= maxYear)
                {
                    lineData.X.Add(player[0].ElementAt(i).Year);
                    lineData.Y.Add(player[0].ElementAt(i).Elo);
                }
            }

            return lineData;
        }

        public int GetIndex(string player)
        {
            return data.Where(line => line.Name.Equals(player)).First().Id_player;
        }

        public string GetName(int playerId)
        {
            return data.Where(line => line.Id_player.Equals(playerId)).First().Name;
        }

        public void Init(string source)
        {
            List<string> csv = File.ReadAllLines(source).Skip(1).ToList();

            // met les données dans un bon format
            csv.ForEach(c =>
            {
                string[] values = c.Split(',');
                DataChess chess_player = new DataChess();
                if (NameOfPlayer.Contains(values[1]))
                {
                    chess_player.Id_player = NameOfPlayer.IndexOf(values[1]);
                }
                else
                {
                    NameOfPlayer.Add(values[1]);
                    chess_player.Id_player = NameOfPlayer.Count - 1;
                }
                chess_player.Name = values[1];
                chess_player.Elo = Convert.ToInt32(values[2]);
                chess_player.Year = Convert.ToInt32(values[3]);
                chess_player.Age = Convert.ToInt32(values[4]);

                data.Add(chess_player);
            });
        }

        public void RemovePlayer(int idPlayer)
        {
            ListIdPlayer.Remove(idPlayer);
        }


    }
}
