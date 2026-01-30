using Master.Entity.Dto.Infra;
using Master.Entity.Dto.Response.Domain.BackOffice.Company;
using Master.Service.Domain.BackOffice.Company.Base;
using System;

namespace Master.Service.Domain.BackOffice.Company
{
    public class SrvCompanyListing : SrvCompanyAdminBase
    {
        public DtoResponseCompanyListing OutDto = null;

        public bool Exec(DtoAuthenticatedUser user, string search)
        {
            if (!CheckCredential(user))
            {
                return this.LogException(new Exception("Suas credenciais não possuem acesso à este serviço."), user);
            }

            try
            {
                StartDatabase(Network);

                var _rpCompany = RepoCompany();

                var lst = _rpCompany.GetCompanies();

                OutDto = new DtoResponseCompanyListing
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
                return this.LogException(ex, user);
            }
        }
    }
}
