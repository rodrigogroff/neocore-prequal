using System;
using System.Diagnostics.CodeAnalysis;

namespace Master.Entity.Dto.Response.Domain.Bureau
{
    [ExcludeFromCodeCoverage]
    public class DtoResponseBureauConsultaPJBasic
    {
        public DateTime? DataAbertura { get; set; }
        public string CNPJ { get; set; }
        public string SituacaoCad { get; set; }
        public string SituacaoCadMotiv { get; set; }
        public string Nome { get; set; }
        public string Fantasia { get; set; }
        public string Porte { get; set; }
        public string Municipio { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }
        public string SitDesc { get; set; }
        public string Cnae { get; set; }
        public string CnaeDesc { get; set; }
        public string CdNatJur { get; set; }
    }
}
