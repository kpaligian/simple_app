using System.Linq;

namespace Simple.Web.Windsor.Owin.Data
{
    public interface IApplicantsRepository
    {
        IQueryable<Applicant> GetApplicants();

        bool Save();
        bool InsertApplicant(Applicant newApplicant);
    }
}