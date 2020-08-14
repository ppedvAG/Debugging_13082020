using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TDDBank
{
    public class Bank
    {
        public string Name { get; set; }

        public HashSet<Customer> Customers { get; set; } = new HashSet<Customer>();


        public bool IsOpen()
        {
 
            return DateTime.Now.DayOfWeek == DayOfWeek.Thursday &&
                   DateTime.Now.Hour >= 10 &&
                   DateTime.Now.Hour <= 11;
        }
        

        
        public void SaveAll()
        {
            using (var sw = new StreamWriter("customers.xml"))
            {
                var serial = new XmlSerializer(typeof(HashSet<Customer>));
                serial.Serialize(sw, Customers);
            }

        }

        public void LoadAll()
        {
            using (var sr = new StreamReader("customers.xml"))
            {
                var serial = new XmlSerializer(typeof(HashSet<Customer>));
                Customers = (HashSet<Customer>)serial.Deserialize(sr);
            }
        }

    }
}
