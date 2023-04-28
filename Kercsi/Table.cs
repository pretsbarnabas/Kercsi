using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        public UInt16 roads;
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
                        tiles[y, x].value = (TileValue)(newRand - 2);
                    }
                }
            }
        }

        private bool RoadBetween(int x, int y)
        {
            bool isRoadBetween = false;
            if (x != playerXIndex)
            {
                if (x < playerXIndex)
                {
                    isRoadBetween = (this.tiles[y, x].roads & (int)Road.Right) != 0;
                }
                else
                {
                    isRoadBetween = (this.tiles[y, x].roads & (int)Road.Left) != 0;
                }
            }
            else if (y != playerYIndex)
            {
                if (y < playerYIndex)
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
            if (x != playerXIndex)
            {
                if (x < playerXIndex)
                {
                    this.tiles[y, x].roads |= (int)Road.Right;
                    this.tiles[playerYIndex, playerXIndex].roads |= (int)Road.Left;
                }
                else
                {
                    this.tiles[y, x].roads |= (int)Road.Left;
                    this.tiles[playerYIndex, playerXIndex].roads |= (int)Road.Right;
                }
            }
            else if (y != playerYIndex)
            {
                if (y < playerYIndex)
                {
                    this.tiles[y, x].roads |= (int)Road.Down;
                    this.tiles[playerYIndex, playerXIndex].roads |= (int)Road.Up;
                }
                else
                {
                    this.tiles[y, x].roads |= (int)Road.Up;
                    this.tiles[playerYIndex, playerXIndex].roads |= (int)Road.Down;
                }
            }

            Debug.WriteLine($"{this.tiles[y, x].roads}, {this.tiles[playerYIndex, playerXIndex].roads}");
        }

        public void MovePlayer(int x, int y)
        {
            if (RoadBetween(x, y))
            {
                this.playerXIndex = x;
                this.playerYIndex = y;
            }
            else if (this.inventory.Road != 0)
            {
                SetRoad(x, y);
                this.playerXIndex = x;
                this.playerYIndex = y;
                this.inventory.Road--;
            }
        }

        public void Treasure()
        {
            var tile = tiles[playerXIndex, playerYIndex].value.ToString();
            if (tiles[playerXIndex, playerYIndex].value.ToString() == "Meadow")
            {
                if (inventory.Shovel >= 1)
                {
                    inventory.Shovel--;
                    inventory.Treasure++;
                }
                else
                {
                    MessageBox.Show("No shovels??", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Not a Meadow", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
