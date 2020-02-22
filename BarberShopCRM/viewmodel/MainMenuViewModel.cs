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
        private MainWindow mainWindow;
        public MainMenuViewModel (MainWindow mainWindow) => this.mainWindow = mainWindow;
        public ICommand OpenProductsWindow => new Command (() => new ProductsWindow ().ShowDialog ());
        public ICommand OpenPurchasesWindow => new Command (() => new PurchasesWindow ().ShowDialog ());
        public ICommand OpenWriteOffWindow => new Command (() => new WriteOffWindow ().ShowDialog ());
        public ICommand OpenOddmentsWindow => new Command (() => new OddmentsWindow ().ShowDialog ());
        public ICommand ExitCommand => new CloseWindowCommand (mainWindow);
    }
}
