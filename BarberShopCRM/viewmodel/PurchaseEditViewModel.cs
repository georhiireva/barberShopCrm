using BarberShopCRM.command;
using BarberShopCRM.model;
using BarberShopCRM.model.database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BarberShopCRM.viewmodel {
    public class PurchaseEditViewModel : ViewModel {
        private Purchase editingPurchase;
        private Purchase startStatePurchase;
        private DateTime paymentDate;
        private DateTime receptionDate;
        private IList<Product> availibleProducts;
        private ObservableCollection<ProductWrapper> addedProducts;
        private int productCount;
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
        public ObservableCollection<ProductWrapper> AddedProducts {
            get => addedProducts;
            set {
                if(addedProducts != value) {
                    addedProducts = value;
                    OnPropertyChanged (this, new System.ComponentModel.PropertyChangedEventArgs (nameof (AddedProducts)));
                }
            } 
        }
        public int ProductCount {
            get => productCount;
            set {
                if(productCount != value) {
                    productCount = value;
                    OnPropertyChanged (this, new System.ComponentModel.PropertyChangedEventArgs (nameof(ProductCount)));
                }
            }
        }
        public DateTime PaymentDate {
            get => paymentDate;
            set {
                if(paymentDate != value) {
                    paymentDate = value;
                    OnPropertyChanged (this, new System.ComponentModel.PropertyChangedEventArgs (nameof (PaymentDate)));
                }
            }
        }
        public DateTime ReceptionDate {
            get => receptionDate;
            set {
                if (receptionDate != value) {
                    receptionDate = value;
                    OnPropertyChanged (this, new System.ComponentModel.PropertyChangedEventArgs (nameof (ReceptionDate)));
                }
            }
        }
        public ICommand AddProductCommand { get => addProductCommand; }
        public ICommand DeleteProductCommand { get => deleteProductCommand; }
        public ICommand SaveAndExitCommand { get => saveAndExitCommand; }
        public PurchaseEditViewModel (Window window) : base (window) {
            addProductCommand = new Command (() => AddProduct());
            deleteProductCommand = new Command (() => DeleteProduct());
            saveAndExitCommand = new Command (() => SaveAndExit());
            AvailibleProducts = Query.Instance.LoadAllProducts ().ToList ();
            AddedProducts = new ObservableCollection<ProductWrapper> ();
            editingPurchase = new Purchase ();
            paymentDate = DateTime.Now;
            receptionDate = DateTime.Now;
        }

        private void AddProduct () {
            var product = (Product)((PurchaseEditWindow)window).availProductsComboBox.SelectedItem;
            if (AddedProducts.Where(elt => elt.Product.Name == product.Name).Count() > 0) {
                throw new Exception ($"{product.Name} уже добавлен!");
            }
            AddedProducts.Add (new ProductWrapper () {Product = product, Count = ProductCount });
        }
        private void DeleteProduct () {
            var removableProductWrapper = (ProductWrapper)((PurchaseEditWindow)window).addedProductsListView.SelectedItem;
            AddedProducts.Remove (removableProductWrapper);
        }

        private void SaveAndExit () {
            editingPurchase.PaymentDate = PaymentDate;
            editingPurchase.ReceptionDate = ReceptionDate;
            editingPurchase.ProductsWithPrices = new Dictionary<ProductWrapper, double> ();
            foreach(var product in AddedProducts) {
                editingPurchase.ProductsWithPrices.Add (product, 0);
            }
            Query.Instance.Add (editingPurchase);
            window.DialogResult = true;
        }
    }
}
