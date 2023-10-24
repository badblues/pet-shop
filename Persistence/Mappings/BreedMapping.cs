using FluentNHibernate.Mapping;
using Persistence.Models;

namespace Persistence.Mappings;

public class BreedMapping : ClassMap<Breed>
{
    public BreedMapping()
    {
        Table("Breeds");

        Id(x => x.Id, "id").GeneratedBy.Identity();
        Map(x => x.Name, "name");
    }
}
