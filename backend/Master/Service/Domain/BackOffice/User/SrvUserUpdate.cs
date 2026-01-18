using System;
using Master.Entity.Database.Domain.User;
using Master.Entity.Dto.Domain.BackOffice.Company;
using Master.Entity.Dto.Domain.BackOffice.User;
using Master.Entity.Dto.Infra;
using Master.Service.Base;

namespace Master.Service.Domain.BackOffice.User
{
    public class SrvUserUpdate : SrvBase
    {
        public bool Exec(DtoAuthenticatedUser user, DtoUserUpdate dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.stName))
                {
                    return false;
                }

                StartDatabase(Network);

                var _rpUser = this.RepoUser();

                if (dto.id == 0)
                {
                    _rpUser.InsertUser(new Tb_User
                    {
                        bActive = dto.bActive,
                        bAdmin = dto.bAdmin,
                        fkCompany = user.fkCompany,

                        
                    });
                }
                else
                {
                    var userDb = _rpUser.GetUser(user.fkCompany, dto.id);

                    if (userDb == null)
                        return false;

                    userDb.stName = dto.stName;
                    userDb.bActive = dto.bActive;

                    _rpUser.UpdateUser(userDb);
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
