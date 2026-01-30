using Master.Controller.Helper;
using Master.Controller.Infra.Manager;
using Master.Entity;
using Master.Entity.Dto.Infra;
using Master.Service.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;

namespace Master.Controller.Infra
{
    [Authorize]
    [ApiController]
    public partial class MasterController : ControllerBase
    {
        public LocalNetwork Network;
        public IMemoryCache MemoryCache;
        public ServiceManager ServiceManager;
        public SrvBase BaseService;
        public HelperJwtComposer JwtComposerHelper = null;

        public MasterController(IOptions<LocalNetwork> network)
        {
            this.Network = network.Value;
            ServiceManager = new ServiceManager(this);
        }

        public MasterController(IMemoryCache memCache, IOptions<LocalNetwork> network)
        {
            this.Network = network.Value;
            this.MemoryCache = memCache;
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
            this.BaseService.endpoint = MyRoute();
            HttpContext.Response.RegisterForDispose(ServiceManager);
            return baseService;
        }

        [NonAction]
        protected string MyRoute()
        {
            var controllerType = this.GetType();

            // Pega a rota do controller --> [Route("api/authenticate-user")]
            var controllerRoute = controllerType
                .GetCustomAttribute<RouteAttribute>()?.Template ?? "";

            return controllerRoute;
        }

        [NonAction]
        public string GetBearerToken()
        {
            const 
                string
                    Authorization = "Authorization",
                    Bearer = "Bearer ";
                        
            return Request.Headers[Authorization].ToString().Replace(Bearer, string.Empty);
        }

        [NonAction]
        public DtoAuthenticatedUser GetAuthenticatedUser()
        {
            const string user = "user";

            var handler = new JwtSecurityTokenHandler();
            var authHeader = GetBearerToken();

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
