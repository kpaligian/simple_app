using System.Data.Entity.ModelConfiguration;
using Simple.Web.Windsor.Owin.Data;

namespace Costa.Data.Mappings
{
    public class ApplicantMap : EntityTypeConfiguration<Applicant>
    {
        public ApplicantMap()
        {
            HasKey(t => t.Id);
            Property(t => t.Name).HasMaxLength(200);
           // Property(t => t.EmailAddress).HasMaxLength(200);
        }
    }
}