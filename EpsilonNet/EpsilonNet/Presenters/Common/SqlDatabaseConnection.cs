using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpsilonNet.Presenters.Common
{
    public class SqlDatabaseConnection
    {
        private static SqlConnection _instance;
        private static readonly object LockObject = new object();

        private SqlDatabaseConnection()
        {
            // Private constructor to prevent direct instantiation
        }

        public static SqlConnection Instance
        {
            get
            {
                lock (LockObject)
                {
                    if (_instance == null)
                    {
                        string sqlConnectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
                        _instance = new SqlConnection(sqlConnectionString);
                        _instance.Open();
                    }

                    return _instance;
                }
            }
        }
    }
}
