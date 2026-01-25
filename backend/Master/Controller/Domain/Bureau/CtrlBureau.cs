using Master.Controller.Infra;
using Master.Entity;
using Master.Entity.Dto.Infra;
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
    [Tags("[5] Bureau consultas (L1)")]
    [Authorize]
    public class CtrlBureauL1 : MasterController
    {
        public CtrlBureauL1(IMemoryCache memCache, IOptions<LocalNetwork> network) : base(memCache, network) { }

        [HttpGet]
        [Route("api/consulta-pj-l1")]
        [ProducesResponseType(typeof(DtoResponsePrequalConfigLeilao), StatusCodes.Status200OK)]
        public async Task<ActionResult> Get([FromQuery] string? documento)
        {
            var srv = RegisterService<SrvPrequalSolicitacaoLeilaoConfigGet>();

            if (!await srv.Exec(GetAuthenticatedUser()))
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
