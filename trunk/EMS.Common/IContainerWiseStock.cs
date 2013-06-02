using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IContainerWiseStock
    {
        DateTime MovementDate { get; set; }
        String MoveAbbr { get; set; }
        string Locabbr { get; set; }
        string NVOCC { get; set; }
        string Size { get; set; }
        string vesselvoyage { get; set; }
        string cfs { get; set; }
        string ety { get; set; }
        string GroupID { get; set; }
    }
}
