using Master.Controller.Infra;
using Master.Entity;
using Master.Entity.Dto.Infra;
using Master.Entity.Dto.Response.Domain.BackOffice.Company;
using Master.Service.Domain.BackOffice.Company;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Master.Controller.Domain.Prequal
{
    [Tags("Financeiro (uso do bureau)")]
    [Authorize]
    public class CtrlFinanceiro : MasterController
    {
        public CtrlFinanceiro(IOptions<LocalNetwork> network) : base(network) { }

        [HttpGet]
        [Route("api/precificacao")]
        [ProducesResponseType(typeof(DtoResponseCompanyFinanceiroGet), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetPrecificacao()
        {
            var srv = RegisterService<SrvCompanyFinanceiroGet>();

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

        [HttpGet]
        [Route("api/fatura-servicos")]
        //[ProducesResponseType(typeof(DtoResponsePrequalConfigLeilao), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetFaturaServicos()
        {
            return Ok();
        }
        [HttpGet]
        [Route("api/fatura-servicos-detalhada")]
        //[ProducesResponseType(typeof(DtoResponsePrequalConfigLeilao), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetFaturaDetalhada()
        {
            return Ok();
        }
    }
}
