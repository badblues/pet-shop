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

    public void Add(Competition competition)
    {
        _ = _session.Save(competition);
    }

    public Competition Get(int id)
    {
        _session.Flush();
        return _session.Get<Competition>(id);
    }

    public ICollection<Competition> GetAll()
    {
        _session.Flush();
        return _session.Query<Competition>().ToList();
    }

    public void Update(Competition competition)
    {
        _session.Update(competition);
    }

    public void Delete(int id)
    {
        Competition competition = Get(id);
        _session.Delete(competition);
    }
}
