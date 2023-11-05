namespace BankAccolite_Spec.Models
{
    public class AccountTransactions
    {
        public string AccountName { get; set; }
        public decimal TransactionAmount { get; set; }
        public TransType TransactionType { get; set; }
    }
    public enum TransType
    {
        Deposit,
        Withdrawl
    }
}