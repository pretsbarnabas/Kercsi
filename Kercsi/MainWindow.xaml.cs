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

        Table mainTable;
        public MainWindow()
        {
            InitializeComponent();
            stackpanel_inventory.DataContext = inventory;
            stackpanel_crafting.DataContext = inventory;
            mainTable = new Table();
            FillGrid();
        }

        private void FillGrid()
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    Label newBtn = new();
                    newBtn.Content = (int)mainTable.tileValues[y, x];
                    newBtn.MouseLeftButtonDown += new MouseButtonEventHandler(Move);
                    Grid.SetColumn(newBtn, x);
                    Grid.SetRow(newBtn, y);

                    MainTable.Children.Add(newBtn);
                }
            }
        }

        private void Move(object sender, MouseButtonEventArgs e)
        {
            Label lbl = sender as Label;
            int[] lblCoords = { Grid.GetColumn(lbl), Grid.GetRow(lbl) };
            
            if (Math.Abs(lblCoords[0] - mainTable.playerPositionIndex[0]) <= 1)
            {
                if (Math.Abs(lblCoords[1] - mainTable.playerPositionIndex[1]) <= 1)
                {
                    if ((lblCoords[1] - mainTable.playerPositionIndex[1]) * (lblCoords[0] - mainTable.playerPositionIndex[0]) == 0)
                    {

                    }
                }
            }
        }

        private void btn_craftroad_Click(object sender, RoutedEventArgs e)
        {
            inventory.CraftRoad();
        }

        private void btn_craftshovel_Click(object sender, RoutedEventArgs e)
        {
            inventory.CraftShovel();
        }
    }
}
