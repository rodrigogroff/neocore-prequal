using Master.Entity.Database.Domain.User;
using Master.Entity.Database.Domain.Company;
using Master.Repository.Domain.User;
using Master.Repository.Domain.Company;
using Master.Service.Domain.Auth;
using Moq;

namespace Master.Test.Service.Domain.Auth
{
    [TestClass]
    public class SrvAuthenticateTests
    {
        private Mock<IUserRepository> _mockUserRepo;
        private Mock<ICompanyRepository> _mockCompanyRepo;
        private SrvAuthenticate _srv;

        [TestInitialize]
        public void Setup()
        {
            _mockUserRepo = new Mock<IUserRepository>();
            _mockCompanyRepo = new Mock<ICompanyRepository>();
            _srv = new SrvAuthenticate
            {
                iRepoUser = _mockUserRepo.Object,
                iRepoCompany = _mockCompanyRepo.Object
            };
        }

        [TestMethod]
        public void Exec_EmailInvalido_DeveRetornarFalse()
        {
            // Arrange
            string email = "email-invalido";
            string password = "123456";

            // Act
            bool result = _srv.ExecLoginUser(email, password);

            // Assert
            Assert.IsFalse(result);
            Assert.IsNull(_srv.OutDto);
        }

        [TestMethod]
        public void Exec_EmailVazio_DeveRetornarFalse()
        {
            // Arrange
            string email = "";
            string password = "123456";

            // Act
            bool result = _srv.ExecLoginUser(email, password);

            // Assert
            Assert.IsFalse(result);
            Assert.IsNull(_srv.OutDto);
        }

        [TestMethod]
        public void Exec_UsuarioNaoEncontrado_DeveRetornarFalse()
        {
            // Arrange
            string email = "naoexiste@empresa.com";
            string password = "123456";

            _mockUserRepo.Setup(r => r.GetUser(email)).Returns((Tb_User)null);

            // Act
            bool result = _srv.ExecLoginUser(email, password);

            // Assert
            Assert.IsFalse(result);
            Assert.IsNull(_srv.OutDto);
        }

        [TestMethod]
        public void Exec_UsuarioInativo_DeveRetornarFalse()
        {
            // Arrange
            string email = "teste@empresa.com";
            string password = "123456";

            _mockUserRepo.Setup(r => r.GetUser(email)).Returns(new Tb_User
            {
                id = 42,
                stEmail = email,
                stName = "João Teste",
                bActive = false,
                fkCompany = 10,
                stPassword = "123456"
            });

            // Act
            bool result = _srv.ExecLoginUser(email, password);

            // Assert
            Assert.IsFalse(result);
            Assert.IsNull(_srv.OutDto);
        }

        [TestMethod]
        public void Exec_EmpresaNaoEncontrada_DeveRetornarFalse()
        {
            // Arrange
            string email = "teste@empresa.com";
            string password = "123456";

            _mockUserRepo.Setup(r => r.GetUser(email)).Returns(new Tb_User
            {
                id = 42,
                stEmail = email,
                stName = "João Teste",
                bActive = true,
                fkCompany = 10,
                stPassword = "123456"
            });

            _mockCompanyRepo.Setup(r => r.GetCompany(10)).Returns((Tb_Company)null);

            // Act
            bool result = _srv.ExecLoginUser(email, password);

            // Assert
            Assert.IsFalse(result);
            Assert.IsNull(_srv.OutDto);
        }

        [TestMethod]
        public void Exec_EmpresaInativa_DeveRetornarFalse()
        {
            // Arrange
            string email = "teste@empresa.com";
            string password = "123456";

            _mockUserRepo.Setup(r => r.GetUser(email)).Returns(new Tb_User
            {
                id = 42,
                stEmail = email,
                stName = "João Teste",
                bActive = true,
                fkCompany = 10,
                stPassword = "123456"
            });

            _mockCompanyRepo.Setup(r => r.GetCompany(10)).Returns(new Tb_Company
            {
                id = 10,
                bActive = false
            });

            // Act
            bool result = _srv.ExecLoginUser(email, password);

            // Assert
            Assert.IsFalse(result);
            Assert.IsNull(_srv.OutDto);
        }
        
