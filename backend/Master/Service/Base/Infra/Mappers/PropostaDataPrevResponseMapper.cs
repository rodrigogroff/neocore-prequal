using Master.Entity.Dto.Request.Domain.Prequal;
using Master.Entity.Dto.Response.Domain.Prequal;
using System.Diagnostics.CodeAnalysis;

namespace Master.Service.Base.Infra.Mappers
{
    [ExcludeFromCodeCoverage]
    public static class PropostaDataPrevResponseMapper
    {
        public static PropostaDataPrevResponse Copy(PropostaDataPrevRequest request)
        {
            var response = new PropostaDataPrevResponse
            {
                IdSolicitacao = request.IdSolicitacao,
                Cpf = request.Cpf,
                Matricula = request.Matricula,
                NumeroInscricaoEmpregador = request.NumeroInscricaoEmpregador,
                ValorLiberado = request.ValorLiberado,
                NroParcelas = request.NroParcelas,
                DataHoraValidadeSolicitacao = request.DataHoraValidadeSolicitacao,
                NomeTrabalhador = request.NomeTrabalhador,
                DataNascimento = request.DataNascimento,
                MargemDisponivel = request.MargemDisponivel,
                ElegivelEmprestimo = request.ElegivelEmprestimo,
                DataAdmissao = request.DataAdmissao,
            };

            if (request.InscricaoEmpregador != null)
            {
                response.InscricaoEmpregador = new IERequest
                {
                    Codigo = request.InscricaoEmpregador.Codigo,
                    Descricao = request.InscricaoEmpregador.Descricao
                };
            }

            if (request.PessoaExpostaPoliticamente != null)
            {
                response.PessoaExpostaPoliticamente = new PEPRequest
                {
                    Codigo = request.PessoaExpostaPoliticamente.Codigo,
                    Descricao = request.PessoaExpostaPoliticamente.Descricao
                };
            }

            if (request.Alertas != null)
            {
                response.Alertas = [];

                foreach (var alerta in request.Alertas)
                {
                    var alertaCopy = new AlertaRequest
                    {
                        DataReferencia = alerta.DataReferencia,
                        IdEvento = alerta.IdEvento,
                        CodigoMotivoAfastamento = alerta.CodigoMotivoAfastamento,
                        DataAfastamento = alerta.DataAfastamento,
                        DataTerminoAfastamento = alerta.DataTerminoAfastamento,
                        CodigoMotivoDesligamento = alerta.CodigoMotivoDesligamento,
                        DataDesligamento = alerta.DataDesligamento,
                        DataTerminoDesligamento = alerta.DataTerminoDesligamento,
                        DataAvisoPrevio = alerta.DataAvisoPrevio,
                        DataFimAvisoPrevio = alerta.DataFimAvisoPrevio
                    };

                    if (alerta.TipoAlerta != null)
                    {
                        alertaCopy.TipoAlerta = new TipoAlertaRequest
                        {
                            Codigo = alerta.TipoAlerta.Codigo,
                            Descricao = alerta.TipoAlerta.Descricao
                        };
                    }

                    response.Alertas.Add(alertaCopy);
                }
            }

            return response;
        }
    }
}
