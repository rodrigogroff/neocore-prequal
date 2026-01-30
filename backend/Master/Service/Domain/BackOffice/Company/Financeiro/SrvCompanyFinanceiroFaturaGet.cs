using Master.Entity.Database.Domain.Company;
using Master.Entity.Dto.Infra;
using Master.Entity.Dto.Request.Domain.BackOffice.Company;
using Master.Entity.Dto.Response.Domain.BackOffice.Company;
using Master.Service.Base;
using Master.Service.Base.Infra.Functions;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Master.Service.Domain.BackOffice.Company
{
    public class SrvCompanyFinanceiroFaturaGet : SrvBase
    {
        private static readonly CultureInfo CulturaPtBr = new("pt-BR");

        public DtoResponseCompanyFinanceiroFaturaGet OutDto { get; private set; }

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
                    OutDto = CriarDtoFromFatura(itemDbFatura, year, month, "Fechada");
                    return true;
                }

                var repoPrequal = RepoPrequal();
                var funcFatura = new FunctionFaturaMensal();
                var fatura = funcFatura.ObterFaturaMensal(repoC, repoPrequal, fkCompany, year, month);

                var mesAnterior = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
                var mesRequisitado = new DateTime(year, month, 1);

                var situacao = mesRequisitado < mesAnterior ? "Não disponível" : "Em aberto";

                OutDto = CriarDtoFromFatura(fatura, year, month, situacao);

                return true;
            }
            catch (Exception ex)
            {
                this.errorCode = "FAIL";
                this.errorMessage = ex.ToString();
                return false;
            }
        }

        private DtoResponseCompanyFinanceiroFaturaGet CriarDtoFromFatura(Tb_CompanyFatura itemDbFatura, int year, int month, string situacao)
        {
            return new DtoResponseCompanyFinanceiroFaturaGet
            {
                ano = year.ToString(),
                mes = month.ToString(),
                dataStr = FormatarDataMesAno(year, month),

                // Level 1
                valorAssinaturaL1 = ArredondarValor(itemDbFatura.vrSubscriptionL1),
                valorPrecoTransacaoItemL1 = ArredondarValor(itemDbFatura.vrL1TransactionItem),
                valorPrecoTransacaoL1 = ArredondarValor(itemDbFatura.vrL1Transaction),
                qtdTransacaoL1 = (int)itemDbFatura.nuQtdL1Trans,
                qtdTransacaoItemL1 = (int)itemDbFatura.nuQtdL1TransItem,
                valorCalcTransacaoL1 = ArredondarValor(itemDbFatura.vrL1TransactionTotal),
                valorCalcTransacaoItemL1 = ArredondarValor(itemDbFatura.vrL1TransactionItemTotal),

                // Level 2
                valorAssinaturaL2 = ArredondarValor(itemDbFatura.vrSubscriptionL2),
                valorPrecoTransacaoItemL2 = ArredondarValor(itemDbFatura.vrL2TransactionItem),
                valorPrecoTransacaoL2 = ArredondarValor(itemDbFatura.vrL2Transaction),
                qtdTransacaoL2 = (int)itemDbFatura.nuQtdL2Trans,
                qtdTransacaoItemL2 = (int)itemDbFatura.nuQtdL2TransItem,
                valorCalcTransacaoL2 = ArredondarValor(itemDbFatura.vrL2TransactionTotal),
                valorCalcTransacaoItemL2 = ArredondarValor(itemDbFatura.vrL2TransactionItemTotal),

                // Totais
                valorSubTotal = ArredondarValor(itemDbFatura.vrSubTotal),
                valorImpostos = ArredondarValor(itemDbFatura.vrImpostos),
                valorTotal = ArredondarValor(itemDbFatura.vrTotal),
                situacao = situacao
            };
        }

        private static string FormatarDataMesAno(int year, int month)
        {
            var data = new DateTime(year, month, 1).ToString("MMMM/yyyy", CulturaPtBr);
            return char.ToUpper(data[0]) + data.Substring(1);
        }

        private static double ArredondarValor(double? valor)
        {
            return Math.Round((double)valor, 2);
        }
    }
}
