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
                    subL1 = itemDb.bActiveSubL1 == true,
                    subL1str = "Pre-eligibilidade de propostas iniciais de leilão CTPS",
                    subL2 = itemDb.bActiveSubL2 == true,
                    subL2str = "Pre-eligibilidade avançada de leilão CTPS (validar empresa)",

                    valorAssinaturaL1 = Math.Round((double)itemDb.vrSubscriptionL1,2),
                    valorTransacaoL1 = Math.Round((double)itemDb.vrL1Transaction, 2),
                    valorTransacaoItemL1 = Math.Round((double)itemDb.vrL1TransactionItem, 2),

                    valorAssinaturaL2 = Math.Round((double)itemDb.vrSubscriptionL2, 2),
                    valorTransacaoL2 = Math.Round((double)itemDb.vrL2Transaction, 2),
                    valorTransacaoItemL2 = Math.Round((double)itemDb.vrL2TransactionItem, 2),
                };

                return true;
            }
            catch (Exception ex) 
            {
                return this.LogException(ex, user);
            }
        }
    }
}
