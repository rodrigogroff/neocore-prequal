using Master.Controller.Infra;
using Master.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Master.Controller.Domain.Prequal
{
    [Tags("[7] Estatísticas (uso do bureau)")]
    [Authorize]
    public class CtrlStats : MasterController
    {
        public CtrlStats(IOptions<LocalNetwork> network) : base(network) { }

        [HttpGet]
        [Route("api/stats-dashboard")]

        public async Task<ActionResult> GetDashboard()
        {
            return Ok("Não implementado!");
        }

        [HttpGet]
        [Route("api/stats-dashboard-traffic")]

        public async Task<ActionResult> GetDashboardTraffic()
        {
            return Ok("Não implementado!");
        }

        [HttpGet]
        [Route("api/stats-dashboard-prequal-info")]

        public async Task<ActionResult> GetDashboardPrequalInfo()
        {
            return Ok("Não implementado!");
        }
    }
}
