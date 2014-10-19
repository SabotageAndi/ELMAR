using System;
using System.IO;
using System.Security.Cryptography;

namespace net.the_engineers.elmar.everywhere.net45
{
    public class MD5SumCreator
    {
        public static string Generate(Stream stream)
        {
            using (var md5 = MD5.Create())
            {
                return BitConverter.ToString(md5.ComputeHash(stream)).ToLower();
            }
        }
    }
}