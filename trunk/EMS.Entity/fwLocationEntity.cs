using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EMS.Common;
namespace EMS.Entity
{
    public class fwLocationEntity : IFwLocation
    {
        #region ILocation Members

        public IAddress LocAddress
        {
            get;
            set;
        }

        public string Abbreviation
        {
            get;
            set;
        }

        public string Phone
        {
            get;
            set;
        }

        public bool IsActive
        {
            get;
            set;
        }

        public int DefaultLocation
        {
            get;
            set;
        }

        public int locID { get; set; }
        public string LocName { get; set; }
        public string CityPin { get; set; }

        #endregion

        #region ICommon Members

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

        #endregion

        #region Constructors

        public fwLocationEntity()
        {
            this.LocAddress = new AddressEntity();
        }

        public fwLocationEntity(DataTableReader reader)
        {
            this.locID = Convert.ToInt32(reader["Id"]);
            this.LocName = Convert.ToString(reader["LocName"]);
            this.LocAddress = new AddressEntity(reader);
            this.Abbreviation = Convert.ToString(reader["LocAbbr"]);
            this.Phone = Convert.ToString(reader["LocPhone"]);

            if (ColumnExists(reader, "CityPin"))
                if (reader["CityPin"] != DBNull.Value)
                    this.CityPin = Convert.ToString(reader["CityPin"]);

            if (ColumnExists(reader, "LocStatus"))
                if (reader["LocStatus"] != DBNull.Value)
                    this.IsActive = Convert.ToBoolean(reader["LocStatus"]);

            if (ColumnExists(reader, "DefaultLocID"))
                if (reader["DefaultLocID"] != DBNull.Value)
                    this.DefaultLocation = Convert.ToInt32(reader["DefaultLocID"]);
        }

        #endregion

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
