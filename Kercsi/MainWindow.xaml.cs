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
        #region Dice
        private static Rectangle diceOnBoard;
        public static Grid Grd_Table;
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
            Grd_Table = Gr_Table;
            mainTable = new();
            stackpanel_inventory.DataContext = mainTable.inventory;
            stackpanel_crafting.DataContext = mainTable.inventory;
            stackpanel_inventory.DataContext = mainTable.inventory;
            stackpanel_crafting.DataContext = mainTable.inventory;
            FillGrid();
        }

        private void btn_craftroad_Click(object sender, RoutedEventArgs e)
        {
            mainTable.inventory.CraftRoad();
        }

        private void btn_craftshovel_Click(object sender, RoutedEventArgs e)
        {
            mainTable.inventory.CraftShovel();
        }

        private void Rt_Dice_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string tile = dice.Roll();
            switch (tile)
            {
                case("Mountain"):
                    metal.Content = int.Parse(metal.Content.ToString()) + 1;
                    break;
                case ("Hill"):
                    clay.Content = int.Parse(clay.Content.ToString()) + 1;
                    break;
                case ("Meadow"):
                    break;
                case ("Sand"):
                    break;
                case ("Forest"):
                    wood.Content = int.Parse(wood.Content.ToString()) + 1;
                    break;
            }
        }
            //Return-ol egy intet   (lbl_Dice.Content = Roll())


        private void Move(object sender, MouseButtonEventArgs e)
        {
            Image img = sender as Image;
            int[] lblCoords = { Grid.GetColumn(img), Grid.GetRow(img) };

            if (Math.Abs(lblCoords[0] - mainTable.playerPositionIndex[0]) <= 1)
            {
                if (Math.Abs(lblCoords[1] - mainTable.playerPositionIndex[1]) <= 1)
                {
                    if ((lblCoords[1] - mainTable.playerPositionIndex[1]) * (lblCoords[0] - mainTable.playerPositionIndex[0]) == 0)
                    {
                        mainTable.MovePlayer(lblCoords[0], lblCoords[1]);
                    }
                }
            }

            MessageBox.Show($"x: {mainTable.playerPositionIndex[0]}, y: {mainTable.playerPositionIndex[1]}");
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
                        Grid grd = new();
                        grd.Background = Brushes.Gray;
                        image.Width = 110;
                        image.Height = 110;
                        image.MouseLeftButtonDown += new MouseButtonEventHandler(Move);
                        image.Source = new BitmapImage(new Uri("img/wood.png", UriKind.Relative));
                        Grid.SetRow(grd, x);
                        Grid.SetColumn(grd, y);
                        grd.Children.Add(image);
                        Gr_Table.Children.Add(grd);
                    }
                    else if (mainTable.tiles[y, x].value.ToString() == "Meadow")
                    {
                        Image image = new Image();
                        Grid grd = new();
                        grd.Background = Brushes.Gray;
                        image.Width = 110;
                        image.Height = 110;
                        image.MouseLeftButtonDown += new MouseButtonEventHandler(Move);
                        image.Source = new BitmapImage(new Uri("img/ret.png", UriKind.Relative));
                        Grid.SetRow(grd, x);
                        Grid.SetColumn(grd, y);
                        grd.Children.Add(image);
                        Gr_Table.Children.Add(grd);
                    }
                    else if (mainTable.tiles[y, x].value.ToString() == "Mountain")
                    {
                        Image image = new Image();
                        Grid grd = new();
                        grd.Background = Brushes.Gray;
                        image.Width = 110;
                        image.Height = 110;
                        image.MouseLeftButtonDown += new MouseButtonEventHandler(Move);
                        image.Source = new BitmapImage(new Uri("img/hegy xd.png", UriKind.Relative));
                        Grid.SetRow(grd, x);
                        Grid.SetColumn(grd, y);
                        grd.Children.Add(image);
                        Gr_Table.Children.Add(grd);
                    }
                    else if (mainTable.tiles[y, x].value.ToString() == "Hill")
                    {
                        Image image = new Image();
                        Grid grd = new();
                        grd.Background = Brushes.Gray;
                        image.Width = 110;
                        image.Height = 110;
                        image.MouseLeftButtonDown += new MouseButtonEventHandler(Move);
                        image.Source = new BitmapImage(new Uri("img/claydomb.png", UriKind.Relative));
                        Grid.SetRow(grd, x);
                        Grid.SetColumn(grd, y);
                        grd.Children.Add(image);
                        Gr_Table.Children.Add(grd);
                    }
                    else if (mainTable.tiles[y, x].value.ToString() == "None")
                    {
                        Image image = new Image();
                        Grid grd = new();
                        grd.Background = Brushes.Gray;
                        image.Width = 110;
                        image.Height = 110;
                        image.MouseLeftButtonDown += new MouseButtonEventHandler(Move);
                        image.Source = new BitmapImage(new Uri("img/sand.jpg", UriKind.Relative));
                        Grid.SetRow(grd, x);
                        Grid.SetColumn(grd, y);
                        grd.Children.Add(image);
                        Gr_Table.Children.Add(grd);
                    }
                }
            }
        }
    }
}
