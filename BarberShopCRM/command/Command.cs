using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BarberShopCRM.command {
    public class Command : ICommand {
        public event EventHandler CanExecuteChanged;
        private Action execute;

        public Command (Action execute) => this.execute = execute;
        
        public bool CanExecute (object parameter) {
            return true;
        }

        virtual public void Execute (object parameter) {
            try {
                execute ();
            } catch (Exception e) {
                MessageBox.Show (e.Message,"Ошибка");
            }
            
        }
    }
}
