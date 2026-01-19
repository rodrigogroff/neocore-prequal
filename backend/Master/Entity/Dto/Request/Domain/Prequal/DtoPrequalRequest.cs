using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Master.Entity.Dto.Request.Domain.Prequal
{
    public class DtoPrequalRequest
    {
        public List<PropostaDataPrev> propostas {  get; set; }

    }

    [ExcludeFromCodeCoverage]
    public class PropostaDataPrev
    {
        public int? IdSolicitacao { get; set; }
        public long? Cpf { get; set; }
        public string? Matricula { get; set; }
        public PD_IC? InscricaoEmpregador { get; set; }
        public long? NumeroInscricaoEmpregador { get; set; }
        public double? ValorLiberado { get; set; }
        public int? NroParcelas { get; set; }
        public DateTime? DataHoraValidadeSolicitacao { get; set; }
        public string? NomeTrabalhador { get; set; }
        public string? DataNascimento { get; set; }
        public double? MargemDisponivel { get; set; }
        public bool? ElegivelEmprestimo { get; set; }
        public string? DataAdmissao { get; set; }

        public PD_PEP? PessoaExpostaPoliticamente { get; set; }

        public List<PD_Alerta>? Alertas { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class PD_PEP
    {
        public int? Codigo { get; set; }
        public string? Descricao { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class PD_IC
    {
        public int? Codigo { get; set; }
        public string? Descricao { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class STP_TipoAlerta
    {
        public int? Codigo { get; set; }
        public string? Descricao { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class PD_Alerta
    {
        public STP_TipoAlerta? TipoAlerta { get; set; }
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
