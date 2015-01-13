using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IfwdAgent : ICommon
    {
        int AgentID { get; set; }
        bool Action { get; set; }
        string NVOCC { get; set; }
        string FPOD { get; set; }
        string FPODID { get; set; }
        string AgentName { get; set; }
        string ContactPerson { get; set; }
        string AgentAddress { get; set; }
        string Phone { get; set; }
        string FAX { get; set; }
        string email { get; set; }
        string PAN { get; set; }
        bool AgentStatus { get; set; }
        int LinerID { get; set; }
    }
}
