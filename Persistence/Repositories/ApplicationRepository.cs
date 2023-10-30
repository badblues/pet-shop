using NHibernate;
using Persistence.Models;

namespace Persistence.Repositories;

public class ApplicationRepository : IRepository<Application>
{
    private readonly ISession _session;

    public ApplicationRepository(ISession session)
    {
        _session = session;
    }

    public void Add(Application application)
    {
        _ = _session.Save(application);
    }

    public Application Get(int id)
    {
        _session.Flush();
        return _session.Get<Application>(id);
    }

    public IEnumerable<Application> GetAll()
    {
        _session.Flush();
        return _session.Query<Application>().ToList();
    }

    public void Update(Application application)
    {
        _session.Update(application);
    }

    public void Delete(int id)
    {
        Application application = Get(id);
        _session.Delete(application);
    }
}
