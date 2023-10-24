using FluentNHibernate.Mapping;
using Persistence.Models;

namespace Persistence.Mappings;

public class BreedMap : ClassMap<Breed>
{
    public BreedMap()
    {
        Table("Breeds");

        Id(x => x.Id, "id").GeneratedBy.Identity();
        Map(x => x.Name, "name");
    }
}
