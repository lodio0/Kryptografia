using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lista_3
{
    class Program
    {
        static void Main(string[] args)
        {
            string password = "";
            string pin = "";
            string keystorePath = "keystore.txt";
            if (File.Exists(keystorePath))
            {
                
                Console.Write("Podaj Pin: ");

                ConsoleKeyInfo info = Console.ReadKey(true);
                while (info.Key != ConsoleKey.Enter)
                {
                    if (info.Key != ConsoleKey.Backspace)
                    {
                        pin += info.KeyChar;
                        info = Console.ReadKey(true);
                    }
                    else if (info.Key == ConsoleKey.Backspace)
                    {
                        if (!string.IsNullOrEmpty(pin))
                        {
                            pin = pin.Substring
                            (0, pin.Length - 1);
                        }
                        info = Console.ReadKey(true);
                    }
                }
                for (int i = 0; i < pin.Length; i++)
                    Console.Write("*");
                Console.WriteLine("");
                String line = "";
                try
                {
                    using (StreamReader sr = new StreamReader(keystorePath))
                    {
                        // Read the stream to a string, and write the string to the console.
                        line = sr.ReadToEnd();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                }
                String[] litery = line.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);
                if (litery[0] == pin)
                {
                     Console.WriteLine("Wybierz encrypt \\ decrypt");
                    string opcja =Console.ReadLine();
                    switch(opcja)
                    {
                        case "encrypt": Szyfrowanie.Encrypt("testa.mp3", "testa2.mp3", 12345678, litery[1]);
                            break;
                        case "decrypt": Szyfrowanie.Decrypt("testa2.mp3", "testa3.mp3", 12345678, litery[1]);
                            break;
                        default: Console.WriteLine("Należy wpisać encrypt lub decrypt");
                            break;

                    }
                }
                else Console.WriteLine("Podałeś zły pin");

            }
            else
            {
                Console.WriteLine("Tworzenie keystora: ");
                Console.WriteLine("Podaj Pin: ");

                ConsoleKeyInfo info = Console.ReadKey(true);
                while (info.Key != ConsoleKey.Enter)
                {
                    if (info.Key != ConsoleKey.Backspace)
                    {
                        pin += info.KeyChar;
                        info = Console.ReadKey(true);
                    }
                    else if (info.Key == ConsoleKey.Backspace)
                    {
                        if (!string.IsNullOrEmpty(pin))
                        {
                            pin = pin.Substring
                            (0, pin.Length - 1);
                        }
                        info = Console.ReadKey(true);
                    }
                }
                for (int i = 0; i < pin.Length; i++)
                    Console.Write("*");

                Console.WriteLine("Podaj haslo: ");

                info = Console.ReadKey(true);
                while (info.Key != ConsoleKey.Enter)
                {
                    if (info.Key != ConsoleKey.Backspace)
                    {
                        password += info.KeyChar;
                        info = Console.ReadKey(true);
                    }
                    else if (info.Key == ConsoleKey.Backspace)
                    {
                        if (!string.IsNullOrEmpty(password))
                        {
                            password = password.Substring
                            (0, password.Length - 1);
                        }
                        info = Console.ReadKey(true);
                    }
                }
                for (int i = 0; i < password.Length; i++)
                    Console.Write("*");
                string keystore=pin + " " + password;
                System.IO.FileStream fs = System.IO.File.Create(keystorePath);
                fs.Close();
                System.IO.File.WriteAllText(keystorePath, keystore);
            }
            
        }
    }
}
