
using System;
using System.Security.Cryptography;
using System.Text;

namespace Harasoft {

    public static class Hashing {

        public static string Sha256Hash (string input) {

            HashAlgorithm algorithm = SHA256.Create();
            byte[] hashedData = algorithm.ComputeHash(Encoding.Unicode.GetBytes(input));

            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hashedData)
                stringBuilder.Append(b.ToString("X2"));

            return stringBuilder.ToString();
        }
    }
}
