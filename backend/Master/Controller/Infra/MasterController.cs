using Master.Controller.Helper;
using Master.Controller.Infra.Manager;
using Master.Entity;
using Master.Entity.Dto.Infra;
using Master.Service.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Master.Controller.Infra
{
    [Authorize]
    [ApiController]
    public partial class MasterController : ControllerBase
    {
        public LocalNetwork Network;
        public ServiceManager ServiceManager;
        public SrvBase BaseService;
        public HelperJwtComposer JwtComposerHelper = null;

        public MasterController( IOptions<LocalNetwork> network)
        {
            this.Network = network.Value;
            ServiceManager = new ServiceManager(this);
        }
        
        [NonAction]
        public void FinishService()
        {
            BaseService.DisposeService();
            JwtComposerHelper = null;
        }

        [NonAction]
        public T RegisterService<T>()
            where T : new()
        {
            var baseService = new T();
            this.BaseService = baseService as SrvBase;
            this.BaseService.Network = Network;
            HttpContext.Response.RegisterForDispose(ServiceManager);
            return baseService;
        }

        [NonAction]
        public DtoAuthenticatedUser GetAuthenticatedUser()
        {
            const string Authorization = "Authorization",
                         Bearer = "Bearer ",
                         user = "user";

            var handler = new JwtSecurityTokenHandler();
            var authHeader = Request.Headers[Authorization].ToString().Replace(Bearer, string.Empty);
            if (string.IsNullOrEmpty(authHeader))
            {
                return null;
            }
                        
            var tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;
            var claims = tokenS.Claims;
            return System.Text.Json.JsonSerializer.Deserialize<DtoAuthenticatedUser>(
                claims.FirstOrDefault(claim => claim.Type == user)?.Value);
        }

        public HelperJwtComposer JwtComposer
        {
            get
            {
                if (this.JwtComposerHelper == null)
                {
                    JwtComposerHelper = new HelperJwtComposer();
                    return JwtComposerHelper;
                }
                else
                {
                    return JwtComposerHelper;
                }
            }
        }
    }
}
