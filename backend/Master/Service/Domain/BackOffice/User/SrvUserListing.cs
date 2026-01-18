using Master.Entity.Dto.Domain.BackOffice.User;
using Master.Service.Base;
using System;

namespace Master.Service.Domain.BackOffice.User
{
    public class SrvUserListing : SrvBase
    {
        public DtoUserListing OutDto = null;

        public bool Exec(int fkCompany, string search)
        {
            try
            {
                StartDatabase(Network);

                var _rpUser = RepoUser();

                var lst = _rpUser.GetUsers(fkCompany);

                OutDto = new DtoUserListing
                {
                    results = []
                };

                foreach (var userDb in lst) 
                {
                    if (!string.IsNullOrEmpty(search)) 
                    {
                        if (userDb.stName == null || !userDb.stName.Contains(search, StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }
                    }

                    OutDto.results.Add(new DtoUserListingItem
                    {
                        id = userDb.id,
                        stName = userDb.stName,
                        bActive = userDb.bActive,
                        bAdmin = userDb.bAdmin,
                        stCPF = userDb.stCPF,
                        stEmail = userDb.stEmail,
                        stPhoneNumber = userDb.stPhoneNumber,                        
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
