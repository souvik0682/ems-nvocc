using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IEqpRepairing
    {
        int pk_RepairID { get; set; }
        string ContainerNo { get; set; }
        string Location { get; set; }
       
        DateTime? TransactionDate { get; set; }
        string EstimateReference { get; set; }
        string Line { get; set; }
       
       // decimal? LaboursEst { get; set; }
        decimal? RepLabourBilled { get; set; }
        decimal? RepLabourAppr { get; set; }
        decimal? RepLabourEst { get; set; }
        decimal? RepMaterialEst { get; set; }
        decimal? RepMaterialBilled { get; set; }
        decimal? RepMaterialAppr { get; set; }

        int ProspectID { get; set; }
        int locId { get; set; }

        int? NVOCCId { get; set; }
        int? fk_UserApproved { get; set; }
        bool onHold { get; set; }
        DateTime? RealeasedOn { get; set; }
        bool Damaged { get; set; }
        DateTime? StockReturnDate { get; set; }
        string Reason { get; set; }
    }
}
