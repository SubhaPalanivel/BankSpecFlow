namespace BankAccolite_Spec.Models
{
    public class AllAccounts
    {
        List<Account> Accounts { get; set; }
    }
    public class Account
    {
        Random rand = new();
        public Account()
        {
            AccountId = rand.Next(0, 1000000).ToString("000000");
        }
        public string AccountId { get; set; }
        public string AccountName { get; set; }
        public decimal TotalAmount { get; set; }
    }
}