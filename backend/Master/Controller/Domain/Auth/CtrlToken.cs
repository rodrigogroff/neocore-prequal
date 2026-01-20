using Master.Controller.Infra;
using Master.Entity;
using Master.Entity.Dto.Infra;
using Master.Entity.Dto.Request.Domain.Auth;
using Master.Entity.Dto.Response.Domain.Auth;
using Master.Service.Domain.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Master.Controller.Domain.Auth
{
    [Tags("_token")]
    public class CtrlToken : MasterController
    {
        public CtrlToken(IOptions<LocalNetwork> network) : base(network) { }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/authenticate")]
        [ProducesResponseType(typeof(DtoResponseToken), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DtoServiceError), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Authenticate([FromBody] DtoRequestLoginInformation request)
        {
            var srv = RegisterService<SrvAuthenticate>();

            if (!srv.Exec(request.email, request.password))
                return BadRequest(new DtoServiceError
                {
                    codigo = srv.errorCode,
                    mensagem = srv.errorMessage
                });

            return Ok(new DtoResponseToken
            {
                token = this.JwtComposer.ComposeTokenForSession(srv.OutDto),
                user = new DtoResponseAuthenticatedUserInfo
                {
                    stEmail = srv.OutDto.stEmail,
                    stName = srv.OutDto.stName,
                }
            });
        }
    }
}
