using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EMS.Common;
using System.Data;

namespace EMS.Entity
{
    public class VesselVoyageEDI : IVesselVoyageEDI
    {

        #region EDIMembers

        public string CallSign { get; set; }

        public string IGMNo { get; set; }

        public DateTime? IGMDate { get; set; }

        public string ShippingLineCode { get; set; }

        public string PANNo { get; set; }

        public string MasterName { get; set; }

        public int CountryId { get; set; }

        public string VesselFlag { get; set; }

        public int? LPortID { get; set; }

        public string LastPortCalled { get; set; }

        public string IMONumber { get; set; }

        public string TotalLines { get; set; }

        public string CargoDesc { get; set; }

        public DateTime? ETADate { get; set; }

        public int? LightHouseDue { get; set; }

        public int SameButtonCargo { get; set; }

        public int ShipStoreSubmitted { get; set; }

        public int CrewList { get; set; }

        public int PassengerList { get; set; }

        public int CrewEffectList { get; set; }

        public int MaritimeList { get; set; }

        public int VesselID { get; set; }

        public int VoyageID { get; set; }

        public string VesselType { get; set; }

        public DateTime? LandingDate
        {
            get;
            set;
        }

        #endregion

        public VesselVoyageEDI() { }
        public VesselVoyageEDI(DataTableReader reader)
        {
            this.CallSign = Convert.ToString(reader["CallSign"]);
            this.CargoDesc = Convert.ToString(reader["CargoDesc"]);
            this.CrewEffectList = Convert.ToInt32(reader["CrewEffectList"]);
            this.CrewList = Convert.ToInt32(reader["CrewList"]);
            //this.ETADate =reader["ETADate"]==null?(Nullable<DateTime>)null: Convert.ToDateTime(reader["ETADate"]);
            this.ETADate =reader["ETADate"]==DBNull.Value?(Nullable<DateTime>)null: Convert.ToDateTime(reader["ETADate"]);
            this.CountryId = Convert.ToInt32(reader["fk_CountryId"]);
            this.IGMDate = Convert.ToDateTime(reader["IGMDate"]);
            this.IGMNo = Convert.ToString(reader["IGMNo"]);
            this.IMONumber = Convert.ToString(reader["IMONumber"]);
            this.LastPortCalled = Convert.ToString(reader["LastPortCalled"]);
            this.LightHouseDue = Convert.ToInt32(reader["LightHouseDue"]);
            this.LPortID = Convert.ToInt32(reader["fk_LPortID"]);
            this.MaritimeList = Convert.ToInt32(reader["MaritimeList"]);
            this.MasterName = Convert.ToString(reader["MasterName"]);
            this.PANNo = Convert.ToString(reader["PANNo"]);
            this.PassengerList = Convert.ToInt32(reader["PassengerList"]);
            this.SameButtonCargo = Convert.ToInt32(reader["SameButtonCargo"]);
            this.ShippingLineCode = Convert.ToString(reader["ShippingLineCode"]);
            this.ShipStoreSubmitted = Convert.ToInt32(reader["ShipStoreSubmitted"]);
            this.TotalLines = Convert.ToString(reader["TotalLines"]);
            this.VesselFlag = Convert.ToString(reader["VesselFlag"]);
            this.VesselType = Convert.ToString(reader["VesselType"]);
            this.LandingDate = reader["LandingDate"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(reader["LandingDate"]); 
        }



    }
}
