using Master.Entity.Database.Domain.Prequal;
using Master.Entity.Dto.Infra;
using Master.Entity.Dto.Request.Domain.Prequal;
using Master.Entity.Dto.Response.Domain.Prequal;
using Master.Entity.Gateway;
using Master.Service.Base;
using Master.Service.Base.Infra.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Master.Service.Domain.Prequal
{
    public class SrvPrequalSolicitacaoMaster : SrvBase
    {
        public DtoResponsePrequalSolicitacoes OutDto;

        public async Task<bool> Exec(
            string token,
            DtoAuthenticatedUser user,
            string localGateway,
            int maxCores,
            DtoRequestPrequalSolicitacoes request)
        {
            try
            {
                Stopwatch sw = Stopwatch.StartNew();

                OutDto = new DtoResponsePrequalSolicitacoes
                {
                    qualificadas = [],
                    rejeitadas = []
                };

                var totalSolics = request.propostas.Count;
                if (totalSolics == 0)
                    return true;

                var effectiveCores = Math.Min(maxCores, totalSolics);
                var batches = DivideBatches(request.propostas, effectiveCores);
                var tasks = new List<Task<ApiResponse<DtoResponsePrequalSolicitacoesNode>>>();

                var fkCompany = (long)user.fkCompany;

                foreach (var batch in batches)
                {
                    var client = new HelperApiClient(localGateway, token);

                    var batchData = new DtoRequestPrequalSolicitacoesNode
                    {
                        fkCompany = fkCompany,
                        propostas = batch
                    };
                    tasks.Add(
                        client.PostAsync<DtoResponsePrequalSolicitacoesNode>(
                            LocalGateway.endpoint_propostas_leilao_ctps_node, 
                            batchData
                            ));
                }

                var responses = await Task.WhenAll(tasks);

                var dtNow = DateTime.Now;

                var logProc = new Tb_LogProcPrequalLeilao
                {
                    dtLog = dtNow,
                    fkCompany = fkCompany,
                    nuYear = dtNow.Year,
                    nuMonth = dtNow.Month,
                    nuDay = dtNow.Day,
                    nuHour = dtNow.Hour,
                    nuMinute = dtNow.Minute,
                };

                foreach (var response in responses)
                {
                    if (response?.IsSuccess != true || response.Data == null)
                        continue;

                    var _rData = response.Data;

                    if (_rData.qualificadas?.Count > 0)
                        OutDto.qualificadas.AddRange(_rData.qualificadas);

                    if (_rData.rejeitadas?.Count > 0)
                        OutDto.rejeitadas.AddRange(_rData.rejeitadas);

                    logProc.nuFilter1 += _rData.filter1; logProc.nuFilter2 += _rData.filter2; logProc.nuFilter3 += _rData.filter3;
                    logProc.nuFilter4 += _rData.filter4; logProc.nuFilter5 += _rData.filter5; logProc.nuFilter6 += _rData.filter6;
                    logProc.nuFilter7 += _rData.filter7; logProc.nuFilter8 += _rData.filter8; logProc.nuFilter9 += _rData.filter9;
                    logProc.nuFilter10 += _rData.filter10; logProc.nuFilter11 += _rData.filter11; logProc.nuFilter12 += _rData.filter12;
                    logProc.nuFilter13 += _rData.filter13; logProc.nuFilter14 += _rData.filter14; logProc.nuFilter15 += _rData.filter15;
                    logProc.nuFilter16 += _rData.filter16; logProc.nuFilter17 += _rData.filter17; 
                }

                StartDatabase(Network);

                var repo = RepoPrequal();

                OutDto.totalQualificadas = OutDto.qualificadas.Count;
                OutDto.totalRejeitadas = OutDto.rejeitadas.Count;
                OutDto.totalProcessamentos = OutDto.totalQualificadas + OutDto.totalRejeitadas;

                sw.Stop();

                OutDto.milis = (int)sw.Elapsed.TotalMilliseconds;

                OutDto.msPerItem = OutDto.totalProcessamentos > 0
                    ? Math.Round((double)OutDto.milis / OutDto.totalProcessamentos, 2)
                    : 0;

                OutDto.pctPreQualificacao = OutDto.totalProcessamentos > 0
                    ? Math.Round((double)OutDto.totalRejeitadas / OutDto.totalProcessamentos * 100, 2)
                    : 0;

                logProc.nuTotMS = OutDto.milis;
                logProc.nuTotProcs = OutDto.totalProcessamentos;
                logProc.nuTotQualificadas = OutDto.totalQualificadas;
                logProc.nuTotRejeitadas = OutDto.totalRejeitadas;
                logProc.nuPctFilter = OutDto.pctPreQualificacao;

                repo.InsertLogProcPrequalLeilao(logProc);
            }
            catch
            {
                
            }

            return true;
        }

        private List<List<PropostaDataPrevRequest>> DivideBatches(List<PropostaDataPrevRequest> items, int batchCount)
        {
            var batches = new List<List<PropostaDataPrevRequest>>();
            var totalItems = items.Count;
            var baseSize = totalItems / batchCount;
            var remainder = totalItems % batchCount;
            var currentIndex = 0;

            for (int i = 0; i < batchCount; i++)
            {
                var batchSize = baseSize + (i < remainder ? 1 : 0);
                batches.Add(items.GetRange(currentIndex, batchSize));
                currentIndex += batchSize;
            }

            return batches;
        }
    }
}
