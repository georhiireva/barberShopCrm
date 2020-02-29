using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopCRM.exception {
    public class DatabaseDuplicateException : Exception {
        public DatabaseDuplicateException (string msg) : base (msg) { }
    }
}
