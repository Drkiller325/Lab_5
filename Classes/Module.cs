using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.Classes
{
    public class Module
    {
        public decimal Amount { get; }
        public DateTime Date { get; }
        public string Notes { get; }

        public Module(decimal amount, DateTime date, string note)
        {
            Amount = amount;
            Date = date;
            Notes = note;
        }
    }

    public class InterestEarningAccount : Control
    {
        public InterestEarningAccount(string name, DateTime date, decimal initialBalance) : base(name, initialBalance)
        {

        }

        public override void PerformMonthEndTransactions()
        {
            if (Balance > 500m)
            {
                decimal interest = Balance * 0.05m;
                MakeDeposit(interest, DateTime.Now, "apply monthly interest");
            }
        }


    }

    public class LineOfCreditAccount : Control
    {
        public LineOfCreditAccount(string name, decimal initialBalance, DateTime date, decimal creditLimit) : base(name, initialBalance, -creditLimit)
        {

        }

        public override void PerformMonthEndTransactions()
        {
            if (Balance < 0)
            {
                // Negate the balance to get a positive interest charge:
                decimal interest = -Balance * 0.07m;
                MakeWithdrawal(interest, DateTime.Now, "Charge monthly interest");
            }
        }

        protected override Module? CheckWithdrawalLimit(bool isOverdrawn) =>
            isOverdrawn
            ? new Module(-20, DateTime.Now, "Apply overdraft fee")
            : default;
    }

    public class GiftCardAccount : Control
    {
        private readonly decimal _monthlyDeposit = 0m;

        public GiftCardAccount(string name, decimal initialBalance, DateTime date, decimal monthlyDeposit = 0) : base(name, initialBalance)
        {
            _monthlyDeposit = monthlyDeposit;

        }

        public override void PerformMonthEndTransactions()
        {
            if (_monthlyDeposit != 0)
            {
                MakeDeposit(_monthlyDeposit, DateTime.Now, "Add monthly deposit");
            }
        }

    }
}
