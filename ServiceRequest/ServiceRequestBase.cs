using BankAccolite_Spec.Models;
using BankAccolite_Spec.ServiceRequest;
using Newtonsoft.Json;
using System.Net;

namespace BankAccolite_Spec.ServiceRequest
{
    public class ServiceRequestBase
    {
        SetRequest setRequest = new();
        public static readonly string baseUrl = "https://localhost:80/";
        public HttpWebResponse CreateAccount(Account account, string endpoint)
        {
            string jsonAccount = JsonConvert.SerializeObject(account);
            string url = baseUrl + endpoint;
            HttpWebRequest request = setRequest.SetPUTRequest(url, "application/json", jsonAccount);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response;
        }
        public HttpWebResponse CreateAccount(List<Account> account, string endpoint)
        {
            string jsonAccount = JsonConvert.SerializeObject(account);
            string url = baseUrl + endpoint;
            HttpWebRequest request = setRequest.SetPUTRequest(url, "application/json", jsonAccount);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response;
        }
        public HttpWebResponse DeleteAccount(string accountName, string endpoint)
        {
            string url = baseUrl + endpoint + accountName;
            HttpWebRequest request = setRequest.SetDeleteRequest(url, "application/json");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response;
        }
        public HttpWebResponse GetAccount(string accountName, string endpoint)
        {
            string url = baseUrl + endpoint + accountName;
            HttpWebRequest request = setRequest.SetGetRequest(url, "application/json");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response;
        }
        public HttpWebResponse MakeTransaction(AccountTransactions transactions, string endpoint)
        {
            string url = baseUrl + endpoint;
            string jsonTransaction = JsonConvert.SerializeObject(transactions);
            HttpWebRequest request = setRequest.SetPUTRequest(url, "application/json", jsonTransaction);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response;
        }
    }
}