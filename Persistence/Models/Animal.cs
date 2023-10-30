using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Models;

public class Animal
{
    public virtual int Id { get; set; }

    [Required]
    public virtual string Name { get; set; }

    public virtual int Age { get; set; }

    [Required]
    public virtual Gender Gender { get; set; }

    [Required]
    public virtual int BreedId { get; set; }

    [Required]
    public virtual Breed Breed { get; set; }

    public virtual string ExteriorDescription { get; set; }

    public virtual string Pedigree { get; set; }

    public virtual string Veterinarian { get; set; }

    public virtual int ClientId { get; set; }

    public virtual  Client Client { get; set; }
}
