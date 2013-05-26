using System.Linq;

namespace WorldMessages.DomainModelFramework.Query
{
    public class QueryResult<T>
    {
        public QueryResult(IQueryable<T> list, int total)
        {
            List = list;
            Total = total;
        }

        public IQueryable<T> List
        {
            get;
            private set;
        }

        public int Total
        {
            get;
            private set;
        }

        public QueryResult<T> Include()
        {
            //List.Include
            return this;
        }
    }
}
