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
            request.ano ??= DateTime.Now.Year;
            request.mes ??= DateTime.Now.Month;

            try
            {
                StartDatabase(Network);

                var fkCompany = user.fkCompany;
                var year = (int)request.ano;
                var month = (int)request.mes;

                var repoC = RepoCompany();
                
                var itemDbFatura = repoC.GetCompanyFatura(fkCompany, year, month);

                if (itemDbFatura != null)
                {
                    OutDto = new DtoResponseCompanyFinanceiroFaturaGet
                    {
                        ano = year.ToString(),
                        mes = month.ToString(),
                        
                        valorAssinaturaL1 = Math.Round((double)itemDbFatura.vrSubscriptionL1, 2),
                        valorPrecoTransacaoItemL1 = Math.Round((double)itemDbFatura.vrL1TransactionItem, 2),
                        valorPrecoTransacaoL1 = Math.Round((double)itemDbFatura.vrL1Transaction, 2),
                        qtdTransacaoL1 = (int)itemDbFatura.nuQtdL1Trans,
                        qtdTransacaoItemL1 = (int)itemDbFatura.nuQtdL1TransItem,
                        valorCalcTransacaoL1 = Math.Round((double)itemDbFatura.vrL1TransactionTotal, 2),
                        valorCalcTransacaoItemL1 = Math.Round((double)itemDbFatura.vrL1TransactionItemTotal, 2),

                        valorAssinaturaL2 = Math.Round((double)itemDbFatura.vrSubscriptionL2, 2),
                        valorPrecoTransacaoItemL2 = Math.Round((double)itemDbFatura.vrL2TransactionItem, 2),
                        valorPrecoTransacaoL2 = Math.Round((double)itemDbFatura.vrL2Transaction, 2),
                        qtdTransacaoL2 = (int)itemDbFatura.nuQtdL2Trans,
                        qtdTransacaoItemL2 = (int)itemDbFatura.nuQtdL2TransItem,
                        valorCalcTransacaoL2 = Math.Round((double)itemDbFatura.vrL2TransactionTotal, 2),
                        valorCalcTransacaoItemL2 = Math.Round((double)itemDbFatura.vrL2TransactionItemTotal, 2),

                        valorSubTotal = Math.Round((double)itemDbFatura.vrSubTotal, 2),
                        valorImpostos = Math.Round((double)itemDbFatura.vrImpostos, 2),
                        valorTotal = Math.Round((double)itemDbFatura.vrTotal, 2),
                        situacao = "fechada"
                    };
                    return true;
                }

                var repoPrequal = RepoPrequal();

                var funcFatura = new FunctionFaturaMensal();

                var fatura = funcFatura.ObterFaturaMensal(repoC, repoPrequal, fkCompany, year, month);

                OutDto = new DtoResponseCompanyFinanceiroFaturaGet
                {
                    ano = year.ToString(),
                    mes = month.ToString(),

                    valorAssinaturaL1 = Math.Round((double)fatura.vrSubscriptionL1, 2),
                    valorPrecoTransacaoItemL1 = Math.Round((double)fatura.vrL1TransactionItem, 2),
                    valorPrecoTransacaoL1 = Math.Round((double)fatura.vrL1Transaction, 2),
                    qtdTransacaoL1 = (int) fatura.nuQtdL1Trans,
                    qtdTransacaoItemL1 = (int) fatura.nuQtdL1TransItem,
                    valorCalcTransacaoL1 = Math.Round((double)fatura.vrL1TransactionTotal, 2),
                    valorCalcTransacaoItemL1 = Math.Round((double)fatura.vrL1TransactionItemTotal, 2),

                    valorAssinaturaL2 = Math.Round((double)fatura.vrSubscriptionL2, 2),
                    valorPrecoTransacaoItemL2 = Math.Round((double)fatura.vrL2TransactionItem, 2),
                    valorPrecoTransacaoL2 = Math.Round((double)fatura.vrL2Transaction, 2),
                    qtdTransacaoL2 = (int)fatura.nuQtdL2Trans,
                    qtdTransacaoItemL2 = (int)fatura.nuQtdL2TransItem,
                    valorCalcTransacaoL2 = Math.Round((double)fatura.vrL2TransactionTotal, 2),
                    valorCalcTransacaoItemL2 = Math.Round((double)fatura.vrL2TransactionItemTotal, 2),

                    valorSubTotal = Math.Round((double)fatura.vrSubTotal, 2),
                    valorImpostos = Math.Round((double)fatura.vrImpostos, 2),
                    valorTotal = Math.Round((double)fatura.vrTotal,2),
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
