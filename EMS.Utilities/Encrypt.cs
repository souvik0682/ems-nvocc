using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.IO;
namespace EMS.Utilities
{

    public class JEncryption
    {
        private static int iterations = 1024;
        private static string password = "keytonePassword";
        string encryptSalt = "";
        //private static byte[] salt = Encoding.ASCII.GetBytes("KeyTone Encryption");
        /// <summary>
        /// this function will take a plaintext as an arg and
        /// returns ciphertext as an O/P
        /// </summary>
        /// <param name=”plaintext”></param>
        /// <returns></returns>
        public static string Encrypt(string plainText)
        {
            string encryptSalt = "7@m3$k4@+!@9m@!l*60m";
            byte[] salt = Encoding.ASCII.GetBytes(encryptSalt + "james");
            Rfc2898DeriveBytes KeyBytes = new Rfc2898DeriveBytes(password, salt, iterations);
            //The deafault iteration count is 1000
            RijndaelManaged alg = new RijndaelManaged();
            alg.Key = KeyBytes.GetBytes(32);
            alg.IV = KeyBytes.GetBytes(16);
            MemoryStream encryptStream = new MemoryStream();
            //Stream to write
            CryptoStream encrypt = new CryptoStream(encryptStream, alg.CreateEncryptor(), CryptoStreamMode.Write);
            //convert plain text to byte array
            byte[] data = Encoding.UTF8.GetBytes(plainText);
            encrypt.Write(data, 0, data.Length); //data to encrypt,start,stop
            encrypt.FlushFinalBlock();//Clear buffer
            encrypt.Close();
            return Convert.ToBase64String(encryptStream.ToArray());//return encrypted data
        }

        /// <summary>
        /// this function will take a ciphertext as an arg and
        /// returns plaintext as an O/P
        /// </summary>
        /// <param name=”plaintext”></param>
        /// <returns></returns>
        public static string Decrypt(string cipherText)
        {
            try
            {
                string encryptSalt = "7@m3$k4@+!@9m@!l*60m";
                //string ss = Encrypt("11/11/2011", "jameskhati@gmail.com");
                byte[] salt = Encoding.ASCII.GetBytes(encryptSalt + "james");
                Rfc2898DeriveBytes KeyBytes = new Rfc2898DeriveBytes(password, salt, iterations);
                //The deafault iteration count is 1000
                RijndaelManaged alg = new RijndaelManaged();
                alg.Key = KeyBytes.GetBytes(32);
                alg.IV = KeyBytes.GetBytes(16);
                MemoryStream decryptStream = new MemoryStream();
                //Stream to read
                CryptoStream decrypt = new CryptoStream(decryptStream, alg.CreateDecryptor(), CryptoStreamMode.Write);
                //convert  ciphertext to byte array
                byte[] data = Convert.FromBase64String(cipherText.Replace(" ","+"));//Convert.FromBase64String(cipherText); //IF using for WEB APPLICATION and getting ciphertext via Querystring change code to : Convert.FromBase64String(ciphertext.Replace(” “,”+”));
                

                decrypt.Write(data, 0, data.Length); //data to encrypt,start,stop
                decrypt.Flush();
                decrypt.Close();
                return Encoding.UTF8.GetString(decryptStream.ToArray());//return PlainText
            }
            catch { }
            return new Guid().ToString();
        }       

    }    

}
