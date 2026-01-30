using Master.Entity.Dto.Response.Domain.BackOffice.User;
using Master.Service.Base;
using System;

namespace Master.Service.Domain.BackOffice.User
{
    public class SrvUserListing : SrvBase
    {
        public DtoResponseUserListing OutDto = null;

        public bool Exec(int fkCompany, string search)
        {
            try
            {
                StartDatabase(Network);

                var _rpUser = RepoUser();

                var lst = _rpUser.GetUsers(fkCompany);

                OutDto = new DtoResponseUserListing
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
                return this.LogException(ex);
            }
        }
    }
}
