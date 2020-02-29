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
    class ProductEditViewModel : ViewModel {
        private Product tempProduct;
        private ICommand addProductCommand;
        public ICommand AddProductCommand => addProductCommand;

        public string Name { get => tempProduct.Name;
            set {
                if (tempProduct.Name != value) {
                    tempProduct.Name = value;
                    OnPropertyChanged (this, new System.ComponentModel.PropertyChangedEventArgs (nameof (Name)));
                }
            }
        }
        public string Note {
            get => tempProduct.Note;
            set {
                if (tempProduct.Note != value) {
                    tempProduct.Note = value;
                    OnPropertyChanged (this, new System.ComponentModel.PropertyChangedEventArgs (nameof (Note)));
                }
            }
        }
        public Unit Unit {
            get => tempProduct.Unit;
            set {
                if (tempProduct.Unit != value) {
                    tempProduct.Unit = value;
                    OnPropertyChanged (this, new System.ComponentModel.PropertyChangedEventArgs (nameof (Unit)));
                }
            }
        }
        public bool Crushable {
            get => tempProduct.Crushable;
            set {
                if (tempProduct.Crushable != value) {
                    tempProduct.Crushable = value;
                    OnPropertyChanged (this, new System.ComponentModel.PropertyChangedEventArgs (nameof (Crushable)));
                }
            }
        }
        public int MinCountInUnits {
            get => tempProduct.MinCountInUnits;
            set {
                if (tempProduct.MinCountInUnits != value) {
                    tempProduct.MinCountInUnits = value;
                    OnPropertyChanged (this, new System.ComponentModel.PropertyChangedEventArgs (nameof (MinCountInUnits)));
                }
            }
        }
        public int CountInUnits {
            get => tempProduct.MinCountInUnits;
            set {
                if (tempProduct.CountInUnits != value) {
                    tempProduct.CountInUnits = value;
                    OnPropertyChanged (this, new System.ComponentModel.PropertyChangedEventArgs (nameof (CountInUnits)));
                }
            }
        }
        public int UnitsInOnePieceCount {
            get => tempProduct.MinCountInUnits;
            set {
                if (tempProduct.UnitsInOnePieceCount != value) {
                    tempProduct.UnitsInOnePieceCount = value;
                    OnPropertyChanged (this, new System.ComponentModel.PropertyChangedEventArgs (nameof (UnitsInOnePieceCount)));
                }
            }
        }

        
        public ProductEditViewModel (Window window, Product product) : base (window) {
            tempProduct = product;
        }

        public ProductEditViewModel (Window window) : base (window) {
            tempProduct = new Product ();
            addProductCommand = new Command (() => AddProduct ());
            logger.log (nameof (ProductEditViewModel), "Вызван конструкор с одним параметром. Временный продукт создан.");
        }

        private void AddProduct () {
            Query.Instance.SaveProduct (tempProduct);
            window.DialogResult = true;
        }
    }
}
