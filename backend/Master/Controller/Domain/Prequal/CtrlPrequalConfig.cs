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
    [Tags("Pré-qualificação (configuração)")]
    [Authorize]
    public class CtrlPrequalConfig : MasterController
    {
        public CtrlPrequalConfig(IOptions<LocalNetwork> network) : base(network) { }

        [HttpPost]
        [Route("api/config-prequal-leilao")]
        [ProducesResponseType(typeof(DtoServiceOk), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DtoServiceError), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update([FromBody] DtoRequestPrequalUpdateConfigLeilao request)
        {
            return Ok();
        }

        [HttpGet]
        [Route("api/config-prequal-leilao")]
        [ProducesResponseType(typeof(DtoResponsePrequalConfigLeilao), StatusCodes.Status200OK)]
        public async Task<ActionResult> Get()
        {
            var srv = RegisterService<SrvPrequalSolicitacaoLeilaoConfigGet>();

            if (!await srv.Exec(GetAuthenticatedUser(), Network.cacheLocation))
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
