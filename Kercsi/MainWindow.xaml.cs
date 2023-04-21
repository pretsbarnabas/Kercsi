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
        Inventory inventory = new Inventory();
        public MainWindow()
        {
            InitializeComponent();
            stackpanel_inventory.DataContext = inventory;
            stackpanel_crafting.DataContext = inventory;
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
