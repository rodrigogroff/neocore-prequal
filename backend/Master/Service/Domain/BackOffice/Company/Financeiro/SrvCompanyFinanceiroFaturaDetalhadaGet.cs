using Master.Entity.Dto.Infra;
using Master.Entity.Dto.Request.Domain.BackOffice.Company;
using Master.Entity.Dto.Response.Domain.BackOffice.Company;
using Master.Service.Base;
using System;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
                ano = request.nuMonth,
                mes = request.nuYear,                
            });

            OutDto = new DtoResponseCompanyFinanceiroFaturaDetalhadaGet
            {
                ano = srvFatura.OutDto.ano,
                mes = srvFatura.OutDto.mes,
                situacao = srvFatura.OutDto.situacao,
                valorAssinaturaL1 = Math.Round((double)srvFatura.OutDto.valorAssinaturaL1, 2),
                valorPrecoTransacaoL1 = Math.Round((double)srvFatura.OutDto.valorPrecoTransacaoL1, 2),
                valorPrecoTransacaoItemL1 = Math.Round((double)srvFatura.OutDto.valorPrecoTransacaoItemL1, 2),
                qtdTransacaoL1 = srvFatura.OutDto.qtdTransacaoL1,
                qtdTransacaoItemL1 = srvFatura.OutDto.qtdTransacaoItemL1,
                valorCalcTransacaoL1 = Math.Round((double)srvFatura.OutDto.valorCalcTransacaoL1, 2),
                valorCalcTransacaoItemL1 = Math.Round((double)srvFatura.OutDto.valorCalcTransacaoItemL1, 2),
                valorAssinaturaL2 = Math.Round((double)srvFatura.OutDto.valorAssinaturaL2, 2),
                valorPrecoTransacaoL2 = Math.Round((double)srvFatura.OutDto.valorPrecoTransacaoL2, 2),
                valorPrecoTransacaoItemL2 = Math.Round((double)srvFatura.OutDto.valorPrecoTransacaoItemL2, 2),
                qtdTransacaoL2 = srvFatura.OutDto.qtdTransacaoL2,
                qtdTransacaoItemL2 = srvFatura.OutDto.qtdTransacaoItemL2,
                valorCalcTransacaoL2 = Math.Round((double)srvFatura.OutDto.valorCalcTransacaoL2, 2),
                valorCalcTransacaoItemL2 = Math.Round((double)srvFatura.OutDto.valorCalcTransacaoItemL2, 2),
                valorImpostos = Math.Round((double)srvFatura.OutDto.valorImpostos, 2),
                valorSubTotal = Math.Round((double)srvFatura.OutDto.valorSubTotal, 2),
                valorTotal = Math.Round((double)srvFatura.OutDto.valorTotal, 2),
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

                                log_item.valorCalcTransacaoL1 = Math.Round((double)log_item.qtdTransacao * srvFatura.OutDto.valorPrecoTransacaoL1, 2);
                                log_item.valorCalcTransacaoL2 = Math.Round((double)log_item.qtdTransacao * srvFatura.OutDto.valorPrecoTransacaoL2, 2);
                                log_item.valorCalcTransacaoItemL1 = Math.Round((double)log_item.qtdTransacaoItem * srvFatura.OutDto.valorPrecoTransacaoItemL1, 2);
                                log_item.valorCalcTransacaoItemL2 = Math.Round((double)log_item.qtdTransacaoItem * srvFatura.OutDto.valorPrecoTransacaoItemL2, 2);

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
