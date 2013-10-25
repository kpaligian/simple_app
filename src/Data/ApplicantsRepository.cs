using System;
using System.Linq;
using Simple.Web.Windsor.Owin.Data;

namespace Costa.Data
{
    public class ApplicantsRepository : IApplicantsRepository
    {
        ApplicantContext _ctx;
        public ApplicantsRepository(ApplicantContext ctx)
        {
           _ctx = ctx; 
        }

        public IQueryable<Applicant> GetApplicants()
        {
            return _ctx.Applicantses;
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
                _ctx.Applicantses.Add(newApplicant);
                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }
    }
}