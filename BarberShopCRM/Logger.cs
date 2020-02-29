using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopCRM {
    public class Logger {
        private string className;

        public Logger (string className) {
            this.className = className;
        }

        public void log (string methodName, string message) {
            Console.WriteLine ($"Класс: {className} \nМетод: {methodName} \nСообщение: {message}");
        }

        public void log (string message) {
            Console.WriteLine ($"Класс: {className} \nСообщение: {message}");
        }
    }
}
