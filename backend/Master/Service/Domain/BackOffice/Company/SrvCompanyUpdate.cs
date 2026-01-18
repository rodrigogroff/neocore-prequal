using System;
using Master.Entity.Database.Domain.Company;
using Master.Entity.Dto.Domain.BackOffice.Company;
using Master.Entity.Dto.Infra;
using Master.Service.Base;

namespace Master.Service.Domain.BackOffice.Company
{
    public class SrvCompanyUpdate : SrvCompanyAdminBase
    {
        public bool Exec(DtoAuthenticatedUser user, DtoCompanyUpdate dto)
        {
            if (!CheckCredential(user))
            {
                this.errorCode = "Admin";
                this.errorMessage = "Suas credenciais não possuem acesso à este serviço.";

                return false;
            }

            try
            {
                if (string.IsNullOrWhiteSpace(dto.stName))
                {
                    return false;
                }

                StartDatabase(Network);

                var _rpCompany = RepoCompany();

                if (dto.id == 0)
                {
                    _rpCompany.InsertCompany(new Tb_Company
                    {
                        bActive = true,
                        client_id = Guid.NewGuid(),
                        stSecret = Guid.NewGuid().ToString().Replace("-", ""),
                        stName = dto.stName,
                    });
                }
                else
                {
                    var compDb = _rpCompany.GetCompany(dto.id);

                    if (compDb == null)
                        return false;

                    compDb.stName = dto.stName;
                    compDb.bActive = dto.bActive;

                    _rpCompany.UpdateCompany(compDb);
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
