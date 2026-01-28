using Master.Service.Base;
using Master.Service.Base.Infra.Functions;
using System;
using System.Threading.Tasks;

namespace Master.Service.Domain.Scheduler
{
    public class SrvProcessaFatura : SrvBase
    {
        public async Task<bool> GeraFaturaMensal()
        {
            var dtMesPassado = DateTime.Now.AddMonths(-1);

            int 
                year = dtMesPassado.Year,
                month = dtMesPassado.Month;

            StartDatabase(Network);

            var repoC = RepoCompany();
            var repoPrequal = RepoPrequal();

            var funcFatura = new FunctionFaturaMensal();

            foreach (var itemCompanyC in repoC.GetCompanies())
            {
                var fkCompany = itemCompanyC.id;

                var itemDbFatura = repoC.GetCompanyFatura(fkCompany, year, month);

                // se já gerou fatura, passa adiante

                if (itemDbFatura != null)
                    continue;

                // senão processou mês passado, gerar fatura

                var novaFatura = funcFatura.ObterFaturaMensal(repoC, repoPrequal, fkCompany, year, month);

                repoC.InsertCompanyFatura(novaFatura);
            }

            return true;
        }
    }
}
