using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kercsi
{
    public enum TileValue {
        Forest = 0,
        Mountain,
        Meadow,
        Hill,
        None,
    }

    internal class Table
    {
        public TileValue[,] tileValues = new TileValue[8, 8];

        public Table()
        {
            Random rnd = new();
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    int newRand = rnd.Next(6);
                        
                    if ((y == 0 && x == 0) || (y == 7 && x == 7) || (y == 0 && x == 7) || (y == 7 && x == 0))
                    {
                        tileValues[y, x] = TileValue.None;
                        continue;
                    }

                    if (newRand < 3)
                    {
                        tileValues[y, x] = TileValue.Forest;
                    }
                    else
                    {
                        tileValues[y, x] = (TileValue)(newRand - 2);
                    }

                }
            }
        }
    }
}
