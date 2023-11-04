using FluentNHibernate.Mapping;
using Persistence.Models;
using Persistence.UserTypes;

namespace Persistence.Mappings;

public class ApplicationMapping : ClassMap<Application>
{

    public ApplicationMapping()
    {
        Table("Applications");

        _ = Id(x => x.Id, "id")
            .GeneratedBy.Identity()
            .Not.Nullable();

        _ = References(x => x.Client, "client_id")
            .Not.Nullable();

        _ = References(x => x.Employee, "employee_id");

        _ = References(x => x.Breed, "breed_id")
            .Not.Nullable();

        _ = Map(x => x.Gender)
            .Column("gender")
            .CustomType<GenderType>();
        _ = Map(x => x.ApplicationDate, "application_date")
            .Not.Nullable();
        _ = Map(x => x.Completed, "completed")
            .Not.Nullable();

    }
}
