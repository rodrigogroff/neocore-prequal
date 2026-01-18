using Master.Controller.Infra;
using Master.Entity;
using Master.Entity.Dto.Domain.Auth;
using Master.Entity.Dto.Infra;
using Master.Service.Domain.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Master.Controller.Domain.Auth
{
    [Tags("_oauth2")]
    public class CtrlAuthenticate : MasterController
    {
        public CtrlAuthenticate(IOptions<LocalNetwork> network) : base(network) { }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/authenticate")]
        [ProducesResponseType(typeof(DtoToken), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DtoServiceError), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Authenticate([FromBody] DtoLoginInformation dto)
        {
            var srv = RegisterService<SrvAuthenticate>();

            if (!srv.Exec(dto.email, dto.password))
                return BadRequest(new DtoServiceError
                {
                    codigo = srv.errorCode,
                    mensagem = srv.errorMessage
                });

            return Ok(new DtoToken
            {
                token = this.JwtComposer.ComposeTokenForSession(srv.OutDto),
                user = new DtoAuthenticatedUserInfo
                {
                    stEmail = srv.OutDto.stEmail,
                    stName = srv.OutDto.stName,
                }
            });
        }
    }
}
