using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopCRM.viewmodel {
    public class ViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected void OnPropertyChanged (object sender, PropertyChangedEventArgs e) {
            PropertyChanged?.Invoke (sender, e);
        }
    }
}
