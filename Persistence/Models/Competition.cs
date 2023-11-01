namespace Persistence.Models;

public class Competition
{
    public virtual int Id { get; set; }

    public virtual required string Name { get; set; }

    public virtual required string Location { get; set; }

    public virtual required DateTime Date { get; set; }
}
