using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Models;

[Table("Employees")]
public class Employee
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
