using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kercsi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //0 dob vagy kincset keres
        //1 lép
        //2 kincset keres vagy tovább adja a kört
        int circle_round = 0;
        #region Dice
        private static Rectangle diceOnBoard;
        public static Rectangle DiceOnBoard
        {
            get { return diceOnBoard; }
            set { diceOnBoard = value; }
        }
        Dice dice = new Dice();
        #endregion

        Table mainTable;
        public MainWindow()
        {
            InitializeComponent();
            diceOnBoard = Rt_Dice;
            mainTable = new();
            stackpanel_inventory.DataContext = mainTable.players[0].inventory;
            stackpanel_crafting.DataContext = mainTable.players[0].inventory;
            img_player0.DataContext = mainTable.players[0];
            img_player1.DataContext = mainTable.players[1];
            mainTable.currentPlayer = mainTable.players[0];
            FillGrid();
        }

        private void btn_craftroad_Click(object sender, RoutedEventArgs e)
        {
            mainTable.currentPlayer.inventory.CraftRoad();
        }

        private void btn_craftshovel_Click(object sender, RoutedEventArgs e)
        {
            mainTable.currentPlayer.inventory.CraftShovel();
        }

        private void btn_tresure(object sender, RoutedEventArgs e)
        {
            mainTable.Treasure();
        }

        private void Rt_Dice_MouseDown(object sender, MouseButtonEventArgs e)
        {

            string tile = dice.Roll();
            switch (tile)
            {
                case ("Mountain"):
                    mainTable.currentPlayer.inventory.Metal++;
                    break;
                case ("Hill"):
                    mainTable.currentPlayer.inventory.Clay++;
                    break;
                case ("Meadow"):
                    break;
                case ("Sand"):
                    break;
                case ("Forest"):
                    mainTable.currentPlayer.inventory.Wood++;
                    break;
            }
            circle_round = Math.Abs(circle_round - 1);
            mainTable.currentPlayer = mainTable.players[circle_round];
            stackpanel_inventory.DataContext = mainTable.currentPlayer.inventory;
            stackpanel_crafting.DataContext = mainTable.currentPlayer.inventory;
        }
            //Return-ol egy intet   (lbl_Dice.Content = Roll())


        private void Move(object sender, MouseButtonEventArgs e)
        {
            Image img = sender as Image;
            int[] lblCoords = { Grid.GetColumn(img), Grid.GetRow(img) };

            if (Math.Abs(lblCoords[0] - mainTable.currentPlayer.playerXIndex) <= 1)
            {
                if (Math.Abs(lblCoords[1] - mainTable.currentPlayer.playerYIndex) <= 1)
                {
                    if ((lblCoords[1] - mainTable.currentPlayer.playerYIndex) * (lblCoords[0] - mainTable.currentPlayer.playerXIndex) == 0)
                    {
                        mainTable.MovePlayer(lblCoords[0], lblCoords[1]);
                    }
                }
            }

        }

        public void FillGrid()
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (mainTable.tiles[y, x].value.ToString() == "Forest")
                    {
                        Image image = new Image();
                        image.MouseLeftButtonDown += new MouseButtonEventHandler(Move);
                        image.Source = new BitmapImage(new Uri("img/wood.png", UriKind.Relative));
                        Grid.SetRow(image, x);
                        Grid.SetColumn(image, y);
                        Gr_Table.Children.Add(image);
                    }
                    else if (mainTable.tiles[y, x].value.ToString() == "Meadow")
                    {
                        Image image = new Image();
                        image.MouseLeftButtonDown += new MouseButtonEventHandler(Move);
                        image.Source = new BitmapImage(new Uri("img/ret.png", UriKind.Relative));
                        Grid.SetRow(image, x);
                        Grid.SetColumn(image, y);
                        Gr_Table.Children.Add(image);
                    }
                    else if (mainTable.tiles[y, x].value.ToString() == "Mountain")
                    {
                        Image image = new Image();
                        image.MouseLeftButtonDown += new MouseButtonEventHandler(Move);
                        image.Source = new BitmapImage(new Uri("img/hegy xd.png", UriKind.Relative));
                        Grid.SetRow(image, x);
                        Grid.SetColumn(image, y);
                        Gr_Table.Children.Add(image);
                    }
                    else if (mainTable.tiles[y, x].value.ToString() == "Hill")
                    {
                        Image image = new Image();
                        image.MouseLeftButtonDown += new MouseButtonEventHandler(Move);
                        image.Source = new BitmapImage(new Uri("img/claydomb.png", UriKind.Relative));
                        Grid.SetRow(image, x);
                        Grid.SetColumn(image, y);
                        Gr_Table.Children.Add(image);
                    }
                    else if (mainTable.tiles[y, x].value.ToString() == "None")
                    {
                        Image image = new Image();
                        image.MouseLeftButtonDown += new MouseButtonEventHandler(Move);
                        image.Source = new BitmapImage(new Uri("img/sand.jpg", UriKind.Relative));
                        Grid.SetRow(image, x);
                        Grid.SetColumn(image, y);
                        Gr_Table.Children.Add(image);
                    }
                }
            }
        }
    }
}
