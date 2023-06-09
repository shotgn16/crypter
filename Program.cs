using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypter
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await csvDecrypter.readCsv(@"C:\Program Files (x86)\PaymentGateway\table1.csv");
            //Console.WriteLine("Value: " + await M1(args[0], args[1], Convert.ToInt32(args[2])));
            Console.ReadKey(true);
        }
        internal static async Task<string> M1(string option, string inputString, int cryptionKey = -1, string returnValue = null)
        {
            string[] Encryption = { "encrypt", "encryption", "Encryption", "Encrypt" };
            string[] Decryption = { "decrypt", "decryption", "Encryption", "Decrypt" };
            string[] DecryptBalance = { "decryptBalance", "DecryptBalance", "balanceDecrypt", "BalanceDecrypt" };

            if (string.IsNullOrEmpty(inputString) || cryptionKey == -1)
            {
                Console.WriteLine("Error! Please enter a string to encrypt/decrypt.");
            }

            else if (!string.IsNullOrEmpty(inputString) && cryptionKey != -1)
            {
                if (Encryption.Any(option.Contains))
                {
                    Console.WriteLine("Method: Encrypt\nKey: {0}\nOriginal String: {1}\n", cryptionKey, inputString);
                    returnValue = await crypter.dbEncrypt(inputString, cryptionKey);
                }

                else if (Decryption.Any(option.Contains))
                {
                    Console.WriteLine("Method: Decrypt\nKey: {0}\nOriginal String: {1}\n", cryptionKey, inputString);
                    returnValue = await crypter.dbDecrypt(inputString, cryptionKey);
                }

                else if (DecryptBalance.Any(option.Contains))
                {
                    Console.WriteLine("Method: Decrypt Balance\nKey: {0}\nOriginal String: {1}\n", cryptionKey, inputString);
                    returnValue = Convert.ToString(await crypter.decryptBalance(inputString));
                }
            }

            return returnValue;
        }
    }
}
