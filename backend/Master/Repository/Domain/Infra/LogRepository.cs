using Dapper;
using Master.Entity.Database.Domain.Infra;

namespace Master.Repository.Domain.Infra
{
    public interface ILogRepository
    {
        long InsertLogApplication(Tb_LogApplication mdl);
    }

    public class LogRepository : BaseRepository, ILogRepository
    {
        public long InsertLogApplication(Tb_LogApplication mdl)
        {
            const string query =
                "INSERT INTO \"LogApplication\" (\"fkCompany\",\"fkUser\",\"stEndpoint\",\"dtLog\",\"stExceptionData\") " +
                "VALUES (@fkCompany,@fkUser,@stEndpoint,@dtLog,@stExceptionData)" +
                "RETURNING \"id\";";

            return db.ExecuteScalar<long>(query, mdl);
        }
    }
}
