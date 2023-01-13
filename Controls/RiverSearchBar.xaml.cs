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

namespace Rivers.Controls
{
    /// <summary>
    /// Interaction logic for RiverSearchBar.xaml
    /// </summary>
    public partial class RiverSearchBar : UserControl
    {
        public string riverID;
        MainWindow mainWindowInst;

        public RiverSearchBar()
        {
            InitializeComponent();
            mainWindowInst = Window.GetWindow(App.Current.MainWindow) as MainWindow;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mainWindowInst.SearchTextBox.Clear();
            mainWindowInst.StackPanel1.Children.Clear();

            mainWindowInst.APICall(riverID);
        }
    }
}
