using Master.Entity.Dto.Domain.BackOffice.Company;
using Master.Entity.Dto.Infra;
using Master.Service.Base;
using System;

namespace Master.Service.Domain.BackOffice.Company
{
    public class SrvCompanyGet : SrvCompanyAdminBase
    {
        public DtoCompanyGet OutDto = null;
        
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

                OutDto = new DtoCompanyGet
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
