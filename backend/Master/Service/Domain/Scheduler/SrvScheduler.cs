using Master.Service.Base;
using System.Threading.Tasks;

namespace Master.Service.Domain.Scheduler
{
    public class SrvScheduler : SrvBase
    {
        public async Task<bool> Process()
        {
            var procFat = this.RegisterService(new SrvProcessaFatura()) as SrvProcessaFatura;

            await procFat.GeraFaturaMensal();

            return true;
        }
    }
}
