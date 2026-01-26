using Master.Entity.Dto.Infra;
using Master.Entity.Dto.Request.Domain.BackOffice.Company;
using Master.Entity.Dto.Response.Domain.BackOffice.Company;
using Master.Service.Base;
using Master.Service.Base.Infra.Functions;
using System;
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

                var repoC = RepoCompany();
                
                var itemDbFatura = repoC.GetCompanyFatura(fkCompany, year, month);

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

                var repoPrequal = RepoPrequal();

                var funcFatura = new FunctionFaturaMensal();

                var fatura = funcFatura.ObterFatura(repoC, repoPrequal, fkCompany, year, month);

                OutDto = new DtoResponseCompanyFinanceiroFaturaGet
                {
                    ano = year.ToString(),
                    mes = month.ToString(),
                    valorAssinaturaL1 = Math.Round(fatura.vrSubscriptionL1, 2),
                    valorPrecoTransacaoItemL1 = Math.Round(fatura.vrL1TransactionItem, 2),
                    valorPrecoTransacaoL1 = Math.Round(fatura.vrL1Transaction, 2),
                    qtdTransacaoL1 = (int) fatura.nuQtdL1Trans,
                    qtdTransacaoItemL1 = (int) fatura.nuQtdL1TransItem,
                    valorCalcTransacaoL1 = fatura.vrL1TransactionTotal,
                    valorCalcTransacaoItemL1 = fatura.vrL1TransactionItemTotal,
                    valorTotal = fatura.vrTotal,
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
