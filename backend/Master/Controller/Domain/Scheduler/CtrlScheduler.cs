using Master.Controller.Infra;
using Master.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Master.Controller.Domain.Scheduler
{
    [Tags("[0] _scheduler (private)")]
    public class CtrlScheduler : MasterController
    {
        public CtrlScheduler(IOptions<LocalNetwork> network) : base(network) { }

        [AllowAnonymous]
        [HttpPost]
#if RELEASE
        [ApiExplorerSettings(IgnoreApi = true)]
#endif
        [Route("api/scheduler")]        
        public async Task<ActionResult> Scheduler()
        {
            return Ok();
        }
    }
}
