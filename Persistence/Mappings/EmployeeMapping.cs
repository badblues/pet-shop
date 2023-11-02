using FluentNHibernate.Mapping;
using Persistence.Models;

namespace Persistence.Mappings;

public class EmployeeMapping : ClassMap<Employee>
{
    public EmployeeMapping()
    {
        Table("Employees");

        _ = Id(x => x.Id, "id")
            .GeneratedBy.Identity()
            .Not.Nullable();
        _ = Map(x => x.Name, "name")
            .Not.Nullable();
        _ = Map(x => x.Address, "address")
            .Not.Nullable();
        _ = Map(x => x.Position, "position")
            .Not.Nullable();
        _ = Map(x => x.Salary, "salary")
            .Not.Nullable();

        _ = HasMany(x => x.Applications)
            .KeyColumn("employee_id");
    }
}
