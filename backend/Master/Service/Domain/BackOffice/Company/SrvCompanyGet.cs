using Master.Entity.Dto.Infra;
using Master.Entity.Dto.Response.Domain.BackOffice.Company;
using Master.Service.Base;
using System;

namespace Master.Service.Domain.BackOffice.Company
{
    public class SrvCompanyGet : SrvCompanyAdminBase
    {
        public DtoResponseCompanyGet OutDto = null;
        
        public bool Exec(DtoAuthenticatedUser user, int id)
        {
            if (!CheckCredential(user))
            {
                this.errorCode = "Admin";
                this.errorMessage = "Suas credenciais não possuem acesso à este serviço.";

                return false;
            }

            try
            {
                StartDatabase(Network);

                var _rpCompany = RepoCompany();

                var companyDb = _rpCompany.GetCompany(id);

                if (companyDb is null)
                {
                    this.errorCode = "C01";
                    this.errorMessage = "Empresa não encontrada";
                    return false;
                }                    

                OutDto = new DtoResponseCompanyGet
                {
                    id = companyDb.id,
                    stName = companyDb.stName,
                    client_id = companyDb.client_id,
                    stSecret = companyDb.stSecret,
                    bActive = companyDb.bActive
                };

                return true;
            }
            catch (Exception ex) 
            {
                this.errorCode = "FAIL";
                this.errorMessage = ex.ToString();

                return false;
            }
        }
    }
}
