using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EMS.Common
{
    public interface ISlot : ICommon
    {
        int SlotID { get; set; }
        int CompanyID { get; set; }
        int LineID { get; set; }
        string NVOCC { get; set; }
        int SlotOperatorID { get; set; }
        string SlotOperatorName { get; set; }
        string POL { get; set; }
        string POD { get; set; }
        Int32 POLID { get; set; }
        Int32 PODID { get; set; }
        DateTime EffectDt { get; set; }
        string PODTerminal { get; set; }
        int MovOrigin { get; set; }
        //int MovDestination { get; set; }
        bool SlotStatus { get; set; }
        //List<IChargeRate> SlotCost { get; set; }
        //string ConvertListToXML(List<IChargeRate> Items);
        //IList<ISlotCost> lstSlotCost { get; set; }
    }
}
