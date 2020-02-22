﻿using BarberShopCRM.viewmodel;
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

namespace BarberShopCRM {
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private ProductsWindow productsWindow;
        private PurchasesWindow purchasesWindow;
        private WriteOffWindow writeOffWindow;
        private OddmentsWindow oddmentsWindow;
        private MainMenuViewModel viewModel;

        public MainMenuViewModel ViewModel => viewModel;
        public MainWindow () {
            InitializeComponent ();
            DataContext = viewModel = new MainMenuViewModel (this);
        }
    }
}
