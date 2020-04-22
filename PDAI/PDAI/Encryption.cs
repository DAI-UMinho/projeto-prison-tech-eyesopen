using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDAI
{
    static class Encryption
    {


        public static byte[] AccountAccessEncryption(string value)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(value);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            return data;
        }


        public static byte[] AccessLevelEncryption(string value)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(value);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            return data;
        }


        public static bool CheckAccountAccess(byte[] accountAccess, byte[] givenData)
        {
            bool val = false;
            if ((System.Text.Encoding.ASCII.GetString(accountAccess)).Equals(System.Text.Encoding.ASCII.GetString(givenData))) val = true;
            return val;
        }


        public static string GetString(byte[] data)
        {
            return System.Text.Encoding.ASCII.GetString(data);
        }

    }
}
