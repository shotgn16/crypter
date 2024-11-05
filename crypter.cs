using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace crypter
{
    internal class crypter
    {
        public static async Task<char[]> initKeys(int KeyNum)
        {
            string[] returnValue = new string[6];

            returnValue[0] = "ENCRYPTION_STRING_1"; //MyQ UserID
            returnValue[1] = "ENCRYPTION_STRING_2"; //ParentPay UserID
            returnValue[2] = "ENCRYPTION_STRING_3"; //MyQ Access Token
            returnValue[3] = "ENCRYPTION_STRING_4"; //ParentPay PaymentID
            returnValue[4] = "ENCRYPTION_STRING_5"; //newBalance
            returnValue[5] = "ENCRYPTION_STRING_6"; //Database Password (for export)

            return Task.FromResult(returnValue[KeyNum].ToCharArray()).Result;
        }

        private static TripleDESCryptoServiceProvider GetCryproProvider(int keyNum)
        {
            var md5 = new MD5CryptoServiceProvider();
            var key = md5.ComputeHash(Encoding.UTF8.GetBytes(initKeys(keyNum).Result));
            return new TripleDESCryptoServiceProvider() { Key = key, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 };
        }

        public static async Task<string> dbEncrypt(string plainString, int keyNum)
        {
            var data = Encoding.UTF8.GetBytes(plainString);
            var tripleDes = GetCryproProvider(keyNum);
            var transform = tripleDes.CreateEncryptor();
            var resultsByteArray = transform.TransformFinalBlock(data, 0, data.Length);
            return Convert.ToBase64String(resultsByteArray);
        }

        public static async Task<string> dbDecrypt(string encryptedString, int keyNum)
        {
            var data = Convert.FromBase64String(encryptedString);
            var tripleDes = GetCryproProvider(keyNum);
            var transform = tripleDes.CreateDecryptor();
            var resultsByteArray = transform.TransformFinalBlock(data, 0, data.Length);
            return Encoding.UTF8.GetString(resultsByteArray);
        }
        public static async Task<decimal> decryptBalance(string enryptedBalance)
        {
            string tmp = await crypter.dbDecrypt(enryptedBalance.ToString(), 4);
            decimal balance = Convert.ToDecimal(tmp);

            return balance;
        }
    }
}
