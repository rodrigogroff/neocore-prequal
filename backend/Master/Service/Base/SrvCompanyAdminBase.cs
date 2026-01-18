using Master.Entity.Dto.Infra;

namespace Master.Service.Base
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
