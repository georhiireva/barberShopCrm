using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BarberShopCRM.model {
    public interface IXmlHolder {
        XElement Find (IXmlStorable obj, XElement xml);
        void Delete (IXmlStorable obj, bool withNotification);
        void Replace (IXmlStorable oldObj, IXmlStorable newObj);
        void Add (IXmlStorable obj);
        bool IsExist (IXmlStorable obj);
    }
}
