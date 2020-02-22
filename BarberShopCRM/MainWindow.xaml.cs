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
        public MainWindow () {
            InitializeComponent ();
        }

        private void OpenProductsWindow (object sender, RoutedEventArgs e) {
            productsWindow = new ProductsWindow ();
            productsWindow.ShowDialog ();
        }

        private void OpenPurchasesWindow (object sender, RoutedEventArgs e) {
            purchasesWindow = new PurchasesWindow ();
            purchasesWindow.ShowDialog ();
        }

        private void OpenWriteOffWindow (object sender, RoutedEventArgs e) {
            writeOffWindow = new WriteOffWindow ();
            writeOffWindow.ShowDialog ();
        }

        private void OpenOddmentsWindow (object sender, RoutedEventArgs e) {
            oddmentsWindow = new OddmentsWindow ();
            oddmentsWindow.ShowDialog ();
        }
    }
}
