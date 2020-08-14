using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HalloTPL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** Hallo TPL ***");

            //Parallel.Invoke(Zähle, Zähle, Zähle, Zähle);
            //Parallel.For(0, 100000, i => Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: {i}"));
            //var texte = new List<string>();
            //var result = texte.Where(x => x.StartsWith("b")).AsParallel();

            var t1 = new Task(() =>
            {
                Console.WriteLine("T1 gestartet");
                Thread.Sleep(800);
                throw new ExecutionEngineException();
                Console.WriteLine("T1 fertig");
            });

            var t2 = new Task<long>(() =>
            {

                Console.WriteLine("T2 gestartet");
                Thread.Sleep(1200);
                Console.WriteLine("T2 fertig");
                return 3456778374578;
            });

            t1.Start();
            t2.Start();

            t1.ContinueWith(t => { Console.WriteLine("T1 Continue"); }); //immer aufgerufen
            t1.ContinueWith(t => { Console.WriteLine("T1 Continue ALLES OK"); }, TaskContinuationOptions.OnlyOnRanToCompletion); //Wenn keine Exception flog
            t1.ContinueWith(t => { Console.WriteLine($"T1 Continue ERROR: {t.Exception.InnerException.Message}"); }, TaskContinuationOptions.OnlyOnFaulted); //Wenn keine Exception flog


            t2.ContinueWith(t => { Console.WriteLine($"T2 Continue [{t.Result}]"); }); //immer aufgerufen


            //            t2.Wait();
            //Console.WriteLine($"T2 Result:{t2.Result}");


            Console.WriteLine("Ende");
            Console.ReadLine();
        }

        private static void Zähle()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: {i}");
            }
        }
    }
}
