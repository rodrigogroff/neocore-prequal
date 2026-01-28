namespace Master.Entity.Database.Domain.Prequal
{
    public class Tb_PrequalLeilaoConfig
    {
        public long id { get; set; }
        public long? fkCompany { get; set; }
        public bool? bEmpregadorCnpj { get; set; }
        public bool? bEmpregadorCpf { get; set; }
        public bool? bAlertaSaude { get; set; }
        public bool? bAlertaAvisoPrevio { get; set; }
        public bool? bPep { get; set; }        
        public int? vrLibMin { get; set; }
        public int? vrLibMax { get; set; }
        public int? nuParcMin { get; set; }
        public int? nuParcMax { get; set; }
        public int? nuIdadeMin { get; set; }
        public int? nuIdadeMax { get; set; }
        public int? vrMargemMin { get; set; }
        public int? vrMargemMax { get; set; }
        public int? nuMesesAdmissaoMin { get; set; }
        public int? nuMesesAdmissaoMax { get; set; }
    }
}
