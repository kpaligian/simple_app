using System.Data.Entity;
using Simple.Web.Windsor.Owin.Data;

namespace Costa.Data
{
    public class ApplicantContext: DbContext
    {
        public ApplicantContext() : base("simple-db")
        {
            
        }

        public DbSet<Applicant> Applicantses { get; set; }
    }
}