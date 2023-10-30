using System.ComponentModel.DataAnnotations;

namespace Persistence.Models;

public class Competition
{
    [Required]
    public virtual int Id { get; set; }

    [Required]
    public virtual required string Name { get; set; }

    [Required]
    public virtual required string Location { get; set; }

    [Required]
    public virtual DateOnly Date { get; set; }
}
