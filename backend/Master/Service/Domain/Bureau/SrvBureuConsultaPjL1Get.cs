using Master.Entity.Database.Domain.Bureau;
using Master.Entity.Dto.External;
using Master.Entity.Dto.Response.Domain.Bureau;
using Master.Entity.Gateway;
using Master.Service.Base;
using Master.Service.Base.Infra.Extensions;
using Master.Service.Base.Infra.Helper;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace Master.Service.Domain.Bureau
{
    public class SrvBureuConsultaPjL1Get : SrvBase
    {
        public const string TOKEN_CACHE_BureauConsultaPJL1 = "TOKEN_CACHE_BureauConsultaPJL1";

        public DtoResponseBureauConsultaPJL1 OutDto = null;

        public async Task<bool> Exec(IMemoryCache memCache, string documento)
        {
            if (string.IsNullOrEmpty(documento))
            {
                errorCode = "E1";
                errorMessage = "documento não disponível";
                return false;
            }

            documento = documento.Trim().Replace("-", "").Replace(".","");

            if (documento.Length != 14)
            {
                errorCode = "E2";
                errorMessage = "documento não corresponde a um cnpj";
                return false;
            }

            var cacheKey = TOKEN_CACHE_BureauConsultaPJL1 + documento;

            if (memCache.TryGetValue(cacheKey, out DtoResponseBureauConsultaPJL1 cached))
            {
                OutDto = cached;
                return true;
            }

            StartDatabase(Network);

            var repo = RepoBureau();

            var itemDb = repo.GetDadosEmpresa(documento);

            if (itemDb == null || (itemDb!= null && itemDb.dtExpire < DateTime.Now))
            {
                var client = new HelperApiClient();

                var taskBrasilAPI = await client.GetAsync<BrasilAPI_CnpjResponse>(ExternalGateway.endpoint_brasil_api_cpnj + documento);

                if (taskBrasilAPI.IsSuccess)
                {
                    var brasilApi = taskBrasilAPI.Data;

                    var cad = itemDb == null;

                    itemDb = new Tb_DadosEmpresa
                    {
                        dtExpire = DateTime.Now.AddMonths(3),
                        dtAberturaL1 = brasilApi.DataInicioAtividade.ToDateTimeBr(),
                        stCNPJ = documento,
                        stSituacaoCadL1 = brasilApi.DescricaoSituacaoCadastral,
                        stSituacaoCadMotivL1 = brasilApi.DescricaoMotivoSituacaoCadastral,
                        stNomeL1 = brasilApi.RazaoSocial,
                        stFantasiaL1 = brasilApi.NomeFantasia,
                        stPorteL1 = brasilApi.Porte,
                        stMunicipioL1 = brasilApi.Municipio,
                        stUfL1 = brasilApi.Uf,
                        stCepL1 = brasilApi.Cep,
                        stCnaeL1 = brasilApi.CnaeFiscal.ToString(),
                        stCnaeDescL1 = brasilApi.CnaeFiscalDescricao,
                        stCdNatJurL1 = brasilApi.NaturezaJuridica,
                    };

                    if (cad)
                        repo.InsertDadosEmpresa(itemDb);
                    else
                        repo.UpdateDadosEmpresa(itemDb);
                }
            }

            OutDto = new DtoResponseBureauConsultaPJL1
            {
                DataAbertura = itemDb.dtAberturaL1,
                CNPJ = itemDb.stCNPJ,
                SituacaoCad = itemDb.stSituacaoCadL1,
                SituacaoCadMotiv = itemDb.stSituacaoCadMotivL1,
                Nome = itemDb.stNomeL1,
                Fantasia = itemDb.stFantasiaL1,
                Porte = itemDb.stPorteL1,
                Municipio = itemDb.stMunicipioL1,
                Uf = itemDb.stUfL1,
                Cep = itemDb.stCepL1,
                Cnae = itemDb.stCnaeL1,
                CnaeDesc = itemDb.stCnaeDescL1,
                CdNatJur = itemDb.stCdNatJurL1
            };

            memCache.Set(cacheKey, OutDto, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(360)
            });

            return true;
        }
    }
}

