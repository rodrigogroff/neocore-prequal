using System;

namespace Master.Entity.Database.Domain.Bureau
{
    public class Tb_DadosEmpresa
    {
        public int id { get; set; }
        public DateTime? dtExpire { get; set; }
        public DateTime? dtAberturaL1 { get; set; }
        public string stCNPJ { get; set; }
        public string stSituacaoCadL1 { get; set; }
        public string stSituacaoCadMotivL1 { get; set; }
        public string stNomeL1 { get; set; }
        public string stFantasiaL1 { get; set; }
        public string stPorteL1 { get; set; }
        public string stMunicipioL1 { get; set; }
        public string stUfL1 { get; set; }
        public string stCepL1 { get; set; }
        public string stCnaeL1 { get; set; }
        public string stCnaeDescL1 { get; set; }
        public string stCdNatJurL1 { get; set; }
        public string bSimples { get; set; }
        public string bMei { get; set; }
    }
}
