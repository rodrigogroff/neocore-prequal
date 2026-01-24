namespace Master.Entity.Dto.Request.Domain.Prequal
{
    public class DtoRequestPrequalUpdateConfigLeilao
    {
        public bool? DescartarEmpregadorCnpj { get; set; }
        public bool? DescartarEmpregadorCpf { get; set; }
        public bool? DescartarPep { get; set; }
        public int? RangeValorLiberadoMin { get; set; }
        public int? RangeValorLiberadoMax { get; set; }
        public int? RangeParcelasMin { get; set; }
        public int? RangeParcelasMax { get; set; }
        public int? RangeIdadeMin { get; set; }
        public int? RangeIdadeMax { get; set; }
        public int? RangeValorMargemMin { get; set; }
        public int? RangeValorMargemMax { get; set; }
        public int? RangeMesesAdmissaoMin { get; set; }
        public int? RangeMesesAdmissaoMax { get; set; }
    }
}
