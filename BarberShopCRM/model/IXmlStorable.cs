using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BarberShopCRM.model {
    public interface IXmlStorable {
        string FilePath { get; }
        string Id { get; set; }
        IXmlStorable MapFromXml (XElement obj);
        XElement MapToXml ();
    }
}
