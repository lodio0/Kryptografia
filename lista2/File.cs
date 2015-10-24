using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kryptografia_lista_1
{
    public class File
    {

        public int[] zwrocZawartosc(String fn)
        {
            String line = "";
            try
            {
                using (StreamReader sr = new StreamReader(fn))
                {
                    
                    line = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return ZwrocTablice(line);
        }
        public String zwrocZawartoscString(String fn)
        {
            String line = "";
            try
            {
                using (StreamReader sr = new StreamReader(fn))
                {
                    line = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return line;
        }
        public byte[] zwrocZawartoscB(String fn)
        {
            String line = "";
            try
            {
                using (StreamReader sr = new StreamReader(fn))
                {
                    line = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return bytew(line);
        }

        private byte[] bytew(string kryptogram)
        {
            byte[] wynik;
            String[] litery = kryptogram.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);
            wynik = new byte[litery.Length];
            for (int i = 0; i < litery.Length; i++)
            {
                wynik[i] = (byte)Convert.ToInt32(litery[i], 2);
            }
            return wynik;
        }
        private int[] ZwrocTablice(string kryptogram)
        {
            int[] wynik;
            String[] litery = kryptogram.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);
            wynik = new int[litery.Length];
            for (int i = 0; i < litery.Length; i++)
            {
                wynik[i] = Convert.ToInt32(litery[i], 2);
            }

            return wynik;
        }
    }
}
