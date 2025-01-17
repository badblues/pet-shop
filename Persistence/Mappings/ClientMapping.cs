﻿using FluentNHibernate.Mapping;
using Persistence.Models;

namespace Persistence.Mappings;

public class ClientMapping : ClassMap<Client>
{
    public ClientMapping()
    {
        Table("Clients");

        _ = Id(x => x.Id, "id")
            .GeneratedBy.Identity()
            .Not.Nullable();
        _ = Map(x => x.Name, "name")
            .Not.Nullable();
        _ = Map(x => x.Address, "address")
            .Not.Nullable();

        _ = HasMany(x => x.Animals)
            .KeyColumn("owner_id");
        _ = HasMany(x => x.Applications)
            .KeyColumn("client_id");

    }
}
