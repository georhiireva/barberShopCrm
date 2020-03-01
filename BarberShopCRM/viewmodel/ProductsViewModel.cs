using BarberShopCRM.command;
using BarberShopCRM.exception;
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
    class ProductsViewModel : ViewModel {
        private Logger logger;
        private ICommand addProductCommand;
        private ICommand editProductCommand;
        private ICommand deleteProductCommand;
        private IEnumerable<Product> products;
        
        public ICommand AddProductCommand => addProductCommand;
        public ICommand EditProductCommand => editProductCommand;
        public ICommand DeleteProductCommand => deleteProductCommand;

        public IEnumerable<Product> Products {
            get => products;
            set {
                if (products != value) {
                    products = value;
                    OnPropertyChanged (this, new System.ComponentModel.PropertyChangedEventArgs (nameof (Products)));
                }

            }
        }

        private Product SelectedProduct => ((ProductsWindow)window).SelectedProduct;

        public ProductsViewModel(Window window) : base(window)
        {
            logger = new Logger(this.GetType().Name);
            LoadProducts();
            addProductCommand = new Command(() => AddProduct());
            editProductCommand = new Command (() => EditProduct());
            deleteProductCommand = new Command(() => DeleteProduct());
        }

        private void AddProduct () {
            var newWindow = new ProductEditWindow ();
          
            if (newWindow.ShowDialog() == true) {
               LoadProducts ();
            }
        }

        public void EditProduct () {
            var newWindow = new ProductEditWindow (SelectedProduct);

            if (newWindow.ShowDialog () == true) {
                LoadProducts ();
            }
        }
        private void DeleteProduct () {
            var removableProductName = SelectedProduct.Name;
            if (MessageBox.Show ($"Вы действительно хотите удалить {removableProductName}?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;
            else {
                try {
                    Query.Instance.DeleteProduct (removableProductName);
                } catch (DatabaseNotFoundException e) {
                    MessageBox.Show (e.Message, "Ошибка");
                }
                
                LoadProducts();
            }
        }

        private void LoadProducts () {
            Products = new List<Product> (Query.Instance.LoadAllProducts ()).OrderBy (elt => elt.Name);
            logger.log (nameof (LoadProducts), $"Загружено продуктов - {products.Count ()}");
        }
    }
}
