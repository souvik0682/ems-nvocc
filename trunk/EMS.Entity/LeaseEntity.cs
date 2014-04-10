using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using System.Data;

namespace EMS.Entity
{
    public class LeaseEntity : ILease
    {
        public int LeaseID
        {
            get;
            set;
        }

          public bool Action
        {
            get;
            set;
        }

        public string LeaseNo
        {
            get;
            set;
        }

        public DateTime LeaseDate
        {
            get;
            set;
        }

        public string EmptyYard
        {
            get;
            set;
        }

        public string LineName
        {
            get;
            set;
        }

        public string LocationName
        {
            get;
            set;
        }

        public int LocationID
        {
            get;
            set;
        }

        public int LinerID
        {
            get;
            set;
        }

        public bool LeaseStatus
        {
            get;
            set;
        }

        public DateTime LeaseValidTill
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public int fk_EmptyYardID { get; set; }


        public string LeaseCompany
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

        

        public LeaseEntity()
        {

        }

        public LeaseEntity(DataTableReader reader)
        {
            this.LeaseID = Convert.ToInt32(reader["pk_LeaseID"]);
            this.LeaseNo = Convert.ToString(reader["LeaseNo"]);
            this.LeaseDate = Convert.ToDateTime(reader["LeaseDate"]);
            this.LinerID = Convert.ToInt32(reader["fk_LIneID"]);
            this.LocationID = Convert.ToInt32(reader["fk_LocationID"]);
            this.LocationName = Convert.ToString(reader["LocationName"]);
            this.LineName = Convert.ToString(reader["LineName"]);
            this.EmptyYard = Convert.ToString(reader["EmptyYard"]);

            if (ColumnExists(reader, "fk_EmptyYardID"))
                if (reader["fk_EmptyYardID"] != DBNull.Value)
                    this.fk_EmptyYardID = Convert.ToInt32(reader["fk_EmptyYardID"]);

            if (ColumnExists(reader, "LeaseStatus"))
                if (reader["LeaseStatus"] != DBNull.Value)
                    this.LeaseStatus = Convert.ToBoolean(reader["LeaseStatus"]);
            this.fk_EmptyYardID = Convert.ToInt32(reader["fk_EmptyYardID"]);
            if (ColumnExists(reader, "LeaseValidTill"))
                if (reader["LeaseValidTill"] != DBNull.Value)
                    this.LeaseValidTill = Convert.ToDateTime(reader["LeaseValidTill"]);
            if (ColumnExists(reader, "Description"))
                if (reader["Description"] != DBNull.Value)
                    this.Description = Convert.ToString(reader["Description"]);
                       
            if (ColumnExists(reader, "LeaseCompany"))
                if (reader["LeaseCompany"] != DBNull.Value)
                    this.LeaseCompany = Convert.ToString(reader["LeaseCompany"]);

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
