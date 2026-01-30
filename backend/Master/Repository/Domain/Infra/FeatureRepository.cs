using Dapper;
using Master.Entity.Database.Domain.Infra;

namespace Master.Repository.Domain.Infra
{
    public interface IFeatureRepository
    {
        Tb_Feature GetFeature(string endpoint);
    }

    public class FeatureRepository : BaseRepository, IFeatureRepository
    {
        public Tb_Feature GetFeature(string endpoint)
        {
            const string query = "select * from \"Feature\" where \"stEndpoint\"=@endpoint";

            return db.QueryFirstOrDefault<Tb_Feature>(query, new { endpoint });
        }
    }
}
