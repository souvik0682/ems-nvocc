using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using System.Data;

namespace EMS.Entity
{
    public class fwLineEntity : IFwLine
    {
        public int LineID { get; set; }

        public string LineName { get; set; }

        public bool LineStatus { get; set; }

        public string LineType { get; set; }

        public string Prefix { get; set; }

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

        public fwLineEntity()
        {
        }

        public fwLineEntity(DataTableReader reader)
        {

            this.LineID = Convert.ToInt32(reader["pk_FLineID"]);
            this.LineName = Convert.ToString(reader["LineName"]);
            this.LineType = Convert.ToString(reader["LineType"]);
            this.Prefix = Convert.ToString(reader["Prefix"]);

            if (ColumnExists(reader, "UserID"))
                if (reader["UserID"] != DBNull.Value)
                    this.CreatedBy = Convert.ToInt32(reader["UserID"]);

            if (ColumnExists(reader, "LineStatus"))
                if (reader["LineStatus"] != DBNull.Value)
                    this.LineStatus = Convert.ToBoolean(reader["LineStatus"]);
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
