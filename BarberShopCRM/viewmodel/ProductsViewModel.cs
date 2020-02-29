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
    class ProductsViewModel : ViewModel {
        private Logger logger;
        private ICommand deleteProductCommand;
        private ICommand addProductCommand;
        public ICommand DeleteProductCommand => deleteProductCommand;
        public ICommand AddProductCommand => addProductCommand;

        private IEnumerable<Product> products;
        public IEnumerable<Product> Products {
            get => products;
            set {
                if (products != value) {
                    products = value;
                    OnPropertyChanged (this, new System.ComponentModel.PropertyChangedEventArgs (nameof (Products)));
                }

            }
        }
        public ProductsViewModel (Window window) : base (window) {
            logger = new Logger (this.GetType ().Name);
            loadProducts ();
            addProductCommand = new Command (() => AddProduct ());
        }

        private void AddProduct () {
            var newWindow = new ProductEditWindow ();
          
            if (newWindow.ShowDialog() == true) {
               loadProducts ();
            }
        }

        private void loadProducts () {
            Products = new List<Product> (Query.Instance.LoadAllProducts ());
            logger.log (nameof (loadProducts), $"Загружено продуктов - {products.Count ()}");
        }

        private void deleteProduct () {
            if (MessageBox.Show ("Внимание", "Вы действительно хотите удалить продукт?", MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;
            else {

            }
        }
    }
}
