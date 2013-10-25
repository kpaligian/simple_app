using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Transactions;
using Costa.Data;
using NUnit.Framework;
using Simple.Web.Windsor.Owin.Data;
using System.Data.Entity;

namespace IntegrationTests
{
    /// <summary>
    /// 
    ///     Init Database (i.e. Create Schema)
    ///     Insert Default (Reference Data)
    ///     Run Tests Inside A TX & Rollback At The End
    /// 
    ///     Store something to the db
    ///     Get it back out, and test the values are correct
    /// </summary>
    [TestFixture]
    public class when_an_applicant_is_saved_to_the_database
    {
        //Customer stored
        //Customer created
        private readonly DummyApplicantRepository _repo = new DummyApplicantRepository(new DummyApplicantContext());
        DummyApplicantContext _context = new DummyApplicantContext();

        [SetUp]
        public void Setup()
        {
            
            Database.SetInitializer(new DropCreateDatabaseAlways<DummyApplicantContext>());
             _context.Database.CreateIfNotExists();

                var newapp = new Applicant();
                newapp.Name = "as adasw";
            newapp.Rate = 400;

            _repo.InsertApplicant(newapp);
             _repo.Save();


            //Candidate, TableName = "recruitment_candidate"
            //CandidateMapping, 
            
            
        }

        [TearDown]

        public void Finish()
        {
             _context.Database.Delete();
        }


        [Test]
        public void It_should_store_the_attributes_correctly()
        {
            
                var newApplicant = new Applicant();
                newApplicant.Name = "assdh awutht";
                newApplicant.Rate = 420;
                _repo.InsertApplicant(newApplicant);
                _repo.Save();

                Applicant lastInsertedApplicant = _repo.GetApplicants().OrderByDescending(a => a.Id).FirstOrDefault();
                Assert.AreEqual(newApplicant.Name,lastInsertedApplicant.Name);
                Assert.AreEqual(newApplicant.Rate, lastInsertedApplicant.Rate);
        }

        [Test]
        public void the_number_of_applicants_should_increase_by_one()
        {
            var newApplicant = new Applicant();
            int applicantsBeforeInsert = _repo.GetApplicants().AsEnumerable().ToArray().Length;
            newApplicant.Name = "asduth awutht";
            newApplicant.Rate = 405;
            _repo.InsertApplicant(newApplicant);
            _repo.Save();
            int applicantsAfterInsert = _repo.GetApplicants().AsEnumerable().ToArray().Length;
            Assert.AreEqual(applicantsBeforeInsert + 1, applicantsAfterInsert);
        }
    }
}