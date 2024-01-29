using System;
using System.Collections.Generic;

namespace ManageEmployees.Entities;

public partial class Department
{
    public int Departmentid { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
