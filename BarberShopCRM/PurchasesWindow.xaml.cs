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
    /// Логика взаимодействия для PurchasesWindow.xaml
    /// </summary>
    public partial class PurchasesWindow : Window {
        private PurchaseEditWindow purchaseEditWindow;
        public PurchasesWindow () {
            InitializeComponent ();
        }

        private void OpenPurchaseEditWindow (object sender, RoutedEventArgs e) {
            purchaseEditWindow = new PurchaseEditWindow ();
            purchaseEditWindow.ShowDialog ();
        }
    }
}
