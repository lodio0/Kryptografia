using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace Kryptografia_lista_1
{
   public class SzukajKlucza
    {

        public Int64 max ;
        int ileZrobiono;
        bool readerFlag = true;
        public SzukajKlucza()
        {
            max = (long)Math.Pow(16, 8);
            ileZrobiono = 400000000;
        }
        public int wykonane()
        {
            int temp;
            lock (this)
            {
                
                if(!readerFlag)
                {
                    try
                    {
                        Monitor.Wait(this);
                    }
                    catch (SynchronizationLockException e)
                    {
                        Console.WriteLine(e);
                    }
                    catch (ThreadInterruptedException e)
                    {
                        Console.WriteLine(e);
                    }
                }
                readerFlag = false;  
                temp=ileZrobiono;
                ileZrobiono++;
                readerFlag = true;  

            }
            return temp;
        }

    }
   
}
