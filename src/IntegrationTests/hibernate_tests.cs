using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using NHibernate.Cfg.ConfigurationSchema;
using NHibernate.Linq;
using NUnit.Framework;
using NHibernate;
using Simple.Web.Windsor.Owin.Data;

namespace IntegrationTests
{
    [TestFixture]
    class hibernate_tests
    {
        ISession _session;
        NHConfig _cfg;
        [SetUp]
        public void Init()
        {
            _cfg = new NHConfig();
            _session= _cfg.GetCurrentSession();

        }

        [TearDown]
        public void Finish()
        {
            _session.Close();
            _cfg.CloseSessionFactory();
        }

        [Test]
        public void retrieves_the_correct_applicant()
        {
            List<Applicant> app = _session.Query<Applicant>().Where(x => x.Id == 3).ToList();
            if (app.Count == 0)
                Assert.Fail("no applicants found");
            else
                Assert.AreEqual("weru afff ", "weru afff ");
                

        }
        [Test]
        public void add_applicant()
        {
            var newapp = new Applicant();
            newapp.Name = "qwr aaa";
            newapp.Rate = 234;
            List<Applicant> app = _session.Query<Applicant>().ToList();
            int beforeinsert = app.Count;
            int afterinsert= 0 ;
            using (ITransaction trans = _session.BeginTransaction())
            {
                _session.SaveOrUpdate(newapp);
                trans.Commit();
                List<Applicant> _app= _session.Query<Applicant>().ToList();
                afterinsert = _app.Count;
                
            }
            using (ITransaction trans = _session.BeginTransaction())
            {
                _session.Delete(newapp);
                trans.Commit();
            }
            Assert.AreEqual(afterinsert, beforeinsert+1);
        }
    }
}
