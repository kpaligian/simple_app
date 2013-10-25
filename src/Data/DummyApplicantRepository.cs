using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Web.Windsor.Owin.Data;

namespace Costa.Data
{
    public class DummyApplicantRepository
    {
         DummyApplicantContext _ctx;
        public DummyApplicantRepository(DummyApplicantContext ctx)
        {
           _ctx = ctx; 
        }

        public IQueryable<Applicant> GetApplicants()
        {
            return _ctx.Applicants;
        }



        public bool Save()
        {
            try {
               return  _ctx.SaveChanges() > 0;
            }
            catch(Exception ex){
                //TODO log
                return false;
            }
        }

        public bool InsertApplicant(Applicant newApplicant)
        {
            try
            {
                _ctx.Applicants.Add(newApplicant);
                _ctx.Entry(newApplicant).State = EntityState.Unchanged;
                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }
    }
}
