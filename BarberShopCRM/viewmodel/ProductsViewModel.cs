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
        private IEnumerable<Product> products;
        
        public ICommand AddProductCommand => addProductCommand;
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
            deleteProductCommand = new Command(() => DeleteProduct());
        }

        private void AddProduct () {
            var newWindow = new ProductEditWindow ();
          
            if (newWindow.ShowDialog() == true) {
               LoadProducts ();
            }
        }

        private void LoadProducts () {
            Products = new List<Product> (Query.Instance.LoadAllProducts ());
            logger.log (nameof (LoadProducts), $"Загружено продуктов - {products.Count ()}");
        }

        private void DeleteProduct () {
            var removableProductName = SelectedProduct.Name;
            if (MessageBox.Show ($"Вы действительно хотите удалить {removableProductName}?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;
            else {
                Query.Instance.DeleteProduct(removableProductName);
                LoadProducts();
            }
        }
    }
}
