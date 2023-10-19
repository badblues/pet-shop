using FluentNHibernate.Mapping;
using Persistence.Models;

namespace Persistence.Mappings;

public class ClientMap : ClassMap<Client>
{
    public ClientMap()
    {
        Table("Clients");

        Id(x => x.Id, "id").GeneratedBy.Identity();
        Map(x => x.Name, "name");
        Map(x => x.Address, "address");

    }
}
