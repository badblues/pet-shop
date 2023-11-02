namespace Persistence.Models;

public class Client
{
    public virtual int Id { get; set; }

    public virtual required string Name { get; set; } = string.Empty;

    public virtual required string Address { get; set; } = string.Empty;

    public virtual ICollection<Animal> Animals { get; set; } = new List<Animal>();

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
}
