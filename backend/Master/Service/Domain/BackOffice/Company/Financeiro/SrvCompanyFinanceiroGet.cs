using Master.Entity.Dto.Infra;
using Master.Entity.Dto.Response.Domain.BackOffice.Company;
using Master.Service.Base;
using System;
using System.Threading.Tasks;

namespace Master.Service.Domain.BackOffice.Company
{
    public class SrvCompanyFinanceiroGet : SrvBase
    {
        public DtoResponseCompanyFinanceiroGet OutDto = null;
        
        public async Task<bool> Exec(DtoAuthenticatedUser user)
        {
            try
            {
                StartDatabase(Network);

                var _repo = RepoCompany();

                var itemDb = _repo.GetCompanyFinanceiro(user.fkCompany);

                OutDto = new DtoResponseCompanyFinanceiroGet
                {
                    valorAssinaturaL1 = itemDb.vrSubscriptionL1,
                    valorTransacaoL1 = itemDb.vrL1Transaction,
                    valorTransacaoItemL1 = itemDb.vrL1TransactionItem,
                };

                return true;
            }
            catch (Exception ex) 
            {
                this.errorCode = "FAIL";
                this.errorMessage = ex.ToString();

                return false;
            }
        }
    }
}
