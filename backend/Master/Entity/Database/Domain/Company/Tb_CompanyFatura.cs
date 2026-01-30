namespace Master.Entity.Database.Domain.Company
{
    public class Tb_CompanyFatura
    {
        public int id { get; set; }
        public int? fkCompany { get; set; }
        public int? nuYear { get; set; }
        public int? nuMonth { get; set; }

        // proc L1 -> cpts basica

        public double? vrSubscriptionL1 { get; set; }
        public double? vrL1Transaction { get; set; }
        public double? vrL1TransactionItem { get; set; }
        public double? vrL1TransactionTotal { get; set; }
        public double? vrL1TransactionItemTotal { get; set; }
        public int? nuQtdL1Trans { get; set; }
        public int? nuQtdL1TransItem { get; set; }

        // proc L2 -> proc dados da empresa

        public double? vrSubscriptionL2 { get; set; }
        public double? vrL2Transaction { get; set; }
        public double? vrL2TransactionItem { get; set; }
        public double? vrL2TransactionTotal { get; set; }
        public double? vrL2TransactionItemTotal { get; set; }
        public int? nuQtdL2Trans { get; set; }
        public int? nuQtdL2TransItem { get; set; }

        // totais

        public double? vrSubTotal { get; set; }
        public double? vrImpostos { get; set; }
        public double? vrTotal { get; set; }
    }
}
