﻿namespace Persistence.Models;

public class Animal
{
    public virtual int Id { get; set; }

    public virtual required string Name { get; set; }

    public virtual int? Age { get; set; }

    public virtual required Gender Gender { get; set; }

    public virtual required Breed Breed { get; set; }

    public virtual string? ExteriorDescription { get; set; }

    public virtual string? Pedigree { get; set; }

    public virtual string? Veterinarian { get; set; }

    public virtual Client? Client { get; set; }

    public virtual ICollection<Participation> Participations { get; set; } = new List<Participation>();
}
