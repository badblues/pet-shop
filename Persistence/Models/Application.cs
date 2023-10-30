using System.ComponentModel.DataAnnotations;

namespace Persistence.Models;

public class Application
{
    public virtual int Id { get; set; }

    [Required]
    public virtual int ClientId { get; set; }

    public virtual required Client Client { get; set; }

    [Required]
    public virtual int EmployeeID { get; set; }

    [Required]
    public virtual required Employee Employee { get; set; }

    [Required]
    public virtual int BreedId { get; set; }

    [Required]
    public virtual required Breed Breed { get; set; }

    public virtual Gender Gender { get; set; }

    [Required]
    public virtual DateTime ApplicationDate { get; set; }

    [Required]
    public virtual bool Completed { get; set; }
}
