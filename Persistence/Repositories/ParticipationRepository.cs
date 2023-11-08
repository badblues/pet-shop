using NHibernate;
using Persistence.Models;

namespace Persistence.Repositories;

public class ParticipationRepository : IRepository<Participation>
{
    private readonly ISession _session;

    public ParticipationRepository(ISession session)
    {
        _session = session;
    }

    public void Add(Participation participation)
    {
        _ = _session.Save(participation);
    }

    public Participation Get(int animalId, int competitionId)
    {
        _session.Flush();
        ICollection<Participation> participations = _session.Query<Participation>().ToList();
        return participations.First(p => p.Animal.Id == animalId && p.Competition.Id == competitionId);
    }

    public ICollection<Participation> GetAll()
    {
        _session.Flush();
        return _session.Query<Participation>().ToList();
    }

    public ICollection<Participation> GetByAnimalId(int animalId)
    {
        string hql = "FROM Participation p WHERE p.Animal.Id = :animalId";
        return _session.CreateQuery(hql)
                       .SetParameter("animalId", animalId)
                       .List<Participation>();
    }

    public ICollection<Participation> GetByCompetitionId(int competitionId)
    {
        string hql = "FROM Participation p WHERE p.Competition.Id = :competitionId";
        return _session.CreateQuery(hql)
                       .SetParameter("competitionId", competitionId)
                       .List<Participation>();
    }


    public void Update(Participation participation)
    {
        _session.Update(participation);
    }

    public void Delete(int animalId, int competitionId)
    {
        Participation participation = Get(animalId, competitionId);
        _session.Delete(participation);
        _session.Flush();
    }

    public Participation Get(int id)
    {
        throw new NotSupportedException("Get(int id) is not supported for Participation.");
    }

    public void Delete(int id)
    {
        throw new NotSupportedException("Delete(int id) is not supported for Participation.");
    }
}
