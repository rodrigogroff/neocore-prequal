using System;

namespace Master.Entity.Database.Domain.Company
{
    public class Tb_Company
    {
        public int id { get; set; }
        public string stName { get; set; }
        public Guid client_id { get; set; }
        public string stSecret { get; set; }
        public bool? bActive { get; set; }
    }
}
