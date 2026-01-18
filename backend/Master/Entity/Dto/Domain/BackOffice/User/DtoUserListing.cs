using System.Collections.Generic;

namespace Master.Entity.Dto.Domain.BackOffice.User
{
    public class DtoUserListing
    {
        public List<DtoUserListingItem> results { get; set; }
    }

    public class DtoUserListingItem
    {
        public int id { get; set; }
        public string stEmail { get; set; }
        public string stCPF { get; set; }
        public string stName { get; set; }
        public string stPhoneNumber { get; set; }
        public string stPassword { get; set; }
        public bool? bActive { get; set; }
        public bool? bAdmin { get; set; }
    }
}
