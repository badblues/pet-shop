using NHibernate;
using Persistence.Models;

namespace Persistence.Repositories;

public class AnimalRepository : IRepository<Animal>
{
    private readonly ISession _session;

    public AnimalRepository(ISession session)
    {
        _session = session;
    }

    public void Add(Animal animal)
    {
        _ = _session.Save(animal);
    }

    public Animal Get(int id)
    {
        _session.Flush();
        return _session.Get<Animal>(id);
    }

    public ICollection<Animal> GetAll()
    {
        _session.Flush();
        return _session.Query<Animal>().ToList();
    }

    public void Update(Animal animal)
    {
        _session.Update(animal);
    }

    public void Delete(int id)
    {
        Animal animal = _session.Get<Animal>(id);
        _session.Delete(animal);
    }
}
