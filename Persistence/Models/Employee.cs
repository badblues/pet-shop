﻿namespace Persistence.Models;

public class Employee
{
    public virtual int Id { get; set; }

    public virtual required string Name { get; set; } = string.Empty;

    public virtual required string Address { get; set; } = string.Empty;

    public virtual required string Position { get; set; } = string.Empty;

    public virtual required double Salary { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
}
