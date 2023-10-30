using System.ComponentModel.DataAnnotations;

namespace Persistence.Models;

public class Animal
{
    public virtual int Id { get; set; }

    [Required]
    public virtual required string Name { get; set; }

    public virtual int Age { get; set; }

    [Required]
    public virtual Gender Gender { get; set; }

    [Required]
    public virtual int BreedId { get; set; }

    [Required]
    public virtual required Breed Breed { get; set; }

    public virtual required string ExteriorDescription { get; set; }

    public virtual required string Pedigree { get; set; }

    public virtual required string Veterinarian { get; set; }

    public virtual int ClientId { get; set; }

    public virtual required Client Client { get; set; }
}
