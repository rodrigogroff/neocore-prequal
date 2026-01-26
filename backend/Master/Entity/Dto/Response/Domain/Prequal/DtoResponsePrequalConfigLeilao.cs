using System.Diagnostics.CodeAnalysis;

namespace Master.Entity.Dto.Response.Domain.Prequal
{

    [ExcludeFromCodeCoverage]
    public class DtoResponsePrequalConfigLeilao
    {
        public bool? DescarteEmpregadorCnpj { get; set; }
        public bool? DescarteEmpregadorCpf { get; set; }
        public bool? DescartePep { get; set; }
        public bool? DescarteAvisoPrevio { get; set; }
        public bool? DescarteAvisoSaude { get; set; }
        public bool? DescarteSimples { get; set; }
        public bool? DescarteMei { get; set; }
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
