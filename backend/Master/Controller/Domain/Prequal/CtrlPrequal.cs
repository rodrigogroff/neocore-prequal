using Master.Controller.Infra;
using Master.Entity;
using Master.Entity.Dto.Infra;
using Master.Entity.Dto.Request.Domain.Prequal;
using Master.Entity.Dto.Response.Domain.Prequal;
using Master.Service.Domain.Prequal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Master.Controller.Domain.Prequal
{
    [Tags("Pré-qualificação")]
    [Authorize]
    public class CtrlPrequal : MasterController
    {
        public CtrlPrequal(IOptions<LocalNetwork> network) : base(network) { }

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

            if (!await srv.Exec(GetBearerToken(), GetAuthenticatedUser(), Network.localGateway, Network.maxCores, request))
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
       // [ApiExplorerSettings(IgnoreApi = true)]
        [Route("api/propostas-leilao-cpts-node")]
        [ProducesResponseType(typeof(DtoResponsePrequalSolicitacoes), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DtoServiceError), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> NodeFilter([FromBody] DtoRequestPrequalSolicitacoes request)
        {            
            var srv = RegisterService<SrvPrequalSolicitacaoNode>();

            // Origem: master
            // --a) receber chunk (um pedaço) de todo o processamento
            // --b) obter regras e listas da cache
            // --c) filtrar propostas válidas
            // --d) montar resposta final

            if (!await srv.Exec(Network.cacheLocation, request))
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
