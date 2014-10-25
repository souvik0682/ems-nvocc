using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using System.Data;

namespace EMS.Entity
{
    public class AdvAdjEntity : IAdvAdj
    {
        public int AdvAdjID { get; set; }

        public int JobID { get; set; }

        public string JobNo { get; set; }

        public string AdjustmentNo { get; set; }

        public DateTime AdjustmentDate {get; set;}

        public bool AdjStatus { get; set; }

        public string DorC { get; set; }

        public int PartyID { get; set; }

        public string PartyType { get; set; }

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

        public AdvAdjEntity()
        {
        }

        public AdvAdjEntity(DataTableReader reader)
        {

            this.AdvAdjID = Convert.ToInt32(reader["pk_AdvAdjID"]);
            this.DorC = Convert.ToString(reader["DorC"]);
            this.JobID = Convert.ToInt32(reader["fk_JobID"]);
            this.AdjustmentNo = Convert.ToString(reader["AdjustmentNo"]);
            this.AdjustmentDate = Convert.ToDateTime(reader["AdjustmentDate"]);

            if (ColumnExists(reader, "PartyType"))
                if (reader["PartyType"] != DBNull.Value)
                    this.PartyType = Convert.ToString(reader["PartyType"]);

            if (ColumnExists(reader, "JobNo"))
                if (reader["JobNo"] != DBNull.Value)
                    this.JobNo = Convert.ToString(reader["JobNo"]);

            if (ColumnExists(reader, "UserID"))
                if (reader["UserID"] != DBNull.Value)
                    this.CreatedBy = Convert.ToInt32(reader["UserID"]);

            if (ColumnExists(reader, "AdjStatus"))
                if (reader["AdjStatus"] != DBNull.Value)
                    this.AdjStatus = Convert.ToBoolean(reader["AdjStatus"]);
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
