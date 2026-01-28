namespace Master.Entity.Database.Domain.Company
{
    public class Tb_CompanyFinanceiro
    {
        public int id { get; set; }
        public int? fkCompany { get; set; }
        public bool? bActiveSubL1 { get; set; }
        public bool? bActiveSubL2 { get; set; }
        public double? vrSubscriptionL1 { get; set; }
        public double? vrL1Transaction { get; set; }
        public double? vrL1TransactionItem { get; set; }
        public double? vrSubscriptionL2 { get; set; }
        public double? vrL2Transaction { get; set; }
        public double? vrL2TransactionItem { get; set; }
    }
}
