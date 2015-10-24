using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;

namespace Kryptografia_lista_1
{
    public class TestKlucza
    {

        SzukajKlucza szukaj;
        string czescKlucza2 = "d3fb313a";
        byte[] kryptogram;
        Alfabet alfabet;
        public TestKlucza(SzukajKlucza szukaj)
        {
            File fl = new File();
            kryptogram = fl.zwrocZawartoscB("zadanie2.txt");
            this.szukaj = szukaj;
            alfabet = new Alfabet();
            byte[] ff = System.Text.Encoding.ASCII.GetBytes(czescKlucza2);

        }
        public void ThreadRun()
        {
            int i = 0;
            byte[] clearText = new byte[kryptogram.Length];
            string czescKlucza1 = "";
            while (i <= szukaj.max)
            {
                i = szukaj.wykonane();

                if (i <= szukaj.max)
                {
                    czescKlucza1 = i.ToString("X");
                    while (czescKlucza1.Length < 8)
                    {
                        czescKlucza1 = "0" + czescKlucza1;
                    }

                    String klucz = czescKlucza1.ToLower() + czescKlucza2;
                    try
                    {
                        RC4Engine rc4 = new RC4Engine();
                        KeyParameter keyParam = new KeyParameter(System.Text.Encoding.ASCII.GetBytes(klucz));
                        rc4.Init(false, keyParam);
                        rc4.ProcessBytes(kryptogram, 0, kryptogram.Length, clearText, 0);

                    }
                    catch (Exception e)
                    {

                    }
                    bool zgodne = true;
                    for (int j = 0; j < clearText.Length; j++)
                    {
                        if (!alfabet.lista_znakow.Contains((char)clearText[j]))
                        {
                            zgodne = false;
                        }
                    }
                    if (zgodne)
                    {
                        Console.Write("Wiadomosc : ");
                        foreach (byte x in clearText)
                        {
                            Console.Write("{0} ", (char)x);
                        }
                        Console.WriteLine("");
                        Console.WriteLine(" klucz: {0}",klucz);
                    }

                }
            }

        }
    }

}
