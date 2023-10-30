using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Models;

[Table("Breeds")]
public class Breed
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }

    [Required]
    public virtual string Name { get; set; } = string.Empty;
}
