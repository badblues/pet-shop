using FluentNHibernate.Mapping;
using Persistence.Models;

namespace Persistence.Mappings;

public class ParticipationMapping : ClassMap<Participation>
{
    public ParticipationMapping()
    {
        Table("Participations");

        CompositeId()
           .KeyReference(x => x.Animal, "animal_id")
           .KeyReference(x => x.Competition, "competition_id");

        Map(x => x.Award, "award").Not.Nullable();
    }
}
