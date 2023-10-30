using FluentNHibernate.Mapping;
using Persistence.Models;

namespace Persistence.Mappings;

public class ClientMapping : ClassMap<Client>
{
    public ClientMapping()
    {
        Table("Clients");

        _ = Id(x => x.Id, "id").GeneratedBy.Identity();
        _ = Map(x => x.Name, "name");
        _ = Map(x => x.Address, "address");

    }
}
