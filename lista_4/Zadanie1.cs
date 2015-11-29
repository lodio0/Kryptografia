using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Lista_4
{
    class Zadanie1
    {

        BigInteger p, q, n, EulerN, e, d;
        int dwKeySize;
        public Zadanie1(int keysize)
        {
            dwKeySize = keysize;
            p = LosujLiczbePierwsza(keysize);
            q = LosujLiczbePierwsza(keysize);
            while (q == p)
            {
                q = LosujLiczbePierwsza(keysize);
            }
           // Console.WriteLine("p:{0} , q:{1}", p, q);
            n = q * p;
          //  Console.WriteLine("n:{0} ", n);
            EulerN = (p - 1) * (q - 1);
         //   Console.WriteLine("EulerN : {0}", EulerN);
            while (GCD(e, EulerN) != 1)
            {
                e = LosujLiczbeZPrzedzialu(1, EulerN - 1);
          //      Console.WriteLine("e:{0} ",e);
            }
                d = modInverse(e, EulerN);
          //  Console.WriteLine("e:{0} , d:{1},e*dmod:{2}", e, d,(d*e)%EulerN);
        }

        private BigInteger GCD(BigInteger a, BigInteger b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }
        BigInteger modInverse(BigInteger a, BigInteger n)
        {
            BigInteger i = n, v = 0, d = 1;
            while (a > 0)
            {
                BigInteger t = i / a, x = a;
                a = i % x;
                i = x;
                x = d;
                d = v - t * x;
                v = x;
            }
            v %= n;
            if (v < 0) v = (v + n) % n;
            return v;
        }


        //
        private static RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        public BigInteger LosujLiczbePierwsza(int n)
        {
            BigInteger liczba = LosujLiczbeNieparzysta(n);
            return TestPierwszosci(liczba) ? liczba : LosujLiczbePierwsza(n);
        }

        public BigInteger LosujLiczbeNieparzysta(int n)
        {
            var bajty = new byte[n / 8];
            rng.GetBytes(bajty);
            return BigInteger.Pow(2, n - 1) + (BigInteger.Abs(new BigInteger(bajty)) >> 2 << 1) + 1;
        }

        private bool TestPierwszosci(BigInteger liczba)
        {
            //przetestowane.Add(liczba);
            return MillerRabin(liczba);
        }

        private bool MillerRabin(BigInteger liczba)
        {
            BigInteger s = liczba - 1;
            int t = 0;
            while ((s & 1) == 0)
            {
                s >>= 1;
                t++;
            }
            if (s == 0)
            {
                return false;
            }
            BigInteger a;

            for (int j = 0; j < 50; j++)
            {
                a = LosujLiczbeZPrzedzialu(2, liczba - 1);
                BigInteger v = BigInteger.ModPow(a, s, liczba);
                if (v == 1 || v == liczba - 1)
                {
                    continue;
                }

                int i;
                for (i = 0; i < t; i++)
                {
                    v = BigInteger.ModPow(v, 2, liczba);
                    if (v == 1)
                    {
                        return false;
                    }
                    if (v == liczba - 1)
                    {
                        break;
                    }
                }
                if (i == t)
                {
                    return false;
                }
            }
            //Console.WriteLine("przetestowano różnych liczb: " + przetestowane.Count);
            return true;
        }

        private BigInteger LosujLiczbeZPrzedzialu(BigInteger wartoscMin, BigInteger wartoscMax)
        {
            var dlugosc = wartoscMax.ToByteArray().Length;
            var bity = new byte[dlugosc];
            BigInteger liczba;
            do
            {
                rng.GetBytes(bity);
                liczba = BigInteger.Abs(new BigInteger(bity));
            }
            while (liczba < wartoscMin || liczba >= wartoscMax);
            return liczba;
        }
        //
        private BigInteger szyfrujRSA(BigInteger text)
        {
            return BigInteger.ModPow(text, e, n);
        }
        public string EncryptString(string inputString)
        {
            byte[] InputByte = System.Text.Encoding.ASCII.GetBytes(inputString);
            char[] charImputTemp = inputString.ToCharArray();
            int liczbaZnakow = n.ToByteArray().Length;
            byte[] pomoc = new byte[liczbaZnakow];
            byte[] wynik;
            int j = 0;
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < InputByte.Length; i++)
            {
                if (j < liczbaZnakow)
                {
                    pomoc[j] = InputByte[i];
                    j++;
                }
                else
                {
                    j = 0;
                    i--;
                    wynik = System.Text.Encoding.ASCII.GetBytes(System.Text.Encoding.ASCII.GetString((szyfrujRSA(new BigInteger(pomoc))).ToByteArray()) + ' ');
                    stringBuilder.Append(System.Text.Encoding.ASCII.GetString(wynik));
                    if (InputByte.Length - i > liczbaZnakow)
                    {
                        pomoc = new byte[liczbaZnakow];
                    }
                    else
                    {
                        pomoc = new byte[InputByte.Length % liczbaZnakow];
                    }
                }
            }
            wynik = System.Text.Encoding.ASCII.GetBytes(System.Text.Encoding.ASCII.GetString((szyfrujRSA(new BigInteger(pomoc))).ToByteArray()));
            stringBuilder.Append(System.Text.Encoding.ASCII.GetString(wynik));
            return stringBuilder.ToString();

        }

        public string DecryptString(string inputString)
        {
            StringBuilder stringBuilder = new StringBuilder();
            byte[] zPliku = System.Text.Encoding.ASCII.GetBytes(inputString);
        String [] odczytane = zPliku.ToString().Split(' ');

        foreach(String o in odczytane){
            BigInteger wynik= deszyfrujRSA(new BigInteger(Encoding.ASCII.GetBytes(o)));
            byte[] temp = wynik.ToByteArray();
            stringBuilder.Append(System.Text.Encoding.ASCII.GetString(wynik.ToByteArray()));
        }

            return stringBuilder.ToString();

        }

        private BigInteger deszyfrujRSA(BigInteger bigInteger)
        {
            return BigInteger.ModPow(bigInteger, d, n);
        }
    }
}
