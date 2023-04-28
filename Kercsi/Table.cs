using System;
using System.Collections.Generic;
using System.ComponentModel;
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

    public struct Road
    {
        public bool Left;
        public bool Right;
        public bool Up;
        public bool Down;
    }

    public class Tile
    {
        public TileValue value;
        public Road roads;
    }

    internal class Table : INotifyPropertyChanged
    {
        public Tile[,] tiles = new Tile[8, 8];
        private int[] playerpositionindex = new int[2];

        private int playerxindex;

        public int playerXIndex
        {
            get { return playerxindex; }
            set 
            { 
                playerxindex = value; 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("playerXIndex"));
            }
        }
        private int playeryindex;

        public int playerYIndex
        {
            get { return playeryindex; }
            set 
            { 
                playeryindex = value; 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("playerYIndex"));
            }
        }

        public Inventory inventory;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Table()
        {
            Random rnd = new();
            playerXIndex = 0;
            playerYIndex = 0;
            inventory = new();
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    int newRand = rnd.Next(6);
                    this.tiles[y, x] = new Tile();
                        
                    if ((y == 0 && x == 0) || (y == 7 && x == 7) || (y == 0 && x == 7) || (y == 7 && x == 0))
                    {
                        tiles[y, x].value = TileValue.None;
                        continue;
                    }

                    if (newRand < 3)
                    {
                        tiles[y, x].value = TileValue.Forest;
                    }
                    else
                    {
                        tiles[y, x].value = (TileValue)(newRand - 3);
                    }
                }
            }
        }

        public bool RoadBetween(int x, int y)
        {
            this.playerXIndex = x;
            this.playerYIndex = y;
            if (this.playerXIndex < x)
            {
                if (this.tiles[y, x].roads.Left)
                {
                    return true;
                }
            }
            if (this.playerXIndex > x)
            {
                if (this.tiles[y, x].roads.Right)
                {
                    return true;
                }
            }
            if (this.playerYIndex < y)
            {
                if (this.tiles[y, x].roads.Down)
                {
                    return true;
                }
            }
            if (this.playerYIndex > y)
            {
                if (this.tiles[y, x].roads.Up)
                {
                    return true;
                }
            }

            return false;
        }

        public void MovePlayer(int x, int y)
        {
            if (this.inventory.Road != 0 || this.RoadBetween(x, y))
            {
                this.playerXIndex = x;
                this.playerYIndex = y;
                this.inventory.Road--;
            }
        }
    }
}
