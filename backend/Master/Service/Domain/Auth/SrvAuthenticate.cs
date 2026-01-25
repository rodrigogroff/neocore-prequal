using Master.Entity.Dto.Infra;
using Master.Service.Base;
using System;

namespace Master.Service.Domain.Auth
{
    public class SrvAuthenticate : SrvBase
    {
        public DtoAuthenticatedUser OutDto { get; set; }

        public bool ExecLoginUser(string email, string password)
        {
            try
            {
                if (!HelperCheck().CheckEmail(email))
                    return false;

                StartDatabase(Network);

                var _rpUser = RepoUser();
                
                var userDb = _rpUser.GetUser(email);

                if (userDb is null)
                {
                    this.errorCode = "A01";
                    this.errorMessage = "Credencial não encontrada";
                    return false;
                }

                if (userDb.bActive == false)
                {
                    this.errorCode = "A02";
                    this.errorMessage = "Credencial não encontrada";
                    return false;
                }

                var _rpCompany = RepoCompany();

                var companyDb = _rpCompany.GetCompany(userDb.fkCompany);

                if (companyDb is null)
                {
                    this.errorCode = "A03";
                    this.errorMessage = "Credencial não encontrada";
                    return false;
                }

                if (companyDb.bActive == false)
                {
                    this.errorCode = "A04";
                    this.errorMessage = "Credencial não encontrada";
                    return false;
                }

                if (string.IsNullOrEmpty(userDb.stPassword))
                {
                    var _cpf = userDb.stCPF.Replace(".", "").Replace("-","");
                    password = password.Replace(".", "").Replace("-", "");

                    if (_cpf != password)
                    {
                        this.errorCode = "A05";
                        this.errorMessage = "Credencial não encontrada";
                        return false;
                    }
                }
                else if (userDb.stPassword != password)
                {
                    this.errorCode = "A06";
                    this.errorMessage = "Credencial não encontrada";
                    return false;
                }

                OutDto = new DtoAuthenticatedUser
                {
                    fkUser = userDb.id,
                    fkCompany = companyDb.id,
                    stName = userDb.stName,
                    stEmail = userDb.stEmail,
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

        public bool ExecLoginMachine(string client_id, string secret)
        {
            try
            {
                StartDatabase(Network);

                var _rpCompany = RepoCompany();

                var companyDb = _rpCompany.GetCompany(Guid.Parse(client_id));

                if (companyDb is null)
                {
                    this.errorCode = "M01";
                    this.errorMessage = "Credencial não encontrada";
                    return false;
                }

                if (companyDb.bActive == false)
                {
                    this.errorCode = "M02";
                    this.errorMessage = "Credencial não encontrada";
                    return false;
                }

                if (companyDb.stSecret != secret)
                {
                    this.errorCode = "M04";
                    this.errorMessage = "Credencial não encontrada";
                    return false;
                }

                OutDto = new DtoAuthenticatedUser
                {
                    fkUser = 0,
                    fkCompany = companyDb.id,
                    stName = "",
                    stEmail = "",
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
