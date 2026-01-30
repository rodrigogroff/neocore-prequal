using Master.Entity.Dto.Response.Domain.BackOffice.User;
using Master.Service.Base;
using System;

namespace Master.Service.Domain.BackOffice.User
{
    public class SrvUserGet : SrvBase
    {
        public DtoResponseUserGet OutDto = null;
        
        public bool Exec(int fkCompany, int id)
        {
            try
            {
                StartDatabase(Network);

                var _rpUser = RepoUser();

                var userDb = _rpUser.GetUser(fkCompany, id);

                if (userDb is null)
                {
                    this.errorCode = "U01";
                    this.errorMessage = "Usuário não encontrado!";
                    return false;
                }

                OutDto = new DtoResponseUserGet
                {
                    id = userDb.id,
                    stName = userDb.stName,
                    bActive = userDb.bActive,
                    bAdmin = userDb.bAdmin,
                    stCPF = userDb.stCPF,
                    stEmail = userDb.stEmail,
                    stPhoneNumber = userDb.stPhoneNumber,
                };

                return true;
            }
            catch (Exception ex)
            {
                return this.LogException(ex);
            }
        }
    }
}
