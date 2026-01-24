using Npgsql;
using System;
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

        public object? GetNull(long? fkValue)
        {
            return fkValue.HasValue ? fkValue.Value : DBNull.Value;
        }

        public object? GetNull(double? fkValue)
        {
            return fkValue.HasValue ? fkValue.Value : DBNull.Value;
        }

        public object? GetNull(DateOnly? fkValue)
        {
            return fkValue.HasValue ? fkValue.Value : DBNull.Value;
        }

        public object? GetNull(Decimal? fkValue)
        {
            return fkValue.HasValue ? fkValue.Value : DBNull.Value;
        }

        public object? GetNull(bool? fkValue)
        {
            return fkValue.HasValue ? fkValue.Value : DBNull.Value;
        }

        public object? GetNull(DateTime? fkValue)
        {
            return fkValue.HasValue ? fkValue.Value : DBNull.Value;
        }

        public object? GetNull(string fkValue)
        {
            return fkValue != null ? fkValue : DBNull.Value;
        }

        public void EnableCache()
        {
            cache = new Hashtable();
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
