using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EMS.Common;

namespace EMS.Entity
{
    public class ImportHaulage : IImportHaulage, ICommon
    {
        #region IImportHaulage Members

        public int HaulageChgID
        {
            get;
            set;
        }

        public int LocationFrom
        {
            get;
            set;
        }

        public int LocationTo
        {
            get;
            set;
        }

        public string ContainerSize
        {
            get;
            set;
        }

        public double WeightFrom
        {
            get;
            set;
        }

        public double WeightTo
        {
            get;
            set;
        }

        public double HaulageRate
        {
            get;
            set;
        }

        public bool HaulageStatus
        {
            get;
            set;
        }

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

        public ImportHaulage()
        {

        }

        public ImportHaulage(DataTableReader reader)
        {
            this.HaulageChgID = Convert.ToInt32(reader["HaulageChgID"]);
            this.LocationFrom = Convert.ToInt32(reader["LocationFrom"]);
            this.LocationTo = Convert.ToInt32(reader["LocationTo"]);
            this.ContainerSize = Convert.ToString(reader["ContainerSize"]);
            this.WeightFrom = Convert.ToDouble(reader["WeightFrom"]);
            this.WeightTo = Convert.ToDouble(reader["WeightTo"]);
            this.HaulageRate = Convert.ToDouble(reader["HaulageRate"]);
            this.HaulageStatus = Convert.ToBoolean(reader["HaulageStatus"]);
        }

        #endregion
    }
}
