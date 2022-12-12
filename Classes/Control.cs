using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.Classes
{
    public class Control : Person
    {
        public string Number { get; }
        public string Owner { get; set; }
        public int AccountType { get; set; }
        public decimal Balance
        {
            get
            {
                decimal balance = 0;
                foreach (var item in allTransactions)
                {
                    balance += item.Amount;
                }

                return balance;
            }
            set { }
            
        }
        private static int accountNumberSeed = 1234567890;
        private readonly decimal _minimumBalance;


        public Control(string name, decimal initialBalance)
        {
            this.Balance = initialBalance;
            this.Owner = name;
            MakeDeposit(initialBalance, DateTime.Now, "Initial balance");
            this.Number = accountNumberSeed.ToString();
            accountNumberSeed++;
        }



        public Control(string name, decimal initialBalance, decimal minimumBalance)
        {
            Number = accountNumberSeed.ToString();
            accountNumberSeed++;

            Owner = name;
            _minimumBalance = minimumBalance;
            if (initialBalance > 0)
                MakeDeposit(initialBalance, DateTime.Now, "Initial balance");
        }

        private List<Module> allTransactions = new List<Module>();


        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
            }
            var deposit = new Module(amount, date, note);
            allTransactions.Add(deposit);
        }

        public void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");
            }
            Module? overdraftTransaction = CheckWithdrawalLimit(Balance - amount < _minimumBalance);
            Module? withdrawal = new(-amount, date, note);
            allTransactions.Add(withdrawal);
            if (overdraftTransaction != null)
                allTransactions.Add(overdraftTransaction);
        }

        protected virtual Module? CheckWithdrawalLimit(bool isOverdrawn)
        {
            if (isOverdrawn)
            {
                throw new InvalidOperationException("Not sufficient funds for this withdrawal");
            }
            else
            {
                return default;
            }
        }

        public string GetAccountHistory()
        {
            var report = new System.Text.StringBuilder();
            Console.WriteLine($"{Owner}'s Account");
            decimal balance = 0;
            report.AppendLine("Date\t\t\tAmount\tBalance\tNote");
            foreach (var item in allTransactions)
            {
                balance += item.Amount;
                report.AppendLine($"{item.Date}\t{item.Amount}\t{balance}\t{item.Notes}");
            }

            return report.ToString();
        }

        public virtual void PerformMonthEndTransactions() { }

        
    }


    public class Person
    {
        public string name { get; set; }
        public decimal balance { get; set; }

        private Random rand = new Random();

        public Person()
        {
            name = Person.GenerateName(5);
            balance = rand.Next(1000, 5000);
        }

        public static string GenerateName(int len)
        {
            Random r = new Random();
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            string Name = "";
            Name += consonants[r.Next(consonants.Length)].ToUpper();
            Name += vowels[r.Next(vowels.Length)];
            int b = 2;
            while (b < len)
            {
                Name += consonants[r.Next(consonants.Length)];
                b++;
                Name += vowels[r.Next(vowels.Length)];
                b++;
            }

            return Name;


        }
    }
}
