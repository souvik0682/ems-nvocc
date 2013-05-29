using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Entity
{
    public class VoyageEntity
    {
        public int pk_VoyageID { get; set; }
        public int fk_CompanyID { get; set; }
        public int fk_VesselID { get; set; }
        public int fl_TerminalID { get; set; }
        public string VoyageNo { get; set; }
        public string IGMNo { get; set; }
        public DateTime? IGMDate { get; set; }
        public DateTime? LandingDate { get; set; }
        public DateTime? OLandingDate { get; set; }
        public DateTime? AddLandingDate { get; set; }
        public string VoyageType { get; set; }
        public string LGNo { get; set; }
        public string AltLGNo { get; set; }
        public int fk_Pod { get; set; }
        public int fk_LPortID { get; set; }
        public int fk_LPortID1 { get; set; }
        public int fk_LPortID2 { get; set; }
        public string VesselType { get; set; }
        public string MotherDaughterDtl { get; set; }
        public string TotalLines { get; set; }
        public string CargoDesc { get; set; }
        public DateTime? ETADate { get; set; }
        public string ETATime { get; set; }
        public int LightHouseDue { get; set; }
        public string SameButtonCargo { get; set; }
        public string ShipStoreSubmitted { get; set; }
        public string CrewList { get; set; }
        public string PassengerList { get; set; }
        public string CrewEffectList { get; set; }
        public string MaritimeList { get; set; }
        public string CallSign { get; set; }
        public decimal ImpXChangeRate { get; set; }
        public string PCCNo { get; set; }
        public DateTime? PCCDate { get; set; }
        public string VIANo { get; set; }
        public string VCN { get; set; }
        public DateTime? SailDate { get; set; }
        public DateTime? ETD { get; set; }
        public decimal ExpXchangeRate { get; set; }
        public DateTime? CutOffDate { get; set; }
        public int EGMNo { get; set; }
        public DateTime? EGMDate { get; set; }
        public string BondNo { get; set; }
        public string VesselApplNo { get; set; }
        public DateTime? VesselAppDate { get; set; }
        public decimal BondAmount { get; set; }
        public decimal BondBalance { get; set; }
        public string VesselSerial { get; set; }
        public DateTime? dtAdded { get; set; }
        public DateTime? dtEdited { get; set; }
        public int fk_UserAdded { get; set; }
        public int fk_UserEdited { get; set; }
        public bool VoyageStatus { get; set; }
        public int? locid { get; set; }

    }
}
