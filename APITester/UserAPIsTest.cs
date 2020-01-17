using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using BackEnd.DTO.Users;
using System.Diagnostics;

namespace APITester
{
    [TestClass]
    public class UserAPIsTest
    {
        // TODO: refactor database
        // create test db migration

        string URL = "https://localhost:44386/api";

        [TestMethod]
        public void RegisterSingle_ExpectOK()
        {
            var RequestObject = new RegisterSingle()
            {
                FirstName = "test2",
                LastName = "test2",
                Email = "test2@test.com",
                Country = "test",
                Password = "test",
                Rights = "test",
                Workplace = "test",
                PositionTitle = "test",
                ContactNumber = "1"
            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(RequestObject);

            var client = new RestClient(URL);
            var request = new RestRequest("/user", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);

            var response = client.Execute(request);
            
            Trace.WriteLine(response.Content);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public void LoginValidationSingle_ExpectOK()
        {
            var RequestObject = new LoginValidationSingle()
            {
                Email = "test2@test.com",
                Password = "test"
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(RequestObject);

            var client = new RestClient(URL);
            var request = new RestRequest("user/login", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);

            var response = client.Execute(request);

            Trace.WriteLine(response.Content);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }
    }
}