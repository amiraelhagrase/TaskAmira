using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TaskAPI.Models
{
    public partial class Msgs
    {
        public int CustId { get; set; }
        public string MsgSubject { get; set; }
        public string MsgBody { get; set; }
        public string Status { get; set; }
    }
}
