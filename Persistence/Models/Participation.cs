namespace Persistence.Models;

public class Participation
{
    public virtual required int AnimalId { get; set; }

    public virtual required Animal Animal { get; set; }

    public virtual required int CompetitionId { get; set; }

    public virtual required Competition Competition { get; set; }

    public virtual required string Award { get; set; }
}
