using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EMS.Common
{
    public interface ISlotCost
    {
       
        DateTime AddedOn { get; set; }
        int? SrlNo { get; set; }
        string CntrSize { get; set; }
        string CargoType { get; set; }
        string SpecialType { get; set; }
        int? ContainerTypeID { get; set; }
        decimal ContainerRate { get; set; }
        decimal RatePerTon { get; set; }
        decimal RetePerCBM { get; set; }
        long SlotCostID { get; set; }
        long? SlotID { get; set; }

        int UserAdded { get; set; }
        int? UserLastEdited { get; set; }
        DateTime? ValidTill { get; set; }
        IList<ISlotCost> GetSlotCost(DataTable dt);
    }
}
