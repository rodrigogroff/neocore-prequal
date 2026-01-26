using Master.Controller.Infra;
using Master.Entity;
using Master.Service.Domain.Scheduler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Master.Controller.Domain.Scheduler
{
    [Tags("_scheduler (local gateway)")]
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
            // 1x por hora
            var srv = RegisterService<SrvScheduler>();

            await srv.Process();

            return Ok();
        }
    }
}
