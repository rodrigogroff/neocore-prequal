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
                var year = (int)request.nuYear;
                var month = (int)request.nuMonth;

                var repoFinanc = RepoCompany();
                
                var itemDbFatura = repoFinanc.GetCompanyFatura(fkCompany, year, month);

                if (itemDbFatura != null)
                {
                    OutDto = new DtoResponseCompanyFinanceiroFaturaGet
                    {
                        ano = year.ToString(),
                        mes = month.ToString(),
                        valorAssinaturaL1 = Math.Round(itemDbFatura.vrSubscriptionL1, 2),
                        valorPrecoTransacaoItemL1 = Math.Round(itemDbFatura.vrL1TransactionItem, 2),
                        valorPrecoTransacaoL1 = Math.Round(itemDbFatura.vrL1Transaction, 2),
                        qtdTransacaoL1 = (int)itemDbFatura.nuQtdL1Trans,
                        qtdTransacaoItemL1 = (int)itemDbFatura.nuQtdL1TransItem,
                        valorCalcTransacaoL1 = itemDbFatura.vrL1TransactionTotal,
                        valorCalcTransacaoItemL1 = itemDbFatura.vrL1TransactionItemTotal,
                        valorTotal = itemDbFatura.vrTotal,
                        situacao = "fechada"
                    };
                    return true;
                }

                var itemDbFinanceiro = repoFinanc.GetCompanyFinanceiro(fkCompany);

                var repoPrequal = RepoPrequal();

                var logs = repoPrequal.GetLogs(fkCompany, year, month);
                var qtdTransL1 = logs.Count;
                var qtdTransItensL1 = (int)logs.Sum(y => y.nuTotProcs);

                var vrCalcTrans = qtdTransL1 * itemDbFinanceiro.vrL1Transaction;
                var vrCalcTransItem = qtdTransItensL1 * itemDbFinanceiro.vrL1TransactionItem;

                OutDto = new DtoResponseCompanyFinanceiroFaturaGet
                {
                    ano = year.ToString(),
                    mes = month.ToString(),
                    valorAssinaturaL1 = Math.Round(itemDbFinanceiro.vrSubscriptionL1, 2),
                    valorPrecoTransacaoItemL1 = Math.Round(itemDbFinanceiro.vrL1TransactionItem, 2),
                    valorPrecoTransacaoL1 = Math.Round(itemDbFinanceiro.vrL1Transaction, 2),
                    qtdTransacaoL1 = qtdTransL1,
                    qtdTransacaoItemL1 = qtdTransItensL1,
                    valorCalcTransacaoL1 = vrCalcTrans,
                    valorCalcTransacaoItemL1 = vrCalcTransItem,
                    valorTotal = Math.Round(vrCalcTrans + vrCalcTransItem + itemDbFinanceiro.vrSubscriptionL1, 2),
                    situacao = "em aberto"
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