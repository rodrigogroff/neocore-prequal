using System;

namespace Master.Entity.Dto.Domain.BackOffice.Company
{
    public class DtoCompanyGet
    {
        public int id { get; set; }
        public string stName { get; set; }
        public Guid client_id { get; set; }
        public string stSecret { get; set; }
        public bool? bActive { get; set; }
    }
}
