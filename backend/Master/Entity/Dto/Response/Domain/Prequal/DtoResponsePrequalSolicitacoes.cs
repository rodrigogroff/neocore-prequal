using Master.Entity.Dto.Request.Domain.Prequal;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Master.Entity.Dto.Response.Domain.Prequal
{
    [ExcludeFromCodeCoverage]
    public class DtoResponsePrequalSolicitacoes
    {
        public List<PropostaDataPrevResponse> qualificadas { get; set; } 
        public List<PropostaDataPrevResponse> rejeitadas { get; set; } 

        public long milis { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class PropostaDataPrevResponse : PropostaDataPrevRequest
    {

    }
}
