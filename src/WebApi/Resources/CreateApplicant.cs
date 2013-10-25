using System.IO;
using Core.Messages;
using Simple.Web.Behaviors;
using Simple.Web.Windsor.Owin.Data;
using MassTransit;

namespace Simple.Web.Windsor.Owin.Resources
{

    [UriTemplate("/resources/create_applicant")]
    public class CreateApplicant : IPost, IInput<Applicant>, IOutput<Applicant>
    {

        private IApplicantsRepository _repo;

         public CreateApplicant(IApplicantsRepository repo)
        {
            _repo = repo;
        }

        public Status Post()
        {
           
            Applicant newApplicant = Input;
            _repo.InsertApplicant(newApplicant);
            _repo.Save();


            Output = newApplicant;

            Bus.Instance.Publish(new CreateApplicantMessage{Applicant = newApplicant});

            return 200;
        }

        public Applicant Input { set; get; }
        public Applicant Output { get; private set; }
    }
}