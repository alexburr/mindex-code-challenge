using challenge.Models;
using code_challenge.Tests.Integration.Extensions;
using code_challenge.Tests.Integration.Helpers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Net.Http;
using System.Text;

namespace code_challenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<TestServerStartup>()
                .UseEnvironment("Development"));

            _httpClient = _testServer.CreateClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void CreateCompensation_Returns_Created()
        {
            // Arrange
            var compensation = new Compensation()
            {
                Employee = "16a596ae-edd3-4847-99fe-c4518e82c86f",
                Salary = 100000,
                EffectiveDate = new DateTime(2020, 4, 16)
            };

            var requestContent = new JsonSerialization().ToJson(compensation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newCompensation = response.DeserializeContent<Compensation>();
            Assert.IsNotNull(newCompensation.Employee);
            Assert.IsNotNull(newCompensation.CompensationId);
            Assert.AreEqual(compensation.Employee, newCompensation.Employee);
            Assert.AreEqual(compensation.Salary, newCompensation.Salary);
            Assert.AreEqual(compensation.EffectiveDate, newCompensation.EffectiveDate);
        }

        [TestMethod]
        public void GetByEmployeeId_Returns_Ok()
        {
            // Arrange
            var compensation = new Compensation()
            {
                Employee = "16a596ae-edd3-4847-99fe-c4518e82c86f",
                Salary = 100000,
                EffectiveDate = new DateTime(2020, 4, 16)
            };

            var postRequestContent = new JsonSerialization().ToJson(compensation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation",
               new StringContent(postRequestContent, Encoding.UTF8, "application/json"));

            var getRequestTask = _httpClient.GetAsync($"api/compensation/{compensation.Employee}");
            var getResponse = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, getResponse.StatusCode);
            var compensationResponse = getResponse.DeserializeContent<Compensation>();
            Assert.IsNotNull(compensationResponse.CompensationId);
            Assert.AreEqual(compensation.Employee, compensationResponse.Employee);
            Assert.AreEqual(compensation.Salary, compensationResponse.Salary);
            Assert.AreEqual(compensation.EffectiveDate, compensationResponse.EffectiveDate);
        }

        [TestMethod]
        public void GetByEmployeeId_Returns_NotFound()
        {
            // Arrange
            var compensation = new Compensation()
            {
                Employee = "Invalid_Id"
            };

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/compensation/{compensation.Employee}");
            var getResponse = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, getResponse.StatusCode);
        }
    }
}
