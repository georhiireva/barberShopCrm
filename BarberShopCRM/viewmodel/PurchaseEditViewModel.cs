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
    public class PurchaseEditViewModel : ViewModel {
        private Purchase editingPurchase;
        private Purchase startStatePurchase;
        private IList<Product> availibleProducts;
        private IList<Product> addedProducts;
        private ICommand addProductCommand;
        private ICommand deleteProductCommand;
        private ICommand saveAndExitCommand;
        public IList<Product> AvailibleProducts {
            get => availibleProducts;
            set {
                if(availibleProducts != value) {
                    availibleProducts = value;
                    OnPropertyChanged (this, new System.ComponentModel.PropertyChangedEventArgs (nameof (AvailibleProducts)));
                }
            }
        }
        public IList<Product> AddedProducts {
            get => addedProducts;
            set {
                if(addedProducts != value) {
                    addedProducts = value;
                    OnPropertyChanged (this, new System.ComponentModel.PropertyChangedEventArgs (nameof (AddedProducts)));
                }
            } 
        }
        public int ProductCount { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime ReceivedDate { get; set; }
        public ICommand AddProductCommand { get => addProductCommand; }
        public ICommand DeleteProductCommand { get => deleteProductCommand; }
        public ICommand SaveAndExitCommand { get => saveAndExitCommand; }
        public PurchaseEditViewModel (Window window) : base (window) {
            addProductCommand = new Command (() => AddProduct());
            deleteProductCommand = new Command (() => DeleteProduct());
            saveAndExitCommand = new Command (() => SaveAndExit());
            AvailibleProducts = Query.Instance.LoadAllProducts ().ToList ();
        }

        private void AddProduct () {
            var product = (Product)((PurchaseEditWindow)window).availProductsComboBox.SelectedItem;
            AvailibleProducts.Add (product);
            OnPropertyChanged (this, new System.ComponentModel.PropertyChangedEventArgs (nameof (AvailibleProducts)));
        }
        private void DeleteProduct () {
            throw new NotImplementedException ();
        }

        private void SaveAndExit () {
            throw new NotImplementedException ();
        }
    }
}
