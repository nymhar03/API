using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Project.WebApi.Utility
{
    public class KeyManager
    {
        //Function to create DES
        //This function will create TripleDES instance.  
        //This is taking a string as key value and will calculate MD5 hash on input parameter string.  
        //This hash value would be used  as real key for the encryption. 
        static TripleDES CreateDES(string key)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            TripleDES des = new TripleDESCryptoServiceProvider();
            des.Key = md5.ComputeHash(Encoding.Unicode.GetBytes(key));
            des.IV = new byte[des.BlockSize / 8];
            return des;
        }
        //Function to Encrypt  
        //This function is taking Plain text to encrypt and key
        //This function is returning a byte array As parameter for CreateDES , I am passing the key.
        private static byte[] ByteText(string PlainText, string key)
        {
            TripleDES des = CreateDES(key);
            ICryptoTransform ct = des.CreateEncryptor();
            byte[] input = Encoding.Unicode.GetBytes(PlainText);
            return ct.TransformFinalBlock(input, 0, input.Length);
        }

        public static string Encryption(string PlainText, string key)
        {
            try
            {
                byte[] buffer = ByteText(PlainText, key);
                string b = Convert.ToBase64String(buffer);
                return b;
            }
            catch
            {
                byte[] buffer = ByteText(PlainText, key);
                string b = Convert.ToBase64String(buffer);
                return b;
            }
        }
        //Function to Decrypt 
        //This function is taking key and CypherText to encrypt.
        //It is returning a string.
        //It is creating TripleDES on given key.
        public static string Decryption(string CypherText, string key)
        {
            try
            {
                byte[] b = Convert.FromBase64String(CypherText);
                TripleDES des = CreateDES(key);
                ICryptoTransform ct = des.CreateDecryptor();
                byte[] output = ct.TransformFinalBlock(b, 0, b.Length);
                return Encoding.Unicode.GetString(output);
            }
            catch
            {
                byte[] b = Convert.FromBase64String(CypherText);
                TripleDES des = CreateDES(key);
                ICryptoTransform ct = des.CreateDecryptor();
                byte[] output = ct.TransformFinalBlock(b, 0, b.Length);
                return Encoding.Unicode.GetString(output);
            }
        }
    }
}