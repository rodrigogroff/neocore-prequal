using Master.Entity.Dto.Request.Domain.Prequal;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Master.Entity.Dto.Response.Domain.Prequal
{
    [ExcludeFromCodeCoverage]
    public class DtoResponsePrequalSolicitacoes : DtoResponsePrequalSolicitacoesNode
    {
        public int milis { get; set; }
        public int totalProcessamentos { get; set; }
        public double msPerItem { get; set; }
        public int totalQualificadas { get; set; }
        public int totalRejeitadas { get; set; }
        public double pctPreQualificacao { get; set; }

    }

    [ExcludeFromCodeCoverage]
    public class PropostaDataPrevResponse : PropostaDataPrevRequest
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? _motivoRejeitado { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? _detalheRejeitado { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class DtoResponsePrequalSolicitacoesNode
    {
        public List<PropostaDataPrevResponse> qualificadas { get; set; }
        public List<PropostaDataPrevResponse> rejeitadas { get; set; }
    }
}
