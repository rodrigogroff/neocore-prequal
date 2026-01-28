using Dapper;
using Master.Entity.Database.Domain.Bureau;
using Npgsql;
using NpgsqlTypes;

namespace Master.Repository.Domain.Bureau
{
    public interface IBureauRepository
    {
        Tb_DadosEmpresa GetDadosEmpresa(string cnpj);
        long InsertDadosEmpresa(Tb_DadosEmpresa mdl, bool retId = false);
        void UpdateDadosEmpresa(Tb_DadosEmpresa mdl);
    }

    public class BureauRepository : BaseRepository, IBureauRepository
    {
        public Tb_DadosEmpresa GetDadosEmpresa(string cnpj)
        {
            const string query = "select * from \"DadosEmpresa\" where \"stCNPJ\"=@cnpj";

            return db.QueryFirstOrDefault<Tb_DadosEmpresa>(query, new { cnpj });
        }

        public void SetParamsDadosEmpresa(NpgsqlCommand cmd, Tb_DadosEmpresa mdl)
        {
            const
               string
                   id = "id",
                   dtExpire = "dtExpire",
                   dtAberturaL1 = "dtAberturaL1",
                   stCNPJ = "stCNPJ",
                   stSituacaoCadL1 = "stSituacaoCadL1",
                   stSituacaoCadMotivL1 = "stSituacaoCadMotivL1",
                   stNomeL1 = "stNomeL1",
                   stFantasiaL1 = "stFantasiaL1",
                   stPorteL1 = "stPorteL1",
                   stMunicipioL1 = "stMunicipioL1",
                   stUfL1 = "stUfL1",
                   stCepL1 = "stCepL1",
                   stCnaeL1 = "stCnaeL1",
                   stCnaeDescL1 = "stCnaeDescL1",
                   stCdNatJurL1 = "stCdNatJurL1";

            cmd.Parameters.AddRange(new NpgsqlParameter[]
            {
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = id, Value = mdl.id },
                new() { NpgsqlDbType = NpgsqlDbType.Date, ParameterName = dtExpire, Value = GetNull(mdl.dtExpire) },
                new() { NpgsqlDbType = NpgsqlDbType.Date, ParameterName = dtAberturaL1, Value = GetNull(mdl.dtAberturaL1) },
                new() { NpgsqlDbType = NpgsqlDbType.Text, ParameterName = stCNPJ, Value = GetNull(mdl.stCNPJ) },
                new() { NpgsqlDbType = NpgsqlDbType.Text, ParameterName = stSituacaoCadL1, Value = GetNull(mdl.stSituacaoCadL1) },
                new() { NpgsqlDbType = NpgsqlDbType.Text, ParameterName = stSituacaoCadMotivL1, Value = GetNull(mdl.stSituacaoCadMotivL1) },
                new() { NpgsqlDbType = NpgsqlDbType.Text, ParameterName = stNomeL1, Value = GetNull(mdl.stNomeL1) },
                new() { NpgsqlDbType = NpgsqlDbType.Text, ParameterName = stFantasiaL1, Value = GetNull(mdl.stFantasiaL1) },
                new() { NpgsqlDbType = NpgsqlDbType.Text, ParameterName = stPorteL1, Value = GetNull(mdl.stPorteL1) },
                new() { NpgsqlDbType = NpgsqlDbType.Text, ParameterName = stMunicipioL1, Value = GetNull(mdl.stMunicipioL1) },
                new() { NpgsqlDbType = NpgsqlDbType.Text, ParameterName = stUfL1, Value = GetNull(mdl.stUfL1) },
                new() { NpgsqlDbType = NpgsqlDbType.Text, ParameterName = stCepL1, Value = GetNull(mdl.stCepL1) },                
                new() { NpgsqlDbType = NpgsqlDbType.Text, ParameterName = stCnaeL1, Value = GetNull(mdl.stCnaeL1) },
                new() { NpgsqlDbType = NpgsqlDbType.Text, ParameterName = stCnaeDescL1, Value = GetNull(mdl.stCnaeDescL1) },
                new() { NpgsqlDbType = NpgsqlDbType.Text, ParameterName = stCdNatJurL1, Value = GetNull(mdl.stCdNatJurL1) },
            });
        }

        public long InsertDadosEmpresa(Tb_DadosEmpresa mdl, bool retId = false)
        {
            const string query =
                "INSERT INTO \"DadosEmpresa\" (\"dtExpire\",\"dtAberturaL1\",\"stCNPJ\",\"stSituacaoCadL1\",\"stSituacaoCadMotivL1\"" +
                ",\"stNomeL1\",\"stFantasiaL1\",\"stPorteL1\",\"stMunicipioL1\",\"stUfL1\",\"stCepL1\",\"stCnaeL1\",\"stCnaeDescL1\",\"stCdNatJurL1\") VALUES " +
                "(@dtExpire,@dtAberturaL1,@stCNPJ,@stSituacaoCadL1,@stSituacaoCadMotivL1,@stNomeL1,@stFantasiaL1,@stPorteL1,@stMunicipioL1,@stUfL1,@stCepL1,@stCnaeL1,@stCnaeDescL1" +
                ",@stCdNatJurL1);";

            const string currval = "select currval('public.\"DadosEmpresa_id_seq\"');";

            using var cmd = new NpgsqlCommand(retId ? query + currval : query, db);
            SetParamsDadosEmpresa(cmd, mdl);
            if (retId) return (long)cmd.ExecuteScalar();
            cmd.ExecuteNonQuery();
            return 0;
        }

        public void UpdateDadosEmpresa(Tb_DadosEmpresa mdl)
        {
            const string query = "update \"DadosEmpresa\" set " +
                "\"dtExpire\"=@dtExpire," +
                "\"dtAberturaL1\"=@dtAberturaL1," +
                "\"stCNPJ\"=@stCNPJ," +
                "\"stSituacaoCadL1\"=@stSituacaoCadL1," +
                "\"stSituacaoCadMotivL1\"=@stSituacaoCadMotivL1," +
                "\"stNomeL1\"=@stNomeL1," +
                "\"stFantasiaL1\"=@stFantasiaL1," +
                "\"stPorteL1\"=@stPorteL1," +
                "\"stMunicipioL1\"=@stMunicipioL1," +
                "\"stUfL1\"=@stUfL1," +
                "\"stCepL1\"=@stCepL1," +
                "\"stCnaeL1\"=@stCnaeL1," +
                "\"stCnaeDescL1\"=@stCnaeDescL1," +
                "\"stCdNatJurL1\"=@stCdNatJurL1 " +
                
                " where id=@id";

            using var cmd = new NpgsqlCommand(query, db);
            SetParamsDadosEmpresa(cmd, mdl);
            cmd.ExecuteNonQuery();
        }
    }
}
