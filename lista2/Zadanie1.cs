using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kryptografia_lista_1
{
    class Zadanie1
    {
        //wynik "zielinska przyszla na impreze w ... worku?"
        int ileK;
        public void deszyfroj(String kryptogram, int ileKryptogramow)
        {
            List<List<int>> klucze = new List<List<int>>();
            ileK = ileKryptogramow + 1;
            File FileReader = new File();
            Alfabet alfabet = new Alfabet();
            int[][] kryptogramy = new int[ileK][];
            kryptogramy[0] = FileReader.zwrocZawartosc("zadanie1.txt");
            int maxDlugosc = kryptogramy[0].Length;
            //przepisanie zawarości plików do tablicy int
            for (int i = 1; i < ileK; i++)
            {
                string temp = "" + i + ".txt";
                kryptogramy[i] = FileReader.zwrocZawartosc(temp);
                if (maxDlugosc < kryptogramy[i].Length) maxDlugosc = kryptogramy[i].Length;
            }
            //przepisanie możliwych kluczy 8 bitowych
            int[] mozliweKlucze = alfabet.kombinacjeKlucza();
            //szukanie kluczy do każdych 8 bitów
            for (int i = 0; i < maxDlugosc; i++)
            {
                List<int> poprawneKlucze= new List<int>();
                foreach (int klucz in mozliweKlucze)
                {
                    bool znaleziono = true;
                    for (int j = 0; j < ileK; j++)
                    {
                        if (kryptogramy[j].Length <= i) continue;
                        int znak = (kryptogramy[j][i] ^ klucz);
                        if (!alfabet.lista_znakow.Contains(znak))
                        {
                            znaleziono = false;
                            break;
                        }

                    }
                    if (znaleziono)
                    {
                        poprawneKlucze.Add(klucz);
                    }
                }
                klucze.Add(poprawneKlucze);
            }


               for(int i=0;i<kryptogramy[0].Length;i++)
               {
                   List<int> kluczeTest=klucze[i];
                   string wynik=" |";
                   foreach(int kl in kluczeTest)
                   {
                       char znak= (char)(kl^kryptogramy[0][i]);
                           wynik+=znak+" | ";
                   }
                   Console.WriteLine(wynik);
               }

        }
    }
}
