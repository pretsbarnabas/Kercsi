﻿using System;
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
            mainTable = new();
            FillGrid();
            

        }
        public void FillGrid()
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (mainTable.tileValues[y, x].ToString() == "Forest")
                    {
                        Image image = new Image();
                        image.Source = new BitmapImage(new Uri("/wood.png", UriKind.Relative));
                        Grid.SetRow(image, x);
                        Grid.SetColumn(image, y);
                        Gr_Table.Children.Add(image);
                    }
                    else if (mainTable.tileValues[y, x].ToString() == "Meadow")
                    {
                        Image image = new Image();
                        image.Source = new BitmapImage(new Uri("/ret.png", UriKind.Relative));
                        Grid.SetRow(image, x);
                        Grid.SetColumn(image, y);
                        Gr_Table.Children.Add(image);
                    }
                    else if (mainTable.tileValues[y, x].ToString() == "Mountain")
                    {
                        Image image = new Image();
                        image.Source = new BitmapImage(new Uri("/hegy xd.png", UriKind.Relative));
                        Grid.SetRow(image, x);
                        Grid.SetColumn(image, y);
                        Gr_Table.Children.Add(image);
                    }
                    else if (mainTable.tileValues[y, x].ToString() == "Hill")
                    {
                        Image image = new Image();
                        image.Source = new BitmapImage(new Uri("/claydomb.png", UriKind.Relative));
                        Grid.SetRow(image, x);
                        Grid.SetColumn(image, y);
                        Gr_Table.Children.Add(image);
                    }
                    else if (mainTable.tileValues[y, x].ToString() == "None")
                    {

                        Image image = new Image();
                        image.Source = new BitmapImage(new Uri("/sand.jpg", UriKind.Relative));
                        Grid.SetRow(image, x);
                        Grid.SetColumn(image, y);
                        Gr_Table.Children.Add(image);
                    }
                }
            }
        }
    }
}
