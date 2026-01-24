using Master.Entity.Database.Domain.Prequal;
using Master.Entity.Dto.Request.Domain.Prequal;
using Master.Entity.Dto.Response.Domain.Prequal;
using Master.Service.Base;
using Master.Service.Mappers;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace Master.Service.Domain.Prequal
{
    public class SrvPrequalSolicitacaoNode : SrvBase
    {
        public const string TOKEN_CACHE_LEILAO_CONFIG = "PrequalLeilaoConfig";

        public DtoResponsePrequalSolicitacoesNode OutDto;

        public async Task<bool> Exec(IMemoryCache memCache, DtoRequestPrequalSolicitacoesNode request)
        {
            OutDto = new DtoResponsePrequalSolicitacoes
            {
                qualificadas = [],
                rejeitadas = [],
            };

            if (request.propostas.Count == 0)
            {
                return true;
            }

            var fkCompany = (int) request.fkCompany;

            Tb_PrequalLeilaoConfig configPrequal;

            if (!memCache.TryGetValue(TOKEN_CACHE_LEILAO_CONFIG + fkCompany, out var cachedConfig))
            {
                StartDatabase(Network);

                var repo = RepoPrequal();

                configPrequal = repo.GetPrequalLeilaoConfig(fkCompany);

                memCache.Set(TOKEN_CACHE_LEILAO_CONFIG + fkCompany, configPrequal, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });
            }
            else
            {
                configPrequal = cachedConfig as Tb_PrequalLeilaoConfig;
            }

            for (int i = 0; i < request.propostas.Count; i++)
            {
                PropostaDataPrevRequest _prop = request.propostas[i];
                
                string rejectMsg = null;

                if (rejectMsg == null && _prop.ElegivelEmprestimo == false)
                {
                    rejectMsg = "!ElegivelEmprestimo";
                }
                    
                if (rejectMsg == null && configPrequal.bEmpregadorCnpj == true)
                    if (_prop.InscricaoEmpregador.Codigo != 1)
                        rejectMsg = "!EmpregadorCnpj";

                if (rejectMsg == null && configPrequal.bEmpregadorCpf == true)
                    if (_prop.InscricaoEmpregador.Codigo == 2)
                        rejectMsg = "!EmpregadorCpf";

                if (rejectMsg == null && configPrequal.bPep == true)
                    if (_prop.PessoaExpostaPoliticamente != null)
                        rejectMsg = "!Pep";

                if (rejectMsg == null)
                {
                    if (_prop.ValorLiberado < configPrequal.vrLibMin)
                        rejectMsg = "ValorLiberado < Min";

                    if (_prop.ValorLiberado > configPrequal.vrLibMax)
                        rejectMsg = "ValorLiberado > Max";
                }

                if (rejectMsg == null)
                {
                    if (_prop.MargemDisponivel < configPrequal.vrMargemMin)
                        rejectMsg = "MargemDisponivel < Min";
                    
                    if (_prop.MargemDisponivel > configPrequal.vrMargemMax)
                        rejectMsg = "MargemDisponivel > Max";
                }

                if (rejectMsg == null)
                {
                    if (_prop.NroParcelas < configPrequal.nuParcMin)
                        rejectMsg = "NroParcelas < Min";

                    if (_prop.NroParcelas > configPrequal.nuParcMax)
                        rejectMsg = "NroParcelas > Max";
                }

                if (rejectMsg == null)
                {
                    // idade ddMMyyy
                    var idade = DateTime.Now.Subtract(DateTime.ParseExact(_prop.DataNascimento, "ddMMyyyy", null)).TotalDays / 365;

                    if (idade < configPrequal.nuIdadeMin)
                        rejectMsg = "Idade < Min";

                    if (idade > configPrequal.nuIdadeMax)
                        rejectMsg = "Idade > Max";
                }

                if (rejectMsg == null)
                {
                    // meses admissão
                    var mesesAdmissao = DateTime.Now.Subtract(DateTime.ParseExact(_prop.DataAdmissao, "ddMMyyyy", null)).TotalDays / 30;

                    if (mesesAdmissao < configPrequal.nuMesesAdmissaoMin)
                        rejectMsg = "Meses Admissao < Min";

                    if (mesesAdmissao > configPrequal.nuMesesAdmissaoMax)
                        rejectMsg = "Meses Admissao > Max";
                }

                var cpy = PropostaDataPrevResponseMapper.Copy(_prop);

                if (!string.IsNullOrEmpty(rejectMsg))
                {
                    cpy._motivoRejeitado = rejectMsg;
                    OutDto.rejeitadas.Add(cpy);
                }
                else
                {
                    cpy._motivoRejeitado = null;
                    OutDto.qualificadas.Add(cpy);
                }
            }

            return true;
        }
    }
}
