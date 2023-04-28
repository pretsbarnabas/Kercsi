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

    public class Player : INotifyPropertyChanged
    {
        public int playerIndex;
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

        public Player(int i)
        {
            playerXIndex = i * 7;
            playerYIndex = 0;
            playerIndex = i;
            inventory = new();
        }
    }

    internal class Table
    {
        public Tile[,] tiles = new Tile[8, 8];

        public Player[] players = new Player[2];
        public Player currentPlayer;

        public Table()
        {
            Random rnd = new();
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new Player(i);
            }

            currentPlayer = players[0];

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
            if (x != currentPlayer.playerXIndex)
            {
                if (x < currentPlayer.playerXIndex)
                {
                    isRoadBetween = (this.tiles[y, x].roads & (int)Road.Right) != 0;
                }
                else
                {
                    isRoadBetween = (this.tiles[y, x].roads & (int)Road.Left) != 0;
                }
            }
            else if (y != currentPlayer.playerYIndex)
            {
                if (y < currentPlayer.playerYIndex)
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
            if (x != currentPlayer.playerXIndex)
            {
                if (x < currentPlayer.playerXIndex)
                {
                    this.tiles[y, x].roads |= (int)Road.Right;
                    this.tiles[currentPlayer.playerYIndex, currentPlayer.playerXIndex].roads |= (int)Road.Left;
                }
                else
                {
                    this.tiles[y, x].roads |= (int)Road.Left;
                    this.tiles[currentPlayer.playerYIndex, currentPlayer.playerXIndex].roads |= (int)Road.Right;
                }
            }
            else if (y != currentPlayer.playerYIndex)
            {
                if (y < currentPlayer.playerYIndex)
                {
                    this.tiles[y, x].roads |= (int)Road.Down;
                    this.tiles[currentPlayer.playerYIndex, currentPlayer.playerXIndex].roads |= (int)Road.Up;
                }
                else
                {
                    this.tiles[y, x].roads |= (int)Road.Up;
                    this.tiles[currentPlayer.playerYIndex, currentPlayer.playerXIndex].roads |= (int)Road.Down;
                }
            }

            Debug.WriteLine($"{this.tiles[y, x].roads}, {this.tiles[currentPlayer.playerYIndex, currentPlayer.playerXIndex].roads}");
        }

        public void MovePlayer(int x, int y)
        {
            if (RoadBetween(x, y))
            {
                this.currentPlayer.playerXIndex = x;
                this.currentPlayer.playerYIndex = y;
            }
            else if (this.currentPlayer.inventory.Road != 0)
            {
                SetRoad(x, y);
                this.currentPlayer.playerXIndex = x;
                this.currentPlayer.playerYIndex = y;
                this.currentPlayer.inventory.Road--;
            }
            GameOver();
        }

        public void Treasure()
        {
            var tile = tiles[currentPlayer.playerXIndex, currentPlayer.playerYIndex].value.ToString();
            if (tiles[currentPlayer.playerXIndex, currentPlayer.playerYIndex].value.ToString() == "Meadow")
            {
                if (currentPlayer.inventory.Shovel >= 1)
                {
                    currentPlayer.inventory.Shovel--;
                    currentPlayer.inventory.Treasure++;
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

        public void GameOver()
        {
            if (this.currentPlayer.playerXIndex == Math.Abs(this.currentPlayer.playerIndex - 1) * 7 && this.currentPlayer.playerYIndex == 7)
            {
                MessageBox.Show("You win!", "Game over", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
