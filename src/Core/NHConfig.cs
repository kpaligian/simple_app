using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using Simple.Web.Windsor.Owin.Data;

namespace Core
{
    public class NHConfig
    {
        static ISessionFactory _factory;
        public void OpenSession()
        {
            var config = new Configuration();
            config.SetProperty("connection.driver_class", "NHibernate.Driver.SqlClientDriver");
            config.SetProperty("dialect","NHibernate.Dialect.MsSql2012Dialect");
            config.SetProperty("connection.connection_string", "Data Source=localhost\\SQLEXPRESS;initial catalog=simple-db;Integrated Security=SSPI;");
            config.SetProperty("connection.release_mode","on_close");


            var mapper = new ApplicationMapper();
            config.AddMapping(mapper.Map());


            _factory = config.BuildSessionFactory();
        }

        public ISession GetCurrentSession()
        {
            if(_factory== null)
                this.OpenSession();

            return _factory.OpenSession();
        }

        public void CloseSessionFactory()
        {
            if (_factory != null)
                _factory.Close();
        }

    }
}
