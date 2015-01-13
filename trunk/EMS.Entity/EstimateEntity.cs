using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using System.Data;

namespace EMS.Entity
{
    public class EstimateEntity
    {
        public long EstimateID { get; set; }
        public int PartyID { get; set; }
        public bool EstimateStatus { get; set; }
        public DateTime EstimateDate { get; set; }
        public string EstimateNo { get; set; }
        public string PorR { get; set; }
        public string TransactionType { get; set; }
        public int fk_JobID { get; set; }
        public string JobNo { get; set; }
        public int fk_BillFromID { get; set; }
        public int CompanyID { get; set; }
        public int CreditDays { get; set; }
        public decimal Charges { get; set; }
        public int CreatedBy
        {
            get;
            set;
        }

        public EstimateEntity()
        {
        }
        public EstimateEntity(DataTableReader reader)
        {
            if (ColumnExists(reader, "pk_EstimateID"))
                if (reader["pk_EstimateID"] != DBNull.Value)
                    EstimateID = Convert.ToInt64(reader["pk_EstimateID"]);

            if (ColumnExists(reader, "EstimateNo"))
                if (reader["EstimateNo"] != DBNull.Value)
                    EstimateNo = Convert.ToString(reader["EstimateNo"]);

            if (ColumnExists(reader, "TransactionType"))
                if (reader["TransactionType"] != DBNull.Value)
                    TransactionType = Convert.ToString(reader["TransactionType"]);

            if (ColumnExists(reader, "EstimateDate"))
                if (reader["EstimateDate"] != DBNull.Value)
                    EstimateDate = Convert.ToDateTime(reader["EstimateDate"]);

            if (ColumnExists(reader, "fk_CompanyID"))
                if (reader["fk_CompanyID"] != DBNull.Value)
                    CompanyID = Convert.ToInt32(reader["fk_CompanyID"]);

            if (ColumnExists(reader, "fk_PartyID"))
                if (reader["fk_PartyID"] != DBNull.Value)
                    PartyID = Convert.ToInt32(reader["fk_PartyID"]);

            if (ColumnExists(reader, "fk_JobID"))
                if (reader["fk_JobID"] != DBNull.Value)
                    fk_JobID = Convert.ToInt32(reader["fk_JobID"]);

            if (ColumnExists(reader, "JobNo"))
                if (reader["JobNo"] != DBNull.Value)
                    JobNo = Convert.ToString(reader["JobNo"]);

            if (ColumnExists(reader, "fk_BillFromID"))
                if (reader["fk_BillFromID"] != DBNull.Value)
                    fk_BillFromID = Convert.ToInt32(reader["fk_BillFromID"]);

            if (ColumnExists(reader, "fk_UserAdded"))
                if (reader["fk_UserAdded"] != DBNull.Value)
                    CreatedBy = Convert.ToInt32(reader["fk_UserAdded"]);

            if (ColumnExists(reader, "CreditDays"))
                if (reader["CreditDays"] != DBNull.Value)
                    CreditDays = Convert.ToInt32(reader["CreditDays"]);

            if (ColumnExists(reader, "EstimateStatus"))
                if (reader["EstimateStatus"] != DBNull.Value)
                    EstimateStatus = Convert.ToBoolean(reader["EstimateStatus"]);
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
