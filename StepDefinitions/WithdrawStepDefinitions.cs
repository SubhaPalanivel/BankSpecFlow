using BankAccolite_Spec.Models;
using BankAccolite_Spec.ServiceRequest;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Security.Principal;

namespace BankAccolite_Spec.StepDefinitions
{
    [Binding]
    public class WithdrawStepDefinitions
    {
        private readonly ServiceRequestBase _serviceRequestBase = new ServiceRequestBase();
        private HttpWebResponse response;
        [Given(@"the user sends a PUT request to the ""([^""]*)"" endpoint to withdraw ""([^""]*)"" Amount from Account ""([^""]*)""")]
        public void GivenTheUserSendsAPUTRequestToTheEndpointToWithdrawAmountFromAccount(string endpoint, string depositAmount, string accName)
        {
            AccountTransactions transactions = new AccountTransactions()
            {
                AccountName = accName,
                TransactionAmount = decimal.Parse(depositAmount),
                TransactionType = TransType.Withdrawl
            };
            response = _serviceRequestBase.MakeTransaction(transactions, endpoint);
        }

        [Then(@"the response should contain balance Amount as ""([^""]*)""")]
        public void ThenTheResponseShouldContainBalanceAmountAs(string totalAmount)
        {
            Stream stream = response.GetResponseStream();
            StreamReader str = new(stream);
            string jsonAccount = str.ReadToEnd();
            Account responseAccount = JsonConvert.DeserializeObject<Account>(jsonAccount);
            Assert.AreEqual(totalAmount, responseAccount.TotalAmount, "Total Amount is not correct");
        }
    }
}
