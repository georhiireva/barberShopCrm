using BarberShopCRM.exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BarberShopCRM.model {
    abstract public class AbstractXmlHolder : IXmlHolder {

        public void Add (IXmlStorable obj) {
            if (IsExist (obj))
                throw new DatabaseDuplicateException ("Объект уже существует в базе данных!");
            var xml = XElement.Load (obj.FilePath);
            obj.Id = Guid.NewGuid ().ToString ();
            var xmlObject = obj.MapToXml ();
            xml.Add (xmlObject);
            xml.Save (obj.FilePath);
        }

        public void Delete (IXmlStorable obj) {
            if (!IsExist (obj)) 
                throw new DatabaseNotFoundException ($"Объект ({obj.GetType().Name} Id: {obj.Id}) не найден в базе данных");

            var xml = XElement.Load (obj.FilePath);
            var xmlObject = Find (obj, xml);
            xmlObject.Remove ();
            xml.Save (obj.FilePath);
        }

        public void Replace (IXmlStorable oldObj, IXmlStorable newObj) {
            if (!IsExist (oldObj))
                throw new DatabaseNotFoundException ($"Объект ({oldObj.GetType ().Name} Id: {oldObj.Id}) не найден в базе данных");

            var xml = XElement.Load (oldObj.FilePath);
            var oldXmlObject = Find (oldObj, xml);
            oldXmlObject.Remove ();
            Add (newObj);
        }

        public bool IsExist (IXmlStorable obj) {
            var xml = XElement.Load (obj.FilePath);
            var duplicate = xml.Descendants ("Id").Where (elt => (string)elt == obj.Id);
            return duplicate != null && duplicate.Count () > 0;
        }

        public XElement Find (IXmlStorable obj, XElement xml) =>
             xml.Elements (obj.GetType ().Name).Where (elt => ((string)elt.Element ("Id")) == obj.Id)?.First ();

    }
}
