using test.Classes;
using System;
using System.Runtime.CompilerServices;
using System.Linq.Expressions;
using System.Xml.Serialization;

List<Control> bankAccounts = new List<Control>();
List<Person> people = new List<Person>();
DateTime date = new DateTime(2022, 12, 6, 8, 0, 0);

Random rand = new Random();
int day = 1;
int hours = 0;

while (true)
{

    Console.WriteLine($"Day {day}");
    Console.WriteLine(date);
    date = date.AddMinutes(30);
    hours++;
    if (rand.Next(1, 5) == 1)
    {
        Person person = new Person();
        people.Add(person);
        Console.WriteLine($"NEW Customer:{person.name} has entered the bank");
        int Choice = rand.Next(1, 4);
        switch (Choice)
        {
            case 1:
                Control account1 = new Control(person.name, person.balance,1);
                Console.WriteLine($"{person.name} made a new Account");
                account1.AccountType = 1;
                bankAccounts.Add(account1);
                break;
            case 2:
                InterestEarningAccount Iaccount = new InterestEarningAccount(person.name,date, person.balance);
                Console.WriteLine($"{person.name} made an Interest account");
                Iaccount.AccountType = 2;
                bankAccounts.Add(Iaccount);
                break;
            case 3:
                int creditLimit = rand.Next(5, 10) * 1000;
                LineOfCreditAccount Credit = new LineOfCreditAccount(person.name, person.balance,date, creditLimit);
                Console.WriteLine($"{person.name} made a credit account with the limit of {creditLimit}");
                Credit.AccountType = 3; 
                bankAccounts.Add(Credit);
                break;
            case 4:
                GiftCardAccount Gaccount = new GiftCardAccount(person.name, person.balance, date);
                Console.WriteLine($"{person.name} opened a Giftcard account");
                Gaccount.AccountType = 4;
                bankAccounts.Add(Gaccount);
                break;
            default:
                Console.WriteLine("hi");
                break;
        }
    }else
    {
        try
        {
            int Index = rand.Next(bankAccounts.Count);
            Console.WriteLine($"{bankAccounts[Index].Owner} has returned to the bank");
            if (bankAccounts[Index].AccountType == 1)
            {
                int Choice = rand.Next(1,3);
                switch (Choice)
                {
                    case 1:
                        bankAccounts[Index].MakeWithdrawal(rand.Next(1, 50)*10, date, "had some Money");
                        Console.WriteLine($"{bankAccounts[Index].Owner} has made a withdraw");
                        break;
                    case 2:
                        bankAccounts[Index].MakeDeposit(rand.Next(1,10)*100, DateTime.Now, "Gas money");
                        Console.WriteLine($"{bankAccounts[Index].Owner} has made a deposit");
                        break;
                    case 3:
                        bankAccounts.Remove(bankAccounts[Index]);
                        people.Remove(people[Index]);
                        Console.WriteLine($"{bankAccounts[Index].Owner} has closed thier account");
                        break;
                    default:
                        Console.WriteLine("hi");
                        break;
                }
            }
            if (bankAccounts[Index].AccountType == 2)
            {
                int Choice = rand.Next(1, 3);
                switch (Choice)
                {
                    case 1:
                        bankAccounts[Index].MakeWithdrawal(rand.Next(1, 50) * 10, date, "Rent payment");
                        Console.WriteLine($"{bankAccounts[Index].Owner} has made a withdraw");
                        break;
                    case 2:
                        bankAccounts[Index].MakeDeposit(rand.Next(1, 10) * 100, DateTime.Now, "Friend paid me back");
                        Console.WriteLine($"{bankAccounts[Index].Owner} has made a deposit");
                        break;
                    case 3:
                        bankAccounts.Remove(bankAccounts[Index]);
                        people.Remove(people[Index]);
                        Console.WriteLine($"{bankAccounts[Index].Owner} has closed thier account");
                        break;
                    default:
                        Console.WriteLine("hi");
                        break;
                }
            }
            if (bankAccounts[Index].AccountType == 3)
            {
                int Choice = rand.Next(1, 3);
                switch (Choice)
                {
                    case 1:
                        bankAccounts[Index].MakeWithdrawal(rand.Next(1, 50) * 10, date, "Car payment");
                        Console.WriteLine($"{bankAccounts[Index].Owner} has made a withdraw");
                        break;
                    case 2:
                        bankAccounts[Index].MakeDeposit(rand.Next(1, 10) * 100, DateTime.Now, "Paycheque");
                        Console.WriteLine($"{bankAccounts[Index].Owner} has made a deposit");
                        break;
                    case 3:
                        bankAccounts.Remove(bankAccounts[Index]);
                        people.Remove(people[Index]);
                        Console.WriteLine($"{bankAccounts[Index].Owner} has closed thier account");
                        break;
                    default:
                        Console.WriteLine("hi");
                        break;
                }
            }
            if (bankAccounts[Index].AccountType == 4)
            {
                int Choice = rand.Next(1, 3);
                switch (Choice)
                {
                    case 1:
                        bankAccounts[Index].MakeWithdrawal(rand.Next(1, 50) * 10, date, "Expensive Coffee");
                        Console.WriteLine($"{bankAccounts[Index].Owner} has made a withdraw");
                        break;
                    case 2:
                        bankAccounts[Index].MakeDeposit(rand.Next(1, 10) * 100, DateTime.Now, "Borrowed Money");
                        Console.WriteLine($"{bankAccounts[Index].Owner} has made a deposit");
                        break;
                    case 3:
                        bankAccounts.Remove(bankAccounts[Index]);
                        people.Remove(people[Index]);
                        Console.WriteLine($"{bankAccounts[Index].Owner} has closed thier account");
                        break;
                    default:
                        Console.WriteLine("hi");
                        break;
                }
            }

        }
        catch
        {

        }

    }
    
    if (hours == 9)
    {
        date = date.AddHours(11);
        day++;
        hours = 0;
        Console.WriteLine();
    }
    Thread.Sleep(500);

    if(day % 7 == 0)
    {
        foreach(Control BA in bankAccounts)
        {
            Console.WriteLine(BA.GetAccountHistory());
        }
        day++;
        Thread.Sleep(10000);
        
    }
    if(day % 30 == 0)
    {
        foreach(Control BA in bankAccounts)
        {
            if(BA.AccountType == 2)
            {
                BA.PerformMonthEndTransactions();
            }
        }
    }
}



