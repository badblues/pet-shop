using System.ComponentModel.DataAnnotations;

namespace Persistence.Models;

public class Employee
{
    [Required]
    public virtual int Id { get; set; }

    [Required]
    public virtual string Name { get; set; } = string.Empty;

    [Required]
    public virtual string Address { get; set; } = string.Empty;

    [Required]
    public virtual string Position { get; set; } = string.Empty;

    [Required]
    public virtual double salary { get; set; }
}
