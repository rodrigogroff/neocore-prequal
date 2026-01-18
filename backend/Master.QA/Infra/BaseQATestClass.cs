using ApiTestFramework;
using Master.Entity.Dto.Domain.Auth;

namespace Master.QA.Infra
{
    public class BaseQATestClass
    {
        public const string MasterUrl = "http://127.0.0.1:59540";
        public ApiTestClient _client = null!;

        public DtoLoginInformation LoginDataOk = new()
        {
            email = "operator@teste.com.br",
            password = "142536",
        };
    }
}
