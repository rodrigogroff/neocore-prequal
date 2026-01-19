using System;
using System.Collections.Generic;

namespace Master.Entity.Dto.Response.Domain.BackOffice.Company
{
    public class DtoResponseCompanyListing
    {
        public List<DtoCompanyListingItem> results { get; set; }
    }

    public class DtoCompanyListingItem 
    { 
        public int id { get; set; }
        public string stName { get; set; }
        public Guid client_id { get; set; }
        public string stSecret { get; set; }
        public bool? bActive { get; set; }
    }
}
