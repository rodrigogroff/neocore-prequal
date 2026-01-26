using Master.Entity.Database.Domain.Company;
using Master.Repository.Domain.Company;
using Master.Repository.Domain.Prequal;
using System;
using System.Linq;

namespace Master.Service.Base.Infra.Functions
{
    public class FunctionFaturaMensal
    {
        public Tb_CompanyFatura ObterFatura (
            ICompanyRepository repoC, 
            IPrequalRepository repoPrequal, 
            int fkCompany, 
            int year, 
            int month)
        {
            var itemDbFinanceiro = repoC.GetCompanyFinanceiro(fkCompany);

            var logs = repoPrequal.GetLogs(fkCompany, year, month);
            var qtdTransL1 = logs.Count;
            var qtdTransItensL1 = (int)logs.Sum(y => y.nuTotProcs);

            var vrCalcTrans = qtdTransL1 * itemDbFinanceiro.vrL1Transaction;
            var vrCalcTransItem = qtdTransItensL1 * itemDbFinanceiro.vrL1TransactionItem;
            var vrTotal = Math.Round(vrCalcTrans + vrCalcTransItem + itemDbFinanceiro.vrSubscriptionL1, 2);

            return new Tb_CompanyFatura
            {
                fkCompany = fkCompany,
                nuMonth = month,
                nuYear = year,
                nuQtdL1Trans = qtdTransL1,
                nuQtdL1TransItem = qtdTransItensL1,
                vrL1Transaction = itemDbFinanceiro.vrL1Transaction,
                vrL1TransactionItem = itemDbFinanceiro.vrL1TransactionItem,
                vrL1TransactionItemTotal = vrCalcTransItem,
                vrL1TransactionTotal = vrCalcTrans,
                vrSubscriptionL1 = itemDbFinanceiro.vrSubscriptionL1,
                vrTotal = vrTotal,
            };
        }
    }
}
