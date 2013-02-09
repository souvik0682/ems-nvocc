using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using System.Data;

namespace EMS.Entity
{
    public class ImportHaulageEntity : IImportHaulage
    {
        public int HaulageChgID
        {
            get;
            set;
        }

        public string LocationFrom
        {
            get;
            set;
        }

        public string LocationTo
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

        public string LFCode
        {
            get;
            set;
        }

        public string LTCode
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



        public ImportHaulageEntity()
        {

        }


        public ImportHaulageEntity(DataTableReader reader)
        {
            this.ContainerSize = Convert.ToString(reader["ContainerSize"]);
            this.HaulageChgID = Convert.ToInt32(reader["HaulageChgID"]);
            this.HaulageRate = Convert.ToDecimal(reader["HaulageRate"]);
            this.HaulageStatus = Convert.ToBoolean(reader["HaulageStatus"]);
            this.LocationFrom = Convert.ToString(reader["LocationFrom"]);
            this.LocationTo = Convert.ToString(reader["LocationTo"]);
            this.WeightFrom = Convert.ToDecimal(reader["WeightFrom"]);
            this.WeightTo = Convert.ToDecimal(reader["WeightTo"]);
            this.LFCode = Convert.ToString(reader["LFCode"]);
            this.LTCode = Convert.ToString(reader["LTCode"]);
        }






    }
}
