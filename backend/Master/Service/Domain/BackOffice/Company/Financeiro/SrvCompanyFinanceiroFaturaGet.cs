using Master.Entity.Dto.Infra;
using Master.Entity.Dto.Request.Domain.BackOffice.Company;
using Master.Entity.Dto.Response.Domain.BackOffice.Company;
using Master.Service.Base;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Master.Service.Domain.BackOffice.Company
{
    public class SrvCompanyFinanceiroFaturaGet : SrvBase
    {
        public DtoResponseCompanyFinanceiroFaturaGet OutDto = null;

        public async Task<bool> Exec(DtoAuthenticatedUser user, DtoRequestCompanyFinanceiroFatura request)
        {
            request.nuYear ??= DateTime.Now.Year;
            request.nuMonth ??= DateTime.Now.Month;

            try
            {
                StartDatabase(Network);

                var fkCompany = user.fkCompany;

                var _repoFinanc = RepoCompany();
                var _repoPrequal = RepoPrequal();

                var itemDbFinanceiro = _repoFinanc.GetCompanyFinanceiro(fkCompany);

                // -- L1 -------------

                var logs = _repoPrequal.GetLogs(fkCompany, (int)request.nuYear, (int)request.nuMonth);

                var sitFatura = "em aberto";

                var qtdTransL1 = logs.Count;
                var qtdTransItensL1 = (int) logs.Sum ( y=> y.nuTotProcs );

                var vrCalcTrans = qtdTransL1 * itemDbFinanceiro.vrL1Transaction;
                var vrCalcTransItem = qtdTransItensL1 * itemDbFinanceiro.vrL1TransactionItem;

                OutDto = new DtoResponseCompanyFinanceiroFaturaGet
                {
                    ano = request.nuYear.ToString(),
                    mes = request.nuMonth.ToString(),
                    valorAssinaturaL1 = Math.Round(itemDbFinanceiro.vrSubscriptionL1, 2),
                    valorPrecoTransacaoItemL1 = Math.Round(itemDbFinanceiro.vrL1TransactionItem, 2),
                    valorPrecoTransacaoL1 = Math.Round(itemDbFinanceiro.vrL1Transaction, 2),
                    qtdTransacaoL1 = qtdTransL1,
                    qtdTransacaoItemL1 = qtdTransItensL1,
                    valorCalcTransacaoL1 = vrCalcTrans,
                    valorCalcTransacaoItemL1 = vrCalcTransItem,
                    valorTotal = Math.Round(vrCalcTrans + vrCalcTransItem + itemDbFinanceiro.vrSubscriptionL1, 2),
                    situacao = sitFatura
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
