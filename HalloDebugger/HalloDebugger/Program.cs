using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HalloDebugger
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** Hallo Debugger ***");

            int z = 5;
            Console.WriteLine($"{z}");
            z += 6;
            Console.WriteLine($"{z}");
            z += 6;
            Console.WriteLine($"{z}");
            z += 6;
            Console.WriteLine($"{z}");
            z += 6;
            Console.WriteLine($"{z}");

            Zähle();

            MehrDaten();

            Console.WriteLine("Ende");
            Console.ReadKey();
        }

        private static void MehrDaten()
        {
            List<Person> personen = new List<Person>();

            for (int i = 0; i < 100; i++)
            {
                personen.Add(new Person()
                { Id = i, Name = "Fred", GebDatum = DateTime.MinValue.AddDays(i) });
            }
        }

        class Person
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime GebDatum { get; set; }
        }
        private static void Zähle()
        {
            int sum = 0;
            for (int i = 0; i < 16; i++)
            {
                sum += i;
                ZeigeZahl(i);
                Console.ReadKey();
            }
        }

        private static void ZeigeZahl(int i)
        {
            Console.WriteLine($"i:{i}");
        }
    }
}
