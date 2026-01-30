using System;

namespace Master.Entity.Database.Domain.Infra
{
    public class Tb_LogApplication
    {
        public int id { get; set; }
        public int fkCompany { get; set; }
        public int fkUser { get; set; }
        public string stEndpoint { get; set; }
        public DateTime? dtLog { get; set; }
        public string stExceptionData { get; set; }
    }
}
