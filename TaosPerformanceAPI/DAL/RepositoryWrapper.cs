namespace TaosPerformanceAPI.DAL
{
    public interface IRepositoryWrapper
    {
        TaosPerformanceDB taosDB { get; }
    }

    public class RepositoryWrapper : IRepositoryWrapper
    {
        private MySQLContext _mysqlContext;
        private TaosPerformanceDB _taosDB;

        public TaosPerformanceDB taosDB
        {
            get
            {
                if (_taosDB == null)
                {
                    _taosDB = new TaosPerformanceDB(_mysqlContext);
                }
                return _taosDB;
            }
        }

        public RepositoryWrapper(MySQLContext context)
        {
            _mysqlContext = context;
        }
    }
}
