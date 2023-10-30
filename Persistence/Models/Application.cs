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
    public int ClientId { get; set; }

    public virtual Client Client { get; set; }

    [ForeignKey("employee_id")]
    public int EmployeeID { get; set; }

    public virtual Employee Employee { get; set; }

    [Required]
    [ForeignKey("breed_id")]
    public int BreedId { get; set; }

    public virtual Breed Breed { get; set; }

    public virtual Gender Gender { get; set; }

    [Required]
    public virtual DateTime ApplicationDate { get; set; }

    [Required]
    public virtual bool Completed { get; set; }
}
