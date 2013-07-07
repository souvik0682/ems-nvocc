using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Entity
{
    public class Vessel
    {
        public int VesselID { get; set; }
        public int VesselPrefix { get; set; }
        public string VesselName { get; set; }
        public int VesselFlag { get; set; }
       // public string CallSign { get; set; }
        public string IMONumber { get; set; }
        public string ShippingLineCode { get; set; }
        public string PANNo { get; set; }
        public string MasterName { get; set; }
        public string AgentCode { get; set; }
        public string CallSign { get; set; }
       // public int LastPortCalled { get; set; }

        


    }
}
