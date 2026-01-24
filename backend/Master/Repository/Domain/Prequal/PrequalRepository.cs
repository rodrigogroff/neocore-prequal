using Dapper;
using Master.Entity.Database.Domain.Prequal;
using Npgsql;
using NpgsqlTypes;

namespace Master.Repository.Domain.Prequal
{
    public interface IPrequalRepository
    {
        Tb_PrequalLeilaoConfig? GetPrequalLeilaoConfig(int fkCompany);                
        long InsertPrequalLeilaoConfig(Tb_PrequalLeilaoConfig mdl, bool retId = false);
        void UpdatePrequalLeilaoConfig(Tb_PrequalLeilaoConfig mdl);
    }

    public class PrequalRepository : BaseRepository, IPrequalRepository
    {
        public Tb_PrequalLeilaoConfig? GetPrequalLeilaoConfig(int fkCompany)
        {
            const string query = "select * from \"PrequalLeilaoConfig\" where \"fkCompany\"=@fkCompany";

            return db.QueryFirstOrDefault<Tb_PrequalLeilaoConfig>(query, new { fkCompany });
        }

        public void SetParamsUser(NpgsqlCommand cmd, Tb_PrequalLeilaoConfig mdl)
        {            
            const
               string
                   id = "id",
                   fkCompany = "fkCompany",
                   bEmpregadorCnpj = "bEmpregadorCnpj",
                   bEmpregadorCpf = "bEmpregadorCpf",
                   bPep = "bPep",
                   vrLibMin = "vrLibMin",
                   vrLibMax = "vrLibMax",
                   nuParcMin = "nuParcMin",
                   nuParcMax = "nuParcMax",
                   nuIdadeMin = "nuIdadeMin",
                   nuIdadeMax = "nuIdadeMax",
                   vrMargemMin = "vrMargemMin",
                   vrMargemMax = "vrMargemMax",
                   nuMesesAdmissaoMin = "nuMesesAdmissaoMin",
                   nuMesesAdmissaoMax = "nuMesesAdmissaoMax";

            cmd.Parameters.AddRange(new NpgsqlParameter[]
            {
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = id, Value = mdl.id },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = fkCompany, Value = GetNull(mdl.fkCompany) },
                new() { NpgsqlDbType = NpgsqlDbType.Boolean, ParameterName = bEmpregadorCnpj, Value = GetNull(mdl.bEmpregadorCnpj) },
                new() { NpgsqlDbType = NpgsqlDbType.Boolean, ParameterName = bEmpregadorCpf, Value = GetNull(mdl.bEmpregadorCpf) },
                new() { NpgsqlDbType = NpgsqlDbType.Boolean, ParameterName = bPep, Value = GetNull(mdl.bPep) },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = vrLibMin, Value = mdl.vrLibMin },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = vrLibMax, Value = mdl.vrLibMax },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuParcMin, Value = mdl.nuParcMin },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuParcMax, Value = mdl.nuParcMax },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuIdadeMin, Value = mdl.nuIdadeMin },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuIdadeMax, Value = mdl.nuIdadeMax },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = vrMargemMin, Value = mdl.vrMargemMin },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = vrMargemMax, Value = mdl.vrMargemMax },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuMesesAdmissaoMin, Value = mdl.nuMesesAdmissaoMin },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuMesesAdmissaoMax, Value = mdl.nuMesesAdmissaoMax },
            });
        }

        public long InsertPrequalLeilaoConfig(Tb_PrequalLeilaoConfig mdl, bool retId = false)
        {
            const string query =
                "INSERT INTO \"PrequalLeilaoConfig\" (\"fkCompany\",\"bEmpregadorCnpj\",\"bEmpregadorCpf\",\"bPep\",\"vrLibMin\"," +
                "\"vrLibMax\",\"nuParcMin\",\"nuParcMax\",\"nuIdadeMin\",\"nuIdadeMax\",\"vrMargemMin\",\"vrMargemMax\",\"nuMesesAdmissaoMin\",\"nuMesesAdmissaoMax\"" +
                ") VALUES " +
                "(@fkCompany,@bEmpregadorCnpj,@bEmpregadorCpf,@bPep,@vrLibMin,@vrLibMax,@nuParcMin,@nuParcMax,@nuIdadeMin,@nuIdadeMax,@vrMargemMin,@vrMargemMax," +
                "@nuMesesAdmissaoMin,@nuMesesAdmissaoMax);";

            const string currval = "select currval('public.\"PrequalLeilaoConfig_id_seq\"');";

            using var cmd = new NpgsqlCommand(retId ? query + currval : query, db);
            SetParamsUser(cmd, mdl);
            if (retId) return (long)cmd.ExecuteScalar();
            cmd.ExecuteNonQuery();
            return 0;
        }

        public void UpdatePrequalLeilaoConfig(Tb_PrequalLeilaoConfig mdl)
        {
            const string query = "update \"PrequalLeilaoConfig\" set " +
                "\"bEmpregadorCnpj\"=@bEmpregadorCnpj," +
                "\"bEmpregadorCpf\"=@bEmpregadorCpf," +
                "\"bPep\"=@bPep," +
                "\"vrLibMin\"=@vrLibMin," +
                "\"vrLibMax\"=@vrLibMax," +
                "\"nuParcMin\"=@nuParcMin," +
                "\"nuParcMax\"=@nuParcMax, " +
                "\"nuIdadeMin\"=@nuIdadeMin," +
                "\"nuIdadeMax\"=@nuIdadeMax," +
                "\"vrMargemMin\"=@vrMargemMin," +
                "\"vrMargemMax\"=@vrMargemMax," +
                "\"nuMesesAdmissaoMin\"=@nuMesesAdmissaoMin," +
                "\"nuMesesAdmissaoMax\"=@nuMesesAdmissaoMax " +
                " where id=@id";

            using var cmd = new NpgsqlCommand(query, db);
            SetParamsUser(cmd, mdl);
            cmd.ExecuteNonQuery();
        }
    }
}
