using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Simple.Web;
using Simple.Web.Behaviors;
using Simple.Web.Windsor.Owin.Data;

namespace Simple.Web.Windsor.Owin.Resources
{
    public class ApplicantResource
    {
        public Uri Self { get; set; }
        public string Name { get; set; }
        public int Rate { get; set; }

    }


    [UriTemplate("/resources/applicants")]
    public class GetApplicants : IGet, IOutput<IEnumerable<ApplicantResource>>
    {
        private IApplicantsRepository _repo;

        public GetApplicants(IApplicantsRepository repo)
        {
            _repo = repo;
        }

        public Status Get()
        {
            Output = _repo.GetApplicants().Select(a => new ApplicantResource{ Name = a.Name , Rate = a.Rate});
            return 200;
        }

        public IEnumerable<ApplicantResource> Output { get; set; }
    }
}