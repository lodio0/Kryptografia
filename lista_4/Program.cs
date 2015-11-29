using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using System.Numerics;
using System.Threading;
using System.Diagnostics;
using System.Collections;


namespace Lista_4
{
    class Program
    {
        static void Main(string[] args)
        {/*
            Stopwatch stopwatch = Stopwatch.StartNew();
            Zadanie2 test = new Zadanie2(8, 1024,2);
           // Console.WriteLine(test.LosujLiczbePierwsza(1024));
            test.startTh();
            List<BigInteger> k= test.wynik();
            //Console.WriteLine(zadanie2.LosujLiczbePierwsza(2048));
            //Console.WriteLine(zadanie2.LosujLiczbePierwsza(3072));
            //Console.WriteLine(zadanie2.LosujLiczbePierwsza(7680));
            //Console.WriteLine(zadanie2.LosujLiczbePierwsza(10));
            //new Alternatywa().start(1024);
            stopwatch.Stop();
            foreach(BigInteger i in k)
            {
                Console.WriteLine("{0}", i);
                Console.WriteLine("--------------------------------------------");
            }
            Console.WriteLine("Czas:~" + stopwatch.ElapsedMilliseconds / 1000);
            Console.ReadLine();
            */
            Zadanie1 tzadanie1 = new Zadanie1(344);
            string tt = "dwa";
            byte[] ss = System.Text.Encoding.UTF8.GetBytes(tt);
            var bits = new BitArray(ss);

          //  Console.WriteLine("ss:{0} , {1}", ss[0], bits.Length);
            string tekst = tzadanie1.EncryptString("test");
            Console.WriteLine("{0}", tekst);
            Console.WriteLine("{0}", tzadanie1.DecryptString(tekst));
        }
    }
}
