using Master.Service.Base;
using System.Threading.Tasks;

namespace Master.Service.Domain.Scheduler
{
    public class SrvProcessaFatura : SrvBase
    {
        public async Task<bool> GeraFaturaMensal()
        {
            StartDatabase(Network);

            return true;
        }
    }
}
