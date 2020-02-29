using BarberShopCRM.viewmodel;
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
using System.Windows.Shapes;

namespace BarberShopCRM {
    /// <summary>
    /// Логика взаимодействия для ProductsWindow.xaml
    /// </summary>
    public partial class ProductsWindow : Window {
        private Window productEditWindow;
        private ProductsViewModel viewModel;
        public ProductsWindow () {
            InitializeComponent ();
            DataContext = viewModel = new ProductsViewModel (this);
        }

        private void OpenEditProductWindow (object sender, RoutedEventArgs e) {

        }

        private void OpenAddProductWindow (object sender, RoutedEventArgs e) {
            productEditWindow = new ProductEditWindow ();
            productEditWindow.ShowDialog ();
        }
    }
}
