﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Models;

public class Competition
{
    [Required]
    public virtual int Id { get; set; }

    [Required]
    public virtual string Name { get; set;}

    [Required]
    public virtual string Location { get; set; }

    [Required]
    public virtual DateOnly Date { get; set; }
}
