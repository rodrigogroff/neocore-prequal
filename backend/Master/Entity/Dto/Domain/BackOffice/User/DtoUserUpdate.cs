namespace Master.Entity.Dto.Domain.BackOffice.User
{
    public class DtoUserUpdate
    {
        public int id { get; set; }
        public string stEmail { get; set; }
        public string stCPF { get; set; }
        public string stName { get; set; }
        public string stPhoneNumber { get; set; }
        public bool? bActive { get; set; }
        public bool? bAdmin { get; set; }
    }
}
