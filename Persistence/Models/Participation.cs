namespace Persistence.Models;

public class Participation
{
    public virtual required Animal Animal { get; set; }

    public virtual required Competition Competition { get; set; }

    public virtual required string Award { get; set; }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Participation other = (Participation)obj;
        return Animal.Id == other.Animal.Id && Competition.Id == other.Competition.Id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Animal.Id, Competition.Id);
    }
}
