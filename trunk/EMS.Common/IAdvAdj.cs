using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IAdvAdj : ICommon
    {
        int AdvAdjID { get; set; }
        int JobID { get; set; }
        bool AdjStatus { get; set; }
        string AdjustmentNo { get; set; }
        DateTime AdjustmentDate { get; set; }
        string DorC { get; set; }
        string PartyType { get; set; }
        int PartyID { get; set; }
        string JobNo { get; set; }

    }
}
