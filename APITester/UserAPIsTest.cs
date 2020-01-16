using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using BackEnd.Data;

namespace APITester
{
    [TestClass]
    public class UserAPIsTest
    {
        // TODO: refactor database
        // create test db migration

        string URL = "https://localhost:44386/api";

        [TestMethod]
        public void PostRegisterUser_ExpectOK()
        {
            // arrange
            var client = new RestClient(URL);
            var request = new RestRequest("/users", Method.POST);

            var user = User.Create("test2", "test2", "test2@test.com", "test", "test", "test", "test", "test", "1");

            // Json to post.
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(user);

            request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;

            // act
            var response = client.Execute(request);

            // assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }
    }
}