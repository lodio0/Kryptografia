﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kryptografia_lista_1
{
    class Program
    {
        static void Main(string[] args)
        {
  
            Zadanie1 test = new Zadanie1();
            test.deszyfroj("zadanie1.txt",20);
            Zadanie2 test2 = new Zadanie2();
            test2.wykonaj();
        }
    }
}
