using BankAccolite_Spec.Models;
using BankAccolite_Spec.ServiceRequest;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Security.Principal;

namespace BankAccolite_Spec.StepDefinitions
{
    [Binding]
    public class DepositStepDefinitions
    {
        private readonly ServiceRequestBase _serviceRequestBase = new ServiceRequestBase();
        private HttpWebResponse response;
        [Given(@"the user sends a PUT request to the ""([^""]*)"" endpoint to deposit ""([^""]*)"" Amount to Account ""([^""]*)""")]
        public void GivenTheUserSendsAPUTRequestToTheEndpointToDepositAmmountToAccount(string endpoint, string depositAmount, string accName)
        {
            AccountTransactions transactions = new AccountTransactions()
            {
                AccountName = accName,
                TransactionAmount = decimal.Parse(depositAmount),
                TransactionType = TransType.Deposit
            };
            response = _serviceRequestBase.MakeTransaction(transactions, endpoint);
        }
        [Then(@"the user sends GET request to the ""([^""]*)"" endpoint to get ""([^""]*)"" account details")]
        public void ThenTheUserSendsGETRequestToTheEndpointToGetAccountDetails(string accName, string endpoint)
        {
            response = _serviceRequestBase.GetAccount(accName, endpoint);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Then(@"the response should contain Total Amount as ""([^""]*)""")]
        public void ThenTheResponseShouldContainTotalAmountAs(string depositAmount)
        {
            Stream stream = response.GetResponseStream();
            StreamReader str = new(stream);
            string jsonAccount = str.ReadToEnd();
            Account responseAccount = JsonConvert.DeserializeObject<Account>(jsonAccount);
            Assert.AreEqual(depositAmount, responseAccount.TotalAmount, "Total Amount is not correct");
        }
    }
}

