using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EMS.Entity.Report
{
    public class DOPrintEntity
    {
        #region Public Properties

        public string AgentCode { get; set; }
        public string LineCode { get; set; }
        public string AddrName { get; set; }
        public string AddrAddress { get; set; }
        public string Phone { get; set; }
        public string BookingNo { get; set; }
        public string LoadPort { get; set; }
        public string PODForm1 { get; set; }
        public string FinalPort { get; set; }
        public string Cargo { get; set; }
        public string HazClass { get; set; }
        public string VesselName { get; set; }
        public string VoyageNo { get; set; }
        public DateTime? ETATerminal { get; set; }
        public DateTime? ETDTerminal { get; set; }
        public DateTime? VesselCutoffDate { get; set; }
        public DateTime? SICutoffDate { get; set; }
        public DateTime? DocumentCutoffDate { get; set; }
        public string Dimensions { get; set; }
        public string VIANo { get; set; }
        public string ROTNo { get; set; }
        public int CntrNos { get; set; }
        public string CntrSize { get; set; }
        public string CntrType { get; set; }
        public string BookingParty { get; set; }
        public string Containers { get; set; }
        public string PickUpFooter { get; set; }
        public string DONo { get; set; }
        public DateTime? DODate { get; set; }
        public string CompanyName { get; set; }
        public string LocationAddress { get; set; }
        public string LineName { get; set; }
        public DateTime? BookingDate { get; set; }


        #endregion

        #region Constructors

        public DOPrintEntity()
        {

        }

        public DOPrintEntity(DataTableReader reader)
        {
            this.AgentCode = Convert.ToString(reader["AgentCode"]);
            this.LineCode = Convert.ToString(reader["LineCode"]);
            this.LoadPort = Convert.ToString(reader["LPortName"]);
            this.PODForm1 = Convert.ToString(reader["DPortName"]);
            this.FinalPort = Convert.ToString(reader["FPortName"]);
            this.Cargo = Convert.ToString(reader["Commodity"]);
            this.HazClass = Convert.ToString(reader["HazCargo"]);
            this.VesselName = Convert.ToString(reader["VesselName"]);
            this.VoyageNo = Convert.ToString(reader["VoyageNo"]);
            if (reader["ETA"] != DBNull.Value) this.ETATerminal = Convert.ToDateTime(reader["ETA"]);
            if (reader["ETD"] != DBNull.Value) this.ETDTerminal = Convert.ToDateTime(reader["ETD"]);
            this.VIANo = Convert.ToString(reader["VIA"]);
            this.ROTNo = Convert.ToString(reader["RotationNo"]);
            if (reader["VesselcutOffDate"] != DBNull.Value) this.VesselCutoffDate = Convert.ToDateTime(reader["VesselcutOffDate"]);
            if (reader["DocsCutOffDate"] != DBNull.Value) this.DocumentCutoffDate = Convert.ToDateTime(reader["DocsCutOffDate"]);
            this.Dimensions = Convert.ToString(reader["TempMin"]);
            this.AddrName = Convert.ToString(reader["AddrName"]);
            this.AddrAddress = Convert.ToString(reader["AddrAddress"]);
            this.Phone = Convert.ToString(reader["Phone"]);
            this.BookingNo = Convert.ToString(reader["BookingNo"]);
            this.LineName = Convert.ToString(reader["LineName"]);
            this.BookingParty = Convert.ToString(reader["BookingParty"]);
            this.Containers = Convert.ToString(reader["Containers"]);
            this.PickUpFooter = Convert.ToString(reader["PickupFooter"]);
            this.DONo = Convert.ToString(reader["DONo"]);
            this.DODate = Convert.ToDateTime(reader["DODate"]);
            this.CompanyName = Convert.ToString(reader["CompName"]);
            this.LocationAddress = Convert.ToString(reader["LocAddress"]);
            this.BookingDate = Convert.ToDateTime(reader["BookingDate"]);
        }

        #endregion
    }
}
