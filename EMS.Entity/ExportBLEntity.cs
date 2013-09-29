using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EMS.Common;

namespace EMS.Entity
{
    public class ExportBLEntity : IExportBL
    {
        //General TAB
        public string BookingNumber { get; set; }
        public long BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public string BLNumber { get; set; }
        public long BLId { get; set; }
        public DateTime BLDate { get; set; }
        public string BookingParty { get; set; }
        public string RefBookingNumber { get; set; }
        public string Location { get; set; }
        public int LocationId { get; set; }
        public string Nvocc { get; set; }
        public int NvoccId { get; set; }
        public int VesselId { get; set; }
        public string Vessel { get; set; }
        public int VoyageId { get; set; }
        public string Voyage { get; set; }
        public string POR { get; set; }
        public string POL { get; set; }
        public string PORDesc { get; set; }
        public string POLDesc { get; set; }
        public string POD { get; set; }
        public string FPOD { get; set; }
        public string PODDesc { get; set; }
        public string FPODDesc { get; set; }
        public int NoOfBL { get; set; }
        public string BLType { get; set; }
        public int ShipmentMode { get; set; }
        public string Commodity { get; set; }
        public string Containers { get; set; }
        public int BLIssuePlaceId { get; set; }
        public string BLIssuePlace { get; set; }
        public decimal NetWeight { get; set; }
        public DateTime BLReleaseDate { get; set; }

        //Other Information Tab
        public string ShipperName { get; set; }
        public string Shipper { get; set; }
        public string Consignee { get; set; }
        public string ConsigneeName { get; set; }
        public string NotifyPartyName { get; set; }
        public string NotifyParty { get; set; }
        public string BLClause { get; set; }
        public string GoodDesc { get; set; }
        public string MarksNumnbers { get; set; }
        public int AgentId { get; set; }

        //Container Tab
        public string TotalTEU { get; set; }
        public string TotalFEU { get; set; }
        public string TotalTon { get; set; }
        public string TotalCBM { get; set; }

