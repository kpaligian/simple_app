using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Simple.Web.Windsor.Owin.Data;

namespace Costa.Data
{
    public class DummyApplicantContext : DbContext
    {
        public DummyApplicantContext() : base("simple-dummy"){
            
        }
        public DbSet<Applicant> Applicants { get; set; }
    }
}
