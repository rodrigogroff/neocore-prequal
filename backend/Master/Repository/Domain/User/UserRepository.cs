using Dapper;
using Master.Entity.Database.Domain.User;
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
            const string query = "SELECT * FROM \"User\" WHERE \"id\"=@id AND \"fkCompany\"=@fkCompany";
            return db.QueryFirstOrDefault<Tb_User>(query, new { id, fkCompany });
        }

        public Tb_User GetUser(string email)
        {
            const string query = "SELECT * FROM \"User\" WHERE \"stEmail\"=@email";
            return db.QueryFirstOrDefault<Tb_User>(query, new { email });
        }

        public List<Tb_User> GetUsers(int fkCompany)
        {
            const string query = "SELECT * FROM \"User\" WHERE \"fkCompany\"=@fkCompany ORDER BY \"id\" DESC";
            return db.Query<Tb_User>(query, new { fkCompany }).ToList();
        }

        public long InsertUser(Tb_User mdl, bool retId = false)
        {
            const string query =
                "INSERT INTO \"User\" (" +
                "\"fkCompany\"," +
                "\"stEmail\"," +
                "\"stCPF\"," +
                "\"stName\"," +
                "\"stPhoneNumber\"," +
                "\"stPassword\"," +
                "\"bActive\"," +
                "\"bAdmin\"" +
                ") VALUES (" +
                "@fkCompany," +
                "@stEmail," +
                "@stCPF," +
                "@stName," +
                "@stPhoneNumber," +
                "@stPassword," +
                "@bActive," +
                "@bAdmin" +
                ") RETURNING \"id\";";

            if (retId)
            {
                return db.ExecuteScalar<long>(query, mdl);
            }

            db.Execute(query, mdl);
            return 0;
        }

        public void UpdateUser(Tb_User mdl)
        {
            const string query =
                "UPDATE \"User\" SET " +
                "\"stEmail\"=@stEmail," +
                "\"stCPF\"=@stCPF," +
                "\"stName\"=@stName," +
                "\"stPhoneNumber\"=@stPhoneNumber," +
                "\"stPassword\"=@stPassword," +
                "\"bActive\"=@bActive," +
                "\"bAdmin\"=@bAdmin " +
                "WHERE \"id\"=@id";

            db.Execute(query, mdl);
        }
    }
}
