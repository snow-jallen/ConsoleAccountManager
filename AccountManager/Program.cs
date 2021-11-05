using System;
using System.Collections.Generic;
using System.IO;

namespace AccountManager
{
    public class Program
    {
        record Account(int Id, string Name, string Email);
        static List<Account> accounts;
        static void Main(string[] args)
        {
            if(args.Length > 0 && args[0] == "test")
            {
                Tests.RunTests();
            }
            accounts = GetAccounts("accounts.txt");
            DisplayAccounts();
        }

        public static string ShortenString(string text, int maxLength)
        {
            if(text.Length <= maxLength)
            {
                return text;
            }
            if(maxLength < 0)
            {
                return string.Empty;
            }
            if(maxLength < 3)
            {
                return text.Substring(0, maxLength);
            }
            var shortened = text.Substring(0, maxLength - 3);
            return shortened + "...";
            
        }

        private static void DisplayAccounts()
        {
            foreach(var account in accounts)
            {
                Console.WriteLine("{0} {1} {2}", account.Id, ShortenString(account.Name, 15), ShortenString(account.Email, 10));
            }
        }

        static List<Account> GetAccounts(string fileName)
        {
            List<Account> accounts = new List<Account> ();
            var streamReader = new StreamReader(fileName);
            while (!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine();
                var parts = line.Split('|');
                var account = new Account(Convert.ToInt32(parts[0]), parts[1], parts[2]);
                accounts.Add(account);
            }
            streamReader.Close();
            return accounts;
        }
    }
}
