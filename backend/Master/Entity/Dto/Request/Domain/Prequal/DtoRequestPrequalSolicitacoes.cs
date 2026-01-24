using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Master.Entity.Dto.Request.Domain.Prequal
{
    [ExcludeFromCodeCoverage]
    public class DtoRequestPrequalSolicitacoesNode
    {
        public long? fkCompany { get; set; }
        public List<PropostaDataPrevRequest> propostas { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class DtoRequestPrequalSolicitacoes
    {
        public List<PropostaDataPrevRequest> propostas {  get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class PropostaDataPrevRequest
    {
        public int? IdSolicitacao { get; set; }
        public long? Cpf { get; set; }
        public string? Matricula { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IERequest? InscricaoEmpregador { get; set; }

        public long? NumeroInscricaoEmpregador { get; set; }
        public double? ValorLiberado { get; set; }
        public int? NroParcelas { get; set; }
        public DateTime? DataHoraValidadeSolicitacao { get; set; }
        public string? NomeTrabalhador { get; set; }
        public string? DataNascimento { get; set; }
        public double? MargemDisponivel { get; set; }
        public bool? ElegivelEmprestimo { get; set; }
        public string? DataAdmissao { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public PEPRequest? PessoaExpostaPoliticamente { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<AlertaRequest>? Alertas { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class PEPRequest
    {
        public int? Codigo { get; set; }
        public string? Descricao { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class IERequest
    {
        public int? Codigo { get; set; }
        public string? Descricao { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class TipoAlertaRequest
    {
        public int? Codigo { get; set; }
        public string? Descricao { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class AlertaRequest
    {
        public TipoAlertaRequest? TipoAlerta { get; set; }
        public string? DataReferencia { get; set; }
        public int? IdEvento { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? CodigoMotivoAfastamento { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? DataAfastamento { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? DataTerminoAfastamento { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? CodigoMotivoDesligamento { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? DataDesligamento { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? DataTerminoDesligamento { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? DataAvisoPrevio { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? DataFimAvisoPrevio { get; set; }
    }
}
