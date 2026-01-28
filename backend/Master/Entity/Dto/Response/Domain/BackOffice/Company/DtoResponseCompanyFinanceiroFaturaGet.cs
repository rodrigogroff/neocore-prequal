namespace Master.Entity.Dto.Response.Domain.BackOffice.Company
{
    public class DtoResponseCompanyFinanceiroFaturaGet
    {
        public string ano { get; set; }
        public string mes { get; set; }
        public string situacao { get; set; }

        public double valorAssinaturaL1 { get; set; }
        public double valorPrecoTransacaoL1 { get; set; }
        public double valorPrecoTransacaoItemL1 { get; set; }
        public int qtdTransacaoL1 { get; set; }
        public int qtdTransacaoItemL1 { get; set; }
        public double valorCalcTransacaoL1 { get; set; }
        public double valorCalcTransacaoItemL1 { get; set; }

        public double valorAssinaturaL2 { get; set; }
        public double valorPrecoTransacaoL2 { get; set; }
        public double valorPrecoTransacaoItemL2 { get; set; }
        public int qtdTransacaoL2 { get; set; }
        public int qtdTransacaoItemL2 { get; set; }
        public double valorCalcTransacaoL2 { get; set; }
        public double valorCalcTransacaoItemL2 { get; set; }

        public double valorImpostos { get; set; }
        public double valorTotal { get; set; }
    }
}
