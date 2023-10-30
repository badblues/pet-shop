using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Models;

public class Breed
{
    [Required]
    public virtual int Id { get; set; }

    [Required]
    public virtual string Name { get; set; } = string.Empty;
}
