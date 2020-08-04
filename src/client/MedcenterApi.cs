using System;
using RestSharp;
using RestSharp.Authenticators;

namespace Client
{
    public class MedcenterApi
    {
        private readonly IRestClient _auth;
        private readonly IRestClient _api;

        public MedcenterApi(IConfiguration conf)
        {
            _auth = new RestClient();
            
            // var client = new RestClient("https://dev-bdadfu3r.eu.auth0.com/oauth/token");
            // var request = new RestRequest(Method.POST);
            // request.AddHeader("content-type", "application/x-www-form-urlencoded");
            // request.AddParameter("application/x-www-form-urlencoded", "grant_type=client_credentials&client_id=YOUR_CLIENT_ID&client_secret=YOUR_CLIENT_SECRET&audience=YOUR_API_IDENTIFIER", ParameterType.RequestBody);
            // IRestResponse response = client.Execute(request);
        }

        public bool Login()
        {
            _api = new RestClient();
        }

        public string ArrangeExam()
        {

        }

        public string CancelExam()
        {

        }
    }
}