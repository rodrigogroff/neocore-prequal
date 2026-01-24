using System.Diagnostics.CodeAnalysis;

namespace Master.Entity.Dto.Response.Domain.Prequal
{
    [ExcludeFromCodeCoverage]
    public class DtoResponsePrequalConfigLeilao
    {
        public bool? ManterEmpregadorCnpj { get; set; }
        public bool? ManterEmpregadorCpf { get; set; }
        public bool? ManterPep { get; set; }
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
