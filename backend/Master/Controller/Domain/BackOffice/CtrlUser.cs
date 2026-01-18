using Master.Controller.Infra;
using Master.Entity;
using Master.Entity.Dto.Domain.BackOffice.User;
using Master.Entity.Dto.Infra;
using Master.Service.Domain.BackOffice.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Master.Controller.Domain.BackOffice
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class CtrlUser : MasterController
    {
        public CtrlUser(IOptions<LocalNetwork> network) : base(network) { }

        [HttpGet]
        [Route("api/user")]
        public async Task<ActionResult> Get([FromQuery] int id)
        {
            var currentUser = GetAuthenticatedUser();

            var srv = RegisterService<SrvUserGet>();

            if (!srv.Exec(currentUser.fkCompany, id))
            {
                return BadRequest(new DtoServiceError
                {
                    codigo = srv.errorCode,
                    mensagem = srv.errorMessage
                });
            }

            return Ok(srv.OutDto);
        }

        [HttpGet]
        [Route("api/userListing")]
        public async Task<ActionResult> Listing([FromQuery] string search)
        {
            var currentUser = GetAuthenticatedUser();

            var srv = RegisterService<SrvUserListing>();

            if (!srv.Exec(currentUser.fkCompany, search))
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
        [Route("api/user")]
        public async Task<ActionResult> CreateOrUpdate([FromBody] DtoUserUpdate dto)
        {
            var currentUser = GetAuthenticatedUser();

            var srv = RegisterService<SrvUserUpdate>();

            if (!srv.Exec(currentUser, dto))
            {
                return BadRequest(new DtoServiceError
                {
                    codigo = srv.errorCode,
                    mensagem = srv.errorMessage
                });
            }

            return Ok();
        }
    }
}