        //Common
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public ExportBLEntity()
        {
        }
        public ExportBLEntity(DataTableReader reader)
        {
            if (ColumnExists(reader, "BookingNumber"))
                if (reader["BookingNumber"] != DBNull.Value)
                    BookingNumber = Convert.ToString(reader["BookingNumber"]);

            if (ColumnExists(reader, "BookingId"))
                if (reader["BookingId"] != DBNull.Value)
                    BookingId = Convert.ToInt64(reader["BookingId"]);

            if (ColumnExists(reader, "BookingDate"))
                if (reader["BookingDate"] != DBNull.Value)
                    BookingDate = Convert.ToDateTime(reader["BookingDate"]);

            if (ColumnExists(reader, "BLNumber"))
                if (reader["BLNumber"] != DBNull.Value)
                    BLNumber = Convert.ToString(reader["BLNumber"]);

            if (ColumnExists(reader, "BLId"))
                if (reader["BLId"] != DBNull.Value)
                    BLId = Convert.ToInt64(reader["BLId"]);

            if (ColumnExists(reader, "BLDate"))
                if (reader["BLDate"] != DBNull.Value)
                    BLDate = Convert.ToDateTime(reader["BLDate"]);

            if (ColumnExists(reader, "BookingParty"))
                if (reader["BookingParty"] != DBNull.Value)
                    BookingParty = Convert.ToString(reader["BookingParty"]);

            if (ColumnExists(reader, "RefBookingNumber"))
                if (reader["RefBookingNumber"] != DBNull.Value)
                    RefBookingNumber = Convert.ToString(reader["RefBookingNumber"]);

            if (ColumnExists(reader, "Location"))
                if (reader["Location"] != DBNull.Value)
                    Location = Convert.ToString(reader["Location"]);

            if (ColumnExists(reader, "LocationId"))
                if (reader["LocationId"] != DBNull.Value)
                    LocationId = Convert.ToInt32(reader["LocationId"]);

            if (ColumnExists(reader, "Nvocc"))
                if (reader["Nvocc"] != DBNull.Value)
                    Nvocc = Convert.ToString(reader["Nvocc"]);

            if (ColumnExists(reader, "NvoccId"))
                if (reader["NvoccId"] != DBNull.Value)
                    NvoccId = Convert.ToInt32(reader["NvoccId"]);

            if (ColumnExists(reader, "Vessel"))
                if (reader["Vessel"] != DBNull.Value)
                    Vessel = Convert.ToString(reader["Vessel"]);

            if (ColumnExists(reader, "VesselId"))
                if (reader["VesselId"] != DBNull.Value)
                    VesselId = Convert.ToInt32(reader["VesselId"]);

            if (ColumnExists(reader, "Voyage"))
                if (reader["Voyage"] != DBNull.Value)
                    Voyage = Convert.ToString(reader["Voyage"]);

            if (ColumnExists(reader, "VoyageId"))
                if (reader["VoyageId"] != DBNull.Value)
                    VoyageId = Convert.ToInt32(reader["VoyageId"]);

            if (ColumnExists(reader, "POR"))
                if (reader["POR"] != DBNull.Value)
                    POR = Convert.ToString(reader["POR"]);

            if (ColumnExists(reader, "POL"))
                if (reader["POL"] != DBNull.Value)
                    POL = Convert.ToString(reader["POL"]);

            if (ColumnExists(reader, "PORDesc"))
            {
                if (reader["PORDesc"] != DBNull.Value)
                    PORDesc = Convert.ToString(reader["PORDesc"]);
            }
            else
            {
                PORDesc = POR;
            }

            if (ColumnExists(reader, "POLDesc"))
            {
                if (reader["POLDesc"] != DBNull.Value)
                    POLDesc = Convert.ToString(reader["POLDesc"]);
            }
            else
            {
                POLDesc = POL;
            }

            if (ColumnExists(reader, "POD"))
                if (reader["POD"] != DBNull.Value)
                    POD = Convert.ToString(reader["POD"]);

            if (ColumnExists(reader, "FPOD"))
                if (reader["FPOD"] != DBNull.Value)
                    FPOD = Convert.ToString(reader["FPOD"]);

            if (ColumnExists(reader, "PODDesc"))
            {
                if (reader["PODDesc"] != DBNull.Value)
                    PODDesc = Convert.ToString(reader["PODDesc"]);
            }
            else
            {
                PODDesc = POD;
            }

            if (ColumnExists(reader, "FPODDesc"))
            {
                if (reader["FPODDesc"] != DBNull.Value)
                    FPODDesc = Convert.ToString(reader["FPODDesc"]);
            }
            else
            {
                FPODDesc = FPOD;
            }

            if (ColumnExists(reader, "NoOfBL"))
                if (reader["NoOfBL"] != DBNull.Value)
                    NoOfBL = Convert.ToInt32(reader["NoOfBL"]);

            if (ColumnExists(reader, "BLType"))
                if (reader["BLType"] != DBNull.Value)
                    BLType = Convert.ToString(reader["BLType"]);

            if (ColumnExists(reader, "ShipmentMode"))
                if (reader["ShipmentMode"] != DBNull.Value)
                    ShipmentMode = Convert.ToInt32(reader["ShipmentMode"]);

            if (ColumnExists(reader, "Commodity"))
                if (reader["Commodity"] != DBNull.Value)
                    Commodity = Convert.ToString(reader["Commodity"]);

            if (ColumnExists(reader, "Containers"))
                if (reader["Containers"] != DBNull.Value)
                    Containers = Convert.ToString(reader["Containers"]);

            if (ColumnExists(reader, "BLIssuePlaceId"))
                if (reader["BLIssuePlaceId"] != DBNull.Value)
                    BLIssuePlaceId = Convert.ToInt32(reader["BLIssuePlaceId"]);

            if (ColumnExists(reader, "BLIssuePlace"))
                if (reader["BLIssuePlace"] != DBNull.Value)
                    BLIssuePlace = Convert.ToString(reader["BLIssuePlace"]);

            if (ColumnExists(reader, "NetWeight"))
                if (reader["NetWeight"] != DBNull.Value)
                    NetWeight = Convert.ToDecimal(reader["NetWeight"]);

            if (ColumnExists(reader, "BLReleaseDate"))
                if (reader["BLReleaseDate"] != DBNull.Value)
                    BLReleaseDate = Convert.ToDateTime(reader["BLReleaseDate"]);

            //Other
            if (ColumnExists(reader, "ShipperName"))
                if (reader["ShipperName"] != DBNull.Value)
                    ShipperName = Convert.ToString(reader["ShipperName"]);

            if (ColumnExists(reader, "Shipper"))
                if (reader["Shipper"] != DBNull.Value)
                    Shipper = Convert.ToString(reader["Shipper"]);

            if (ColumnExists(reader, "Consignee"))
                if (reader["Consignee"] != DBNull.Value)
                    Consignee = Convert.ToString(reader["Consignee"]);

            if (ColumnExists(reader, "ConsigneeName"))
                if (reader["ConsigneeName"] != DBNull.Value)
                    ConsigneeName = Convert.ToString(reader["ConsigneeName"]);

            if (ColumnExists(reader, "NotifyPartyName"))
                if (reader["NotifyPartyName"] != DBNull.Value)
                    NotifyPartyName = Convert.ToString(reader["NotifyPartyName"]);

            if (ColumnExists(reader, "NotifyParty"))
                if (reader["NotifyParty"] != DBNull.Value)
                    NotifyParty = Convert.ToString(reader["NotifyParty"]);

            if (ColumnExists(reader, "BLClause"))
                if (reader["BLClause"] != DBNull.Value)
                    BLClause = Convert.ToString(reader["BLClause"]);

            if (ColumnExists(reader, "GoodDesc"))
                if (reader["GoodDesc"] != DBNull.Value)
                    GoodDesc = Convert.ToString(reader["GoodDesc"]);

            if (ColumnExists(reader, "MarksNumnbers"))
                if (reader["MarksNumnbers"] != DBNull.Value)
                    MarksNumnbers = Convert.ToString(reader["MarksNumnbers"]);

            if (ColumnExists(reader, "AgentId"))
                if (reader["AgentId"] != DBNull.Value)
                    AgentId = Convert.ToInt32(reader["AgentId"]);
            
            //Container
            if (ColumnExists(reader, "TotalTEU"))
                if (reader["TotalTEU"] != DBNull.Value)
                    TotalTEU = Convert.ToString(reader["TotalTEU"]);

            if (ColumnExists(reader, "TotalFEU"))
                if (reader["TotalFEU"] != DBNull.Value)
                    TotalFEU = Convert.ToString(reader["TotalFEU"]);

            if (ColumnExists(reader, "TotalTon"))
                if (reader["TotalTon"] != DBNull.Value)
                    TotalTon = Convert.ToString(reader["TotalTon"]);

            if (ColumnExists(reader, "TotalCBM"))
                if (reader["TotalCBM"] != DBNull.Value)
                    TotalCBM = Convert.ToString(reader["TotalCBM"]);
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
