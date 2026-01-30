namespace Master.Entity.Dto.Response.Domain.BackOffice.Company
{
    public class DtoResponseCompanyFinanceiroGet
    {
        public bool subL1 { get; set; }
        public string subL1str { get; set; }
        public bool subL2 { get; set; }
        public string subL2str { get; set; }

        public double valorAssinaturaL1 { get; set; }
        public double valorTransacaoL1 { get; set; }
        public double valorTransacaoItemL1 { get; set; }

        public double valorAssinaturaL2 { get; set; }
        public double valorTransacaoL2 { get; set; }
        public double valorTransacaoItemL2 { get; set; }
    }
}
