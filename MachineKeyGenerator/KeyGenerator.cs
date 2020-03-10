using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MachineKeyGenerator
{
    public static class KeyGenerator
    {
        
        public static string CreateKey(int numBytes)
        {
            //hacky way to do this but UTF-16 characters are 2 bytes.
            numBytes = numBytes / 2;
            var RNG = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[numBytes];

            RNG.GetBytes(buffer);
            return BytesToHexString(buffer);
        }

        public static string BytesToHexString(byte[] bytes)
        {
            var HexString = new StringBuilder(bytes.Length);
            for (int i = 0; i < bytes.Length; i++)
            {
                HexString.Append(String.Format("{0:X2}", bytes[i]));
            }
            return HexString.ToString();
        }
    }
}
