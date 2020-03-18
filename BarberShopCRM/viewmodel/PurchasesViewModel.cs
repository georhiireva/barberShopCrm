using BarberShopCRM.command;
using BarberShopCRM.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BarberShopCRM.viewmodel {
    public class PurchasesViewModel : ViewModel {
        private Logger logger;
        private ICommand addPurchaseCommand;
        private ICommand editPurchaseCommand;
        private ICommand deletePurchaseCommand;
        private IEnumerable<Purchase> purchases;

        public ICommand AddPurchaseCommand => addPurchaseCommand;
        public ICommand EditPurchaseCommand => editPurchaseCommand;
        public ICommand DeletePurchasetCommand => deletePurchaseCommand;

        public IEnumerable<Purchase> Purchases {
            get => purchases;
            set {
                if (purchases != value) {
                    purchases = value;
                    OnPropertyChanged (this, new System.ComponentModel.PropertyChangedEventArgs (nameof (Purchases)));
                }

            }
        }

        private Purchase SelectedProduct => ((PurchasesWindow)window).SelectedPurchase;
        public PurchasesViewModel (Window window) : base (window) {
            logger = new Logger (this.GetType ().Name);

            addPurchaseCommand = new Command (() => AddPurchase ());
            editPurchaseCommand = new Command (() => EditPurchase ());
            deletePurchaseCommand = new Command (() => DeletePurchase ());
        }

        private void AddPurchase () {
            var newWindow = new PurchaseEditWindow ();

            if (newWindow.ShowDialog() == true) {

            }
            throw new NotImplementedException ();
        }
        private void EditPurchase () {
            throw new NotImplementedException ();
        }
        private void DeletePurchase () {
            throw new NotImplementedException ();
        }
    }
}
