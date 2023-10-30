using System.ComponentModel.DataAnnotations;

namespace Persistence.Models;

public class Participation
{
    [Required]
    public virtual int AnimalId { get; set; }

    [Required]
    public virtual required Animal Animal { get; set; }

    [Required]
    public virtual int CompetitionId { get; set; }

    [Required]
    public virtual required Competition Competition { get; set; }

    [Required]
    public virtual string Award {  get; set; }

}
