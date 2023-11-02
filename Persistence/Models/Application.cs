namespace Persistence.Models;

public class Application
{
    public virtual int Id { get; set; }

    public virtual required Client Client { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual required Breed Breed { get; set; }

    public virtual Gender? Gender { get; set; }

    public virtual required DateTime ApplicationDate { get; set; }

    public virtual required bool Completed { get; set; }
}
