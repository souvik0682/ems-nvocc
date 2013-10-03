using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IService : ICommon
    {
        int ServiceID { get; set; }
        bool Action { get; set; }
        string NVOCC { get; set; }
        string FPOD { get; set; }
        string FPODID { get; set; }
        string ServiceName { get; set; }
        bool ServiceStatus { get; set; }
        int LinerID { get; set; }
        int ServiceNameID {get; set;}
    }
}
