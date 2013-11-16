
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using System.Data;

namespace EMS.Entity
{
    public class expVoyageEntity : IexpVoyage
    {
        public long VoyageID { get; set; }
        public int CompanyID { get; set; }
        public long VesselID { get; set; }
        public int LocationID { get; set; }
        public string VoyageNo { get; set; }
        public string VesselName { get; set; }
        public Int32 POL { get; set; }
        public DateTime? ETD { get; set; }
        public int TerminalID { get; set; }
        public string TerminalName { get; set; }
        public Int32 NextPortID { get; set; }
        public string VIA { get; set; }
        public string RotationNo { get; set; }
        public DateTime? RotationDate { get; set; }
        public DateTime? VesselCutOffDate { get; set; }
        public DateTime? DocsCutOffDate { get; set; }
        public string LoadPort { get; set; }
        public Int32 POD { get; set; }
        public string AgentCode { get; set; }
        public string LineCode { get; set; }
        public string PCCNo { get; set; }
        public DateTime? PCCDate { get; set; }
        public DateTime? ETA { get; set; }
        public DateTime? ETANextPort { get; set; }
        public string NextPort { get; set; }
        public string VCNNo { get; set; }
        public DateTime? SailDate { get; set; }
        public DateTime? dtAdded { get; set; }
        public DateTime? dtEdited { get; set; }
        public long UserAdded { get; set; }
        public long UserEdited { get; set; }
        public bool VoyageStatus { get; set; }
        public expVoyageEntity()
        {
        }
        public expVoyageEntity(DataTableReader reader)
        {
            if (ColumnExists(reader, "VoyageID"))
                if (reader["VoyageID"] != DBNull.Value)
                    VoyageID = Convert.ToInt64(reader["VoyageID"]);

            if (ColumnExists(reader, "fk_CompanyID"))
                if (reader["fk_CompanyID"] != DBNull.Value)
                    CompanyID = Convert.ToInt32(reader["fk_CompanyID"]);

            if (ColumnExists(reader, "VesselID"))
                if (reader["VesselID"] != DBNull.Value)
                    VesselID = Convert.ToInt64(reader["VesselID"]);

            if (ColumnExists(reader, "VesselName"))
                if (reader["VesselName"] != DBNull.Value)
                    VesselName= Convert.ToString(reader["VesselName"]);

            if (ColumnExists(reader, "TerminalName"))
                if (reader["TerminalName"] != DBNull.Value)
                    TerminalName = Convert.ToString(reader["TerminalName"]);

            if (ColumnExists(reader, "VoyageNo"))
                if (reader["VoyageNo"] != DBNull.Value)
                    VoyageNo = Convert.ToString(reader["VoyageNo"]);

            if (ColumnExists(reader, "fk_POL"))
                if (reader["fk_POL"] != DBNull.Value)
                    POL = Convert.ToInt32(reader["fk_POL"]);

            if (ColumnExists(reader, "ETD"))
                if (reader["ETD"] != DBNull.Value)
                    ETD = Convert.ToDateTime(reader["ETD"]);

            if (ColumnExists(reader, "TerminalID"))
                if (reader["TerminalID"] != DBNull.Value)
                    TerminalID = Convert.ToInt32(reader["TerminalID"]);

            if (ColumnExists(reader, "NextPortID"))
                if (reader["NextPortID"] != DBNull.Value)
                    NextPortID = Convert.ToInt32(reader["NextPortID"]);

            if (ColumnExists(reader, "VIA"))
                if (reader["VIA"] != DBNull.Value)
                    VIA = Convert.ToString(reader["VIA"]);

            if (ColumnExists(reader, "RotationNo"))
                if (reader["RotationNo"] != DBNull.Value)
                    RotationNo = Convert.ToString(reader["RotationNo"]);

            if (ColumnExists(reader, "RotationDate"))
                if (reader["RotationDate"] != DBNull.Value)
                    RotationDate = Convert.ToDateTime(reader["RotationDate"]);

            if (ColumnExists(reader, "VesselCutOffDate"))
                if (reader["VesselCutOffDate"] != DBNull.Value)
                    VesselCutOffDate = Convert.ToDateTime(reader["VesselCutOffDate"]);

            if (ColumnExists(reader, "DocsCutOffDate"))
                if (reader["DocsCutOffDate"] != DBNull.Value)
                    DocsCutOffDate = Convert.ToDateTime(reader["DocsCutOffDate"]);

            if (ColumnExists(reader, "LoadPort"))
                if (reader["LoadPort"] != DBNull.Value)
                    LoadPort = Convert.ToString(reader["LoadPort"]);

            if (ColumnExists(reader, "NextPort"))
                if (reader["NextPort"] != DBNull.Value)
                    NextPort = Convert.ToString(reader["NextPort"]);

            if (ColumnExists(reader, "POD"))
                if (reader["POD"] != DBNull.Value)
                    POD = Convert.ToInt32(reader["POD"]);

            if (ColumnExists(reader, "AgentCode"))
                if (reader["AgentCode"] != DBNull.Value)
                    AgentCode = Convert.ToString(reader["AgentCode"]);
                else
                    AgentCode = "";


            if (ColumnExists(reader, "LineCode"))
                if (reader["LineCode"] != DBNull.Value)
                    LineCode = Convert.ToString(reader["LineCode"]);

            if (ColumnExists(reader, "PCCNo"))
                if (reader["PCCNo"] != DBNull.Value)
                    PCCNo = Convert.ToString(reader["PCCNo"]);

            if (ColumnExists(reader, "PCCDate"))
                if (reader["PCCDate"] != DBNull.Value)
                    PCCDate = Convert.ToDateTime(reader["PCCDate"]);

            if (ColumnExists(reader, "ETANextPort"))
                if (reader["ETANextPort"] != DBNull.Value)
                    ETANextPort = Convert.ToDateTime(reader["ETANextPort"]);

            if (ColumnExists(reader, "ETA"))
                if (reader["ETA"] != DBNull.Value)
                    ETA = Convert.ToDateTime(reader["ETA"]);

            if (ColumnExists(reader, "VCNNo"))
                if (reader["VCNNo"] != DBNull.Value)
                    VCNNo = Convert.ToString(reader["VCNNo"]);

            if (ColumnExists(reader, "fk_LocationID"))
                if (reader["fk_LocationID"] != DBNull.Value)
                    LocationID= Convert.ToInt32(reader["fk_LocationID"]);

            if (ColumnExists(reader, "SailDate"))
                if (reader["SailDate"] != DBNull.Value)
                    SailDate = Convert.ToDateTime(reader["SailDate"]);

            if (ColumnExists(reader, "dtAdded"))
                if (reader["dtAdded"] != DBNull.Value)
                    dtAdded = Convert.ToDateTime(reader["dtAdded"]);

            if (ColumnExists(reader, "dtEdited"))
                if (reader["dtEdited"] != DBNull.Value)
                    dtEdited = Convert.ToDateTime(reader["dtEdited"]);

            if (ColumnExists(reader, "UserAdded"))
                if (reader["UserAdded"] != DBNull.Value)
                    UserAdded = Convert.ToInt64(reader["UserAdded"]);

            if (ColumnExists(reader, "UserEdited"))
                if (reader["UserEdited"] != DBNull.Value)
                    UserEdited = Convert.ToInt64(reader["UserEdited"]);

            if (ColumnExists(reader, "VoyageStatus"))
                if (reader["VoyageStatus"] != DBNull.Value)
                    VoyageStatus = Convert.ToBoolean(reader["VoyageStatus"]);
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
