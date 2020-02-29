using BarberShopCRM.viewmodel;
using System;
using System.Collections.Generic;
using System.IO;
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
        private MainMenuViewModel viewModel;

        public MainMenuViewModel ViewModel => viewModel;
        public MainWindow () {
            InitializeComponent ();
            DataContext = viewModel = new MainMenuViewModel (this);
        }

        private void Button_Click (object sender, RoutedEventArgs e) {
            using (var writer = File.CreateText ("path")) { }
        }
    }
}
