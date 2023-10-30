using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Models;

[Table("Applications")]
public class Application
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }

    [Required]
    [ForeignKey("client_id")]
    public virtual int ClientId { get; set; }

    public virtual required Client Client { get; set; }

    [ForeignKey("employee_id")]
    public virtual int EmployeeID { get; set; }

    public virtual required Employee Employee { get; set; }

    [Required]
    [ForeignKey("breed_id")]
    public virtual int BreedId { get; set; }

    public virtual required Breed Breed { get; set; }

    public virtual Gender Gender { get; set; }

    [Required]
    public virtual DateTime ApplicationDate { get; set; }

    [Required]
    public virtual bool Completed { get; set; }
}
