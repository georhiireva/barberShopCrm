using BarberShopCRM.model;
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
    /// Логика взаимодействия для PurchasesWindow.xaml
    /// </summary>
    public partial class PurchasesWindow : Window {
        private PurchasesViewModel viewModel;
        public Purchase SelectedPurchase => (Purchase)this.purchaseList.SelectedItem;
        public PurchasesWindow () {
            InitializeComponent ();
            DataContext = viewModel = new PurchasesViewModel (this);
        }
    }
}
