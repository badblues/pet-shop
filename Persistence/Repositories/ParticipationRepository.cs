﻿using NHibernate;
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
        return _session.Get<Participation>((animalId, competitionId));
    }

    public IEnumerable<Participation> GetAll()
    {
        _session.Flush();
        return _session.Query<Participation>().ToList();
    }

    public void Update(Participation participation)
    {
        _session.Update(participation);
    }

    public void Delete(int animalId, int competitionId)
    {
        Participation participation = Get(animalId, competitionId);
        _session.Delete(participation);
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
