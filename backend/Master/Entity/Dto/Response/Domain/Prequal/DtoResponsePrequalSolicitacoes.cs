using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Master.Entity.Dto.Response.Domain.Prequal
{
    [ExcludeFromCodeCoverage]
    public class DtoResponsePrequalSolicitacoes
    {
        public List<PropostaDataPrevResponse> qualificadas { get; set; } = [];
        public List<PropostaDataPrevResponse> rejeitadas { get; set; } = [];
    }

    [ExcludeFromCodeCoverage]
    public class PropostaDataPrevResponse
    {
        public int? IdSolicitacao { get; set; }
        public long? Cpf { get; set; }
        public string? Matricula { get; set; }
        public IEResponse? InscricaoEmpregador { get; set; }
        public long? NumeroInscricaoEmpregador { get; set; }
        public double? ValorLiberado { get; set; }
        public int? NroParcelas { get; set; }
        public DateTime? DataHoraValidadeSolicitacao { get; set; }
        public string? NomeTrabalhador { get; set; }
        public string? DataNascimento { get; set; }
        public double? MargemDisponivel { get; set; }
        public bool? ElegivelEmprestimo { get; set; }
        public string? DataAdmissao { get; set; }

        public PEPResponse? PessoaExpostaPoliticamente { get; set; }

        public List<AlertaResponse>? Alertas { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class PEPResponse
    {
        public int? Codigo { get; set; }
        public string? Descricao { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class IEResponse
    {
        public int? Codigo { get; set; }
        public string? Descricao { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class TipoAlertaResponse
    {
        public int? Codigo { get; set; }
        public string? Descricao { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class AlertaResponse
    {
        public TipoAlertaResponse? TipoAlerta { get; set; }
        public string? DataReferencia { get; set; }
        public int? IdEvento { get; set; }
        public int? CodigoMotivoAfastamento { get; set; }
        public string? DataAfastamento { get; set; }
        public string? DataTerminoAfastamento { get; set; }
        public int? CodigoMotivoDesligamento { get; set; }
        public string? DataDesligamento { get; set; }
        public string? DataTerminoDesligamento { get; set; }
        public string? DataAvisoPrevio { get; set; }
        public string? DataFimAvisoPrevio { get; set; }
    }
}
