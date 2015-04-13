
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using System.Data;

namespace EMS.Entity
{
    public class GroupEntity : IGroup
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public string GroupAddress { get; set; }
        public int UserID { get; set; }
        public bool GroupStatus { get; set; }

         public GroupEntity()
    {
        //Initalize();
    }

        public GroupEntity(DataTableReader reader)
        {
            //Initalize()
            if (ColumnExists(reader, "ID"))
                if (reader["ID"] != DBNull.Value)
                    GroupID = Convert.ToInt32(reader["ID"]);

            if (ColumnExists(reader, "Name"))
                if (reader["Name"] != DBNull.Value)
                    GroupName = Convert.ToString(reader["Name"]);

            if (ColumnExists(reader, "Address"))
                if (reader["Address"] != DBNull.Value)
                    GroupAddress = Convert.ToString(reader["Address"]);

            if (ColumnExists(reader, "GroupStatus"))
                if (reader["GroupStatus"] != DBNull.Value)
                    GroupStatus = Convert.ToBoolean(reader["GroupStatus"]);

            if (ColumnExists(reader, "fk_UserAdded"))
                if (reader["fk_UserAdded"] != DBNull.Value)
                    UserID = Convert.ToInt32(reader["fk_UserAdded"]);
        }

        public GroupEntity(DataRow reader)
        {
            //Initalize();

            if (ColumnExists(reader, "ID"))
                if (reader["ID"] != DBNull.Value)
                    GroupID = Convert.ToInt32(reader["ID"]);

            if (ColumnExists(reader, "Name"))
                if (reader["Name"] != DBNull.Value)
                    GroupName = Convert.ToString(reader["Name"]);

            if (ColumnExists(reader, "Address"))
                if (reader["Address"] != DBNull.Value)
                    GroupAddress = Convert.ToString(reader["Address"]);

            if (ColumnExists(reader, "GroupStatus"))
                if (reader["GroupStatus"] != DBNull.Value)
                    GroupStatus = Convert.ToBoolean(reader["GroupStatus"]);

            if (ColumnExists(reader, "fk_UserAdded"))
                if (reader["fk_UserAdded"] != DBNull.Value)
                    UserID = Convert.ToInt32(reader["fk_UserAdded"]);
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

        public bool ColumnExists(DataRow reader, string columnName)
        {
            try { var row = reader[columnName];
            return true;
            }
            catch { }
            return false;
        }
    }

   
}
