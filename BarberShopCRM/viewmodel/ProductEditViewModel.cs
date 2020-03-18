using BarberShopCRM.command;
using BarberShopCRM.exception;
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
        private Product editingProduct;
        private Product startStateProduct;
        private ICommand addProductCommand;
        public ICommand AddProductCommand => addProductCommand;

        public string Name { get => editingProduct.Name;
            set {
                if (editingProduct.Name != value) {
                    editingProduct.Name = value;
                    OnPropertyChanged (this, new System.ComponentModel.PropertyChangedEventArgs (nameof (Name)));
                }
            }
        }
        public string Note {
            get => editingProduct.Note;
            set {
                if (editingProduct.Note != value) {
                    editingProduct.Note = value;
                    OnPropertyChanged (this, new System.ComponentModel.PropertyChangedEventArgs (nameof (Note)));
                }
            }
        }
        public Unit Unit {
            get => editingProduct.Unit;
            set {
                if (editingProduct.Unit != value) {
                    editingProduct.Unit = value;
                    OnPropertyChanged (this, new System.ComponentModel.PropertyChangedEventArgs (nameof (Unit)));
                }
            }
        }
        public bool Crushable {
            get => editingProduct.Crushable;
            set {
                if (editingProduct.Crushable != value) {
                    editingProduct.Crushable = value;
                    OnPropertyChanged (this, new System.ComponentModel.PropertyChangedEventArgs (nameof (Crushable)));
                }
            }
        }
        public int MinCountInUnits {
            get => editingProduct.MinCountInUnits;
            set {
                if (editingProduct.MinCountInUnits != value) {
                    editingProduct.MinCountInUnits = value;
                    OnPropertyChanged (this, new System.ComponentModel.PropertyChangedEventArgs (nameof (MinCountInUnits)));
                }
            }
        }
        public int CountInUnits {
            get => editingProduct.CountInUnits;
            set {
                if (editingProduct.CountInUnits != value) {
                    editingProduct.CountInUnits = value;
                    OnPropertyChanged (this, new System.ComponentModel.PropertyChangedEventArgs (nameof (CountInUnits)));
                }
            }
        }
        public int UnitsInOnePieceCount {
            get => editingProduct.UnitsInOnePieceCount;
            set {
                if (editingProduct.UnitsInOnePieceCount != value) {
                    editingProduct.UnitsInOnePieceCount = value;
                    OnPropertyChanged (this, new System.ComponentModel.PropertyChangedEventArgs (nameof (UnitsInOnePieceCount)));
                }
            }
        }

        public Unit[] Units {
            get {
                return (Unit[]) Enum.GetValues (typeof (Unit));
            }
        }
        
        public ProductEditViewModel (Window window, Product product) : base (window) {
            editingProduct = product.Clone ();
            startStateProduct = product;
            addProductCommand = new Command (() => ReplaceProduct ());
        }

        public ProductEditViewModel (Window window) : base (window) {
            editingProduct = new Product ();
            addProductCommand = new Command (() => AddProduct ());
            logger.log (nameof (ProductEditViewModel), "Вызван конструкор с одним параметром. Временный продукт создан.");
        }

        private void AddProduct () {
             Query.Instance.Add (editingProduct);
            window.DialogResult = true;
        }

        private void ReplaceProduct () {
            try {
                Query.Instance.Replace (startStateProduct, editingProduct);
                window.DialogResult = true;
            } catch (DatabaseNotFoundException e) {
                MessageBox.Show (e.Message, "Ошибка");
                window.DialogResult = false;
            }
        }
    }
}
