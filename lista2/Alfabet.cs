using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Kryptografia_lista_1
{
    class Alfabet
    {
        public List<int> lista_znakow;

        public Alfabet()
        {
            lista_znakow=new List<int>();

            lista_znakow.Add((int)' ');
            lista_znakow.Add((int)'!');
            lista_znakow.Add((int)'.');
            lista_znakow.Add((int)',');
            lista_znakow.Add((int)'?');
            lista_znakow.Add((int)':');
            lista_znakow.Add((int)'-');
            lista_znakow.Add((int)';');
            lista_znakow.Add((int)'(');
            lista_znakow.Add((int)')');
            lista_znakow.Add((int)'"');
            lista_znakow.Add((int)';');

            //0-9
            for (int i = 48; i <= 57; i++)
                lista_znakow.Add(i);

            for (int i = 65; i <= 90; i++)
                lista_znakow.Add(i);

            for (int i = 97; i <= 122; i++)
                lista_znakow.Add(i);
        }

        public int[] kombinacjeKlucza()
        {
            int[] wynik = new int[256];
            for(int i=0;i<256;i++)
            {
                wynik[i] = i;
            }
            return wynik;
        }
    }
}
