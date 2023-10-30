using System.ComponentModel.DataAnnotations;

namespace Persistence.Models;

public class Client
{
    [Required]
    public virtual int Id { get; set; }

    [Required]
    public virtual string Name { get; set; } = string.Empty;

    [Required]
    public virtual string Address { get; set; } = string.Empty;

}
