using Master.Controller.Infra;
using Master.Entity;
using Master.Entity.Dto.Infra;
using Master.Entity.Dto.Response.Domain.Bureau;
using Master.Service.Domain.Bureau;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Master.Controller.Domain.Prequal
{
    [Tags("[5] Bureau consultas (avulsas)")]
    [Authorize]
    public class CtrlBureauL1 : MasterController
    {
        public CtrlBureauL1(IMemoryCache memCache, IOptions<LocalNetwork> network) : base(memCache, network) { }

        [HttpGet]
        [Route("api/consulta-pj-l1")]
        [ProducesResponseType(typeof(DtoResponseBureauConsultaPJL1), StatusCodes.Status200OK)]
        public async Task<ActionResult> Get([FromQuery] string documento)
        {
            var srv = RegisterService<SrvBureuConsultaPjL1Get>();

            if (!await srv.Exec(this.MemoryCache, documento))
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
