using Master.Entity.Database.Domain.Prequal;
using Master.Entity.Dto.Infra;
using Master.Entity.Dto.Response.Domain.Prequal;
using Master.Service.Base;
using System;
using System.Threading.Tasks;

namespace Master.Service.Domain.Prequal
{
    public class SrvPrequalSolicitacaoLeilaoConfigGet : SrvBase
    {
        public DtoResponsePrequalConfigLeilao OutDto = null;

        public async Task<bool> Exec(DtoAuthenticatedUser user)
        {
            StartDatabase(Network);

            var repo = RepoPrequal();

            var itemDb = repo.GetPrequalLeilaoConfig(user.fkCompany);

            if (itemDb == null)
            {
                itemDb = new Tb_PrequalLeilaoConfig
                {
                    fkCompany = user.fkCompany,
                    bEmpregadorCnpj = true,
                    bEmpregadorCpf = true,
                    bPep = false,
                    bAlertaAvisoPrevio = true,
                    bAlertaSaude = true,
                    nuIdadeMax = 99,
                    nuIdadeMin = 21,
                    nuMesesAdmissaoMax = 200,
                    nuMesesAdmissaoMin = 9,
                    nuParcMax = 50,
                    nuParcMin = 6,
                    vrLibMax = 200000,
                    vrLibMin = 5000,
                    vrMargemMin = 200,
                    vrMargemMax = 20000,
                };

                itemDb.id = repo.InsertPrequalLeilaoConfig(itemDb, retId: true);
            }

            OutDto = new DtoResponsePrequalConfigLeilao
            {
                DescarteEmpregadorCnpj = itemDb.bEmpregadorCnpj,
                DescarteEmpregadorCpf = itemDb.bEmpregadorCpf,
                DescartePep = itemDb.bPep,
                DescarteAvisoPrevio = itemDb.bAlertaAvisoPrevio,
                DescarteAvisoSaude = itemDb.bAlertaSaude,
                RangeIdadeMax = itemDb.nuIdadeMax,
                RangeIdadeMin = itemDb.nuIdadeMin,
                RangeMesesAdmissaoMax = itemDb.nuMesesAdmissaoMax,
                RangeMesesAdmissaoMin = itemDb.nuMesesAdmissaoMin,
                RangeParcelasMax = itemDb.nuParcMax,
                RangeParcelasMin = itemDb.nuParcMin,
                RangeValorLiberadoMax = itemDb.vrLibMax,
                RangeValorLiberadoMin = itemDb.vrLibMin,
                RangeValorMargemMax = itemDb.vrMargemMax,
                RangeValorMargemMin = itemDb.vrMargemMin,
            };

            return true;
        }
    }
}
