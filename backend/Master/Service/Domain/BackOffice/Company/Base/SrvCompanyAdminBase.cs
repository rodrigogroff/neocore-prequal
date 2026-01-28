using Master.Entity.Dto.Infra;
using Master.Service.Base;

namespace Master.Service.Domain.BackOffice.Company.Base
{
    public class SrvCompanyAdminBase : SrvBase
    {
        public bool CheckCredential(DtoAuthenticatedUser user)
        {
            if (user.fkCompany != 1)
                return false;

            return true;
        }
        
    }
}
