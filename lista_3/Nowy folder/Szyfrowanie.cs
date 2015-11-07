using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Lista_3
{
   public class Szyfrowanie
    {

        public static void Encrypt(string srcPath, string destPath, int salt, string encryptionKey)
        {
            DeriveBytes rgb = new Rfc2898DeriveBytes(encryptionKey, Encoding.Unicode.GetBytes(salt.ToString()));

            using (SymmetricAlgorithm aes = new RijndaelManaged())
            {
                aes.BlockSize = 128;
                aes.KeySize = 256;
                aes.Key = rgb.GetBytes(aes.KeySize >> 3);
                aes.IV = rgb.GetBytes(aes.BlockSize >> 3);
                aes.Mode = CipherMode.CBC;
                string key = "";
                string iv = "";
                using (FileStream fsCrypt = new FileStream(destPath, FileMode.Create))
                {
                    using (ICryptoTransform encryptor = aes.CreateEncryptor())
                    {
                        using (CryptoStream cs = new CryptoStream(fsCrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (FileStream fsIn = new FileStream(srcPath, FileMode.Open))
                            {
                                int data;
                                while ((data = fsIn.ReadByte()) != -1)
                                {
                                    cs.WriteByte((byte)data);
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void Decrypt(string srcPath, string destPath, int salt, string encryptionKey)
        {
            DeriveBytes rgb = new Rfc2898DeriveBytes(encryptionKey, Encoding.Unicode.GetBytes(salt.ToString()));

            using (SymmetricAlgorithm aes = new RijndaelManaged())
            {
                aes.BlockSize = 128;
                aes.KeySize = 256;
                aes.Key = rgb.GetBytes(aes.KeySize >> 3);
                aes.IV = rgb.GetBytes(aes.BlockSize >> 3);
                aes.Mode = CipherMode.CBC;
                Console.WriteLine("key : {0} , IV {1}\n", aes.Key.Length, aes.IV.Length);
                using (FileStream fsCrypt = new FileStream(srcPath, FileMode.Open))
                {
                    using (FileStream fsOut = new FileStream(destPath, FileMode.Create))
                    {
                        using (ICryptoTransform decryptor = aes.CreateDecryptor())
                        {
                            using (CryptoStream cs = new CryptoStream(fsCrypt, decryptor, CryptoStreamMode.Read))
                            {
                                int data;
                                while ((data = cs.ReadByte()) != -1)
                                {
                                    fsOut.WriteByte((byte)data);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
