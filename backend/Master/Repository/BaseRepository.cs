using Npgsql;
using System.Collections;

namespace Master.Repository
{
    public class BaseRepository
    {
        public NpgsqlConnection db;
        public Hashtable cache = null;

        public const string pct = "%", offset = " OFFSET ", limit = " LIMIT ";

        public string Pagination(int? pageSize, int? pageNumber)
        {
            return offset + ((pageNumber - 1) * pageSize) + limit + pageSize;
        }

        public void EnableCache()
        {
            cache = [];
        }

        public void DisposeRepository()
        {
            if (cache != null)
            {
                cache.Clear();
                cache = null;
                db = null;
            }
        }
    }
}
