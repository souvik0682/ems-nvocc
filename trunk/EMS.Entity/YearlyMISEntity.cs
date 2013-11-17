using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EMS.Common;

namespace EMS.Entity.Report
{
    public class YearlyMISEntity
    {
        #region Public Properties

        public int LocID
        {
            get;
            set;
        }

        public string LocName
        {
            get;
            set;
        }


        public int LineID
        {
            get;
            set;
        }

        public string LineName
        {
            get;
            set;
        }

        int TEU
        {
            get;
            set;
        }

        int FEU
        {
            get;
            set;
        }

        int Month1
        {
            get;
            set;
        }

        int Month2
        {
            get;
            set;
        }

        int Month3
        {
            get;
            set;
        }

        int Month4
        {
            get;
            set;
        }

        int Month5
        {
            get;
            set;
        }

        int Month6
        {
            get;
            set;
        }

        int Month7
        {
            get;
            set;
        }

        int Month8
        {
            get;
            set;
        }

        int Month9
        {
            get;
            set;
        }

        int Month10
        {
            get;
            set;
        }

        int Month11
        {
            get;
            set;
        }

        int Month12
        {
            get;
            set;
        }

        int Total
        {
            get;
            set;
        }
        #endregion

        #region Constructors

        public YearlyMISEntity()
        {

        }

        public YearlyMISEntity(DataTableReader reader)
        {
   
            if (HasColumn(reader, "LocationName") && reader["LocationName"] != DBNull.Value)
                this.LocName = Convert.ToString(reader["LocationName"]);

            if (HasColumn(reader, "ProspectFor") && reader["ProspectFor"] != DBNull.Value)
                this.LineName = Convert.ToString(reader["ProspectFor"]);

            if (HasColumn(reader, "Month1") && reader["Month1"] != DBNull.Value)
                this.Month1 = Convert.ToInt32(reader["Month1"]);

            if (HasColumn(reader, "Month2") && reader["Month2"] != DBNull.Value)
                this.Month2 = Convert.ToInt32(reader["Month2"]);

            if (HasColumn(reader, "Month3") && reader["Month3"] != DBNull.Value)
                this.Month3 = Convert.ToInt32(reader["Month3"]);

            if (HasColumn(reader, "Month4") && reader["Month4"] != DBNull.Value)
                this.Month4 = Convert.ToInt32(reader["Month4"]);

            if (HasColumn(reader, "Month5") && reader["Month5"] != DBNull.Value)
                this.Month5 = Convert.ToInt32(reader["Month5"]);

            if (HasColumn(reader, "Month6") && reader["Month6"] != DBNull.Value)
                this.Month6 = Convert.ToInt32(reader["Month6"]);

            if (HasColumn(reader, "Month7") && reader["Month7"] != DBNull.Value)
                this.Month7 = Convert.ToInt32(reader["Month7"]);

            if (HasColumn(reader, "Month8") && reader["Month8"] != DBNull.Value)
                this.Month8 = Convert.ToInt32(reader["Month8"]);

            if (HasColumn(reader, "Month9") && reader["Month9"] != DBNull.Value)
                this.Month9 = Convert.ToInt32(reader["Month9"]);

            if (HasColumn(reader, "Month10") && reader["Month10"] != DBNull.Value)
                this.Month10 = Convert.ToInt32(reader["Month10"]);

            if (HasColumn(reader, "Month11") && reader["Month11"] != DBNull.Value)
                this.Month11 = Convert.ToInt32(reader["Month11"]);

            if (HasColumn(reader, "Month12") && reader["Month12"] != DBNull.Value)
                this.Month12 = Convert.ToInt32(reader["Month12"]);

            if (HasColumn(reader, "Total") && reader["Total"] != DBNull.Value)
                this.Total = Convert.ToInt32(reader["Total"]);
            
        }

        #endregion

        #region Private Methods

        private bool HasColumn(DataTableReader reader, string columnName)
        {
            try
            {
                return reader.GetOrdinal(columnName) >= 0;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        #endregion
    }
}
