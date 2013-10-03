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
        public long BookingID
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

        public string BookingParty
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

        public string PpCc { get; set; }

        public string Containers { get; set; }

        public int FreightPayableId { get; set; }
        public string FreightPayableName { get; set; }

        public bool BrokeragePayable { get; set; }

        public decimal BrokeragePercentage { get; set; }

        public int BrokeragePayableId { get; set; }

        public string BrokeragePayableName { get; set; }

        public bool RefundPayable { get; set; }

        public int RefundPayableId { get; set; }
        public string RefundPayableName { get; set; }

        public string ExportRemarks { get; set; }

        public string RateReference { get; set; }
        public string RateType { get; set; }
        public string UploadPath { get; set; }
        public int SlotOperatorId { get; set; }
        public string Shipper { get; set; }

        public int Customer { get; set; }
        public string CustomerERAS { get; set; }

        public BookingEntity(DataTableReader reader)
        {
            if (ColumnExists(reader, "pk_BookingID"))
                if (reader["pk_BookingID"] != DBNull.Value)
                    this.BookingID = Convert.ToInt32(reader["pk_BookingID"]);
            //this.BookingID = Convert.ToInt32(reader["pk_BookingID"]);

            if (ColumnExists(reader, "LocationName"))
                if (reader["LocationName"] != DBNull.Value)
                    this.LocationName = Convert.ToString(reader["LocationName"]);

            if (ColumnExists(reader, "fk_LocationID"))
                if (reader["fk_LocationID"] != DBNull.Value)
                    this.LocationID = Convert.ToInt32(reader["fk_LocationID"]);

            if (ColumnExists(reader, "BookingNo"))
                if (reader["BookingNo"] != DBNull.Value)
                    this.BookingNo = Convert.ToString(reader["BookingNo"]);


            if (ColumnExists(reader, "BookingDate"))
                if (reader["BookingDate"] != DBNull.Value)
                    this.BookingDate = Convert.ToDateTime(reader["BookingDate"]);

            if (ColumnExists(reader, "fk_NVOCCID"))
                if (reader["fk_NVOCCID"] != DBNull.Value)
                    this.LinerID = Convert.ToInt32(reader["fk_NVOCCID"]);

            if (ColumnExists(reader, "LineName"))
                if (reader["LineName"] != DBNull.Value)
                    this.NVOCC = Convert.ToString(reader["LineName"]);

            if (ColumnExists(reader, "FPortName"))
                if (reader["FPortName"] != DBNull.Value)
                    this.FPOD = Convert.ToString(reader["FPortName"]);

            if (ColumnExists(reader, "FK_FPOD"))
                if (reader["FK_FPOD"] != DBNull.Value)
                    this.FPODID = Convert.ToString(reader["FK_FPOD"]);

            if (ColumnExists(reader, "DPortName"))
                if (reader["DPortName"] != DBNull.Value)
                    this.POD = Convert.ToString(reader["DPortName"]);

            if (ColumnExists(reader, "FK_POD"))
                if (reader["FK_POD"] != DBNull.Value)
                    this.PODID = Convert.ToString(reader["FK_POD"]);

            if (ColumnExists(reader, "RPortName"))
                if (reader["RPortName"] != DBNull.Value)
                    this.POR = Convert.ToString(reader["RPortName"]);

            if (ColumnExists(reader, "FK_POR"))
                if (reader["FK_POR"] != DBNull.Value)
                    this.PORID = Convert.ToString(reader["FK_POR"]);

            if (ColumnExists(reader, "LPortName"))
                if (reader["LPortName"] != DBNull.Value)
                    this.POL = Convert.ToString(reader["LPortName"]);

            if (ColumnExists(reader, "FK_POL"))
                if (reader["FK_POL"] != DBNull.Value)
                    this.POLID = Convert.ToString(reader["FK_POL"]);

            if (ColumnExists(reader, "FK_VesselID"))
                if (reader["FK_VesselID"] != DBNull.Value)
                    this.VesselID = Convert.ToInt32(reader["fk_VesselID"]);

            if (ColumnExists(reader, "VesselName"))
                if (reader["VesselName"] != DBNull.Value)
                    this.VesselName = Convert.ToString(reader["VesselName"]);

            if (ColumnExists(reader, "MainLineVesselName"))
                if (reader["MainLineVesselName"] != DBNull.Value)
                    this.MainLineVesselName = Convert.ToString(reader["MainLineVesselName"]);

            if (ColumnExists(reader, "VoyageNo"))
                if (reader["VoyageNo"] != DBNull.Value)
                    this.VoyageNo = Convert.ToString(reader["VoyageNo"]);

            if (ColumnExists(reader, "fk_VoyageID"))
                if (reader["fk_VoyageID"] != DBNull.Value)
                    this.VoyageID = Convert.ToInt32(reader["fk_VoyageID"]);

            if (ColumnExists(reader, "fk_MainLineVesselID"))
                if (reader["fk_MainLineVesselID"] != DBNull.Value)
                    this.MainLineVesselID = Convert.ToInt32(reader["fk_MainLineVesselID"]);

            if (ColumnExists(reader, "fk_MainLineVoyageID"))
                if (reader["fk_MainLineVoyageID"] != DBNull.Value)
                    this.MainLineVoyageID = Convert.ToInt32(reader["fk_MainLineVoyageID"]);

            if (ColumnExists(reader, "RefBookingNo"))
                if (reader["RefBookingNo"] != DBNull.Value)
                    this.RefBookingNo = Convert.ToString(reader["RefBookingNo"]);

            if (ColumnExists(reader, "RefBookingDate"))
                if (reader["RefBookingDate"] != DBNull.Value)
                    this.RefBookingDate = Convert.ToDateTime(reader["RefBookingDate"]);

            if (ColumnExists(reader, "HazCargo"))
                if (reader["HazCargo"] != DBNull.Value)
                    this.HazCargo = Convert.ToBoolean(reader["HazCargo"]);

            if (ColumnExists(reader, "UNO"))
                if (reader["UNO"] != DBNull.Value)
                    this.UNO = Convert.ToString(reader["UNO"]);

            if (ColumnExists(reader, "IMO"))
                if (reader["IMO"] != DBNull.Value)
                    this.IMO = Convert.ToString(reader["IMO"]);

            if (ColumnExists(reader, "Commodity"))
                if (reader["Commodity"] != DBNull.Value)
                    this.Commodity = Convert.ToString(reader["Commodity"]);

            if (ColumnExists(reader, "ShipmentType"))
                if (reader["ShipmentType"] != DBNull.Value)
                    this.ShipmentType = Convert.ToChar(reader["ShipmentType"]);

            if (ColumnExists(reader, "BLThruApp"))
                if (reader["BLThruApp"] != DBNull.Value)
                    this.BLThruApp = Convert.ToBoolean(reader["BLThruApp"]);

            if (ColumnExists(reader, "ServiceName"))
                if (reader["ServiceName"] != DBNull.Value)
                    this.Services = Convert.ToString(reader["ServiceName"]);

            if (ColumnExists(reader, "BookingParty"))
                if (reader["BookingParty"] != DBNull.Value)
                    this.BookingParty = Convert.ToString(reader["BookingParty"]);

            //this.NVOCC = Convert.ToString(reader["LineName"]);
            //this.FPOD = Convert.ToString(reader["FPortName"]);
            //this.FPODID = Convert.ToString(reader["FK_FPOD"]);
            //this.POD = Convert.ToString(reader["DPortName"]);
            //this.PODID = Convert.ToString(reader["FK_FPOD"]);
            //this.POR = Convert.ToString(reader["RPortName"]);
            //this.PORID = Convert.ToString(reader["FK_POR"]);
            //this.POL = Convert.ToString(reader["LPortName"]);
            //this.POLID = Convert.ToString(reader["FK_POL"]);
            //this.VesselID = Convert.ToInt32(reader["fk_VesselID"]);
            //this.VesselName = Convert.ToString(reader["VesselName"]);
            //this.MainLineVesselName = Convert.ToString(reader["MainLineVesselName"]);
            //this.VoyageNo = Convert.ToString(reader["VoyageNo"]);
            //this.VoyageID = Convert.ToInt32(reader["fk_VoyageID"]);
            //this.MainLineVesselID = Convert.ToInt32(reader["fk_MainLineVesselID"]);
            //this.MainLineVoyageID = Convert.ToInt32(reader["fk_MainLineVoyageID"]);
            //this.RefBookingNo = Convert.ToString(reader["RefBookingNo"]);
            //this.RefBookingDate = Convert.ToDateTime(reader["RefBookingDate"]);
            //this.HazCargo = Convert.ToBoolean(reader["HazCargo"]); 
            //this.UNO = Convert.ToString(reader["UNO"]);
            //this.IMO = Convert.ToString(reader["IMO"]);
            //this.Commodity = Convert.ToString(reader["Commodity"]);
            //this.ShipmentType = Convert.ToChar(reader["ShipmentType"]);
            //this.BLThruApp = Convert.ToBoolean(reader["BLThruApp"]);
            //this.Services = Convert.ToString(reader["ServiceName"]);


            //this.NVOCC = Convert.ToString(reader["LineName"]);
            //this.FPOD = Convert.ToString(reader["FPortName"]);
            //this.FPODID = Convert.ToString(reader["FK_FPOD"]);
            //this.POD = Convert.ToString(reader["DPortName"]);
            //this.PODID = Convert.ToString(reader["FK_FPOD"]);
            //this.POR = Convert.ToString(reader["RPortName"]);
            //this.PORID = Convert.ToString(reader["FK_POR"]);
            //this.POL = Convert.ToString(reader["LPortName"]);
            //this.POLID = Convert.ToString(reader["FK_POL"]);
            //this.VesselID = Convert.ToInt32(reader["fk_VesselID"]);
            //this.VesselName = Convert.ToString(reader["VesselName"]);
            //this.MainLineVesselName = Convert.ToString(reader["MainLineVesselName"]);
            //this.VoyageNo = Convert.ToString(reader["VoyageNo"]);
            //this.VoyageID = Convert.ToInt32(reader["fk_VoyageID"]);
            //this.MainLineVesselID = Convert.ToInt32(reader["fk_VesselID"]);
            //this.MainLineVoyageID = Convert.ToInt32(reader["fk_VoyageID"]);
            //this.RefBookingNo = Convert.ToString(reader["RefBookingNo"]);
            //this.RefBookingDate = Convert.ToDateTime(reader["RefBookingDate"]);
            //this.HazCargo = Convert.ToBoolean(reader["HazCargo"]);
            //this.UNO = Convert.ToString(reader["UNO"]);
            //this.IMO = Convert.ToString(reader["IMO"]);
            //this.Commodity = Convert.ToString(reader["Commodity"]);
            //this.ShipmentType = Convert.ToChar(reader["ShipmentType"]);
            //this.BLThruApp = Convert.ToBoolean(reader["BLThruApp"]);
            //this.Services = Convert.ToString(reader["ServiceName"]);
            if (ColumnExists(reader, "fk_ServiceID"))
                if (!String.IsNullOrEmpty(Convert.ToString(reader["fk_ServiceID"])))
                    this.ServicesID = Convert.ToInt32(reader["fk_ServiceID"]);

            if (ColumnExists(reader, "Accounts"))
                if (!String.IsNullOrEmpty(Convert.ToString(reader["Accounts"])))
                    this.Accounts = Convert.ToString(reader["Accounts"]);

            if (ColumnExists(reader, "GrossWt"))
                if (!String.IsNullOrEmpty(Convert.ToString(reader["GrossWt"])))
                    this.GrossWt = Convert.ToDecimal(reader["GrossWt"]);

            if (ColumnExists(reader, "CBM"))
                if (!String.IsNullOrEmpty(Convert.ToString(reader["CBM"])))
                    this.CBM = Convert.ToDecimal(reader["CBM"]);

            if (ColumnExists(reader, "Reefer"))
                this.Reefer = Convert.ToBoolean(reader["Reefer"]);

            if (ColumnExists(reader, "TempMax"))
                if (!String.IsNullOrEmpty(Convert.ToString(reader["TempMax"])))
                    this.TempMax = Convert.ToDecimal(reader["TempMax"]);

            if (ColumnExists(reader, "TempMin"))
                if (!String.IsNullOrEmpty(Convert.ToString(reader["TempMin"])))
                    this.TempMin = Convert.ToDecimal(reader["TempMin"]);

            if (ColumnExists(reader, "BookingStatus"))
                this.BookingStatus = Convert.ToBoolean(reader["BookingStatus"]);

            if (ColumnExists(reader, "PpCc"))
            {
                if (!String.IsNullOrEmpty(Convert.ToString(reader["PpCc"])))
                    this.PpCc = Convert.ToString(reader["PpCc"]);
                else
                    this.PpCc = string.Empty;
            }

            if (ColumnExists(reader, "Containers"))
                if (!String.IsNullOrEmpty(Convert.ToString(reader["Containers"])))
                    this.Containers = Convert.ToString(reader["Containers"]);
                else
                    this.Containers = string.Empty;

            if (ColumnExists(reader, "FreightPayableId"))
                if (!String.IsNullOrEmpty(Convert.ToString(reader["FreightPayableId"])))
                    this.FreightPayableId = Convert.ToInt32(reader["FreightPayableId"]);
                else
                    this.FreightPayableId = 0;

            if (ColumnExists(reader, "FreightPayableName"))
                if (!String.IsNullOrEmpty(Convert.ToString(reader["FreightPayableName"])))
                    this.FreightPayableName = Convert.ToString(reader["FreightPayableName"]);
                else
                    this.FreightPayableName = string.Empty;
            
            if (ColumnExists(reader, "BrokeragePayable"))
                if (!String.IsNullOrEmpty(Convert.ToString(reader["BrokeragePayable"])))
                    this.BrokeragePayable = Convert.ToBoolean(reader["BrokeragePayable"]);
                else
                    this.BrokeragePayable = false;

            if (ColumnExists(reader, "BrokeragePercentage"))
                if (!String.IsNullOrEmpty(Convert.ToString(reader["BrokeragePercentage"])))
                    this.BrokeragePercentage = Convert.ToDecimal(reader["BrokeragePercentage"]);
                else
                    this.BrokeragePercentage = 0;

            if (ColumnExists(reader, "BrokeragePercentage"))
                if (!String.IsNullOrEmpty(Convert.ToString(reader["BrokeragePercentage"])))
                    this.BrokeragePayableId = Convert.ToInt32(reader["BrokeragePayableId"]);
                else
                    this.BrokeragePayableId = 0;

            if (ColumnExists(reader, "BrokeragePayableName"))
                if (!String.IsNullOrEmpty(Convert.ToString(reader["BrokeragePayableName"])))
                    this.BrokeragePayableName = Convert.ToString(reader["BrokeragePayableName"]);
                else
                    this.BrokeragePayableName = string.Empty;

            if (ColumnExists(reader, "RefundPayable"))
                if (!String.IsNullOrEmpty(Convert.ToString(reader["RefundPayable"])))
                    this.RefundPayable = Convert.ToBoolean(reader["RefundPayable"]);
                else
                    this.RefundPayable = false;

            if (ColumnExists(reader, "RefundPayableId"))
                if (!String.IsNullOrEmpty(Convert.ToString(reader["RefundPayableId"])))
                    this.RefundPayableId = Convert.ToInt32(reader["RefundPayableId"]);
                else
                    this.RefundPayableId = 0;

            if (ColumnExists(reader, "RefundPayableName"))
                if (!String.IsNullOrEmpty(Convert.ToString(reader["RefundPayableName"])))
                    this.RefundPayableName = Convert.ToString(reader["RefundPayableName"]);
                else
                    this.RefundPayableName = string.Empty;

            if (ColumnExists(reader, "ExportRemarks"))
                if (!String.IsNullOrEmpty(Convert.ToString(reader["ExportRemarks"])))
                    this.ExportRemarks = Convert.ToString(reader["ExportRemarks"]);
                else
                    this.ExportRemarks = string.Empty;

            if (ColumnExists(reader, "RateReference"))
                if (!String.IsNullOrEmpty(Convert.ToString(reader["RateReference"])))
                    this.RateReference = Convert.ToString(reader["RateReference"]);
                else
                    this.RateReference = string.Empty;

            if (ColumnExists(reader, "RateType"))
                if (!String.IsNullOrEmpty(Convert.ToString(reader["RateType"])))
                    this.RateType = Convert.ToString(reader["RateType"]);
                else
                    this.RateType = string.Empty;

            if (ColumnExists(reader, "UploadPath"))
                if (!String.IsNullOrEmpty(Convert.ToString(reader["UploadPath"])))
                    this.UploadPath = Convert.ToString(reader["UploadPath"]);
                else
                    this.UploadPath = string.Empty;

            if (ColumnExists(reader, "SlotOperatorId"))
                if (!String.IsNullOrEmpty(Convert.ToString(reader["SlotOperatorId"])))
                    this.SlotOperatorId = Convert.ToInt32(reader["SlotOperatorId"]);
                else
                    this.SlotOperatorId = 0;

            if (ColumnExists(reader, "Shipper"))
                if (!String.IsNullOrEmpty(Convert.ToString(reader["Shipper"])))
                    this.Shipper = Convert.ToString(reader["Shipper"]);
                else
                    this.Shipper = string.Empty;

            if (ColumnExists(reader, "Customer"))
                if (!String.IsNullOrEmpty(Convert.ToString(reader["Customer"])))
                    this.Customer = Convert.ToInt32(reader["Customer"]);
                else
                    this.Customer = 0;

            if (ColumnExists(reader, "CustName"))
                if (!String.IsNullOrEmpty(Convert.ToString(reader["CustName"])))
                    this.CustomerERAS = Convert.ToString(reader["CustName"]);
                else
                    this.CustomerERAS = string.Empty;
        }

        public bool ColumnExists(IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).ToUpper() == columnName.ToUpper())
                {
                    return true;
                }
            }

            return false;
        }
    }
}
