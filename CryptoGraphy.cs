using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Security.Claims;
using System.Runtime.InteropServices;

namespace T8_20230629
{
    //internal class CryptoGraphy
    //{
    //    //public RSA Rsa { get; set; }
    //    //public RSAParameters Key { get; set; }
    //    //public RSA Encryptor { get; set; }
    //    //public RSA Decryptor { get; set; }

    //    public CryptoGraphy()
    //    {
    //        //Rsa = RSA.Create();
    //        //Key = Rsa.ExportParameters(true);
    //        //Encryptor = RSA.Create();
    //        //Decryptor = RSA.Create();
    //    }

    //    public byte[] EncryptionRSA(RSAParameters key, string text)
    //    {
    //        RSA encryptor = RSA.Create();
    //        encryptor.ImportParameters(key);
    //        byte[] bytes = encryptor.Encrypt(Encoding.UTF8.GetBytes(text), RSAEncryptionPadding.OaepSHA256);                     
    //        return bytes;
    //    }
    //    public string Encryption(string text)
    //    {
    //        RSA rsa = RSA.Create();
    //        RSAParameters key = rsa.ExportParameters(true);            
    //        byte[] bytes = EncryptionRSA(key, text);
    //        string str = Convert.ToHexString(bytes);
    //        return str;            
    //    }
    //    //public string Decryption(byte[] bytes)
    //    //{
    //    //    Decryptor.ImportParameters(Key);
    //    //    byte[] result = Decryptor.Decrypt(bytes, RSAEncryptionPadding.OaepSHA256);
    //    //    return Encoding.UTF8.GetString(result);
    //    //}
    //}
    internal class CryptoGraphy
    {
        public CryptoGraphy()
        {
        }

        public static string Hash(string text, string alg)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            byte[] hashBytes = null;
            // MD5
            if (alg.ToLower().Equals("md5"))
            {
                MD5 md5 = MD5.Create();
                hashBytes = md5.ComputeHash(bytes);
            }
            else if (alg.ToLower().Equals("sha512"))
            {
                SHA512 sha = SHA512.Create();
                hashBytes = sha.ComputeHash(bytes);
            }

            return Convert.ToHexString(hashBytes);
        }
    }    
}
