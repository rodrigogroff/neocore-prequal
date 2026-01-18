namespace Master.Entity.Dto.Infra
{
    public class DtoAuthenticatedUser
    {
        public int fkUser { get; set; }
        public int fkCompany { get; set; }
        public string stName { get; set; }
        public string stEmail { get; set; }
    }
}
