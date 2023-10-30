using FluentNHibernate.Mapping;
using Persistence.Models;

namespace Persistence.Mappings;

public class CompetitionMapping : ClassMap<Competition>
{
    public CompetitionMapping()
    {
        Table("Participations");

        _ = Id(x => x.Id, "id").GeneratedBy.Identity();

        _ = Map(x => x.Name, "name");
        _ = Map(x => x.Location, "location");
        _ = Map(x => x.Date, "date");
    }
}
