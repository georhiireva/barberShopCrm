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
    class WriteOffViewModel : ViewModel {
        private ICommand addWriteOffCommand;
        private ICommand editWriteOffCommand;
        private ICommand deleteWriteOffCommand;
        private IEnumerable<WriteOff> writeOffList;

        public ICommand AddWriteOffCommand => addWriteOffCommand;
        public ICommand EditWriteOffCommand => editWriteOffCommand;
        public ICommand DeleteWriteOffCommand => deleteWriteOffCommand;

        public IEnumerable<WriteOff> WriteOffList {
            get => writeOffList;
            set {
                if (writeOffList != value) {
                    writeOffList = value;
                    OnPropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(WriteOffList)));
                }
            }
        }

        public WriteOff SelectedWriteOff => ((WriteOffWindow)window).SelectedWriteOff;
        public WriteOffViewModel(Window window) : base(window) {
            addWriteOffCommand = new Command(() => AddWriteOff());
            editWriteOffCommand = new Command(() => EditWriteOff());
            deleteWriteOffCommand = new Command(() => DeleteWriteOff());
            WriteOffList = Query.Instance.LoadAllWriteOffs();
        }

        private void AddWriteOff() {
            var newWindow = new WriteOffEditWindow(new WriteOff());

            if (newWindow.ShowDialog() == true) {
                WriteOffList = Query.Instance.LoadAllWriteOffs();
            }
        }

        private void EditWriteOff() {
            if (SelectedWriteOff == null) {
                MessageBox.Show(
                    "Выберите элемент",
                    "Предупреждение",
                    MessageBoxButton.OK
                );
            }
            var newWindow = new WriteOffEditWindow(SelectedWriteOff);
            if (newWindow.ShowDialog() == true) {
                WriteOffList = Query.Instance.LoadAllWriteOffs();
            }
        }

        private void DeleteWriteOff() {
            var result = MessageBox.Show(
                "Вы действительно хотите удалить списание?",
                "Предупреждение",
                MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No) {
                return;
            } else {
                Query.Instance.Delete(SelectedWriteOff);
                WriteOffList = Query.Instance.LoadAllWriteOffs();
            }
        }
    }
}
