using System;
using System.Collections.Generic;
using System.Diagnostics;
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

    public enum Road
    {
        Left = 0b1,
        Right = 0b10,
        Up = 0b100,
        Down = 0b1000,
    }

    public class Tile
    {
        public TileValue value;
        public int roads;
    }

    internal class Table
    {
        public Tile[,] tiles = new Tile[8, 8];
        public int[] playerPositionIndex = new int[2];
        public Inventory inventory;

        public Table()
        {
            Random rnd = new();
            playerPositionIndex = new int[2];
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

        private bool RoadBetween(int x, int y)
        {
            bool isRoadBetween = false;
            if (x != playerPositionIndex[0])
            {
                if (x < playerPositionIndex[0])
                {
                    isRoadBetween = (this.tiles[y, x].roads & (int)Road.Right) != 0;
                }
                else
                {
                    isRoadBetween = (this.tiles[y, x].roads & (int)Road.Left) != 0;
                }
            }
            else if (y != playerPositionIndex[1])
            {
                if (y < playerPositionIndex[1])
                {
                    isRoadBetween = (this.tiles[y, x].roads & (int)Road.Down) != 0;
                }
                else
                {
                    isRoadBetween = (this.tiles[y, x].roads & (int)Road.Up) != 0;
                }
            }

            return isRoadBetween;
        }

        private void SetRoad(int x, int y)
        {
            if (x != playerPositionIndex[0])
            {
                if (x < playerPositionIndex[0])
                {
                    this.tiles[y, x].roads |= (int)Road.Right;
                    this.tiles[playerPositionIndex[1], playerPositionIndex[0]].roads |= (int)Road.Left;
                }
                else
                {
                    this.tiles[y, x].roads |= (int)Road.Left;
                    this.tiles[playerPositionIndex[1], playerPositionIndex[0]].roads |= (int)Road.Right;
                }
            }
            else if (y != playerPositionIndex[1])
            {
                if (y < playerPositionIndex[1])
                {
                    this.tiles[y, x].roads |= (int)Road.Down;
                    this.tiles[playerPositionIndex[1], playerPositionIndex[0]].roads |= (int)Road.Up;
                }
                else
                {
                    this.tiles[y, x].roads |= (int)Road.Up;
                    this.tiles[playerPositionIndex[1], playerPositionIndex[0]].roads |= (int)Road.Down;
                }
            }

            Debug.WriteLine($"{this.tiles[y, x].roads}, {this.tiles[playerPositionIndex[1], playerPositionIndex[0]].roads}");
        }

        public void MovePlayer(int x, int y)
        {
            if (RoadBetween(x, y))
            {
                this.playerPositionIndex[0] = x;
                this.playerPositionIndex[1] = y;
            }
            else if (this.inventory.Road != 0)
            {
                SetRoad(x, y);
                this.playerPositionIndex[0] = x;
                this.playerPositionIndex[1] = y;
                this.inventory.Road--;
            }
        }
    }
}
