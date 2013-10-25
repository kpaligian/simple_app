using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Simple.Web.Windsor.Owin.Data
{
    public class Applicant
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int Rate { get; set; }
       // public virtual string EmailAddress { get; set; }
    }

    public class ApplicantManager
    {
        readonly IApplicantsRepository _applicantsRepository;

        public ApplicantManager(IApplicantsRepository applicantsRepository)
        {
            _applicantsRepository = applicantsRepository;
        }

        

    }

}