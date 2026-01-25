using Master.Controller.Infra;
using Master.Entity;
using Master.Entity.Dto.Infra;
using Master.Entity.Dto.Request.Domain.Prequal;
using Master.Entity.Dto.Response.Domain.Prequal;
using Master.Service.Domain.Prequal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Master.Controller.Domain.Prequal
{
    [Tags("[4] Pré-qualificação L1 (descarte leilão ctps / dataprev)")]
    [Authorize]
    public class CtrlPrequalLeilao : MasterController
    {
        public CtrlPrequalLeilao(IMemoryCache memCache, IOptions<LocalNetwork> network) : base(memCache, network) { }

        [HttpPost]
        [Route("api/propostas-leilao-cpts")]
        [ProducesResponseType(typeof(DtoResponsePrequalSolicitacoes), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DtoServiceError), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Filter([FromBody] DtoRequestPrequalSolicitacoes request)
        {
            var srv = RegisterService<SrvPrequalSolicitacaoMaster>();

            // Origem: cliente
            // --a) distribuir processamento no cluster
            // --b) coletar respostas dos nodos
            // --c) montar resposta final com tudo

            var token = GetBearerToken(); // para imbutir no request autorizado do node
            var user = GetAuthenticatedUser(); // para descobrir a empresa a processar
            var localGateway = this.Network.localGateway; // roteador interno do cluster
            var maxCores = this.Network.maxCores; // quantos nodos usar

            if (!await srv.Exec(token, user, localGateway, maxCores, request))
            {
                return BadRequest(new DtoServiceError
                {
                    codigo = srv.errorCode,
                    mensagem = srv.errorMessage
                });
            }

            return Ok(srv.OutDto);
        }

        [HttpPost]
        #if RELEASE
        [ApiExplorerSettings(IgnoreApi = true)]
        #endif
        [Route("api/propostas-leilao-cpts-node")]
        [ProducesResponseType(typeof(DtoResponsePrequalSolicitacoesNode), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DtoServiceError), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> NodeFilter([FromBody] DtoRequestPrequalSolicitacoesNode request)
        {            
            var srv = RegisterService<SrvPrequalSolicitacaoNode>();

            // Origem: master
            // --a) receber chunk (um pedaço) de todo o processamento
            // --b) obter regras e listas da cache
            // --c) filtrar propostas válidas
            // --d) montar resposta final

            if (!await srv.Exec(this.MemoryCache, request))
            {
                return BadRequest(new DtoServiceError
                {
                    codigo = srv.errorCode,
                    mensagem = srv.errorMessage
                });
            }

            return Ok(srv.OutDto);
        }
    }
}
