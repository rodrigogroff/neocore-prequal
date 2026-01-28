using Master.Entity.Dto.Infra;
using Master.Entity.Dto.Request.Domain.Prequal;
using Master.Service.Base;
using System.Threading.Tasks;

namespace Master.Service.Domain.Prequal
{
    public class SrvPrequalSolicitacaoLeilaoConfigSet : SrvBase
    {
        public async Task<bool> Exec(DtoAuthenticatedUser user, DtoRequestPrequalConfigLeilao request)
        {
            StartDatabase(Network);

            var repo = RepoPrequal();

            var itemDb = repo.GetPrequalLeilaoConfig(user.fkCompany);

            itemDb.bAlertaAvisoPrevio = request.DescarteAvisoPrevio;
            itemDb.bAlertaSaude = request.DescarteAvisoSaude;
            itemDb.bPep = request.DescartePep;
            itemDb.bEmpregadorCnpj = request.DescarteEmpregadorCnpj;
            itemDb.bEmpregadorCpf = request.DescarteEmpregadorCpf;
            itemDb.vrLibMin = request.RangeValorLiberadoMin;
            itemDb.vrLibMax = request.RangeValorLiberadoMax;
            itemDb.vrMargemMax = request.RangeValorMargemMax;
            itemDb.vrMargemMin = request.RangeValorMargemMin;
            itemDb.nuParcMin = request.RangeParcelasMin;
            itemDb.nuParcMax = request.RangeParcelasMax;
            itemDb.nuIdadeMax = request.RangeIdadeMax;
            itemDb.nuIdadeMin = request.RangeIdadeMin;
            itemDb.nuMesesAdmissaoMax = request.RangeMesesAdmissaoMax;
            itemDb.nuMesesAdmissaoMin = request.RangeMesesAdmissaoMin;
            itemDb.nuMesesAberturaEmpresaMin = request.MesesAberturaEmpresaMin;

            repo.UpdatePrequalLeilaoConfig(itemDb);

            return true;
        }
    }
}
