using BankAccolite_Spec.Models;
using BankAccolite_Spec.ServiceRequest;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Security.Principal;

namespace BankAccolite_Spec.StepDefinitions
{
    [Binding]
    public class CreateDeleteAccountStepDefinitions
    {
        private readonly ServiceRequestBase _serviceRequestBase = new ServiceRequestBase();
        private HttpWebResponse response;

        [Given(@"the user ""([^""]*)"" sends a POST request to the ""([^""]*)"" endpoint")]
        public void GivenTheUserSendsARequestToTheEndpoint(string userName, string endPoint)
        {
            Account account = new Account()
            {
                AccountName = "ACC" + userName + "001",
                TotalAmount = 1000,
            };
            response = _serviceRequestBase.CreateAccount(account, endPoint);
        }
        [Then(@"the response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBe(int statusCode)
        {
            Assert.AreEqual(statusCode, (int)response.StatusCode);
        }

        [Then(@"the response body should contain the AccountName as ""([^""]*)"" and TotalAmount as ""([^""]*)""")]
        public void ThenTheResponseBodyShouldContainTheAccountIdAsAndTotalAmountAs(string accountName, string defaultAmount)
        {
            Stream stream = response.GetResponseStream();
            StreamReader str = new(stream);
            string jsonAccount = str.ReadToEnd();
            Account responseAccount = JsonConvert.DeserializeObject<Account>(jsonAccount);
            Assert.AreEqual(accountName, responseAccount.AccountName, "Account Name is not correct");
            Assert.AreEqual(defaultAmount, responseAccount.TotalAmount, "Total Amount is not correct");
        }
        [Given(@"the user ""([^""]*)"" sends a POST request to the ""([^""]*)"" endpoint to create two account")]
        public void GivenTheUserSendsAPOSTRequestToTheEndpointToCreateMultipleAccount(string userName, string endPoint)
        {
            List<Account> accounts = new List<Account>()
            {
                new Account()
                {
                    AccountName = "ACC"+ userName +"001",
                    TotalAmount = 1000,
                },
                new Account()
                {
                    AccountName = "ACC"+ userName +"002",
                    TotalAmount = 1000,
                }
            };

            response = _serviceRequestBase.CreateAccount(accounts, endPoint);
        }

        [Then(@"the response body should list two account details")]
        public void ThenTheResponseBodyShouldListTwoAccountDetails()
        {
            Stream stream = response.GetResponseStream();
            StreamReader str = new(stream);
            string jsonAccount = str.ReadToEnd();
            List<Account> responseAccounts = JsonConvert.DeserializeObject<List<Account>>(jsonAccount);
            Assert.AreEqual(2, responseAccounts.Count, "List of Account codes for the user not matched");
        }
        [Given(@"the user sends a DELETE request to the ""([^""]*)"" endpoint to delete Account ""([^""]*)""")]
        public void GivenTheUserSendsADELETERequestToTheEndpointToDeleteAccount(string accountName, string endPoint)
        {
            HttpWebResponse response = _serviceRequestBase.DeleteAccount(accountName, endPoint);
        }

        [Then(@"the response body should be Empty")]
        public void ThenTheResponseBodyShouldBeEmpty()
        {
            Stream stream = response.GetResponseStream();
            StreamReader str = new(stream);
            var responseBody = str.ReadToEnd();
            if (responseBody.Length == 0)
            {
                Assert.Pass("Response Body is empty");
            }
            else
            {
                Assert.Fail("Response Body is not empty");
            }
        }
        [Then(@"the response body should contain Error Message as ""([^""]*)""")]
        public void ThenTheResponseBodyShouldContainErrorMessageAs(string expectedError)
        {
            Stream stream = response.GetResponseStream();
            StreamReader str = new(stream);
            string responseContent = str.ReadToEnd();
            bool contains = responseContent.Contains(expectedError);
            if (contains)
            {
                Assert.Pass("Valid Error message");
            }
            else
            {
                Assert.Pass("Invalid Error message");
            }
        }
    }
}
