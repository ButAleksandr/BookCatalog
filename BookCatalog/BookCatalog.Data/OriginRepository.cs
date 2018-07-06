namespace BookCatalog.Data
{
    using System.Linq;
    using System.Data.SqlClient;
    using Dapper;

    public class OriginRepository
    {
        protected readonly string connString;

        public OriginRepository(string connString)
        {
            this.connString = connString;
        }

        public long GetCount(string query)
        {
            using (var db = new SqlConnection(this.connString))
            {
                return db.Query<long>(query).FirstOrDefault();
            }
        }
    }
}
