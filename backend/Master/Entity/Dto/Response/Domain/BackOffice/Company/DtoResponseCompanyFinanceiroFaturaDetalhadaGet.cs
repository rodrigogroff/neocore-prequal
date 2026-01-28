using System.Collections.Generic;

namespace Master.Entity.Dto.Response.Domain.BackOffice.Company
{
    public class DtoResponseCompanyFinanceiroFaturaDetalhadaGet : DtoResponseCompanyFinanceiroFaturaGet
    {
        public List<FatDetItemDay> Conteudo { get; set; }
    }

    public class FatDetItemDay
    {
        public int day { get; set; }

        public List<FatDetItemHour> Conteudo { get; set; }
    }

    public class FatDetItemHour
    {
        public int hour { get; set; }

        public List<FatDetItem> Conteudo { get; set; }
    }
    
    public class FatDetItem 
    {
        public int qtdTransacao { get; set; }
        public int qtdTransacaoItem { get; set; }
        public double valorCalcTransacaoL1 { get; set; }
        public double valorCalcTransacaoItemL1 { get; set; }
        public double valorCalcTransacaoL2 { get; set; }
        public double valorCalcTransacaoItemL2 { get; set; }

        public int qualificadas { get; set; }
        public int rejeitadas { get; set; }
    }
}
