using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Simple.Web.Windsor.Owin.Data;

namespace Costa.Data.Mappings
{
    class ApplicantNHMappings:ClassMap<Applicant>
    {
        public ApplicantNHMappings()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Rate);
        }
    }
}
