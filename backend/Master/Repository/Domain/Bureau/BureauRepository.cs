using Dapper;
using Master.Entity.Database.Domain.Bureau;

namespace Master.Repository.Domain.Bureau
{
    public interface IBureauRepository
    {
        Tb_DadosEmpresa GetDadosEmpresa(string cnpj);
        long InsertDadosEmpresa(Tb_DadosEmpresa mdl);
        void UpdateDadosEmpresa(Tb_DadosEmpresa mdl);
    }

    public class BureauRepository : BaseRepository, IBureauRepository
    {
        public Tb_DadosEmpresa GetDadosEmpresa(string cnpj)
        {
            const string query = "SELECT * FROM \"DadosEmpresa\" WHERE \"stCNPJ\"=@cnpj";
            return db.QueryFirstOrDefault<Tb_DadosEmpresa>(query, new { cnpj });
        }

        public long InsertDadosEmpresa(Tb_DadosEmpresa mdl)
        {
            const string query =
                "INSERT INTO \"DadosEmpresa\" (" +
                "\"dtExpire\"," +
                "\"dtAberturaL1\"," +
                "\"stCNPJ\"," +
                "\"stSituacaoCadL1\"," +
                "\"stSituacaoCadMotivL1\"," +
                "\"stNomeL1\"," +
                "\"stFantasiaL1\"," +
                "\"stPorteL1\"," +
                "\"stMunicipioL1\"," +
                "\"stUfL1\"," +
                "\"stCepL1\"," +
                "\"stCnaeL1\"," +
                "\"stCnaeDescL1\"," +
                "\"stCdNatJurL1\"" +
                ") VALUES (" +
                "@dtExpire," +
                "@dtAberturaL1," +
                "@stCNPJ," +
                "@stSituacaoCadL1," +
                "@stSituacaoCadMotivL1," +
                "@stNomeL1," +
                "@stFantasiaL1," +
                "@stPorteL1," +
                "@stMunicipioL1," +
                "@stUfL1," +
                "@stCepL1," +
                "@stCnaeL1," +
                "@stCnaeDescL1," +
                "@stCdNatJurL1" +
                ") RETURNING \"id\";";

            return db.ExecuteScalar<long>(query, mdl);
        }

        public void UpdateDadosEmpresa(Tb_DadosEmpresa mdl)
        {
            const string query =
                "UPDATE \"DadosEmpresa\" SET " +
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
                "WHERE \"id\"=@id";

            db.Execute(query, mdl);
        }
    }
}
