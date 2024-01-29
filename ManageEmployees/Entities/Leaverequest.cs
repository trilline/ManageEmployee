using System;
using System.Collections.Generic;

namespace ManageEmployees.Entities;

public partial class Leaverequest
{
    public int Leaverequestid { get; set; }

    public int? Employeeid { get; set; }

    public DateTime Startdate { get; set; }

    public DateTime Enddate { get; set; }

    public int? Statusleaverequestid { get; set; }

    public DateTime? Requestdate { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Statusleaverequest? Statusleaverequest { get; set; }
}
