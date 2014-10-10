
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using System.Data;
namespace EMS.Entity
{
    public class JobEntity : IJob
    {
        public long JobID { get; set; }
        public int CompanyID { get; set; }
        public bool JobStatus { get; set; }
        public DateTime JobDate { get; set; }
        public string JobType { get; set; }
        public int JobTypeID { get; set; }
        public string JobPrefix { get; set; }
        public string JobNo { get; set; }
        public string PJobNo { get; set; }
        public int OpsLocID { get; set; }
        public string OpsLoc { get; set; }
        public int jobLocID { get; set; }
        public string JobLoc { get; set; }
        public int SalesmanID { get; set; }
        public int SmodeID { get; set; }
        public string ShippingMode { get; set; }
        public int PrDocID { get; set; }
        public bool PrintHBL { get; set; }
        public int HBLFormatID { get; set; }
        public int ttl20 { get; set; }
        public int ttl40 { get; set; }
        public decimal grwt { get; set; }
        public decimal VolWt { get; set; }
        public decimal weightMT { get; set; }
        public decimal volCBM { get; set; }
        public decimal RevTon { get; set; }
        public int FLineID { get; set; }
        public string PlaceOfReceipt { get; set; }
        public int fk_LportID { get; set; }
        public int fk_DportID { get; set; }
        public string PlaceOfDelivery { get; set; }
        public int fk_CustID { get; set; }
        public int fk_CustAgentID { get; set; }
        public int fk_TransID { get; set; }
        public int fk_OSID { get; set; }
        public decimal EstPayable { get; set; }
        public decimal EstReceivable { get; set; }
        public char JobActive { get; set; }

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
        public JobEntity()
        {
        }
        public JobEntity(DataTableReader reader)
        {
            if (ColumnExists(reader, "pk_JobID"))
                if (reader["pk_JobID"] != DBNull.Value)
                    JobID = Convert.ToInt64(reader["pk_JobID"]);

             if (ColumnExists(reader, "JobType"))
                 if (reader["JobType"] != DBNull.Value)
                     JobType = Convert.ToString(reader["JobType"]);

             if (ColumnExists(reader, "JobDate"))
                 if (reader["JobDate"] != DBNull.Value)
                     JobDate = Convert.ToDateTime(reader["JobDate"]);

            if (ColumnExists(reader, "fk_CompanyID"))
                if (reader["fk_CompanyID"] != DBNull.Value)
                    CompanyID = Convert.ToInt32(reader["fk_CompanyID"]);

            if (ColumnExists(reader, "JobPrefix"))
                if (reader["JobPrefix"] != DBNull.Value)
                    JobPrefix = Convert.ToString(reader["JobPrefix"]);

            if (ColumnExists(reader, "JobNo"))
                if (reader["JobNo"] != DBNull.Value)
                    JobNo = Convert.ToString(reader["JobNo"]);

            if (ColumnExists(reader, "PJobNo"))
                if (reader["PJobNo"] != DBNull.Value)
                    PJobNo = Convert.ToString(reader["PJobNo"]);

            if (ColumnExists(reader, "OpsLoc"))
                if (reader["OpsLoc"] != DBNull.Value)
                    OpsLoc = Convert.ToString(reader["OpsLoc"]);

            if (ColumnExists(reader, "JobLoc"))
                if (reader["JobLoc"] != DBNull.Value)
                    JobLoc = Convert.ToString(reader["JobLoc"]);

            if (ColumnExists(reader, "fk_OpsLocID"))
                if (reader["fk_OpsLocID"] != DBNull.Value)
                    OpsLocID = Convert.ToInt32(reader["fk_OpsLocID"]);

            if (ColumnExists(reader, "fk_JobLocID"))
                if (reader["fk_JobLocID"] != DBNull.Value)
                    jobLocID = Convert.ToInt32(reader["fk_JobLocID"]);

            if (ColumnExists(reader, "fk_SalesmanID"))
                if (reader["fk_SalesmanID"] != DBNull.Value)
                    SalesmanID = Convert.ToInt32(reader["fk_SalesmanID"]);

            if (ColumnExists(reader, "fk_SmodeID"))
                if (reader["fk_SmodeID"] != DBNull.Value)
                    SmodeID = Convert.ToInt32(reader["fk_SmodeID"]);

            if (ColumnExists(reader, "PrDocID"))
                if (reader["PrDocID"] != DBNull.Value)
                    PrDocID = Convert.ToInt32(reader["PrDocID"]);

            if (ColumnExists(reader, "ShippingMode"))
                if (reader["ShippingMode"] != DBNull.Value)
                    ShippingMode = Convert.ToString(reader["ShippingMode"]);

            if (ColumnExists(reader, "PrintHBL"))
                if (reader["PrintHBL"] != DBNull.Value)
                    PrintHBL = Convert.ToBoolean(reader["PrintHBL"]);

            if (ColumnExists(reader, "HBLFormatID"))
                if (reader["HBLFormatID"] != DBNull.Value)
                    HBLFormatID = Convert.ToInt32(reader["HBLFormatID"]);

            if (ColumnExists(reader, "ttl20"))
                if (reader["ttl20"] != DBNull.Value)
                    ttl20 = Convert.ToInt32(reader["ttl20"]);

            if (ColumnExists(reader, "ttl40"))
                if (reader["ttl40"] != DBNull.Value)
                    ttl40 = Convert.ToInt32(reader["ttl40"]);

            if (ColumnExists(reader, "grwt"))
                if (reader["grwt"] != DBNull.Value)
                    grwt = Convert.ToDecimal(reader["grwt"]);

            if (ColumnExists(reader, "VolWt"))
                if (reader["VolWt"] != DBNull.Value)
                    VolWt = Convert.ToDecimal(reader["VolWt"]);

            if (ColumnExists(reader, "VolCBM"))
                if (reader["VolCBM"] != DBNull.Value)
                    volCBM = Convert.ToDecimal(reader["VolCBM"]);

            if (ColumnExists(reader, "weightMT"))
                if (reader["weightMT"] != DBNull.Value)
                    weightMT = Convert.ToDecimal(reader["weightMT"]);

            if (ColumnExists(reader, "revTon"))
                if (reader["revTon"] != DBNull.Value)
                    RevTon = Convert.ToDecimal(reader["revTon"]);

            if (ColumnExists(reader, "fk_FLineID"))
                if (reader["fk_FLineID"] != DBNull.Value)
                    FLineID = Convert.ToInt32(reader["fk_FLineID"]);

            if (ColumnExists(reader, "PlaceOfReceipt"))
                if (reader["PlaceOfReceipt"] != DBNull.Value)
                    PlaceOfReceipt = Convert.ToString(reader["PlaceOfReceipt"]);

            if (ColumnExists(reader, "PlaceOfDelivery"))
                if (reader["PlaceOfDelivery"] != DBNull.Value)
                    PlaceOfDelivery = Convert.ToString(reader["PlaceOfDelivery"]);

            if (ColumnExists(reader, "fk_LPortID"))
                if (reader["fk_LPortID"] != DBNull.Value)
                    fk_LportID = Convert.ToInt32(reader["fk_LPortID"]);

            if (ColumnExists(reader, "fk_DPortID"))
                if (reader["fk_DPortID"] != DBNull.Value)
                    fk_DportID = Convert.ToInt32(reader["fk_DPortID"]);

            if (ColumnExists(reader, "fk_CustID"))
                if (reader["fk_CustID"] != DBNull.Value)
                    fk_CustID = Convert.ToInt32(reader["fk_CustID"]);

            if (ColumnExists(reader, "fk_CustAgentID"))
                if (reader["fk_CustAgentID"] != DBNull.Value)
                    fk_CustAgentID = Convert.ToInt32(reader["fk_CustAgentID"]);

            if (ColumnExists(reader, "fk_TransID"))
                if (reader["fk_TransID"] != DBNull.Value)
                    fk_TransID = Convert.ToInt32(reader["fk_TransID"]);

            if (ColumnExists(reader, "fk_OSID"))
                if (reader["fk_OSID"] != DBNull.Value)
                    fk_OSID = Convert.ToInt32(reader["fk_OSID"]);

            if (ColumnExists(reader, "EstPayable"))
                if (reader["EstPayable"] != DBNull.Value)
                    EstPayable = Convert.ToDecimal(reader["EstPayable"]);

            if (ColumnExists(reader, "EstReceivable"))
                if (reader["EstReceivable"] != DBNull.Value)
                    EstReceivable = Convert.ToDecimal(reader["EstReceivable"]);

            if (ColumnExists(reader, "dtAdded"))
                if (reader["dtAdded"] != DBNull.Value)
                    CreatedOn = Convert.ToDateTime(reader["dtAdded"]);

            if (ColumnExists(reader, "UserAdded"))
                if (reader["UserAdded"] != DBNull.Value)
                    CreatedBy = Convert.ToInt32(reader["UserAdded"]);

            if (ColumnExists(reader, "JobStatus"))
                if (reader["VoyageStatus"] != DBNull.Value)
                    JobStatus = Convert.ToBoolean(reader["JobStatus"]);
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
