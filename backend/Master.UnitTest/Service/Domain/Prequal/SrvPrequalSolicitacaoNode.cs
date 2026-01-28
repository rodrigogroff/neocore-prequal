using Master.Entity.Database.Domain.Bureau;
using Master.Entity.Database.Domain.Company;
using Master.Entity.Database.Domain.Prequal;
using Master.Entity.Dto.Request.Domain.Prequal;
using Master.Repository.Domain.Bureau;
using Master.Repository.Domain.Company;
using Master.Repository.Domain.Prequal;
using Master.Service.Domain.Prequal;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace Master.Test.Service.Domain.Prequal
{
    [TestClass]
    public class SrvPrequalSolicitacaoNodeTests
    {
        private Mock<IPrequalRepository> _mockPrequalRepo;
        private Mock<ICompanyRepository> _mockCompanyRepo;
        private Mock<IBureauRepository> _mockBureauRepo;
        private IMemoryCache _memoryCache;
        private SrvPrequalSolicitacaoNode _srv;

        [TestInitialize]
        public void Setup()
        {
            _mockPrequalRepo = new Mock<IPrequalRepository>();
            _mockCompanyRepo = new Mock<ICompanyRepository>();
            _mockBureauRepo = new Mock<IBureauRepository>();
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
            _srv = new SrvPrequalSolicitacaoNode
            {
                iRepoBureau = _mockBureauRepo.Object,
                iRepoCompany = _mockCompanyRepo.Object,
                iRepoPrequal = _mockPrequalRepo.Object,
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            _memoryCache?.Dispose();
        }

        private DtoRequestPrequalSolicitacoesNode CriarRequestPadrao()
        {
            return new DtoRequestPrequalSolicitacoesNode
            {
                fkCompany = 1,
                propostas = new List<PropostaDataPrevRequest>()
            };
        }

        private Tb_PrequalLeilaoConfig CriarConfigPadrao()
        {
            return new Tb_PrequalLeilaoConfig
            {
                vrLibMin = 1000,
                vrLibMax = 50000,
                vrMargemMin = 100,
                vrMargemMax = 5000,
                nuParcMin = 12,
                nuParcMax = 84,
                nuIdadeMin = 18,
                nuIdadeMax = 70,
                nuMesesAdmissaoMin = 6,
                nuMesesAdmissaoMax = 0,
                nuMesesAberturaEmpresaMin = 12,
                bEmpregadorCnpj = false,
                bEmpregadorCpf = false,
                bPep = true,
                bAlertaAvisoPrevio = true,
                bAlertaSaude = true
            };
        }

        private Tb_CompanyFinanceiro CriarCompanyFinanceiroPadrao()
        {
            return new Tb_CompanyFinanceiro
            {
                bActiveSubL2 = false
            };
        }

        private PropostaDataPrevRequest CriarPropostaPadrao()
        {
            return new PropostaDataPrevRequest
            {
                ElegivelEmprestimo = true,
                ValorLiberado = 10000,
                MargemDisponivel = 500,
                NroParcelas = 48,
                DataNascimento = "01011980",
                DataAdmissao = "01012020",
                NumeroInscricaoEmpregador = 12345678000199,
                InscricaoEmpregador = new IERequest { Codigo = 1 },
                PessoaExpostaPoliticamente = null,
                Alertas = new List<AlertaRequest>()
            };
        }

        [TestMethod]
        public async Task Exec_ListaVazia_DeveRetornarTrue()
        {
            // Arrange
            var request = CriarRequestPadrao();

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.IsNotNull(_srv.OutDto);
            Assert.AreEqual(0, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(0, _srv.OutDto.rejeitadas.Count);
        }

        [TestMethod]
        public async Task Exec_PropostaElegivelFalse_DeveRejeitarComMotivoCorreto()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.ElegivelEmprestimo = false;
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            var financ = CriarCompanyFinanceiroPadrao();

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(1, _srv.OutDto.rejeitadas.Count);
            Assert.AreEqual(SrvPrequalSolicitacaoNode.DESCARTE_ElegivelEmprestimo, _srv.OutDto.rejeitadas[0]._motivoRejeitado);
        }

        [TestMethod]
        public async Task Exec_EmpregadorCnpjBloqueado_DeveRejeitarProposta()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.InscricaoEmpregador.Codigo = 1;
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.bEmpregadorCnpj = true;
            var financ = CriarCompanyFinanceiroPadrao();

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(1, _srv.OutDto.rejeitadas.Count);
            Assert.AreEqual(SrvPrequalSolicitacaoNode.DESCARTE_EmpregadorCnpj, _srv.OutDto.rejeitadas[0]._motivoRejeitado);
        }

        [TestMethod]
        public async Task Exec_EmpregadorCpfBloqueado_DeveRejeitarProposta()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.InscricaoEmpregador.Codigo = 2;
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.bEmpregadorCpf = true;
            var financ = CriarCompanyFinanceiroPadrao();

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(1, _srv.OutDto.rejeitadas.Count);
            Assert.AreEqual(SrvPrequalSolicitacaoNode.DESCARTE_EmpregadorCpf, _srv.OutDto.rejeitadas[0]._motivoRejeitado);
        }

        [TestMethod]
        public async Task Exec_PessoaExpostaPoliticamente_DeveRejeitarProposta()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.PessoaExpostaPoliticamente = new PEPRequest();
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.bPep = true;
            var financ = CriarCompanyFinanceiroPadrao();

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(1, _srv.OutDto.rejeitadas.Count);
            Assert.AreEqual(SrvPrequalSolicitacaoNode.DESCARTE_Pep, _srv.OutDto.rejeitadas[0]._motivoRejeitado);
        }

        [TestMethod]
        public async Task Exec_AlertaAvisoPrevio_DeveRejeitarProposta()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.Alertas = new List<AlertaRequest>
            {
                new AlertaRequest { TipoAlerta = new TipoAlertaRequest { Codigo = 1 } }
            };
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.bAlertaAvisoPrevio = true;
            var financ = CriarCompanyFinanceiroPadrao();

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(1, _srv.OutDto.rejeitadas.Count);
            Assert.AreEqual(SrvPrequalSolicitacaoNode.DESCARTE_AlertaAvisoPrevio, _srv.OutDto.rejeitadas[0]._motivoRejeitado);
        }

        [TestMethod]
        public async Task Exec_AlertaSaude_DeveRejeitarProposta()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.Alertas = new List<AlertaRequest>
            {
                new AlertaRequest { TipoAlerta = new TipoAlertaRequest { Codigo = 2 } }
            };
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.bAlertaSaude = true;
            var financ = CriarCompanyFinanceiroPadrao();

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(1, _srv.OutDto.rejeitadas.Count);
            Assert.AreEqual(SrvPrequalSolicitacaoNode.DESCARTE_AlertaSaude, _srv.OutDto.rejeitadas[0]._motivoRejeitado);
        }

        [TestMethod]
        public async Task Exec_ValorLiberadoMenorQueMinimo_DeveRejeitarProposta()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.ValorLiberado = 500;
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.vrLibMin = 1000;
            var financ = CriarCompanyFinanceiroPadrao();

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(1, _srv.OutDto.rejeitadas.Count);
            Assert.AreEqual(SrvPrequalSolicitacaoNode.DESCARTE_ValorLiberadoMin, _srv.OutDto.rejeitadas[0]._motivoRejeitado);
        }

        [TestMethod]
        public async Task Exec_ValorLiberadoMaiorQueMaximo_DeveRejeitarProposta()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.ValorLiberado = 60000;
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.vrLibMax = 50000;
            var financ = CriarCompanyFinanceiroPadrao();

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(1, _srv.OutDto.rejeitadas.Count);
            Assert.AreEqual(SrvPrequalSolicitacaoNode.DESCARTE_ValorLiberadoMax, _srv.OutDto.rejeitadas[0]._motivoRejeitado);
        }

        [TestMethod]
        public async Task Exec_MargemDisponivelMenorQueMinimo_DeveRejeitarProposta()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.MargemDisponivel = 50;
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.vrMargemMin = 100;
            var financ = CriarCompanyFinanceiroPadrao();

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(1, _srv.OutDto.rejeitadas.Count);
            Assert.AreEqual(SrvPrequalSolicitacaoNode.DESCARTE_MargemDisponivelMin, _srv.OutDto.rejeitadas[0]._motivoRejeitado);
        }

        [TestMethod]
        public async Task Exec_MargemDisponivelMaiorQueMaximo_DeveRejeitarProposta()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.MargemDisponivel = 6000;
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.vrMargemMax = 5000;
            var financ = CriarCompanyFinanceiroPadrao();

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(1, _srv.OutDto.rejeitadas.Count);
            Assert.AreEqual(SrvPrequalSolicitacaoNode.DESCARTE_MargemDisponivelMax, _srv.OutDto.rejeitadas[0]._motivoRejeitado);
        }

        [TestMethod]
        public async Task Exec_NumeroParcelasMenorQueMinimo_DeveRejeitarProposta()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.NroParcelas = 6;
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.nuParcMin = 12;
            var financ = CriarCompanyFinanceiroPadrao();

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(1, _srv.OutDto.rejeitadas.Count);
            Assert.AreEqual(SrvPrequalSolicitacaoNode.DESCARTE_NroParcelasMin, _srv.OutDto.rejeitadas[0]._motivoRejeitado);
        }

        [TestMethod]
        public async Task Exec_NumeroParcelasMaiorQueMaximo_DeveRejeitarProposta()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.NroParcelas = 96;
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.nuParcMax = 84;
            var financ = CriarCompanyFinanceiroPadrao();

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(1, _srv.OutDto.rejeitadas.Count);
            Assert.AreEqual(SrvPrequalSolicitacaoNode.DESCARTE_NroParcelasMax, _srv.OutDto.rejeitadas[0]._motivoRejeitado);
        }

        [TestMethod]
        public async Task Exec_IdadeMenorQueMinimo_DeveRejeitarProposta()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.DataNascimento = "01012010"; // 15 anos
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.nuIdadeMin = 18;
            var financ = CriarCompanyFinanceiroPadrao();

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(1, _srv.OutDto.rejeitadas.Count);
            Assert.AreEqual(SrvPrequalSolicitacaoNode.DESCARTE_IdadeMin, _srv.OutDto.rejeitadas[0]._motivoRejeitado);
        }

        [TestMethod]
        public async Task Exec_IdadeMaiorQueMaximo_DeveRejeitarProposta()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.DataNascimento = "01011945"; // 80 anos
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.nuIdadeMax = 70;
            var financ = CriarCompanyFinanceiroPadrao();

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(1, _srv.OutDto.rejeitadas.Count);
            Assert.AreEqual(SrvPrequalSolicitacaoNode.DESCARTE_IdadeMax, _srv.OutDto.rejeitadas[0]._motivoRejeitado);
        }

        [TestMethod]
        public async Task Exec_MesesAdmissaoMenorQueMinimo_DeveRejeitarProposta()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.DataAdmissao = DateTime.Now.AddMonths(-3).ToString("ddMMyyyy"); // 3 meses atrás
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.nuMesesAdmissaoMin = 6;
            var financ = CriarCompanyFinanceiroPadrao();

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(1, _srv.OutDto.rejeitadas.Count);
            Assert.AreEqual(SrvPrequalSolicitacaoNode.DESCARTE_MesesAdmissaoMin, _srv.OutDto.rejeitadas[0]._motivoRejeitado);
        }

        [TestMethod]
        public async Task Exec_MesesAdmissaoMaiorQueMaximo_DeveRejeitarProposta()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.DataAdmissao = "01011990"; // ~35 anos atrás
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.nuMesesAdmissaoMax = 240; // 20 anos
            var financ = CriarCompanyFinanceiroPadrao();

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(1, _srv.OutDto.rejeitadas.Count);
            Assert.AreEqual(SrvPrequalSolicitacaoNode.DESCARTE_MesesAdmissaoMax, _srv.OutDto.rejeitadas[0]._motivoRejeitado);
        }

        [TestMethod]
        public async Task Exec_PropostaValida_DeveQualificar()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            var financ = CriarCompanyFinanceiroPadrao();

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(0, _srv.OutDto.rejeitadas.Count);
            Assert.IsNull(_srv.OutDto.qualificadas[0]._motivoRejeitado);
        }

        [TestMethod]
        public async Task Exec_MultiplasProposta_DeveSepararQualificadasERejeitadas()
        {
            // Arrange
            var request = CriarRequestPadrao();

            var propostaValida = CriarPropostaPadrao();
            var propostaInvalida = CriarPropostaPadrao();
            propostaInvalida.ElegivelEmprestimo = false;

            request.propostas.Add(propostaValida);
            request.propostas.Add(propostaInvalida);

            var config = CriarConfigPadrao();
            var financ = CriarCompanyFinanceiroPadrao();

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(1, _srv.OutDto.rejeitadas.Count);
        }

        [TestMethod]
        public async Task Exec_CacheConfig_DeveUsarCacheNaSegundaChamada()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            var financ = CriarCompanyFinanceiroPadrao();

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            await _srv.Exec(_memoryCache, request);

            var srv2 = new SrvPrequalSolicitacaoNode();
            await srv2.Exec(_memoryCache, request);

            // Assert
            _mockPrequalRepo.Verify(r => r.GetPrequalLeilaoConfig(It.IsAny<int>()), Times.Once);
            _mockCompanyRepo.Verify(r => r.GetCompanyFinanceiro(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public async Task Exec_SubL2Ativo_DadosEmpresaNaoEncontrados_DeveRejeitarProposta()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.NumeroInscricaoEmpregador = 12345678000199;
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.nuMesesAberturaEmpresaMin = 12;
            var financ = CriarCompanyFinanceiroPadrao();
            financ.bActiveSubL2 = true;

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);
            _mockBureauRepo.Setup(r => r.GetDadosEmpresa(It.IsAny<string>())).Returns((Tb_DadosEmpresa)null);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(1, _srv.OutDto.rejeitadas.Count);
            Assert.AreEqual(SrvPrequalSolicitacaoNode.REJECT_MSG_L2_DADOS_NF, _srv.OutDto.rejeitadas[0]._motivoRejeitado);
            Assert.IsTrue(_srv.OutDto.rejeitadas[0]._detalheRejeitado.Contains("12345678000199"));
        }

        [TestMethod]
        public async Task Exec_SubL2Ativo_MesesAberturaEmpresaMenorQueMinimo_DeveRejeitarProposta()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.NumeroInscricaoEmpregador = 12345678000199;
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.nuMesesAberturaEmpresaMin = 24; // 24 meses mínimo
            var financ = CriarCompanyFinanceiroPadrao();
            financ.bActiveSubL2 = true;

            var dadosEmpresa = new Tb_DadosEmpresa
            {
                dtAberturaL1 = DateTime.Now.AddMonths(-12) // Empresa com 12 meses
            };

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);
            _mockBureauRepo.Setup(r => r.GetDadosEmpresa(It.IsAny<string>())).Returns(dadosEmpresa);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(1, _srv.OutDto.rejeitadas.Count);
            Assert.AreEqual(SrvPrequalSolicitacaoNode.REJECT_MSG_MesesAberturaEmpresaMin, _srv.OutDto.rejeitadas[0]._motivoRejeitado);
        }

        [TestMethod]
        public async Task Exec_SubL2Ativo_EmpresaComIdadeSuficiente_DeveQualificar()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.NumeroInscricaoEmpregador = 12345678000199;
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.nuMesesAberturaEmpresaMin = 12;
            var financ = CriarCompanyFinanceiroPadrao();
            financ.bActiveSubL2 = true;

            var dadosEmpresa = new Tb_DadosEmpresa
            {
                dtAberturaL1 = DateTime.Now.AddMonths(-24) // Empresa com 24 meses
            };

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);
            _mockBureauRepo.Setup(r => r.GetDadosEmpresa(It.IsAny<string>())).Returns(dadosEmpresa);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(0, _srv.OutDto.rejeitadas.Count);
        }

        [TestMethod]
        public async Task Exec_SubL2Inativo_NaoDeveValidarDadosEmpresa()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.NumeroInscricaoEmpregador = 12345678000199;
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.nuMesesAberturaEmpresaMin = 12;
            var financ = CriarCompanyFinanceiroPadrao();
            financ.bActiveSubL2 = false; // L2 desativado

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(0, _srv.OutDto.rejeitadas.Count);
            _mockBureauRepo.Verify(r => r.GetDadosEmpresa(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public async Task Exec_MargemZerada_NaoDeveValidarMargem()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.MargemDisponivel = 50;
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.vrMargemMin = 0; // Validação desativada
            config.vrMargemMax = 0; // Validação desativada
            var financ = CriarCompanyFinanceiroPadrao();

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(0, _srv.OutDto.rejeitadas.Count);
        }

        [TestMethod]
        public async Task Exec_AlertasVazio_NaoDeveRejeitarPorAlertas()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.Alertas = new List<AlertaRequest>(); // Lista vazia
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.bAlertaAvisoPrevio = true;
            config.bAlertaSaude = true;
            var financ = CriarCompanyFinanceiroPadrao();

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(0, _srv.OutDto.rejeitadas.Count);
        }

        [TestMethod]
        public async Task Exec_AlertasNull_NaoDeveRejeitarPorAlertas()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.Alertas = null; // Null
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.bAlertaAvisoPrevio = true;
            config.bAlertaSaude = true;
            var financ = CriarCompanyFinanceiroPadrao();

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(0, _srv.OutDto.rejeitadas.Count);
        }

        [TestMethod]
        public async Task Exec_ConfigValidacoesDesabilitadas_DeveQualificarTodasPropostas()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.InscricaoEmpregador.Codigo = 1;
            proposta.PessoaExpostaPoliticamente = new PEPRequest();
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.bEmpregadorCnpj = false; // Todas desabilitadas
            config.bEmpregadorCpf = false;
            config.bPep = false;
            config.bAlertaAvisoPrevio = false;
            config.bAlertaSaude = false;
            var financ = CriarCompanyFinanceiroPadrao();

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(0, _srv.OutDto.rejeitadas.Count);
        }

        [TestMethod]
        public async Task Exec_DezPropostas_CincoQualificadasCincoRejeitadas_DeveProcessarCorretamente()
        {
            // Arrange
            var request = CriarRequestPadrao();

            // Adiciona 5 válidas
            for (int i = 0; i < 5; i++)
            {
                request.propostas.Add(CriarPropostaPadrao());
            }

            // Adiciona 5 inválidas
            for (int i = 0; i < 5; i++)
            {
                var propostaInvalida = CriarPropostaPadrao();
                propostaInvalida.ValorLiberado = 500; // Menor que mínimo
                request.propostas.Add(propostaInvalida);
            }

            var config = CriarConfigPadrao();
            var financ = CriarCompanyFinanceiroPadrao();

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(5, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(5, _srv.OutDto.rejeitadas.Count);
        }

        [TestMethod]
        public async Task Exec_CacheDadosEmpresa_DeveUsarCacheNaSegundaChamada()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.NumeroInscricaoEmpregador = 12345678000199;
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.nuMesesAberturaEmpresaMin = 12;
            var financ = CriarCompanyFinanceiroPadrao();
            financ.bActiveSubL2 = true;

            var dadosEmpresa = new Tb_DadosEmpresa
            {
                dtAberturaL1 = DateTime.Now.AddMonths(-24)
            };

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);
            _mockBureauRepo.Setup(r => r.GetDadosEmpresa(It.IsAny<string>())).Returns(dadosEmpresa);

            // Act - Primeira chamada
            await _srv.Exec(_memoryCache, request);

            // Act - Segunda chamada com mesmo cache
            var srv2 = new SrvPrequalSolicitacaoNode();
            await srv2.Exec(_memoryCache, request);

            // Assert
            _mockBureauRepo.Verify(r => r.GetDadosEmpresa(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public async Task Exec_EmpresaSemDataAbertura_DeveQualificar()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.NumeroInscricaoEmpregador = 12345678000199;
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.nuMesesAberturaEmpresaMin = 12;
            var financ = CriarCompanyFinanceiroPadrao();
            financ.bActiveSubL2 = true;

            var dadosEmpresa = new Tb_DadosEmpresa
            {
                dtAberturaL1 = null // Data de abertura null
            };

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);
            _mockBureauRepo.Setup(r => r.GetDadosEmpresa(It.IsAny<string>())).Returns(dadosEmpresa);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(0, _srv.OutDto.rejeitadas.Count);
        }

        [TestMethod]
        public async Task Exec_MesesAberturaEmpresaMinZerado_NaoDeveValidarAberturaEmpresa()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.NumeroInscricaoEmpregador = 12345678000199;
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.nuMesesAberturaEmpresaMin = 0; // Validação desativada
            var financ = CriarCompanyFinanceiroPadrao();
            financ.bActiveSubL2 = true;

            var dadosEmpresa = new Tb_DadosEmpresa
            {
                dtAberturaL1 = DateTime.Now.AddMonths(-1) // Empresa muito nova
            };

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);
            _mockBureauRepo.Setup(r => r.GetDadosEmpresa(It.IsAny<string>())).Returns(dadosEmpresa);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(0, _srv.OutDto.rejeitadas.Count);
        }

        [TestMethod]
        public async Task Exec_IdadeMinZerada_NaoDeveValidarIdadeMinima()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.DataNascimento = "01012010"; // 15 anos
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.nuIdadeMin = 0; // Validação min desativada
            config.nuIdadeMax = 70; // Validação max ativa
            var financ = CriarCompanyFinanceiroPadrao();

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(0, _srv.OutDto.rejeitadas.Count);
        }

        [TestMethod]
        public async Task Exec_IdadeMaxZerada_NaoDeveValidarIdadeMaxima()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.DataNascimento = "01011940"; // 85 anos
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.nuIdadeMin = 18; // Validação min ativa
            config.nuIdadeMax = 0; // Validação max desativada
            var financ = CriarCompanyFinanceiroPadrao();

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(0, _srv.OutDto.rejeitadas.Count);
        }

        [TestMethod]
        public async Task Exec_AmbasIdadesZeradas_NaoDeveValidarIdade()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.DataNascimento = "01012010"; // 15 anos
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.nuIdadeMin = 0; // Ambas desativadas
            config.nuIdadeMax = 0;
            var financ = CriarCompanyFinanceiroPadrao();

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(0, _srv.OutDto.rejeitadas.Count);
        }

        [TestMethod]
        public async Task Exec_MesesAdmissaoMinZerado_NaoDeveValidarMesesAdmissaoMinimo()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.DataAdmissao = DateTime.Now.AddMonths(-1).ToString("ddMMyyyy"); // 1 mês
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.nuMesesAdmissaoMin = 0; // Validação min desativada
            config.nuMesesAdmissaoMax = 120; // Validação max ativa
            var financ = CriarCompanyFinanceiroPadrao();

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(0, _srv.OutDto.rejeitadas.Count);
        }

        [TestMethod]
        public async Task Exec_MesesAdmissaoMaxZerado_NaoDeveValidarMesesAdmissaoMaximo()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.DataAdmissao = "01011990"; // ~35 anos
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.nuMesesAdmissaoMin = 6; // Validação min ativa
            config.nuMesesAdmissaoMax = 0; // Validação max desativada
            var financ = CriarCompanyFinanceiroPadrao();

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(0, _srv.OutDto.rejeitadas.Count);
        }

        [TestMethod]
        public async Task Exec_AmbosMesesAdmissaoZerados_NaoDeveValidarMesesAdmissao()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.DataAdmissao = DateTime.Now.AddMonths(-1).ToString("ddMMyyyy");
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.nuMesesAdmissaoMin = 0; // Ambas desativadas
            config.nuMesesAdmissaoMax = 0;
            var financ = CriarCompanyFinanceiroPadrao();

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(0, _srv.OutDto.rejeitadas.Count);
        }

        [TestMethod]
        public async Task Exec_ParcelasMinZerado_NaoDeveValidarParcelasMinimo()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.NroParcelas = 1; // Muito baixo
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.nuParcMin = 0; // Validação min desativada
            config.nuParcMax = 84; // Validação max ativa
            var financ = CriarCompanyFinanceiroPadrao();

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(0, _srv.OutDto.rejeitadas.Count);
        }

        [TestMethod]
        public async Task Exec_ParcelasMaxZerado_NaoDeveValidarParcelasMaximo()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.NroParcelas = 200; // Muito alto
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.nuParcMin = 12; // Validação min ativa
            config.nuParcMax = 0; // Validação max desativada
            var financ = CriarCompanyFinanceiroPadrao();

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _srv.OutDto.qualificadas.Count);
            Assert.AreEqual(0, _srv.OutDto.rejeitadas.Count);
        }

        [TestMethod]
        public async Task Exec_ExcecaoNoProcessamento_DeveRetornarTrue()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            request.propostas.Add(proposta);

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>()))
                .Throws(new Exception("Erro simulado"));

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result); // Mesmo com exceção, retorna true
        }

        [TestMethod]
        public async Task Exec_FormatacaoDocumentoEmpregador_DevePadLeftCom14Zeros()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.NumeroInscricaoEmpregador = 123; // Número pequeno
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.nuMesesAberturaEmpresaMin = 12;
            var financ = CriarCompanyFinanceiroPadrao();
            financ.bActiveSubL2 = true;

            var dadosEmpresa = new Tb_DadosEmpresa
            {
                dtAberturaL1 = DateTime.Now.AddMonths(-24)
            };

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);
            _mockBureauRepo.Setup(r => r.GetDadosEmpresa("00000000000123")).Returns(dadosEmpresa);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            _mockBureauRepo.Verify(r => r.GetDadosEmpresa("00000000000123"), Times.Once);
        }

        [TestMethod]
        public async Task Exec_DetalheRejeicaoValorLiberado_DeveConterValoresCorretos()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.ValorLiberado = 500;
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.vrLibMin = 1000;
            var financ = CriarCompanyFinanceiroPadrao();

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _srv.OutDto.rejeitadas.Count);
            Assert.IsTrue(_srv.OutDto.rejeitadas[0]._detalheRejeitado.Contains("500"));
            Assert.IsTrue(_srv.OutDto.rejeitadas[0]._detalheRejeitado.Contains("1000"));
            Assert.IsTrue(_srv.OutDto.rejeitadas[0]._detalheRejeitado.Contains("<"));
        }

        [TestMethod]
        public async Task Exec_DetalheRejeicaoMargemDisponivel_DeveConterValoresCorretos()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.MargemDisponivel = 6000;
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.vrMargemMax = 5000;
            var financ = CriarCompanyFinanceiroPadrao();

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _srv.OutDto.rejeitadas.Count);
            Assert.IsTrue(_srv.OutDto.rejeitadas[0]._detalheRejeitado.Contains("6000"));
            Assert.IsTrue(_srv.OutDto.rejeitadas[0]._detalheRejeitado.Contains("5000"));
            Assert.IsTrue(_srv.OutDto.rejeitadas[0]._detalheRejeitado.Contains(">"));
        }

        [TestMethod]
        public async Task Exec_DetalheRejeicaoNroParcelas_DeveConterValoresCorretos()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.NroParcelas = 96;
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.nuParcMax = 84;
            var financ = CriarCompanyFinanceiroPadrao();

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _srv.OutDto.rejeitadas.Count);
            Assert.IsTrue(_srv.OutDto.rejeitadas[0]._detalheRejeitado.Contains("96"));
            Assert.IsTrue(_srv.OutDto.rejeitadas[0]._detalheRejeitado.Contains("84"));
        }

        [TestMethod]
        public async Task Exec_DetalheRejeicaoIdade_DeveConterAnos()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.DataNascimento = "01011945"; // ~80 anos
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.nuIdadeMax = 70;
            var financ = CriarCompanyFinanceiroPadrao();

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _srv.OutDto.rejeitadas.Count);
            Assert.IsTrue(_srv.OutDto.rejeitadas[0]._detalheRejeitado.Contains("anos"));
            Assert.IsTrue(_srv.OutDto.rejeitadas[0]._detalheRejeitado.Contains("70"));
        }

        [TestMethod]
        public async Task Exec_DetalheRejeicaoMesesAdmissao_DeveConterMeses()
        {
            // Arrange
            var request = CriarRequestPadrao();
            var proposta = CriarPropostaPadrao();
            proposta.DataAdmissao = DateTime.Now.AddMonths(-3).ToString("ddMMyyyy");
            request.propostas.Add(proposta);

            var config = CriarConfigPadrao();
            config.nuMesesAdmissaoMin = 6;
            var financ = CriarCompanyFinanceiroPadrao();

            _mockPrequalRepo.Setup(r => r.GetPrequalLeilaoConfig(It.IsAny<int>())).Returns(config);
            _mockCompanyRepo.Setup(r => r.GetCompanyFinanceiro(It.IsAny<int>())).Returns(financ);

            // Act
            bool result = await _srv.Exec(_memoryCache, request);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _srv.OutDto.rejeitadas.Count);
            Assert.IsTrue(_srv.OutDto.rejeitadas[0]._detalheRejeitado.Contains("meses"));
            Assert.IsTrue(_srv.OutDto.rejeitadas[0]._detalheRejeitado.Contains("6"));
        }
    }
}