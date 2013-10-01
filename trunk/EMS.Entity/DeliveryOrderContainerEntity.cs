using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using System.Data;

namespace EMS.Entity
{
    public class DeliveryOrderContainerEntity : IDeliveryOrderContainer
    {
        #region DeliveryOrderContainerEntity Members

        public int ContainerTypeId
        {
            get;
            set;
        }

        public string ContainerType
        {
            get;
            set;
        }

        public string ContainerSize
        {
            get;
            set;
        }

        public int AvailableUnit
        {
            get;
            set;
        }

        public int RequiredUnit
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public DeliveryOrderContainerEntity()
        {

        }

        public DeliveryOrderContainerEntity(DataTableReader reader)
        {
            this.ContainerTypeId = Convert.ToInt32(reader["ContainerTypeId"]);
            this.ContainerType = Convert.ToString(reader["ContainerType"]);
            this.ContainerSize = Convert.ToString(reader["ContainerSize"]);
            this.AvailableUnit = Convert.ToInt32(reader["AvailableUnit"]);
            this.RequiredUnit = Convert.ToInt32(reader["RequiredUnit"]);
        }

        #endregion
    }
}
