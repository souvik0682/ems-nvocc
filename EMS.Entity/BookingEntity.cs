using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using System.Data;

namespace EMS.Entity
{
    public class BookingEntity : IBooking
    {
        public int BookingID
        {
            get;
            set;
        }

        public bool Action
        {
            get;
            set;
        }

        public string NVOCC
        {
            get;
            set;
        }

        public string FPOD
        {
            get;
            set;
        }

        public string POD
        {
            get;
            set;
        }

        public string POL
        {
            get;
            set;
        }

        public string POR
        {
            get;
            set;
        }

        public string Services
        {
            get;
            set;
        }

        public int ServicesID
        {
            get;
            set;
        }

        public int LinerID
        {
            get;
            set;
        }
        public string FPODID
        {
            get;
            set;
        }

        public string PODID
        {
            get;
            set;
        }

        public string POLID
        {
            get;
            set;
        }

        public string PORID
        {
            get;
            set;
        }
        public string Accounts
        {
            get;
            set;
        }

        public int CustID
        {
            get;
            set;
        }

        public int LocationID
        {
            get;
            set;
        }

        public string LocationName
        {
            get;
            set;
        }

        public int VesselID
        {
            get;
            set;
        }


        public string VesselName
        {
            get;
            set;
        }


        public Int32 VoyageID
        {
            get;
            set;
        }

        public string VoyageNo
        {
            get;
            set;
        }


        public int MainLineVesselID
        {
            get;
            set;
        }

        public string MainLineVesselName
        {
            get;
            set;
        }

        public Int32 MainLineVoyageID
        {
            get;
            set;
        }

        public string BookingNo
        {
            get;
            set;
        }

        public DateTime BookingDate
        {
            get;
            set;
        }

        public string RefBookingNo
        {
            get;
            set;
        }

        public DateTime RefBookingDate
        {
            get;
            set;
        }

        public bool HazCargo
        {
            get;
            set;
        }

        public string UNO
        {
            get;
            set;
        }

        public string IMO
        {
            get;
            set;
        }

        public string Commodity
        {
            get;
            set;
        }

        public char ShipmentType
        {
            get;
            set;
        }

        public bool BLThruApp
        {
            get;
            set;
        }

        public decimal GrossWt
        {
            get;
            set;
        }

        public decimal CBM
        {
            get;
            set;
        }

        public int TotalTEU
        {
            get;
            set;
        }

        public int TotalFEU
        {
            get;
            set;
        }

        public bool Reefer
        {
            get;
            set;
        }

        public decimal TempMax
        {
            get;
            set;
        }

        public decimal TempMin
        {
            get;
            set;
        }

        public bool AcceptBooking
        {
            get;
            set;
        }

        public bool BookingStatus
        {
            get;
            set;
        }

        public int CreatedBy
        {
            get;
            set;
        }

        public DateTime CreatedOn
        {
            get;
            set;
        }

        public int ModifiedBy
        {
            get;
            set;
        }

        public DateTime ModifiedOn
        {
            get;
            set;
        }

        public BookingEntity()
        {

        }


        public BookingEntity(DataTableReader reader)
        {
            this.BookingID = Convert.ToInt32(reader["pk_BookingID"]);
            //this.AgentName = Convert.ToString(reader["AgentName"]);
            this.LocationName = Convert.ToString(reader["LocationName"]);

            this.BookingNo = Convert.ToString(reader["BookingNo"]);
            this.BookingDate = Convert.ToDateTime(reader["BookingDate"]);
            this.LinerID = Convert.ToInt32(reader["fk_NVOCCID"]);

            this.NVOCC = Convert.ToString(reader["LineName"]);
            this.FPOD = Convert.ToString(reader["FPortName"]);
            this.FPODID = Convert.ToString(reader["FK_FPOD"]);
            this.POD = Convert.ToString(reader["DPortName"]);
            this.PODID = Convert.ToString(reader["FK_FPOD"]);
            this.POR = Convert.ToString(reader["RPortName"]);
            this.PORID = Convert.ToString(reader["FK_POR"]);
            this.POL = Convert.ToString(reader["LPortName"]);
            this.POLID = Convert.ToString(reader["FK_POL"]);
            this.VesselID = Convert.ToInt32(reader["fk_VesselID"]);
            this.VesselName = Convert.ToString(reader["VesselName"]);
            this.MainLineVesselName = Convert.ToString(reader["MainLineVesselName"]);
            this.VoyageNo = Convert.ToString(reader["VoyageNo"]);
            this.VoyageID = Convert.ToInt32(reader["fk_VoyageID"]);
            this.MainLineVesselID = Convert.ToInt32(reader["fk_VesselID"]);
            this.MainLineVoyageID = Convert.ToInt32(reader["fk_VoyageID"]);
            this.RefBookingNo = Convert.ToString(reader["RefBookingNo"]);
            this.RefBookingDate = Convert.ToDateTime(reader["RefBookingDate"]);
            this.HazCargo = Convert.ToBoolean(reader["HazCargo"]); 
            this.UNO = Convert.ToString(reader["UNO"]);
            this.IMO = Convert.ToString(reader["IMO"]);
            this.Commodity = Convert.ToString(reader["Commodity"]);
            this.ShipmentType = Convert.ToChar(reader["ShipmentType"]);
            this.BLThruApp = Convert.ToBoolean(reader["BLThruApp"]);
            this.Services = Convert.ToString(reader["ServiceName"]);
            if (!String.IsNullOrEmpty(Convert.ToString(reader["fk_ServiceID"])))
                this.ServicesID = Convert.ToInt32(reader["fk_ServiceID"]);
            this.BookingStatus = Convert.ToBoolean(reader["BookingStatus"]);
            if (!String.IsNullOrEmpty(Convert.ToString(reader["Accounts"])))
                this.Accounts = Convert.ToString(reader["Accounts"]);
            if (!String.IsNullOrEmpty(Convert.ToString(reader["GrossWt"])))
                this.GrossWt = Convert.ToDecimal(reader["GrossWt"]);
            if (!String.IsNullOrEmpty(Convert.ToString(reader["CBM"])))
                this.CBM = Convert.ToDecimal(reader["CBM"]);
            this.Reefer = Convert.ToBoolean(reader["Reefer"]);
            if (!String.IsNullOrEmpty(Convert.ToString(reader["TempMax"])))
                this.TempMax = Convert.ToDecimal(reader["TempMax"]);
            if (!String.IsNullOrEmpty(Convert.ToString(reader["TempMin"])))
                this.TempMin = Convert.ToDecimal(reader["TempMin"]);
        }
    }
}
