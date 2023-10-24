
using NHibernate;
using Persistence.Models;

namespace Persistence.Repositories;

public class BreedRepository : IRepository<Breed>
{

    private ISession _session;

    public BreedRepository(ISession session)
    {
        _session = session;
    }

    public void Add(Breed breed)
    {
        _session.Save(breed);
    }

    public Breed Get(int id)
    {
        _session.Flush();
        return _session.Get<Breed>(id);
    }

    public IEnumerable<Breed> GetAll()
    {
        _session.Flush();
        return _session.Query<Breed>().ToList();
    }

    public void Update(Breed breed)
    {
        _session.Update(breed);
    }
    public void Delete(int id)
    {
        var item = Get(id);
        _session.Delete(item);
    }
}
