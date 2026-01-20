using Master.Entity.Dto.Infra;
using Master.Entity.Dto.Request.Domain.Prequal;
using Master.Entity.Dto.Response.Domain.Prequal;
using Master.Service.Base;
using Master.Service.Infra;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Master.Service.Domain.Prequal
{
    public class SrvPrequalSolicitacaoMaster : SrvBase
    {
        public DtoResponsePrequalSolicitacoes OutDto;

        public async Task<bool> Exec(
            string token,
            DtoAuthenticatedUser user,
            string apiRouter,
            int maxCores,
            DtoRequestPrequalSolicitacoes request)
        {
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
            var tasks = new List<Task<ApiResponse<DtoResponsePrequalSolicitacoes>>>();

            foreach (var batch in batches)
            {
                var client = new ApiClient(apiRouter, token);
                var batchData = new DtoRequestPrequalSolicitacoes
                {
                    propostas = batch
                };
                tasks.Add(client.PostAsync<DtoResponsePrequalSolicitacoes>("/api/propostas-cpts-node", batchData));
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

            return true;
        }

        /// <summary>
        /// 100 items / 4 cores = [25, 25, 25, 25]
        /// 102 items / 4 cores = [26, 26, 25, 25]
        /// </summary>
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
