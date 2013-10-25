using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using Simple.Web.Windsor.Owin.Data;

namespace Core
{
    public class ApplicationMapper
    {
        const string TableName = "Applicant";

        public ApplicationMapper() { }

        public HbmMapping Map()
        {
            var mapper = new ModelMapper();
            mapper.Class<Applicant>(table =>
            {
                table.Table(TableName);

                table.Id(p => p.Id, g => g.Generator(Generators.Native));
                table.Property(p => p.Name);
                table.Property(p=> p.Rate);
                

            });

          

            return mapper.CompileMappingForAllExplicitlyAddedEntities();
        }
    }
}