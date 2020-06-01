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
    /// Логика взаимодействия для WriteOffWindow.xaml
    /// </summary>
    public partial class WriteOffWindow : Window {
        private WriteOffViewModel viewModel;
        public WriteOff SelectedWriteOff => (WriteOff)this.writeOffList.SelectedItem;
        public WriteOffWindow () {
            InitializeComponent ();
            DataContext = viewModel = new WriteOffViewModel (this);
        }
    }
}
