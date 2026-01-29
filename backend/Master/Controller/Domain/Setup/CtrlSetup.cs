using Master.Controller.Infra;
using Master.Entity;
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
        [Route("api/setup-filter-listing")]

        public async Task<ActionResult> GetFilterListing()
        {
            return Ok("Não implementado!");
        }

        [HttpGet]
        [Route("api/setup-porte-listing")]

        public async Task<ActionResult> GetPorteListing()
        {
            return Ok("Não implementado!");
        }

        [HttpGet]
        [Route("api/setup-cnae-listing")]
        
        public async Task<ActionResult> GetCnaeListing()
        {
            return Ok("Não implementado!");
        }

        [HttpGet]
        [Route("api/setup-nat-jur-listing")]

        public async Task<ActionResult> GetNatJur()
        {
            return Ok("Não implementado!");
        }
    }
}
