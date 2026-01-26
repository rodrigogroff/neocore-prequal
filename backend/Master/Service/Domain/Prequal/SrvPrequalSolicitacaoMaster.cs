using Master.Entity.Database.Domain.Prequal;
using Master.Entity.Dto.Infra;
using Master.Entity.Dto.Request.Domain.Prequal;
using Master.Entity.Dto.Response.Domain.Prequal;
using Master.Entity.Gateway;
using Master.Service.Base;
using Master.Service.Infra;
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
                    var client = new ApiClient(localGateway, token);

                    var batchData = new DtoRequestPrequalSolicitacoesNode
                    {
                        fkCompany = fkCompany,
                        propostas = batch
                    };
                    tasks.Add(
                        client.PostAsync<DtoResponsePrequalSolicitacoesNode>(
                            LocalGateway.endpoint_propostas_leilao_cpts_node, 
                            batchData
                            ));
                }

                var responses = await Task.WhenAll(tasks);

                foreach (var response in responses)
                {
                    if (response?.IsSuccess != true || response.Data == null)
                        continue;

                    var _rData = response.Data;

                    if (_rData.qualificadas?.Count > 0)
                        OutDto.qualificadas.AddRange(_rData.qualificadas);

                    if (_rData.rejeitadas?.Count > 0)
                        OutDto.rejeitadas.AddRange(_rData.rejeitadas);
                }

                StartDatabase(Network);

                var repo = RepoPrequal();

                var dtNow = DateTime.Now;

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

                repo.InsertLogProcPrequalLeilao(new Tb_LogProcPrequalLeilao
                {
                    dtLog = dtNow,
                    fkCompany = fkCompany,
                    nuYear = dtNow.Year,
                    nuMonth = dtNow.Month,
                    nuDay = dtNow.Day,
                    nuHour = dtNow.Hour,
                    nuMinute = dtNow.Minute,
                    nuTotMS = OutDto.milis,
                    nuTotProcs = OutDto.totalProcessamentos,
                    nuTotQualificadas = OutDto.totalQualificadas,
                    nuTotRejeitadas = OutDto.totalRejeitadas,
                    nuPctFilter = OutDto.pctPreQualificacao,
                });
            }
            catch (Exception ex)
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
