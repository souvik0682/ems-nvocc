using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IVesselVoyageEDI 
    {
        int VesselID { get; set; }
        int VoyageID { get; set; }

        string CallSign { get; set; }
        string IGMNo { get; set; }
        DateTime? IGMDate { get; set; }
        string ShippingLineCode { get; set; }
        string PANNo { get; set; }
        string MasterName { get; set; }
        int CountryId { get; set; }
        string VesselFlag { get; set; }
        int? LPortID { get; set; }
        string LastPortCalled { get; set; }
        string IMONumber { get; set; }
        string TotalLines { get; set; }
        string CargoDesc { get; set; }
        DateTime? ETADate { get; set; }
        int? LightHouseDue { get; set; }
        int SameButtonCargo { get; set; }
        int ShipStoreSubmitted { get; set; }
        int CrewList { get; set; }
        int PassengerList { get; set; }
        int CrewEffectList { get; set; }
        int MaritimeList { get; set; }
        string VesselType { get; set; }
        DateTime? LandingDate { get; set; }
        int? podid { get; set; }
        string pod { get; set; }
    }
}
