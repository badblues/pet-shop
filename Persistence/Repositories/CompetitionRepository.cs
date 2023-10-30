using NHibernate;
using Persistence.Models;

namespace Persistence.Repositories;

public class CompetitionRepository : IRepository<Competition>
{
    private readonly ISession _session;

    public CompetitionRepository(ISession session)
    {
        _session = session;
    }

    public void Add(Competition participation)
    {
        _ = _session.Save(participation);
    }

    public Competition Get(int id)
    {
        _session.Flush();
        return _session.Get<Competition>(id);
    }

    public IEnumerable<Competition> GetAll()
    {
        _session.Flush();
        return _session.Query<Competition>().ToList();
    }

    public void Update(Competition participation)
    {
        _session.Update(participation);
    }

    public void Delete(int id)
    {
        Competition participation = Get(id);
        _session.Delete(participation);
    }
}
