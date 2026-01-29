using Master.Controller.Infra;
using Master.Entity;
using Master.Entity.Const;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Master.Controller.Domain.Prequal
{
    [Tags("[6] Setup (ambiente)")]
    [Authorize]
    public class CtrlSetup : MasterController
    {
        public CtrlSetup(IOptions<LocalNetwork> network) : base(network) { }

        [HttpGet]
        [Route("api/setup-cbo")]
        public async Task<ActionResult> GetCboListing([FromQuery] string search)
        {
            return Ok(new { Conteudo = PrequalCbo.Busca(search) });
        }

        [HttpGet]
        [Route("api/setup-cnae")]
        public async Task<ActionResult> GetCnaeListing([FromQuery] string search)
        {
            return Ok(new { Conteudo = PrequalCnae.Busca(search) });
        }

        [HttpGet]
        [Route("api/setup-nat-jur")]
        public async Task<ActionResult> GetnatJurListing([FromQuery] string search)
        {
            return Ok(new { Conteudo = PrequalNaturezaJurica.Busca(search) });
        }

        [HttpGet]
        [Route("api/setup-porte")]
        public async Task<ActionResult> GetPorteListing()
        {
            return Ok(new { Conteudo = PrequalPorteEmpresa.Vector });
        }

        [HttpGet]
        [Route("api/setup-tipo-pessoa")]
        public async Task<ActionResult> GetTipoPessoaListing()
        {
            return Ok(new { Conteudo = PrequalTipoPessoa.Vector });
        }

        [HttpGet]
        [Route("api/setup-whitelist-situacao")]
        public async Task<ActionResult> GetWhitelistSituacaoListing()
        {
            return Ok(new { Conteudo = PrequalWhiteListSituacao.Vector });
        }
    }
}
