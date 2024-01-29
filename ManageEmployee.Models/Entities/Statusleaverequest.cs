using System;
using System.Collections.Generic;

namespace ManageEmployee.Models.Entities
{
    public partial class Statusleaverequest
    {
        public int Statusleaverequestid { get; set; }

        public string Statuslabel { get; set; } = null!;

        public virtual ICollection<Leaverequest> Leaverequests { get; set; } = new List<Leaverequest>();
    }
}

