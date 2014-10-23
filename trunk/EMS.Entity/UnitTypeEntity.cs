
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using System.Data;

namespace EMS.Entity
{
    public class UnitTypeEntity
    {
        public long UnitTypeID { get; set; }
        public int CompanyID { get; set; }
        public string UnitName { get; set; }
        public string Prefix { get; set; }
        public bool UnitStatus { get; set; }
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
       
        public UnitTypeEntity()
        {
        }

        public UnitTypeEntity(DataTableReader reader)
        {
            if (ColumnExists(reader, "pk_UnitTypeID"))
                if (reader["pk_UnitTypeID"] != DBNull.Value)
                    UnitTypeID = Convert.ToInt64(reader["pk_UnitTypeID"]);

            if (ColumnExists(reader, "UnitName"))
                if (reader["UnitName"] != DBNull.Value)
                    UnitName = Convert.ToString(reader["UnitName"]);

            if (ColumnExists(reader, "Prefix"))
                if (reader["Prefix"] != DBNull.Value)
                    Prefix = Convert.ToString(reader["Prefix"]);

            if (ColumnExists(reader, "fk_CompanyID"))
                if (reader["fk_CompanyID"] != DBNull.Value)
                    CompanyID = Convert.ToInt32(reader["fk_CompanyID"]);

            if (ColumnExists(reader, "fk_UserAdded"))
                if (reader["fk_UserAdded"] != DBNull.Value)
                    CreatedBy = Convert.ToInt32(reader["fk_UserAdded"]);

            if (ColumnExists(reader, "UnitStatus"))
                if (reader["UnitStatus"] != DBNull.Value)
                    UnitStatus = Convert.ToBoolean(reader["UnitStatus"]);
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
