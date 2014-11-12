using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using System.Data;
namespace EMS.Entity
{
    public class SettlementEntity : ISettlement
    {
        public int CompanyID
        {
            get;
            set;
        }

        public int LocationID
        {
            get;
            set;
        }

        public string LocName
        {
            get;
            set;
        }

        public string Line
        {
            get;
            set;
        }

        public string BLNo
        {
            get;
            set;
        }

        public long BLID
        {
            get;
            set;
        }

        public int NVOCCID
        {
            get;
            set;
        }

        public int pk_SettlementID
        {
            get;
            set;
        }

        public DateTime SettlementDate
        {
            get;
            set;
        }

        public string SettlementNo
        {
            get;
            set;
        }

        public string PorR
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

        public decimal SettlementAmount
        {
            get;
            set;
        }

        public decimal OutstandingAmount
        {
            get;
            set;
        }

        public DateTime? ChequeDate
        {
            get;
            set;
        }

        public string PayRcvd { get; set; }
        public string ChequeDetail { get; set; }
        public string BankName { get; set; }
        public string RRFileUploadPath { get; set; }
        public string CLFileUploadPath { get; set; }

        public SettlementEntity()
        {
            
        }

        public SettlementEntity(DataTableReader reader)
        {
            this.BLID = Convert.ToInt32(reader["BLID"]);
            this.CompanyID = Convert.ToInt32(reader["CompanyID"]);
            this.pk_SettlementID = Convert.ToInt32(reader["pk_SettlementID"]);
            this.LocationID = Convert.ToInt32(reader["LocationID"]);
            this.NVOCCID = Convert.ToInt32(reader["NVOCCID"]);
            this.SettlementDate = Convert.ToDateTime(reader["SettlementDate"]);
            this.SettlementNo = Convert.ToString(reader["SettlementNo"]);
            this.LocName = Convert.ToString(reader["LocName"]);
            this.Line = Convert.ToString(reader["Line"]);
            this.BLNo = Convert.ToString(reader["ImpLineBLNO"]);
            this.SettlementAmount = Convert.ToDecimal(reader["SettlementAmount"]);
            if (ColumnExists(reader, "PorR"))
                if (reader["PorR"] != DBNull.Value)
                    this.PorR = Convert.ToString(reader["PorR"]);
            if (ColumnExists(reader, "PayRcvd"))
                if (reader["PayRcvd"] != DBNull.Value)
                    this.PayRcvd = Convert.ToString(reader["PayRcvd"]);
            if (ColumnExists(reader, "ChequeDetail"))
                if (reader["ChequeDetail"] != DBNull.Value)
                    this.ChequeDetail = Convert.ToString(reader["ChequeDetail"]);
            if (ColumnExists(reader, "BankName"))
                if (reader["BankName"] != DBNull.Value)
                    this.BankName = Convert.ToString(reader["BankName"]);
            if (ColumnExists(reader, "ChequeDate"))
                if (reader["ChequeDate"] != DBNull.Value)
                    this.ChequeDate = Convert.ToDateTime(reader["ChequeDate"]);
            if (ColumnExists(reader, "RRFileUploadPath"))
                if (reader["RRFileUploadPath"] != DBNull.Value)
                    this.BankName = Convert.ToString(reader["RRFileUploadPath"]);
            if (ColumnExists(reader, "CLFileUploadPath"))
                if (reader["CLFileUploadPath"] != DBNull.Value)
                    this.BankName = Convert.ToString(reader["CLFileUploadPath"]);

        }

        public bool ColumnExists(IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i) == columnName)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
