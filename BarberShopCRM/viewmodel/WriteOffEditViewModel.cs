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
    public class WriteOffEditViewModel : ViewModel {
        private WriteOff editingWriteOff;
        private DateTime writeOfftDate;
        private string masterName;
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
                if (availibleProducts != value) {
                    availibleProducts = value;
                    OnPropertyChanged (this, new System.ComponentModel.PropertyChangedEventArgs (nameof (AvailibleProducts)));
                }
            }
        }

        public ObservableCollection<ProductWrapper> AddedProducts {
            get => addedProducts;
            set {
                if (addedProducts != value) {
                    addedProducts = value;
                    OnPropertyChanged (this, new System.ComponentModel.PropertyChangedEventArgs (nameof (AddedProducts)));
                }
            }
        }

        public int ProductCount {
            get => productCount;
            set {
                if (productCount != value) {
                    productCount = value;
                    OnPropertyChanged (this, new System.ComponentModel.PropertyChangedEventArgs (nameof (ProductCount)));
                }
            }
        }

        public double ProductPrice {
            get => productPrice;
            set {
                if (productPrice != value) {
                    productPrice = value;
                    OnPropertyChanged (this, new System.ComponentModel.PropertyChangedEventArgs (nameof (ProductPrice)));
                }
            }
        }

        public ProductWrapper SelectedProduct {
            get {
                return selectedProduct;
            }
            set {
                if (selectedProduct != value && value != null) {
                    selectedProduct = value;
                    OnPropertyChanged (this, new System.ComponentModel.PropertyChangedEventArgs (nameof (SelectedProduct)));
                    ProductCount = selectedProduct.ClosedCount;
                    ProductPrice = selectedProduct.Price;
                    ((WriteOffEditWindow)window).availProductsComboBox.SelectedItem = AvailibleProducts
                        .Where (elt => ((Product)elt).Name == selectedProduct.Product.Name).First ();
                }
            }
        }

        public string MasterName {
            get => masterName;
            set {
                if(masterName != value) {
                    masterName = value;
                    OnPropertyChanged (this, new System.ComponentModel.PropertyChangedEventArgs (nameof (MasterName)));
                }
            }
        }
        public DateTime WriteOffDate {
            get => writeOfftDate;
            set {
                if(writeOfftDate != value) {
                    writeOfftDate = value;
                    OnPropertyChanged (this, new System.ComponentModel.PropertyChangedEventArgs (nameof (WriteOffDate)));
                }
            }
        }

        private void SelectedProductChanged (object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
            SelectedProduct = (ProductWrapper)((WriteOffEditWindow)window).addedProductsListView.SelectedItem;
        }

        private void RefreshAddedProducts () {
            if (AddedProducts == null || AddedProducts.Count == 0)
                return;
            AddedProducts.Remove (SelectedProduct);
            AddedProducts.Add (SelectedProduct);
            SelectedProduct = null;
        }

        public ICommand AddProductCommand { get => addProductCommand; }
        public ICommand DeleteProductCommand { get => deleteProductCommand; }
        public ICommand SaveAndExitCommand { get => saveAndExitCommand; }
        public WriteOffEditViewModel(Window window, WriteOff writeOff) : base(window) {
            AddedProducts = writeOff.ProductsWithPrices != null
                ? new ObservableCollection<ProductWrapper> (writeOff.ProductsWithPrices)
                : new ObservableCollection<ProductWrapper> ();
            Init (writeOff);
            editingWriteOff = writeOff;
            MasterName = writeOff.MasterName;
            WriteOffDate = writeOff.WriteOffDate;
        }

        private void Init (WriteOff writeOff) {
            Init ();
            var selectedProduct = writeOff?.ProductsWithPrices?.First ();
            if(selectedProduct != null && selectedProduct != SelectedProduct) {
                SelectedProduct = selectedProduct;
            }
        }

        public void Init () {
            addProductCommand = new Command (() => AddProduct ());
            deleteProductCommand = new Command (() => DeleteProduct ());
            saveAndExitCommand = new Command (() => SaveAndExit ());
            AvailibleProducts = Query.Instance.LoadAllProducts ().ToList ();
            var weWindow = (WriteOffEditWindow)window;
            weWindow.addedProductsListView.SelectionChanged += SelectedProductChanged;
            SelectedProduct = new ProductWrapper () { Product = AvailibleProducts.First (), ClosedCount = 1, Price = 0 };
        }

        private void SaveAndExit () {
            editingWriteOff.MasterName = MasterName;
            editingWriteOff.WriteOffDate = WriteOffDate;
            editingWriteOff.ProductsWithPrices = new List<ProductWrapper> ();
            foreach (var product in AddedProducts) {
                editingWriteOff.ProductsWithPrices.Add (product);
            }
            Query.Instance.Add (editingWriteOff);
            window.DialogResult = true;
        }

        private void AddProduct () {
            var product = (Product)((WriteOffEditWindow)window).availProductsComboBox.SelectedItem;
            if (AddedProducts.Where (elt => elt.Product.Name == product.Name).Count () > 0) {
                throw new Exception ($"{product.Name} уже добавлен!");
            }
            AddedProducts.Add (new ProductWrapper () { Product = product, ClosedCount = ProductCount, Price = ProductPrice });
        }
        private void DeleteProduct () {
            var removableProductWrapper = (ProductWrapper)((WriteOffEditWindow)window).addedProductsListView.SelectedItem;
            AddedProducts.Remove (removableProductWrapper);
        }
    }
}
