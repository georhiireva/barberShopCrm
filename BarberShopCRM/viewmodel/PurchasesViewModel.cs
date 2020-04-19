using BarberShopCRM.command;
using BarberShopCRM.model;
using BarberShopCRM.model.database;
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

        private Purchase SelectedPurchase => ((PurchasesWindow)window).SelectedPurchase;
        public PurchasesViewModel (Window window) : base (window) {
            logger = new Logger (this.GetType ().Name);

            addPurchaseCommand = new Command (() => AddPurchase ());
            editPurchaseCommand = new Command (() => EditPurchase ());
            deletePurchaseCommand = new Command (() => DeletePurchase ());

            Purchases = Query.Instance.LoadAllPurchases ();
        }

        private void AddPurchase () {
            var newWindow = new PurchaseEditWindow ();

            if (newWindow.ShowDialog() == true) {
                Purchases = Query.Instance.LoadAllPurchases ();
            }
            
        }
        private void EditPurchase () {
            var newWindow = new PurchaseEditWindow (SelectedPurchase);

            if (newWindow.ShowDialog () == true) {
                Purchases = Query.Instance.LoadAllPurchases ();
            }
        }
        private void DeletePurchase () {
            var result = MessageBox.Show ("Вы действительно хотите удалить закупку?", "Предупреждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
                return;
            else {
                Query.Instance.Delete (SelectedPurchase);
                Purchases = Query.Instance.LoadAllPurchases ();
            }
        }
    }
}
