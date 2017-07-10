using ATTeamE.web.Interfaces.Sql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATTeamE.web.Providers
{
    public sealed class DataProvider
    {
        private DataProvider() { }

        public static IDao SqlInstance
        {
            get
            {
                return SqlDao.Instance;
            }
        }

    }
}
