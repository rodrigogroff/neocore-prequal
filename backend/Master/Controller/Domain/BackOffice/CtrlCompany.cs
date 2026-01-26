using Master.Controller.Infra;
using Master.Entity;
using Master.Entity.Dto.Infra;
using Master.Entity.Dto.Request.Domain.BackOffice.Company;
using Master.Service.Domain.BackOffice.Company;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Master.Controller.Domain.BackOffice
{
    [Tags("_company (private)")]
#if RELEASE
    [ApiExplorerSettings(IgnoreApi = true)]
#endif
    public class CtrlCompany : MasterController
    {
        public CtrlCompany(IOptions<LocalNetwork> network) : base(network) { }

        [Route("api/company")]
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] int id)
        {
            var currentUser = GetAuthenticatedUser();

            var srv = RegisterService<SrvCompanyGet>();

            if (!srv.Exec(currentUser, id))
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
        [Route("api/companyListing")]
        public async Task<ActionResult> Listing([FromQuery] string search)
        {
            var currentUser = GetAuthenticatedUser();

            var srv = RegisterService<SrvCompanyListing>();

            if (!srv.Exec(currentUser, search))
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
        [Route("api/company")]
        public async Task<ActionResult> CreateOrUpdate([FromBody] DtoRequestCompanyUpdate request)
        {
            var currentUser = GetAuthenticatedUser();

            var srv = RegisterService<SrvCompanyUpdate>();

            if (!srv.Exec(currentUser, request))
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
