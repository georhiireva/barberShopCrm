using BarberShopCRM.command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BarberShopCRM.viewmodel {
    public class ViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected Window window;
        protected Logger logger;

        public ViewModel (Window window) {
            this.window = window;
            logger = new Logger (this.GetType().Name);
        }

        public ICommand ExitCommand => new Command (() => window.Close ());

        protected void OnPropertyChanged (object sender, PropertyChangedEventArgs e) {
            PropertyChanged?.Invoke (sender, e);
        }
    }
}
