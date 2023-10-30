using FluentNHibernate.Mapping;
using Persistence.Models;

namespace Persistence.Mappings;

public class EmployeeMapping : ClassMap<Employee>
{
    public EmployeeMapping()
    {
        Table("Employees");

        _ = Id(x => x.Id, "id").GeneratedBy.Identity();
        _ = Map(x => x.Name, "name");
        _ = Map(x => x.Address, "address");
        _ = Map(x => x.Position, "position");
        _ = Map(x => x.salary, "salary");
    }
}
