namespace Master.Entity.Database.Domain.User
{
    public class Tb_User
    {
        public int id { get; set; }
        public int fkCompany { get; set; }
        public string stEmail { get; set; }
        public string stCPF { get; set; }
        public string stName { get; set; }
        public string stPhoneNumber { get; set; }
        public string stPassword { get; set; }
        public bool? bActive { get; set; }
        public bool? bAdmin { get; set; }
    }
}
