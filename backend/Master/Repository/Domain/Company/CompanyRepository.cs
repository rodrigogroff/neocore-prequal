using Dapper;
using Master.Entity.Database.Domain.Company;
using Npgsql;
using NpgsqlTypes;
using System.Collections.Generic;
using System.Linq;

namespace Master.Repository.Domain.Company
{
    public interface ICompanyRepository 
    {
        Tb_Company GetCompany(long id);
        List<Tb_Company> GetCompanies();
        long InsertCompany(Tb_Company mdl, bool retId = false);
        void UpdateCompany(Tb_Company mdl);
    }
        
    public class CompanyRepository : BaseRepository, ICompanyRepository
    {
        public Tb_Company GetCompany(long id)
        {
            const string query = "select * from \"Company\" where \"id\"=@id";

            return db.QueryFirstOrDefault<Tb_Company>(query, new { id });
        }

        public List<Tb_Company> GetCompanies()
        {
            const string query = "select * from \"Company\" order by \"id\" desc";

            return db.Query<Tb_Company>(query).ToList();
        }

        public void SetParamsCompany(NpgsqlCommand cmd, Tb_Company mdl)
        {
            const
               string
                   id = "id",
                   stName = "stName",
                   client_id = "client_id",
                   stSecret = "stSecret",
                   bActive = "bActive";

            cmd.Parameters.AddRange(new NpgsqlParameter[]
            {
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = id, Value = mdl.id },
                new() { NpgsqlDbType = NpgsqlDbType.Uuid, ParameterName = client_id, Value = mdl.client_id },
                new() { NpgsqlDbType = NpgsqlDbType.Text, ParameterName = stName, Value = mdl.stName },
                new() { NpgsqlDbType = NpgsqlDbType.Text, ParameterName = stSecret, Value = mdl.stSecret },
                new() { NpgsqlDbType = NpgsqlDbType.Boolean, ParameterName = bActive, Value = mdl.bActive },
            });
        }

        public long InsertCompany(Tb_Company mdl, bool retId = false)
        {
            const string query =
                "INSERT INTO \"Company\" (\"stName\",\"client_id\",\"stSecret\",\"bActive\") VALUES " +
                "(@stName,@client_id,@stSecret,@bActive);";

            const string currval = "select currval('public.\"Company_id_seq\"');";

            using var cmd = new NpgsqlCommand(retId ? query + currval : query, db);
            SetParamsCompany(cmd, mdl);
            if (retId) return (long)cmd.ExecuteScalar();
            cmd.ExecuteNonQuery();
            return 0;
        }

        public void UpdateCompany(Tb_Company mdl)
        {
            const string query = "update \"Company\" set " +
                "\"stName\"=@stName," +
                "\"client_id\"=@client_id," +
                "\"stSecret\"=@stSecret," +
                "\"bActive\"=@bActive " +
                "where id=@id";

            using var cmd = new NpgsqlCommand(query, db);
            SetParamsCompany(cmd, mdl);
            cmd.ExecuteNonQuery();
        }
    }
}
