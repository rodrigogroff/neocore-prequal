using Master.Entity.Database.Domain.Company;
using Master.Repository.Domain.Company;
using Master.Repository.Domain.Prequal;
using System;
using System.Linq;

namespace Master.Service.Base.Infra.Functions
{
    public class FunctionFaturaMensal
    {
        public Tb_CompanyFatura ObterFaturaMensal (
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
            var vrCalcTransL1 = qtdTransL1 * itemDbFinanceiro.vrL1Transaction;
            var vrCalcTransL1Item = qtdTransItensL1 * itemDbFinanceiro.vrL1TransactionItem;

            var qtdTransL2 = logs.Count;
            var qtdTransItensL2 = (int)logs.Sum(y => y.nuTotProcs);
            var vrCalcTransL2 = qtdTransL2 * itemDbFinanceiro.vrL2Transaction;
            var vrCalcTransL2Item = qtdTransItensL2 * itemDbFinanceiro.vrL2TransactionItem;

            if (itemDbFinanceiro.bActiveSubL1 == false)
            {
                qtdTransL1 = 0;
                qtdTransItensL1 = 0;
                vrCalcTransL1 = 0;
                vrCalcTransL1Item = 0;
            }

            if (itemDbFinanceiro.bActiveSubL2 == false)
            {
                qtdTransL2 = 0;
                qtdTransItensL2 = 0;
                vrCalcTransL2 = 0;
                vrCalcTransL2Item = 0;
            }

            var vrSubTotal = Math.Round(
                (vrCalcTransL1 ?? 0) +
                (vrCalcTransL1Item ?? 0) +
                (vrCalcTransL2 ?? 0) +
                (vrCalcTransL2Item ?? 0) +
                (itemDbFinanceiro.vrSubscriptionL1 ?? 0) + 
                (itemDbFinanceiro.vrSubscriptionL2 ?? 0),
                2);

            var vrImpostos = Math.Round(vrSubTotal * 0.07, 2); // 7% do subtotal

            var vrTotal = vrSubTotal + vrImpostos;

            return new Tb_CompanyFatura
            {
                fkCompany = fkCompany,
                nuMonth = month,
                nuYear = year,
                
                nuQtdL1Trans = qtdTransL1,
                nuQtdL1TransItem = qtdTransItensL1,                
                vrL1TransactionItemTotal = vrCalcTransL1Item,
                vrL1TransactionTotal = vrCalcTransL1,
                vrSubscriptionL1 = itemDbFinanceiro.vrSubscriptionL1,
                vrL1Transaction = itemDbFinanceiro.vrL1Transaction,
                vrL1TransactionItem = itemDbFinanceiro.vrL1TransactionItem,

                nuQtdL2Trans = qtdTransL2,
                nuQtdL2TransItem = qtdTransItensL2,
                vrL2TransactionItemTotal = vrCalcTransL2Item,
                vrL2TransactionTotal = vrCalcTransL2,
                vrSubscriptionL2 = itemDbFinanceiro.vrSubscriptionL2,
                vrL2Transaction = itemDbFinanceiro.vrL2Transaction,
                vrL2TransactionItem = itemDbFinanceiro.vrL2TransactionItem,

                vrSubTotal = vrSubTotal,
                vrImpostos = vrImpostos,
                vrTotal = vrTotal,
            };
        }
    }
}
