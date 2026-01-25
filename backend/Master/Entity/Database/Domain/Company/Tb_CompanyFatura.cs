namespace Master.Entity.Database.Domain.Company
{
    public class Tb_CompanyFatura
    {
        public int id { get; set; }
        public int fkCompany { get; set; }
        public int nuYear { get; set; }
        public int nuMonth { get; set; }
        public double vrSubscriptionL1 { get; set; }
        public double vrL1Transaction { get; set; }
        public double vrL1TransactionItem { get; set; }
        public int? nuQtdL1Trans { get; set; }
        public int? nuQtdL1TransItem { get; set; }
        public double vrL1TransactionTotal { get; set; }
        public double vrL1TransactionItemTotal { get; set; }
        public double vrTotal { get; set; }
    }
}
