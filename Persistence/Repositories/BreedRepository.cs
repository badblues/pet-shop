
using NHibernate;
using Persistence.Models;

namespace Persistence.Repositories;

public class BreedRepository : IRepository<Breed>
{

    private readonly ISession _session;

    public BreedRepository(ISession session)
    {
        _session = session;
    }

    public void Add(Breed breed)
    {
        _ = _session.Save(breed);
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
        Breed breed = Get(id);
        _session.Delete(breed);
    }
}
