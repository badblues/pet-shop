using NHibernate;
using Persistence.Models;

namespace Persistence.Repositories;

public class EmployeeRepository : IRepository<Employee>
{

    private readonly ISession _session;

    public EmployeeRepository(ISession session)
    {
        _session = session;
    }

    public void Add(Employee employee)
    {
        _ = _session.Save(employee);
    }

    public Employee Get(int id)
    {
        _session.Flush();
        return _session.Get<Employee>(id);
    }

    public IEnumerable<Employee> GetAll()
    {
        _session.Flush();
        return _session.Query<Employee>().ToList();
    }

    public void Update(Employee employee)
    {
        _session.Update(employee);
    }
    public void Delete(int id)
    {
        Employee employee = _session.Get<Employee>(id);
        _session.Delete(employee);
    }
}
