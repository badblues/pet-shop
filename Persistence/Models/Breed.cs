namespace Persistence.Models;

public class Breed
{
    public virtual int Id { get; set; }

    public virtual required string Name { get; set; } = string.Empty;
}
