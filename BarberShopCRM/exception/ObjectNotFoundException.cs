using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopCRM.exception
{
    public class ObjectNotFoundException : Exception
    {
        public ObjectNotFoundException(string msg) : base(msg) { }
    }
}
