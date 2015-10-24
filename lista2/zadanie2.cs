using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kryptografia_lista_1
{
    //wiadomosc : Apple Took Down An Ad Blocker For Apps Over Privacy Concerns
    //klucz: 
    class Zadanie2
    {
        public void wykonaj()
        {
            SzukajKlucza szukaj = new SzukajKlucza();
            TestKlucza testK1 = new TestKlucza(szukaj);
            TestKlucza testK2 = new TestKlucza(szukaj);
            TestKlucza testK3 = new TestKlucza(szukaj);
            TestKlucza testK4 = new TestKlucza(szukaj);
            TestKlucza testK5 = new TestKlucza(szukaj);
            TestKlucza testK6 = new TestKlucza(szukaj);
            TestKlucza testK7 = new TestKlucza(szukaj);
            TestKlucza testK8 = new TestKlucza(szukaj);
            TestKlucza testK9 = new TestKlucza(szukaj);
            TestKlucza testK10 = new TestKlucza(szukaj);
            TestKlucza testK11 = new TestKlucza(szukaj);

            Thread t1 = new Thread(new ThreadStart(testK1.ThreadRun));
            Thread t2 = new Thread(new ThreadStart(testK2.ThreadRun));
            Thread t3 = new Thread(new ThreadStart(testK3.ThreadRun));
            Thread t4 = new Thread(new ThreadStart(testK4.ThreadRun));
            Thread t5 = new Thread(new ThreadStart(testK5.ThreadRun));
            Thread t6 = new Thread(new ThreadStart(testK6.ThreadRun));
            Thread t7 = new Thread(new ThreadStart(testK7.ThreadRun));
            Thread t8 = new Thread(new ThreadStart(testK8.ThreadRun));
            Thread t9 = new Thread(new ThreadStart(testK9.ThreadRun));
            Thread t10 = new Thread(new ThreadStart(testK10.ThreadRun));
            Thread t11= new Thread(new ThreadStart(testK11.ThreadRun));

            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();
            t5.Start();
            t6.Start();
            t7.Start();
            t8.Start();
            t9.Start();
            t10.Start();
            t11.Start();

            t1.Join();
            t2.Join();
            t3.Join();
            t4.Join();
            t5.Join();
            t6.Join();
            t7.Join();
            t8.Join();
            t9.Join();
            t10.Join();
            t11.Join();

        }
    }

}
