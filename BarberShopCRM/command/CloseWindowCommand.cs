using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BarberShopCRM.command {
    class CloseWindowCommand : ICommand {
        public event EventHandler CanExecuteChanged;
        private MainWindow window;

        public CloseWindowCommand (MainWindow window) => this.window = window;

        public bool CanExecute (object parameter) {
            return true;
        }

        public void Execute (object parameter) {
            window.Close ();
        }
    }
}
