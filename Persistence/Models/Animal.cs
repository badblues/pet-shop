using System.ComponentModel.DataAnnotations;

namespace Persistence.Models;

public class Animal
{
    [Required]
    public virtual int Id { get; set; }

    [Required]
    public virtual required string Name { get; set; }

    public virtual int? Age { get; set; }

    [Required]
    public virtual Gender Gender { get; set; }

    [Required]
    public virtual int BreedId { get; set; }

    [Required]
    public virtual required Breed Breed { get; set; }

    public virtual string? ExteriorDescription { get; set; }

    public virtual string? Pedigree { get; set; }

    public virtual string? Veterinarian { get; set; }

    public virtual int? ClientId { get; set; }

    public virtual Client? Client { get; set; }
}
