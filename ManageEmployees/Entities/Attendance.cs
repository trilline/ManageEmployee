using System;
using System.Collections.Generic;

namespace ManageEmployees.Entities;

public partial class Attendance
{
    public int Attendanceid { get; set; }

    public int? Employeeid { get; set; }

    public DateTime Arrivaldate { get; set; }

    public DateTime? Departuredate { get; set; }

    public virtual Employee? Employee { get; set; }
}
