using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Master.Entity.Dto.External
{
    public class ReceitaWs_CnpjResponse
    {
        public string Abertura { get; set; }
        public string Situacao { get; set; }
        public string Tipo { get; set; }
        public string Nome { get; set; }
        public string Fantasia { get; set; }
        public string Porte { get; set; }

        [JsonPropertyName("natureza_juridica")]
        public string NaturezaJuridica { get; set; }

        public List<QsaMember> Qsa { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Municipio { get; set; }
        public string Bairro { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }

        [JsonPropertyName("data_situacao")]
        public string DataSituacao { get; set; }

        [JsonPropertyName("motivo_situacao")]
        public string MotivoSituacao { get; set; }

        public string Cnpj { get; set; }

        [JsonPropertyName("ultima_atualizacao")]
        public DateTime UltimaAtualizacao { get; set; }

        public string Status { get; set; }
        public string Complemento { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Efr { get; set; }

        [JsonPropertyName("situacao_especial")]
        public string SituacaoEspecial { get; set; }

        [JsonPropertyName("data_situacao_especial")]
        public string DataSituacaoEspecial { get; set; }

        [JsonPropertyName("atividade_principal")]
        public List<Atividade> AtividadePrincipal { get; set; }

        [JsonPropertyName("atividades_secundarias")]
        public List<Atividade> AtividadesSecundarias { get; set; }

        [JsonPropertyName("capital_social")]
        public string CapitalSocial { get; set; }

        public SimplesInfo Simples { get; set; }
        public SimeiInfo Simei { get; set; }
        public ExtraInfo Extra { get; set; }
        public BillingInfo Billing { get; set; }
    }

    public class QsaMember
    {
        public string Nome { get; set; }
        public string Qual { get; set; }
    }

    public class Atividade
    {
        public string Code { get; set; }
        public string Text { get; set; }
    }

    public class SimplesInfo
    {
        public bool Optante { get; set; }

        [JsonPropertyName("data_opcao")]
        public DateTime? DataOpcao { get; set; }

        [JsonPropertyName("data_exclusao")]
        public DateTime? DataExclusao { get; set; }

        [JsonPropertyName("ultima_atualizacao")]
        public DateTime UltimaAtualizacao { get; set; }
    }

    public class SimeiInfo
    {
        public bool Optante { get; set; }

        [JsonPropertyName("data_opcao")]
        public DateTime? DataOpcao { get; set; }

        [JsonPropertyName("data_exclusao")]
        public DateTime? DataExclusao { get; set; }

        [JsonPropertyName("ultima_atualizacao")]
        public DateTime UltimaAtualizacao { get; set; }
    }

    public class ExtraInfo
    {
        // Empty object - add properties as needed when they appear
    }

    public class BillingInfo
    {
        public bool Free { get; set; }
        public bool Database { get; set; }
    }
}
