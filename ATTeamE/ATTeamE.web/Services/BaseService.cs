using ATTeamE.web.Interfaces.Sql;
using ATTeamE.web.Providers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Configuration;

namespace ATTeamE.web.Services
{
    public abstract class BaseService
    {
        public BaseService() {
            _connectionKey = "DefaultConnection";
        }

        public BaseService(string connectionKey) { _connectionKey = connectionKey; }

        private string _connectionKey;
        private string _path = Path.GetFullPath(Path.Combine(HttpContext.Current.Server.MapPath("~"), "../Nostreets Services/Querys/"));

        public SqlConnection Connection { get { return new SqlConnection(WebConfigurationManager.ConnectionStrings[_connectionKey].ConnectionString); } }

        protected static IDao DataProvider
        {

            get { return Providers.DataProvider.SqlInstance; }
        }

    }
}