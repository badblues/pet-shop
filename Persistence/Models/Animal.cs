using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Models;

[Table("Animals")]
public class Animal
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }

    [Required]
    public virtual required string Name { get; set; }

    public virtual int Age { get; set; }

    [Required]
    public virtual Gender Gender { get; set; }

    [Required]
    [ForeignKey("breed_id")]
    public virtual int BreedId { get; set; }

    public virtual required Breed Breed { get; set; }

    public virtual required string ExteriorDescription { get; set; }

    public virtual required string Pedigree { get; set; }

    public virtual required string Veterinarian { get; set; }

    [ForeignKey("owner_id")]
    public virtual int ClientId { get; set; }

    public virtual required Client Client { get; set; }
}
