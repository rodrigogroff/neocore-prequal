using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Master.Entity.Dto.Request.Domain.Prequal
{
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

        public PEPRequest? PessoaExpostaPoliticamente { get; set; }

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
