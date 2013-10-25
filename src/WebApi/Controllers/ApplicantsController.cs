using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Core.Messages;
using MassTransit;
using Simple.Web.Windsor.Owin.Data;

namespace Simple.Web.Windsor.Owin.Controllers
{
    public class ApplicantsController : ApiController
    {
        private IApplicantsRepository _repo;

        public ApplicantsController(IApplicantsRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Applicant> Get()
        {
            return _repo.GetApplicants();
        }

        public HttpResponseMessage Post([FromBody]Applicant newApplicant) {
            if (_repo.InsertApplicant(newApplicant) &&
            _repo.Save()) {
                Bus.Instance.Publish(new CreateApplicantMessage { Applicant = newApplicant });
                return Request.CreateResponse(HttpStatusCode.Created);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
