
using System;
using System.Security.Cryptography;
using System.Text;

namespace Harasoft {

    public static class Encryption {

        public static byte[] Encrypt (byte[] input, string key) {

            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            Console.WriteLine("test");
            byte[] output = cTransform.TransformFinalBlock(input, 0, input.Length);

            tripleDES.Clear();

            return output;
        }

        public static byte[] Decrypt (byte[] input, string key) {

            try {

                TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
                tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
                tripleDES.Mode = CipherMode.ECB;
                tripleDES.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tripleDES.CreateDecryptor();
                Console.WriteLine("tesst");
                byte[] output = cTransform.TransformFinalBlock(input, 0, input.Length);

                tripleDES.Clear();

                return output;

            } catch {

                throw new ArgumentException(
                    "Decryption Error; Failed to Decrypt input! It's likely the Decryption-Key wasn't a match for the data."
                );
            }
        }
    }
}
