using Dapper;
using Master.Entity.Database.Domain.User;
using Npgsql;
using NpgsqlTypes;
using System.Collections.Generic;
using System.Linq;

namespace Master.Repository.Domain.User
{
    public interface IUserRepository
    {
        Tb_User GetUser(int fkCompany, long id);
        Tb_User GetUser(string email);
        List<Tb_User> GetUsers(int fkCompany);
        long InsertUser(Tb_User mdl, bool retId = false);
        void UpdateUser(Tb_User mdl);
    }

    public class UserRepository : BaseRepository, IUserRepository
    {
        public Tb_User GetUser(int fkCompany, long id)
        {
            const string query = "select * from \"User\" where \"id\"=@id and \"fkCompany\"=@fkCompany";

            return db.QueryFirstOrDefault<Tb_User>(query, new { id, fkCompany });
        }

        public Tb_User GetUser(string email)
        {
            const string query = "select * from \"User\" where \"stEmail\"=@email";

            return db.QueryFirstOrDefault<Tb_User>(query, new { email });
        }

        public List<Tb_User> GetUsers(int fkCompany)
        {
            const string query = "select * from \"User\" \"fkCompany\"=@fkCompany order by \"id\" desc";

            return db.Query<Tb_User>(query, new { fkCompany }).ToList();
        }
             
        public void SetParamsUser(NpgsqlCommand cmd, Tb_User mdl)
        {
            const
               string
                   id = "id",
                   fkCompany = "fkCompany",
                   stEmail = "stEmail",
                   stCPF = "stCPF",
                   stName = "stName",
                   stPhoneNumber = "stPhoneNumber",
                   stPassword = "stPassword",
                   bActive = "bActive",
                   bAdmin = "bAdmin";

            cmd.Parameters.AddRange(new NpgsqlParameter[]
            {
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = id, Value = mdl.id },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = fkCompany, Value = GetNull(mdl.fkCompany) },
                new() { NpgsqlDbType = NpgsqlDbType.Text, ParameterName = stEmail, Value = mdl.stEmail },
                new() { NpgsqlDbType = NpgsqlDbType.Text, ParameterName = stCPF, Value = mdl.stCPF },
                new() { NpgsqlDbType = NpgsqlDbType.Text, ParameterName = stName, Value = mdl.stName },
                new() { NpgsqlDbType = NpgsqlDbType.Text, ParameterName = stPhoneNumber, Value = mdl.stPhoneNumber },
                new() { NpgsqlDbType = NpgsqlDbType.Text, ParameterName = stPassword, Value = mdl.stPassword },
                new() { NpgsqlDbType = NpgsqlDbType.Boolean, ParameterName = bActive, Value = GetNull(mdl.bActive) },
                new() { NpgsqlDbType = NpgsqlDbType.Boolean, ParameterName = bAdmin, Value = GetNull(mdl.bAdmin) },
            });
        }

        public long InsertUser(Tb_User mdl, bool retId = false)
        {
            const string query =
                "INSERT INTO \"User\" (\"fkCompany\",\"stEmail\",\"stCPF\",\"stName\",\"stPhoneNumber\",\"stPassword\",\"bActive\",\"bAdmin\") VALUES " +
                "(@fkCompany,@stEmail,@stCPF,@stName,@stPhoneNumber,@stPassword,@bActive,@bAdmin);";

            const string currval = "select currval('public.\"User_id_seq\"');";

            using var cmd = new NpgsqlCommand(retId ? query + currval : query, db);
            SetParamsUser(cmd, mdl);
            if (retId) return (long)cmd.ExecuteScalar();
            cmd.ExecuteNonQuery();
            return 0;
        }

        public void UpdateUser(Tb_User mdl)
        {
            const string query = "update \"User\" set " +
                "\"stEmail\"=@stEmail," +
                "\"stCPF\"=@stCPF," +
                "\"stName\"=@stName," +
                "\"stPhoneNumber\"=@stPhoneNumber," +
                "\"stPassword\"=@stPassword," +
                "\"bActive\"=@bActive," +
                "\"bAdmin\"=@bAdmin " +
                "where id=@id";

            using var cmd = new NpgsqlCommand(query, db);
            SetParamsUser(cmd, mdl);
            cmd.ExecuteNonQuery();
        }
    }
}
