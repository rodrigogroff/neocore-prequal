using Master.Entity.Dto.Request.Domain.Prequal;
using Master.Entity.Dto.Response.Domain.Prequal;
using Master.Service.Base;
using Master.Service.Mappers;
using System.Threading.Tasks;

namespace Master.Service.Domain.Prequal
{
    public class SrvPrequalSolicitacaoNode : SrvBase
    {
        public DtoResponsePrequalSolicitacoes OutDto;

        public async Task<bool> Exec(string cacheServer, DtoRequestPrequalSolicitacoes request)
        {
            OutDto = new DtoResponsePrequalSolicitacoes
            {
                qualificadas = [],
                rejeitadas = [],
            };

            if (request.propostas.Count == 0)
            {
                return true;
            }

            for (int i = 0; i < request.propostas.Count; i++)
            {
                PropostaDataPrevRequest _prop = request.propostas[i];
                
                bool reject = false;

                if (_prop.ElegivelEmprestimo == false)
                {
                    reject = true;
                }

                var cpy = PropostaDataPrevResponseMapper.Copy(_prop);

                if (reject)
                {
                    OutDto.rejeitadas.Add(cpy);
                }
                else
                {
                    OutDto.qualificadas.Add(cpy);
                }
            }

            return true;
        }
    }
}
