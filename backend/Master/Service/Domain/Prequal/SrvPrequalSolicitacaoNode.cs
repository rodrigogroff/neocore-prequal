using Master.Entity.Database.Domain.Prequal;
using Master.Entity.Dto.Request.Domain.Prequal;
using Master.Entity.Dto.Response.Domain.Bureau;
using Master.Entity.Dto.Response.Domain.Prequal;
using Master.Service.Base;
using Master.Service.Base.Infra.Mappers;
using Master.Service.Domain.Bureau;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Master.Service.Domain.Prequal
{
    public class SrvPrequalSolicitacaoNode : SrvBase
    {
        public const string
            TOKEN_CACHE_LEILAO_CONFIG = "PrequalLeilaoConfig",
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
            DATE_FORMAT = "ddMMyyyy";

        private static readonly CultureInfo InvariantCulture = CultureInfo.InvariantCulture;

        public DtoResponsePrequalSolicitacoesNode OutDto;

        public async Task<bool> Exec(IMemoryCache memCache, DtoRequestPrequalSolicitacoesNode request)
        {
            try
            {

                var propostas = request.propostas;
                var count = propostas.Count;

                OutDto = new DtoResponsePrequalSolicitacoes
                {
                    qualificadas = new List<PropostaDataPrevResponse>(count),
                    rejeitadas = new List<PropostaDataPrevResponse>(count)
                };

                if (count == 0)
                {
                    return true;
                }

                var fkCompany = (int)request.fkCompany;
                var configPrequal = await GetPrequalConfig(memCache, fkCompany);
                var currentDate = DateTime.Now;

                for (int i = 0; i < count; i++)
                {
                    var prop = propostas[i];

                    if (TryRejectProposal(prop, configPrequal, currentDate, out var rejectMsg, out var rejectMsgDetalhe))
                    {
                        var rejected = PropostaDataPrevResponseMapper.Copy(prop);
                        rejected._motivoRejeitado = rejectMsg;
                        rejected._detalheRejeitado = rejectMsgDetalhe;
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
            catch (Exception ex)
            {
                
            }

            return true;
        }

        private async Task<Tb_PrequalLeilaoConfig> GetPrequalConfig(IMemoryCache memCache, int fkCompany)
        {
            var cacheKey = TOKEN_CACHE_LEILAO_CONFIG + fkCompany;

            if (memCache.TryGetValue(cacheKey, out Tb_PrequalLeilaoConfig cached))
            {
                return cached;
            }
                        
            StartDatabase(Network);
            var repo = RepoPrequal();
            var config = repo.GetPrequalLeilaoConfig(fkCompany);

            memCache.Set(cacheKey, config, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });
            
            return config;
        }

        private bool TryRejectProposal(
            PropostaDataPrevRequest prop,
            Tb_PrequalLeilaoConfig config,
            DateTime currentDate,
            out string rejectMsg,
            out string rejectMsgDetalhe)
        {
            rejectMsg = null;
            rejectMsgDetalhe = null;

            if (prop.ElegivelEmprestimo == false)
            {
                rejectMsg = DESCARTE_ElegivelEmprestimo;
                rejectMsgDetalhe = REJECT_MSG_ElegivelEmprestimo;
                return true;
            }

            if (config.bEmpregadorCnpj == true && prop.InscricaoEmpregador.Codigo == 1)
            {
                rejectMsg = DESCARTE_EmpregadorCnpj;
                rejectMsgDetalhe = REJECT_MSG_InscricaoEmpregadorCNPJ;
                return true;
            }

            if (config.bEmpregadorCpf == true && prop.InscricaoEmpregador.Codigo == 2)
            {
                rejectMsg = DESCARTE_EmpregadorCpf;
                rejectMsgDetalhe = REJECT_MSG_InscricaoEmpregadorCPF;
                return true;
            }

            if (config.bPep == true && prop.PessoaExpostaPoliticamente != null)
            {
                rejectMsg = DESCARTE_Pep;
                rejectMsgDetalhe = REJECT_MSG_Pep;
                return true;
            }

            // ----------------------------------
            // campos específicos de empresa
            // ----------------------------------

            /*
            if (config.bSimples == true)
            {
                rejectMsg = DESCARTE_Pep;
                rejectMsgDetalhe = REJECT_MSG_Pep;
                return true;
            }

            if (config.bMei == true)
            {
                rejectMsg = DESCARTE_Pep;
                rejectMsgDetalhe = REJECT_MSG_Pep;
                return true;
            }
            */

            if (config.bAlertaAvisoPrevio == true && prop.Alertas != null && prop.Alertas.Count > 0 && prop.Alertas[0].TipoAlerta.Codigo == 1)
            {
                rejectMsg = DESCARTE_AlertaAvisoPrevio;
                rejectMsgDetalhe = REJECT_MSG_AlertaAvisoPrevio;
                return true;
            }

            if (config.bAlertaSaude == true && prop.Alertas != null && prop.Alertas.Count > 0 && prop.Alertas[0].TipoAlerta.Codigo == 2)
            {
                rejectMsg = DESCARTE_AlertaSaude;
                rejectMsgDetalhe = REJECT_MSG_AlertaSaude;
                return true;
            }

            if (prop.ValorLiberado < config.vrLibMin)
            {
                rejectMsg = DESCARTE_ValorLiberadoMin;
                rejectMsgDetalhe = $"{REJECT_MSG_ValorLiberado}{prop.ValorLiberado}{REJECT_MSG_MINUS}{config.vrLibMin}";
                return true;
            }
            if (prop.ValorLiberado > config.vrLibMax)
            {
                rejectMsg = DESCARTE_ValorLiberadoMax;
                rejectMsgDetalhe = $"{REJECT_MSG_ValorLiberado}{prop.ValorLiberado}{REJECT_MSG_PLUS}{config.vrLibMax}";
                return true;
            }

            if (config.vrMargemMin > 0 && prop.MargemDisponivel < config.vrMargemMin)
            {
                rejectMsg = DESCARTE_MargemDisponivelMin;
                rejectMsgDetalhe = $"{REJECT_MSG_MargemDisponivel}{prop.MargemDisponivel}{REJECT_MSG_MINUS}{config.vrMargemMin}";
                return true;
            }
            if (config.vrMargemMax > 0 && prop.MargemDisponivel > config.vrMargemMax)
            {
                rejectMsg = DESCARTE_MargemDisponivelMax;
                rejectMsgDetalhe = $"{REJECT_MSG_MargemDisponivel}{prop.MargemDisponivel}{REJECT_MSG_PLUS}{config.vrMargemMax}";
                return true;
            }

            if (config.nuParcMin > 0 && prop.NroParcelas < config.nuParcMin)
            {
                rejectMsg = DESCARTE_NroParcelasMin;
                rejectMsgDetalhe = $"{REJECT_MSG_NroParcelas}{prop.NroParcelas}{REJECT_MSG_MINUS}{config.nuParcMin}";
                return true;
            }
            if (config.nuParcMax > 0 && prop.NroParcelas > config.nuParcMax)
            {
                rejectMsg = DESCARTE_NroParcelasMax;
                rejectMsgDetalhe = $"{REJECT_MSG_NroParcelas}{prop.NroParcelas}{REJECT_MSG_PLUS}{config.nuParcMax}";
                return true;
            }

            if (config.nuIdadeMin > 0 || config.nuIdadeMax > 0)
            {
                var dataNascimento = DateTime.ParseExact(prop.DataNascimento, DATE_FORMAT, InvariantCulture);
                var idade = (int)currentDate.Subtract(dataNascimento).TotalDays / 365;

                if (config.nuIdadeMin > 0 && idade < config.nuIdadeMin)
                {
                    rejectMsg = DESCARTE_IdadeMin;
                    rejectMsgDetalhe = $"{REJECT_MSG_DataNascimento}{idade}{REJECT_MSG_ANOS}{REJECT_MSG_MINUS}{config.nuIdadeMin}";
                    return true;
                }
                if (config.nuIdadeMax > 0 && idade > config.nuIdadeMax)
                {
                    rejectMsg = DESCARTE_IdadeMax;
                    rejectMsgDetalhe = $"{REJECT_MSG_DataNascimento}{idade}{REJECT_MSG_ANOS}{REJECT_MSG_PLUS}{config.nuIdadeMax}";
                    return true;
                }
            }

            if (config.nuMesesAdmissaoMin > 0 || config.nuMesesAdmissaoMax > 0)
            {
                var dataAdmissao = DateTime.ParseExact(prop.DataAdmissao, DATE_FORMAT, InvariantCulture);
                var mesesAdmissao = (int)currentDate.Subtract(dataAdmissao).TotalDays / 30;

                if (config.nuMesesAdmissaoMin > 0 && mesesAdmissao < config.nuMesesAdmissaoMin)
                {
                    rejectMsg = DESCARTE_MesesAdmissaoMin;
                    rejectMsgDetalhe = $"{REJECT_MSG_DataAdmissao}{mesesAdmissao}{REJECT_MSG_MESES}{REJECT_MSG_MINUS}{config.nuMesesAdmissaoMin}";
                    return true;
                }
                if (config.nuMesesAdmissaoMax > 0 && mesesAdmissao > config.nuMesesAdmissaoMax)
                {
                    rejectMsg = DESCARTE_MesesAdmissaoMax;
                    rejectMsgDetalhe = $"{REJECT_MSG_DataAdmissao}{mesesAdmissao}{REJECT_MSG_MESES}{REJECT_MSG_PLUS}{config.nuMesesAdmissaoMax}";
                    return true;
                }
            }

            return false;
        }
    }
}
