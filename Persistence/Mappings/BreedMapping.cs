using FluentNHibernate.Mapping;
using Persistence.Models;

namespace Persistence.Mappings;

public class BreedMapping : ClassMap<Breed>
{
    public BreedMapping()
    {
        Table("Breeds");

        _ = Id(x => x.Id, "id").GeneratedBy.Identity();
        _ = Map(x => x.Name, "name");
    }
}
