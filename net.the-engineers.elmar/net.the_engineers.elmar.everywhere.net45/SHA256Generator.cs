using System;
using System.Security.Cryptography;
using System.Text;

namespace net.the_engineers.elmar.everywhere.net45
{
    public class SHA256Generator
    {
        public static string HashText(string text2Hash)
        {
            if (text2Hash == null)
                text2Hash = String.Empty;

            byte[] data = Encoding.ASCII.GetBytes(text2Hash);

            var SHA256 = new SHA256Managed();
            byte[] hashedData = SHA256.ComputeHash(data);

            string hashText = null;
            foreach (var resultByte in hashedData)
            {
                string temp = Convert.ToString(resultByte, 16);
                if (temp.Length == 1)
                    hashText += "0";

                hashText += temp;
            }

            return hashText;
        }
    }
}