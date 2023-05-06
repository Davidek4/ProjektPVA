using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBOWN
{
    internal class DB
    {
        public static List<GAME> gamesF(List<GAME> games, string name, string studio, int review, int year, string description)
        {
            int id = 0;
            int tmp = 0;
            for (int i = 0; i < games.Count; i++)
            {
                if (games[i].ID > tmp)
                {
                    tmp = games[i].ID;
                }
            }
            id = tmp + 1;
            games.Add(new GAME { ID = id, Name = name, Studio = studio, Rating = review, Year = year, Description = description });
            
            return games;
        }
        

        

    }
}
