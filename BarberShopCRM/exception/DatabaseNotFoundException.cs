using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopCRM.exception {
    class DatabaseNotFoundException : Exception {
        public DatabaseNotFoundException (string msg) : base (msg) { }
    }
}
