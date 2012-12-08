using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EMS.Common;

namespace EMS.Entity
{
    public class ImportHaulageEntity : IImportHaulage, ICommon
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

        public decimal WeightFrom
        {
            get;
            set;
        }

        public decimal WeightTo
        {
            get;
            set;
        }

        public decimal HaulageRate
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

        public ImportHaulageEntity()
        {

        }

        public ImportHaulageEntity(DataTableReader reader)
        {
            this.HaulageChgID = Convert.ToInt32(reader["HaulageChgID"]);
            this.LocationFrom = Convert.ToInt32(reader["LocationFrom"]);
            this.LocationTo = Convert.ToInt32(reader["LocationTo"]);
            this.ContainerSize = Convert.ToString(reader["ContainerSize"]);
            this.WeightFrom = Convert.ToDecimal(reader["WeightFrom"]);
            this.WeightTo = Convert.ToDecimal(reader["WeightTo"]);
            this.HaulageRate = Convert.ToDecimal(reader["HaulageRate"]);
            this.HaulageStatus = Convert.ToBoolean(reader["HaulageStatus"]);
        }

        #endregion
    }
}
