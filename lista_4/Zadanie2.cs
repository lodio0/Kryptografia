using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using System.Numerics;
using System.Threading;

namespace Lista_4
{
    public class Zadanie2
    {
        private static RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        private List<BigInteger> przetestowane,liczbyPierwsze;
        int ThreadNumber,NumberLength,PrimeNumbers;
        int znalezniono;
        public Zadanie2(int ThreadNumber,int NumberLength,int PrimeNumbers)
        {
            this.ThreadNumber = ThreadNumber;
            this.NumberLength = NumberLength;
            this.PrimeNumbers = PrimeNumbers;
            przetestowane = new List<BigInteger>();
            liczbyPierwsze = new List<BigInteger>();
            znalezniono = 0;
        }

        public void startTh()
        {
            Thread[] threads = new Thread[ThreadNumber];

            for(int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(new ThreadStart(ThreadWork));
            }

            for(int i = 0; i < threads.Length; i++)
            {
                threads[i].Start();
            }

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }
        }
        private void ThreadWork()
        {
            while (znalezniono<PrimeNumbers)
            {
                BigInteger temp = LosujLiczbePierwsza(NumberLength);
                if (temp > 0)
                {
                    liczbyPierwsze.Add(temp);
                    znalezniono++;
                }
            }
        }
        public List<BigInteger> wynik()
        {
            return liczbyPierwsze;
        }
        public BigInteger LosujLiczbePierwsza(int n)
        {
            if (znalezniono >= PrimeNumbers) return 0;
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

    }
}
