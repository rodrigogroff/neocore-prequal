using ApiTestFramework;
using Master.Entity.Dto.Request.Domain.Auth;
using Master.Entity.Dto.Response.Domain.Auth;
using Master.QA.Infra;
using System.Net;

namespace Master.QA.UseCase.Domain.Login
{
    [TestClass]
    public sealed class LoginQATest : BaseQATestClass
    {
        [TestInitialize]
        public void Setup()
        {
            _client = new ApiTestClient(MasterUrl);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _client?.Dispose();
        }

        [TestMethod]
        public async Task OK()
        {
            var loginData = this.LoginDataOk;
            var response = await _client.PostAsync<DtoResponseToken>("/api/authenticate", loginData);

            Assert.IsTrue(response.IsSuccess, $"Login failed: {response.ErrorMessage}");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Data, "Token data should not be null");
        }

        [TestMethod]
        public async Task NOK_Email()
        {
            var loginData = new DtoRequestLoginInformation
            {
                email = "x",
                password = "y",
            };

            var response = await _client.PostAsync<DtoResponseToken>("/api/authenticate", loginData);

            Assert.IsFalse(response.IsSuccess);            
        }
    }
}
