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
        private DateTime paymentDate;
        private DateTime receptionDate;
        private IList<Product> availibleProducts;
        private ObservableCollection<ProductWrapper> addedProducts;
        private ProductWrapper selectedProduct;
        private int productCount;
        private double productPrice;
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
                    SelectedProduct.Count = productCount;
                    RefreshAddedProducts ();
                }
            }
        }
        public double ProductPrice {
            get => productPrice;
            set {
                if (productPrice != value) {
                    productPrice = value;
                    OnPropertyChanged (this, new System.ComponentModel.PropertyChangedEventArgs (nameof (ProductPrice)));
                    SelectedProduct.Price = productPrice;
                    RefreshAddedProducts ();
                }
            }
        }

        private void RefreshAddedProducts () {
            AddedProducts.Remove (SelectedProduct);
            AddedProducts.Add (SelectedProduct);
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
        public ProductWrapper SelectedProduct {
            get {
                return selectedProduct;
            }
            set {
                if(selectedProduct != value && value != null) {
                    selectedProduct = value;
                    OnPropertyChanged (this, new System.ComponentModel.PropertyChangedEventArgs (nameof (SelectedProduct)));
                    ProductCount = selectedProduct.Count;
                    ProductPrice = selectedProduct.Price;
                    ((PurchaseEditWindow)window).availProductsComboBox.SelectedItem = AvailibleProducts
                        .Where (elt => ((Product)elt).Name == selectedProduct.Product.Name).First ();
                }
            }
        }
        public ICommand AddProductCommand { get => addProductCommand; }
        public ICommand DeleteProductCommand { get => deleteProductCommand; }
        public ICommand SaveAndExitCommand { get => saveAndExitCommand; }
        public PurchaseEditViewModel (Window window) : base (window) {
            Init ();
            AddedProducts = new ObservableCollection<ProductWrapper> ();
            editingPurchase = new Purchase();
            PaymentDate = DateTime.Now;
            ReceptionDate = DateTime.Now;
            ProductCount = 1;
            ProductPrice = 0;
        }

        public PurchaseEditViewModel(Window window, Purchase purchase) : base(window) {
            Init ();
            AddedProducts = purchase.ProductsWithPrices != null 
                ? new ObservableCollection<ProductWrapper> (purchase.ProductsWithPrices)
                : new ObservableCollection<ProductWrapper> ();
            editingPurchase = purchase;
            PaymentDate = purchase.PaymentDate != null ? purchase.PaymentDate : DateTime.Now;
            ReceptionDate = purchase.ReceptionDate != null ? purchase.ReceptionDate : DateTime.Now;
            ProductCount = SelectedProduct != null ? SelectedProduct.Count : 1;
            ProductPrice = SelectedProduct != null ? SelectedProduct.Price : 0;
        }

        private void Init() {
            addProductCommand = new Command (() => AddProduct ());
            deleteProductCommand = new Command (() => DeleteProduct ());
            saveAndExitCommand = new Command (() => SaveAndExit ());
            AvailibleProducts = Query.Instance.LoadAllProducts ().ToList ();
            var peWindow = (PurchaseEditWindow)window;
            peWindow.addedProductsListView.SelectionChanged += SelectedProductChanged;
            SelectedProduct = (ProductWrapper)((PurchaseEditWindow)window).addedProductsListView.SelectedItem;
        }

        private void SelectedProductChanged (object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
            SelectedProduct = (ProductWrapper)((PurchaseEditWindow)window).addedProductsListView.SelectedItem;
        }

        private void AddProduct () {
            var product = (Product)((PurchaseEditWindow)window).availProductsComboBox.SelectedItem;
            if (AddedProducts.Where(elt => elt.Product.Name == product.Name).Count() > 0) {
                throw new Exception ($"{product.Name} уже добавлен!");
            }
            AddedProducts.Add (new ProductWrapper () {Product = product, Count = ProductCount, Price = ProductPrice });
        }
        private void DeleteProduct () {
            var removableProductWrapper = (ProductWrapper)((PurchaseEditWindow)window).addedProductsListView.SelectedItem;
            AddedProducts.Remove (removableProductWrapper);
        }

        private void SaveAndExit () {
            editingPurchase.PaymentDate = PaymentDate;
            editingPurchase.ReceptionDate = ReceptionDate;
            editingPurchase.ProductsWithPrices = new List<ProductWrapper> ();
            foreach(var product in AddedProducts) {
                editingPurchase.ProductsWithPrices.Add (product);
            }
            Query.Instance.Add (editingPurchase);
            window.DialogResult = true;
        }
    }
}
 