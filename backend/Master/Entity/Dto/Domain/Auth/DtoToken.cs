namespace Master.Entity.Dto.Domain.Auth
{
    public class DtoToken
    {
        public string token { get; set; }
        public DtoAuthenticatedUserInfo user { get; set; }
    }
}