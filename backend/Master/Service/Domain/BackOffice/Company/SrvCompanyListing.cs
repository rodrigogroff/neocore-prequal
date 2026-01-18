using Master.Entity.Dto.Domain.BackOffice.Company;
using Master.Entity.Dto.Infra;
using Master.Service.Base;
using System;

namespace Master.Service.Domain.BackOffice.Company
{
    public class SrvCompanyListing : SrvCompanyAdminBase
    {
        public DtoCompanyListing OutDto = null;

        public bool Exec(DtoAuthenticatedUser user, string search)
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

                var lst = _rpCompany.GetCompanies();

                OutDto = new DtoCompanyListing
                {
                    results = []
                };

                foreach (var c in lst) 
                {
                    if (!string.IsNullOrEmpty(search)) 
                    {
                        if (c.stName == null || !c.stName.Contains(search, StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }
                    }

                    OutDto.results.Add(new DtoCompanyListingItem
                    {
                        id = c.id,
                        stName = c.stName,
                        bActive = c.bActive,
                        client_id = c.client_id,
                        stSecret = c.stSecret
                    });                    
                }

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
