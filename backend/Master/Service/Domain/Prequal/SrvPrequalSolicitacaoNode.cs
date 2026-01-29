using Master.Entity.Database.Domain.Bureau;
using Master.Entity.Database.Domain.Company;
using Master.Entity.Database.Domain.Prequal;
using Master.Entity.Dto.External;
using Master.Entity.Dto.Request.Domain.Prequal;
using Master.Entity.Dto.Response.Domain.Prequal;
using Master.Entity.Dto.Temp;
using Master.Entity.Gateway;
using Master.Repository.Domain.Bureau;
using Master.Repository.Domain.Company;
using Master.Repository.Domain.Prequal;
using Master.Service.Base;
using Master.Service.Base.Infra.Helper;
using Master.Service.Base.Infra.Mappers;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Master.Service.Domain.Prequal
{
    public class SrvPrequalSolicitacaoNode : SrvBase
    {
        public const string
            TOKEN_CACHE_LEILAO_CONFIG = "PrequalLeilaoConfig",
            TOKEN_CACHE_LEILAO_FINANC = "PrequalLeilaoFinanceiro",
            TOKEN_CACHE_LEILAO_EMP = "PrequalLeilaoEmp",
            DESCARTE_ElegivelEmprestimo = "!ElegivelEmprestimo",
            DESCARTE_EmpregadorCnpj = "!EmpregadorCnpj",
            DESCARTE_EmpregadorCpf = "!EmpregadorCpf",
            DESCARTE_Pep = "!Pep",
            DESCARTE_Simples = "!Simples",
            DESCARTE_Mei = "!Mei",
            DESCARTE_AlertaAvisoPrevio = "!AlertaAvisoPrevio",
            DESCARTE_AlertaSaude = "!AlertaSaude",
            DESCARTE_ValorLiberadoMin = "!ValorLiberado < Min",
            DESCARTE_ValorLiberadoMax = "!ValorLiberado > Max",
            DESCARTE_MargemDisponivelMin = "!MargemDisponivel < Min",
            DESCARTE_MargemDisponivelMax = "!MargemDisponivel > Max",
            DESCARTE_NroParcelasMin = "!NroParcelas < Min",
            DESCARTE_NroParcelasMax = "!NroParcelas > Max",
            DESCARTE_IdadeMin = "!Idade < Min",
            DESCARTE_IdadeMax = "!Idade > Max",
            DESCARTE_MesesAdmissaoMin = "!MesesAdmissao < Min",
            DESCARTE_MesesAdmissaoMax = "!MesesAdmissao > Max",
            
            REJECT_MSG_ElegivelEmprestimo = "_prop.ElegivelEmprestimo == false",
            REJECT_MSG_InscricaoEmpregadorCNPJ = "_prop.InscricaoEmpregador.Codigo == 1",
            REJECT_MSG_InscricaoEmpregadorCPF = "_prop.InscricaoEmpregador.Codigo == 2",
            REJECT_MSG_Pep = "_prop.PessoaExpostaPoliticamente != null",
            REJECT_MSG_AlertaAvisoPrevio = "_prop.Alertas.TipoAlerta.Codigo == 1",
            REJECT_MSG_AlertaSaude = "_prop.Alertas.TipoAlerta.Codigo == 2",
            REJECT_MSG_ValorLiberado = "_prop.ValorLiberado ",
            REJECT_MSG_MargemDisponivel = "_prop.MargemDisponivel ",
            REJECT_MSG_NroParcelas = "_prop.NroParcelas ",
            REJECT_MSG_DataNascimento = "_prop.DataNascimento (",
            REJECT_MSG_DataAdmissao = "_prop.DataAdmissao (",
            REJECT_MSG_ANOS = " anos) ",
            REJECT_MSG_MESES = " meses) ",
            REJECT_MSG_MINUS = " < ",
            REJECT_MSG_PLUS = " > ",
            REJECT_MSG_L2_DADOS_NF = "Dados da empresa não encontrados",
            REJECT_MSG_L2_DADOS_NF1 = "Não encontramos o documento ",
            REJECT_MSG_L2_DADOS_NF2 = " na base de dados",
            REJECT_MSG_MesesAberturaEmpresaMin = "!MesesAberturaEmpresaMin < Min",
            REJECT_MSG_Det_MesesAberturaEmpresaMin = "itemDbDadosEmpresa.dtAberturaL1 = ",
            DATE_FORMAT = "ddMMyyyy";

        private static readonly CultureInfo InvariantCulture = CultureInfo.InvariantCulture;

        public DtoResponsePrequalSolicitacoesNode OutDto;

        public async Task<bool> Exec(IMemoryCache memCache, DtoRequestPrequalSolicitacoesNode request)
        {
            try
            {
                var propostas = request.propostas;
                var count = propostas.Count;

                OutDto = new DtoResponsePrequalSolicitacoesNode
                {
                    qualificadas = [],
                    rejeitadas = [],
                    filter1 = 0,
                    filter2 = 0,
                    filter3 = 0,
                    filter4 = 0,
                    filter5 = 0,
                    filter6 = 0,
                    filter7 = 0,
                    filter8 = 0,
                    filter9 = 0,
                    filter10 = 0,
                    filter11 = 0,
                    filter12 = 0,
                    filter13 = 0,
                    filter14 = 0,
                    filter15 = 0,
                    filter16 = 0,
                    filter17 = 0,
                };

                if (count == 0)
                    return true;

                var fkCompany = (int)request.fkCompany;

                StartDatabase(Network);

                var repoPrequal = RepoPrequal();
                var repoCompany = RepoCompany();
                var repoBureau = RepoBureau();

                // pegar configurações 
                var configPrequal = await GetCachePrequalConfig(repoPrequal, memCache, fkCompany);

                // pegar subscrições
                var configFinanc = await GetCacheCompanyFinanc(repoCompany, memCache, fkCompany);

                // processar listas
                for (int i = 0; i < count; i++)
                {
                    var prop = propostas[i];

                    var tmpRP = await TryRejectProposal(prop, configPrequal, configFinanc, repoBureau, memCache, DateTime.Now);

                    if (tmpRP != null)
                    {
                        var rejected = PropostaDataPrevResponseMapper.Copy(prop);
                        rejected._motivoRejeitado = tmpRP.rejectMsg;
                        rejected._detalheRejeitado = tmpRP.rejectMsgDetalhe;
                        OutDto.rejeitadas.Add(rejected);
                    }
                    else
                    {
                        var qualified = PropostaDataPrevResponseMapper.Copy(prop);
                        qualified._motivoRejeitado = null;
                        qualified._detalheRejeitado = null;
                        OutDto.qualificadas.Add(qualified);
                    }
                }
            }
            catch
            {
                
            }

            return true;
        }

        internal async Task<Tb_PrequalLeilaoConfig> GetCachePrequalConfig(IPrequalRepository repo, IMemoryCache memCache, int fkCompany)
        {
            var cacheKey = TOKEN_CACHE_LEILAO_CONFIG + fkCompany;

            if (memCache.TryGetValue(cacheKey, out Tb_PrequalLeilaoConfig cached))
            {
                return cached;
            }

            var config = repo.GetPrequalLeilaoConfig(fkCompany);

            memCache.Set(cacheKey, config, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });
            
            return config;
        }

        internal async Task<Tb_CompanyFinanceiro> GetCacheCompanyFinanc(ICompanyRepository repo, IMemoryCache memCache, int fkCompany)
        {
            var cacheKey = TOKEN_CACHE_LEILAO_FINANC + fkCompany;

            if (memCache.TryGetValue(cacheKey, out Tb_CompanyFinanceiro cached))
            {
                return cached;
            }

            var itemDb = repo.GetCompanyFinanceiro(fkCompany);

            memCache.Set(cacheKey, itemDb, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });

            return itemDb;
        }

        internal async Task<Tb_DadosEmpresa> GetCacheDadosEmpresa(IBureauRepository repo, IMemoryCache memCache, string documento)
        {
            var cacheKey = TOKEN_CACHE_LEILAO_EMP + documento;

            if (memCache.TryGetValue(cacheKey, out Tb_DadosEmpresa cached))
            {
                return cached;
            }

            var itemDb = repo.GetDadosEmpresa(documento);

            if (itemDb == null)
            {
                HelperApiClient clientHttp = new(emulateBrowser: true);

                try
                {
                    var taskBrasilAPI = await clientHttp.GetAsync<BrasilAPI_CnpjResponse>(ExternalGateway.endpoint_brasil_api_cpnj + documento);

                    if (taskBrasilAPI.IsSuccess)
                    {
                        itemDb = DadosEmpresa_BrasilAPIMapper.Copy(taskBrasilAPI.Data);
                    }                    
                }
                catch
                {
                    
                }

                if (itemDb != null)
                {
                    repo.InsertDadosEmpresa(itemDb);
                }
            }

            if (itemDb != null)
            {
                memCache.Set(cacheKey, itemDb, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });
            }

            return itemDb;
        }

        private async Task<RejectProposal> TryRejectProposal(
            PropostaDataPrevRequest prop,
            Tb_PrequalLeilaoConfig configPrequal,
            Tb_CompanyFinanceiro configFinanc,
            IBureauRepository bureau,            
            IMemoryCache memCache,
            DateTime currentDate)
        {

            // ----------------------------------------------------
            // descarte imediato pela dataprev
            // ----------------------------------------------------

            if (prop.ElegivelEmprestimo == false)
            {
                OutDto.filter1++;
                return new RejectProposal
                {
                    rejectMsg = DESCARTE_ElegivelEmprestimo,
                    rejectMsgDetalhe = REJECT_MSG_ElegivelEmprestimo
                };
            }

            // ----------------------------------------------------
            // campos específicos de pessoa física L1
            // ----------------------------------------------------

            if (configPrequal.bEmpregadorCnpj == true && prop.InscricaoEmpregador.Codigo == 1)
            {
                OutDto.filter2++;

                return new RejectProposal
                {
                    rejectMsg = DESCARTE_EmpregadorCnpj,
                    rejectMsgDetalhe = REJECT_MSG_InscricaoEmpregadorCNPJ
                };
            }

            if (configPrequal.bEmpregadorCpf == true && prop.InscricaoEmpregador.Codigo == 2)
            {
                OutDto.filter3++;

                return new RejectProposal
                {
                    rejectMsg = DESCARTE_EmpregadorCpf,
                    rejectMsgDetalhe = REJECT_MSG_InscricaoEmpregadorCPF
                };
            }

            if (configPrequal.bPep == true && prop.PessoaExpostaPoliticamente != null)
            {
                OutDto.filter4++;

                return new RejectProposal
                {
                    rejectMsg = DESCARTE_Pep,
                    rejectMsgDetalhe = REJECT_MSG_Pep
                };
            }

            if (configPrequal.bAlertaAvisoPrevio == true && prop.Alertas != null && prop.Alertas.Count > 0 && prop.Alertas[0].TipoAlerta.Codigo == 1)
            {
                OutDto.filter5++;

                return new RejectProposal
                {
                    rejectMsg = DESCARTE_AlertaAvisoPrevio,
                    rejectMsgDetalhe = REJECT_MSG_AlertaAvisoPrevio
                };
            }

            if (configPrequal.bAlertaSaude == true && prop.Alertas != null && prop.Alertas.Count > 0 && prop.Alertas[0].TipoAlerta.Codigo == 2)
            {
                OutDto.filter6++;

                return new RejectProposal
                {
                    rejectMsg = DESCARTE_AlertaSaude,
                    rejectMsgDetalhe = REJECT_MSG_AlertaSaude
                };
            }

            if (prop.ValorLiberado < configPrequal.vrLibMin)
            {
                OutDto.filter7++;

                return new RejectProposal
                {
                    rejectMsg = DESCARTE_ValorLiberadoMin,
                    rejectMsgDetalhe = $"{REJECT_MSG_ValorLiberado}{prop.ValorLiberado}{REJECT_MSG_MINUS}{configPrequal.vrLibMin}"
                };
            }

            if (prop.ValorLiberado > configPrequal.vrLibMax)
            {
                OutDto.filter8++;

                return new RejectProposal
                {
                    rejectMsg = DESCARTE_ValorLiberadoMax,
                    rejectMsgDetalhe = $"{REJECT_MSG_ValorLiberado}{prop.ValorLiberado}{REJECT_MSG_PLUS}{configPrequal.vrLibMax}"
                };
            }

            if (configPrequal.vrMargemMin > 0 && prop.MargemDisponivel < configPrequal.vrMargemMin)
            {
                OutDto.filter9++;

                return new RejectProposal
                {
                    rejectMsg = DESCARTE_MargemDisponivelMin,
                    rejectMsgDetalhe = $"{REJECT_MSG_MargemDisponivel}{prop.MargemDisponivel}{REJECT_MSG_MINUS}{configPrequal.vrMargemMin}"
                };
            }

            if (configPrequal.vrMargemMax > 0 && prop.MargemDisponivel > configPrequal.vrMargemMax)
            {
                OutDto.filter10++;

                return new RejectProposal
                {
                    rejectMsg = DESCARTE_MargemDisponivelMax,
                    rejectMsgDetalhe = $"{REJECT_MSG_MargemDisponivel}{prop.MargemDisponivel}{REJECT_MSG_PLUS}{configPrequal.vrMargemMax}"
                };
            }

            if (configPrequal.nuParcMin > 0 && prop.NroParcelas < configPrequal.nuParcMin)
            {
                OutDto.filter11++;

                return new RejectProposal
                {
                    rejectMsg = DESCARTE_NroParcelasMin,
                    rejectMsgDetalhe = $"{REJECT_MSG_NroParcelas}{prop.NroParcelas}{REJECT_MSG_MINUS}{configPrequal.nuParcMin}"
                };
            }

            if (configPrequal.nuParcMax > 0 && prop.NroParcelas > configPrequal.nuParcMax)
            {
                OutDto.filter12++;

                return new RejectProposal
                {
                    rejectMsg = DESCARTE_NroParcelasMax,
                    rejectMsgDetalhe = $"{REJECT_MSG_NroParcelas}{prop.NroParcelas}{REJECT_MSG_PLUS}{configPrequal.nuParcMax}"
                };
            }

            if (configPrequal.nuIdadeMin > 0 || configPrequal.nuIdadeMax > 0)
            {
                var dataNascimento = DateTime.ParseExact(prop.DataNascimento, DATE_FORMAT, InvariantCulture);
                var idade = (int)currentDate.Subtract(dataNascimento).TotalDays / 365;

                if (configPrequal.nuIdadeMin > 0 && idade < configPrequal.nuIdadeMin)
                {
                    OutDto.filter13++;

                    return new RejectProposal
                    {
                        rejectMsg = DESCARTE_IdadeMin,
                        rejectMsgDetalhe = $"{REJECT_MSG_DataNascimento}{idade}{REJECT_MSG_ANOS}{REJECT_MSG_MINUS}{configPrequal.nuIdadeMin}"
                    };
                }

                if (configPrequal.nuIdadeMax > 0 && idade > configPrequal.nuIdadeMax)
                {
                    OutDto.filter14++;

                    return new RejectProposal
                    {
                        rejectMsg = DESCARTE_IdadeMax,
                        rejectMsgDetalhe = $"{REJECT_MSG_DataNascimento}{idade}{REJECT_MSG_ANOS}{REJECT_MSG_PLUS}{configPrequal.nuIdadeMax}"
                    };
                }
            }

            if (configPrequal.nuMesesAdmissaoMin > 0 || configPrequal.nuMesesAdmissaoMax > 0)
            {
                var dataAdmissao = DateTime.ParseExact(prop.DataAdmissao, DATE_FORMAT, InvariantCulture);
                var mesesAdmissao = (int)currentDate.Subtract(dataAdmissao).TotalDays / 30;

                if (configPrequal.nuMesesAdmissaoMin > 0 && mesesAdmissao < configPrequal.nuMesesAdmissaoMin)
                {
                    OutDto.filter15++;

                    return new RejectProposal
                    {
                        rejectMsg = DESCARTE_MesesAdmissaoMin,
                        rejectMsgDetalhe = $"{REJECT_MSG_DataAdmissao}{mesesAdmissao}{REJECT_MSG_MESES}{REJECT_MSG_MINUS}{configPrequal.nuMesesAdmissaoMin}"
                    };
                }

                if (configPrequal.nuMesesAdmissaoMax > 0 && mesesAdmissao > configPrequal.nuMesesAdmissaoMax)
                {
                    OutDto.filter16++;

                    return new RejectProposal
                    {
                        rejectMsg = DESCARTE_MesesAdmissaoMax,
                        rejectMsgDetalhe = $"{REJECT_MSG_DataAdmissao}{mesesAdmissao}{REJECT_MSG_MESES}{REJECT_MSG_PLUS}{configPrequal.nuMesesAdmissaoMax}"
                    };
                }
            }

            // ----------------------------------
            // campos específicos de empresa L2
            // ----------------------------------

            if (configFinanc.bActiveSubL2 == true)
            {
                var doc = prop.NumeroInscricaoEmpregador.ToString().PadLeft(14, '0');

                var itemDbDadosEmpresa = await GetCacheDadosEmpresa(bureau, memCache, doc);
                                
                if (itemDbDadosEmpresa == null)
                {
                    return new RejectProposal
                    {
                        rejectMsg = REJECT_MSG_L2_DADOS_NF,
                        rejectMsgDetalhe = REJECT_MSG_L2_DADOS_NF1 + doc + REJECT_MSG_L2_DADOS_NF2
                    };
                }

                if (configPrequal.nuMesesAberturaEmpresaMin > 0)
                {
                    if (itemDbDadosEmpresa.dtAberturaL1 != null)
                    {
                        var meses = DateTime.Now.Subtract(itemDbDadosEmpresa.dtAberturaL1.Value).TotalDays / 30;

                        if (meses < configPrequal.nuMesesAberturaEmpresaMin)
                        {
                            OutDto.filter17++;

                            return new RejectProposal
                            {
                                rejectMsg = REJECT_MSG_MesesAberturaEmpresaMin,
                                rejectMsgDetalhe = REJECT_MSG_Det_MesesAberturaEmpresaMin,
                            };
                        }
                    }
                }
            }

            return null;
        }
    }
}
