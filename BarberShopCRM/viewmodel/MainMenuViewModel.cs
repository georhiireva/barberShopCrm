using BarberShopCRM.command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BarberShopCRM.viewmodel {
    public class MainMenuViewModel : ViewModel {
        public ICommand OpenProductsWindow => new Command (() => new ProductsWindow ().Show ());
        public ICommand OpenPurchasesWindow => new Command (() => new PurchasesWindow ().ShowDialog ());
        public ICommand OpenWriteOffWindow => new Command (() => new WriteOffWindow ().ShowDialog ());
        public ICommand OpenOddmentsWindow => new Command (() => new OddmentsWindow ().ShowDialog ());

        public MainMenuViewModel (Window mainWindow) : base (mainWindow) { }
         
    }
}
