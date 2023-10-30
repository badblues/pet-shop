using FluentNHibernate.Mapping;
using Persistence.Models;
using Persistence.UserTypes;

namespace Persistence.Mappings;

public class AnimalMapping : ClassMap<Animal>
{
    public AnimalMapping()
    {
        Table("Animals");

        _ = Id(x => x.Id, "id").GeneratedBy.Identity();

        _ = References(x => x.Breed, "breed_id")
            .Cascade.SaveUpdate();

        _ = References(x => x.Client, "owner_id")
            .Cascade.SaveUpdate();

        _ = Map(x => x.Name, "name");
        _ = Map(x => x.Age, "age");
        _ = Map(x => x.ExteriorDescription, "exterior_description");
        _ = Map(x => x.Pedigree, "pedigree");
        _ = Map(x => x.Veterinarian, "veterinarian");
        _ = Map(x => x.Gender)
            .Column("gender")
            .CustomType<GenderType>();

    }
}

