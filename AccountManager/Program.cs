using System;
using System.Collections.Generic;
using System.IO;

namespace AccountManager
{
    public class Program
    {
        const string FileName = "accounts.txt";
        record Account(int Id, string Name, string Email);
        record Transaction(DateTime Date, Decimal Amount, string Description);
        static List<Account> accounts;
        static int Main(string[] args)
        {
            accounts = GetAccounts(FileName);
            while (true)
            {
                switch (readInt("Make a choice:\n0) Exit Program\n1) Display existing account\n2) Create new account", 0, 2))
                {
                    case 0:
                        return 0;
                    case 1:
                        DisplayAccounts();
                        var selectedAccount = readInt("select an account", 1, accounts.Count);
                        var activity = readActivity(selectedAccount);
                        displayActivity(activity);
                        break;
                    case 2:
                        createNewAccount();
                        break;
                }
            }
        }

        private static void createNewAccount()
        {
            var newId = accounts.Count + 1;
            var newName = readString("Name on the account:", minLength: 3);
            var newEmail = readString("Email address", minLength: 5, maxLength: 512);

            File.AppendAllText(FileName, $"{newId}|{newName}|{newEmail}\n");
            accounts.Add(new Account(newId, newName, newEmail));
        }

        private static string readString(string prompt, int minLength = 0, int maxLength = 26)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                var input = Console.ReadLine();
                if (input.Length >= minLength && input.Length <= maxLength)
                {
                    return input;
                }

                Console.WriteLine($"Please enter something more than {minLength} and less than {maxLength} characters");
            }
        }

        private static void displayActivity(List<Transaction> activity)
        {
            if (activity.Count == 0)
            {
                Console.WriteLine("This account has no activity.");
            }
            else
            {
                foreach (var transaction in activity)
                {
                    Console.WriteLine("{0,10:d} {1,12:c} {2}", transaction.Date, transaction.Amount, transaction.Description);
                }
            }
        }

        private static List<Transaction> readActivity(int selectedAccount)
        {
            var fileName = $"{selectedAccount}.txt";

            List<Transaction> transactions = new List<Transaction>();
            if (File.Exists(fileName))
            {
                var streamReader = new StreamReader(fileName);
                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine();
                    var parts = line.Split('|');
                    var transaction = new Transaction(Convert.ToDateTime(parts[0]), Decimal.Parse(parts[1]), parts[2]);
                    transactions.Add(transaction);
                }
                streamReader.Close();
                return transactions;
            }
            else
            {
                return new List<Transaction>();
            }
        }

        private static int readInt(string prompt, int min, int max)
        {

            while (true)
            {
                Console.WriteLine(prompt);
                try
                {
                    var input = int.Parse(Console.ReadLine());
                    if (input >= min && input <= max)
                    {
                        return input;
                    }
                    else
                    {
                        Console.WriteLine($"Enter a value between {min} and {max}.");
                    }
                }
                catch
                {
                    Console.WriteLine("That is not a valid integer.");
                    continue;
                }
            }
        }

        public static string ShortenString(string text, int maxLength)
        {
            if (text.Length <= maxLength)
            {
                return text;
            }
            if (maxLength < 0)
            {
                return string.Empty;
            }
            if (maxLength < 3)
            {
                return text.Substring(0, maxLength);
            }
            var shortened = text.Substring(0, maxLength - 3);
            return shortened + "...";

        }

        private static void DisplayAccounts()
        {
            foreach (var account in accounts)
            {
                Console.WriteLine("{0,3:000} {1,-15} {2}", account.Id, ShortenString(account.Name, 15), ShortenString(account.Email, 18));
            }
        }

        static List<Account> GetAccounts(string fileName)
        {
            List<Account> accounts = new List<Account>();
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
