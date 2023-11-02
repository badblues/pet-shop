using FluentNHibernate.Mapping;
using Persistence.Models;

namespace Persistence.Mappings;

public class CompetitionMapping : ClassMap<Competition>
{
    public CompetitionMapping()
    {
        Table("Competitions");

        _ = Id(x => x.Id, "id")
            .GeneratedBy.Identity()
            .Not.Nullable();

        _ = Map(x => x.Name, "name")
            .Not.Nullable();
        _ = Map(x => x.Location, "location")
            .Not.Nullable();
        _ = Map(x => x.Date, "date")
            .Not.Nullable();

        _ = HasMany(x => x.Participations)
            .KeyColumn("competition_id");
    }
}
