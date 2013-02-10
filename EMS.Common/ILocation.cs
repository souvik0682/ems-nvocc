using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface ILocation : IBase<int>, ICommon
    {
        IAddress LocAddress { get; set; }
        string Abbreviation { get; set; }
        string Phone { get; set; }
        int? PGRFreeDays { get; set; }
        string CanFooter { get; set; }
        string SlotFooter { get; set; }
        string CartingFooter { get; set; }
        string PickUpFooter { get; set; }
        string CustomHouseCode { get; set; }
        string GatewayPort { get; set; }
        string ICEGateLoginD { get; set; }
        string PCSLoginID { get; set; }
        string ISO20 { get; set; }
        string ISO40 { get; set; }        
        char IsActive { get; set; }
        int DefaultLocation { get; set; }
    }
}
