using Master.Entity.Database.Domain.Bureau;
using Master.Entity.Dto.External;
using Master.Service.Base.Infra.Extensions;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Master.Service.Base.Infra.Mappers
{
    [ExcludeFromCodeCoverage]
    public static class DadosEmpresa_BrasilAPIMapper
    {
        public static Tb_DadosEmpresa Copy (BrasilAPI_CnpjResponse brasilApi)
        {
            var itemDb = new Tb_DadosEmpresa
            {
                dtExpire = DateTime.Now.AddMonths(3),
                dtAberturaL1 = brasilApi.DataInicioAtividade.ToDateTimeBr(),
                stCNPJ = brasilApi.Cnpj,
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

            return itemDb;
        }

    }
}
