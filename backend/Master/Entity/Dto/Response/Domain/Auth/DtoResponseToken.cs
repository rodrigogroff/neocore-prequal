namespace Master.Entity.Dto.Response.Domain.Auth
{
    public class DtoResponseToken
    {
        public string token { get; set; }
        public DtoResponseAuthenticatedUserInfo user { get; set; }
    }
}