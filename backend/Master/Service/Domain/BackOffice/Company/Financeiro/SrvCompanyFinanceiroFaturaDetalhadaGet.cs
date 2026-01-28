using Master.Entity.Dto.Infra;
using Master.Entity.Dto.Request.Domain.BackOffice.Company;
using Master.Entity.Dto.Response.Domain.BackOffice.Company;
using Master.Service.Base;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Master.Service.Domain.BackOffice.Company.Financeiro
{
    public class SrvCompanyFinanceiroFaturaDetalhadaGet : SrvBase
    {
        public DtoResponseCompanyFinanceiroFaturaDetalhadaGet OutDto = null;

        public async Task<bool> Exec(DtoAuthenticatedUser user, DtoRequestCompanyFinanceiroFaturaDetalhada request)
        {
            var srvFatura = this.RegisterService(new SrvCompanyFinanceiroFaturaGet()) as SrvCompanyFinanceiroFaturaGet;

            await srvFatura.Exec(user, new DtoRequestCompanyFinanceiroFatura()
            {
                nuMonth = request.nuMonth,
                nuYear = request.nuYear,
            });

            OutDto = new DtoResponseCompanyFinanceiroFaturaDetalhadaGet
            {
                ano = srvFatura.OutDto.ano,
                mes = srvFatura.OutDto.mes,
                situacao = srvFatura.OutDto.situacao,
                valorAssinaturaL1= srvFatura.OutDto.valorAssinaturaL1,
                valorPrecoTransacaoL1 = srvFatura.OutDto.valorPrecoTransacaoL1,
                valorPrecoTransacaoItemL1 = srvFatura.OutDto.valorPrecoTransacaoItemL1,
                qtdTransacaoL1 = srvFatura.OutDto.qtdTransacaoL1,
                qtdTransacaoItemL1 = srvFatura.OutDto.qtdTransacaoItemL1,
                valorCalcTransacaoL1 = srvFatura.OutDto.valorCalcTransacaoL1,
                valorCalcTransacaoItemL1 = srvFatura.OutDto.valorCalcTransacaoItemL1,
                valorAssinaturaL2 = srvFatura.OutDto.valorAssinaturaL2,
                valorPrecoTransacaoL2 = srvFatura.OutDto.valorPrecoTransacaoL2,
                valorPrecoTransacaoItemL2 = srvFatura.OutDto.valorPrecoTransacaoItemL2,
                qtdTransacaoL2 = srvFatura.OutDto.qtdTransacaoL2,
                qtdTransacaoItemL2 = srvFatura.OutDto.qtdTransacaoItemL2,
                valorCalcTransacaoL2 = srvFatura.OutDto.valorCalcTransacaoL2,
                valorCalcTransacaoItemL2 = srvFatura.OutDto.valorCalcTransacaoItemL2,
                valorImpostos = srvFatura.OutDto.valorImpostos,
                valorTotal = srvFatura.OutDto.valorTotal,
                Conteudo = [],
            };

            request.nuYear ??= DateTime.Now.Year;
            request.nuMonth ??= DateTime.Now.Month;

            try
            {
                StartDatabase(Network);

                var fkCompany = user.fkCompany;
                var year = (int)request.nuYear;
                var month = (int)request.nuMonth;

                var repo = RepoPrequal();

                var logs = repo.GetLogs(fkCompany, year, month);

                if (logs != null)
                {
                    if (logs.Count > 0)
                    {
                        var days = logs.Select(y => y.nuDay).Distinct().ToList();

                        foreach (var day in days)
                        {
                            var resFatDetItemDay = new FatDetItemDay() 
                            { 
                                day = (int) day,
                                Conteudo = []                                
                            };

                            OutDto.Conteudo.Add(resFatDetItemDay);

                            var hourLogs = logs.Where(y => y.nuDay == day).OrderBy(y=> y.nuHour).ToList();

                            var hours = hourLogs.Select(y => y.nuHour).Distinct().ToList();

                            foreach (var hour in hours)
                            {
                                var resFatHour = new FatDetItemHour
                                {
                                    hour = (int)hour,
                                    Conteudo = []
                                };

                                resFatDetItemDay.Conteudo.Add(resFatHour);

                                var minute_logs = hourLogs.Where(y => y.nuHour == hour).OrderBy(y => y.nuMinute).ToList();

                                var log_item = new FatDetItem
                                {
                                    qtdTransacao = 0,
                                    qtdTransacaoItem = 0,
                                    valorCalcTransacaoL1 = 0,
                                    valorCalcTransacaoItemL1 = 0,
                                    valorCalcTransacaoL2 = 0,
                                    valorCalcTransacaoItemL2 = 0,
                                };

                                foreach (var minute in minute_logs)
                                {
                                    log_item.qtdTransacao++;
                                    log_item.qtdTransacaoItem += (int)minute.nuTotProcs;
                                    log_item.qualificadas += (int) minute.nuTotQualificadas;
                                    log_item.rejeitadas += (int)minute.nuTotRejeitadas;
                                }

                                log_item.valorCalcTransacaoL1 = log_item.qtdTransacao * srvFatura.OutDto.valorPrecoTransacaoL1;
                                log_item.valorCalcTransacaoL2 = log_item.qtdTransacao * srvFatura.OutDto.valorPrecoTransacaoL2;
                                log_item.valorCalcTransacaoItemL1 = log_item.qtdTransacaoItem * srvFatura.OutDto.valorPrecoTransacaoItemL1;
                                log_item.valorCalcTransacaoItemL2 = log_item.qtdTransacaoItem * srvFatura.OutDto.valorPrecoTransacaoItemL2;

                                resFatHour.Conteudo.Add(log_item);
                            }
                        }
                    }
                }

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