        [TestMethod]
        public void Exec_SenhaIncorreta_DeveRetornarFalse()
        {
            // Arrange
            string email = "teste@empresa.com";
            string password = "senhaErrada";

            _mockUserRepo.Setup(r => r.GetUser(email)).Returns(new Tb_User
            {
                id = 42,
                stEmail = email,
                stName = "João Teste",
                bActive = true,
                fkCompany = 10,
                stPassword = "senhaCorreta"
            });

            _mockCompanyRepo.Setup(r => r.GetCompany(10)).Returns(new Tb_Company
            {
                id = 10,
                bActive = true
            });

            // Act
            bool result = _srv.ExecLoginUser(email, password);

            // Assert
            Assert.IsFalse(result);
            Assert.IsNull(_srv.OutDto);
        }

        [TestMethod]
        public void Exec_SenhaVaziaUsaCPF_CPFCorreto_DeveRetornarTrue()
        {
            // Arrange
            string email = "teste@empresa.com";
            string cpf = "123.456.789-00";
            string password = "12345678900";

            _mockUserRepo.Setup(r => r.GetUser(email)).Returns(new Tb_User
            {
                id = 42,
                stEmail = email,
                stName = "João Teste",
                bActive = true,
                fkCompany = 10,
                stPassword = null,
                stCPF = cpf
            });

            _mockCompanyRepo.Setup(r => r.GetCompany(10)).Returns(new Tb_Company
            {
                id = 10,
                bActive = true
            });

            // Act
            bool result = _srv.ExecLoginUser(email, password);

            // Assert
            Assert.IsTrue(result);
            Assert.IsNotNull(_srv.OutDto);
            Assert.AreEqual(42, _srv.OutDto.fkUser);
            Assert.AreEqual(10, _srv.OutDto.fkCompany);
            Assert.AreEqual("João Teste", _srv.OutDto.stName);
            Assert.AreEqual(email, _srv.OutDto.stEmail);
        }

        [TestMethod]
        public void Exec_SenhaVaziaUsaCPF_CPFComFormatacao_DeveRetornarTrue()
        {
            // Arrange
            string email = "teste@empresa.com";
            string cpf = "123.456.789-00";
            string password = "123.456.789-00";

            _mockUserRepo.Setup(r => r.GetUser(email)).Returns(new Tb_User
            {
                id = 42,
                stEmail = email,
                stName = "João Teste",
                bActive = true,
                fkCompany = 10,
                stPassword = "",
                stCPF = cpf
            });

            _mockCompanyRepo.Setup(r => r.GetCompany(10)).Returns(new Tb_Company
            {
                id = 10,
                bActive = true
            });

            // Act
            bool result = _srv.ExecLoginUser(email, password);

            // Assert
            Assert.IsTrue(result);
            Assert.IsNotNull(_srv.OutDto);
        }

        [TestMethod]
        public void Exec_SenhaVaziaUsaCPF_CPFIncorreto_DeveRetornarFalse()
        {
            // Arrange
            string email = "teste@empresa.com";
            string cpf = "123.456.789-00";
            string password = "99999999999";

            _mockUserRepo.Setup(r => r.GetUser(email)).Returns(new Tb_User
            {
                id = 42,
                stEmail = email,
                stName = "João Teste",
                bActive = true,
                fkCompany = 10,
                stPassword = null,
                stCPF = cpf
            });

            _mockCompanyRepo.Setup(r => r.GetCompany(10)).Returns(new Tb_Company
            {
                id = 10,
                bActive = true
            });

            // Act
            bool result = _srv.ExecLoginUser(email, password);

            // Assert
            Assert.IsFalse(result);
            Assert.IsNull(_srv.OutDto);
        }
        
        [TestMethod]
        public void Exec_CredenciaisValidas_DeveRetornarTrue()
        {
            // Arrange
            string email = "teste@empresa.com";
            string password = "123456";

            _mockUserRepo.Setup(r => r.GetUser(email)).Returns(new Tb_User
            {
                id = 42,
                stEmail = email,
                stName = "João Teste",
                bActive = true,
                fkCompany = 10,
                stPassword = password
            });

            _mockCompanyRepo.Setup(r => r.GetCompany(10)).Returns(new Tb_Company
            {
                id = 10,
                bActive = true
            });

            // Act
            bool result = _srv.ExecLoginUser(email, password);

            // Assert
            Assert.IsTrue(result);
            Assert.IsNotNull(_srv.OutDto);
            Assert.AreEqual(42, _srv.OutDto.fkUser);
            Assert.AreEqual(10, _srv.OutDto.fkCompany);
            Assert.AreEqual("João Teste", _srv.OutDto.stName);
            Assert.AreEqual(email, _srv.OutDto.stEmail);
        }
    }
}
