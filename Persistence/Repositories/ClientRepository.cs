using NHibernate;
using Persistence.Models;

namespace Persistence.Repositories;

public class ClientRepository : IRepository<Client>
{

    private readonly ISession _session;

    public ClientRepository(ISession session)
    {
        _session = session;
    }

    public void Add(Client client)
    {
        _ = _session.Save(client);
    }

    public Client Get(int id)
    {
        _session.Flush();
        return _session.Get<Client>(id);
    }

    public IEnumerable<Client> GetAll()
    {
        _session.Flush();
        return _session.Query<Client>().ToList();
    }

    public void Update(Client client)
    {
        _session.Update(client);
    }

    public void Delete(int id)
    {
        Client client = Get(id);
        _session.Delete(client);
    }
}
