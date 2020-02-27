using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Sha256 = System.Security.Cryptography.SHA256;

namespace TaosPerformanceAPI.Security
{
    public class SHA256
    {
        public static String RandomHashString()
        {
            Byte[] buffer = new Byte[32];
            RandomNumberGenerator gen = RandomNumberGenerator.Create();
            gen.GetBytes(buffer);
            return ToStringHash(Encode(ToStringHash(buffer)));
        }

        public static Byte[] ReverseHashString(String value)
        {
            Byte[] buffer = new Byte[32];
            if (value.Length != 64)
            {
                return null;
            }
            return Enumerable.Range(0, value.Length)
                     .Where(x => x % 2 == 0)
                     .Select(x => Convert.ToByte(value.Substring(x, 2), 16))
                     .ToArray();
        }

        public static Byte[] RandomHash()
        {
            Byte[] buffer = new Byte[32];
            RandomNumberGenerator gen = RandomNumberGenerator.Create();
            gen.GetBytes(buffer);
            return Encode(ToStringHash(buffer));
        }

        public static String EncodeString(String value)
        {
            Sha256 encoder = Sha256.Create();
            return ToStringHash(encoder.ComputeHash(Encoding.UTF8.GetBytes(value)));
        }

        public static Byte[] Encode(String value)
        {
            Sha256 encoder = Sha256.Create();
            return encoder.ComputeHash(Encoding.UTF8.GetBytes(value));
        }

        public static String ToStringHash(Byte[] value)
        {
            string hashString = string.Empty;
            foreach (byte x in value)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }
    }
}
