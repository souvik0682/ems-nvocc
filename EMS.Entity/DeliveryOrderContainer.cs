using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;

namespace EMS.Entity
{
    public class DeliveryOrderContainer:IDeliveryOrderContainer
    {
        #region IDeliveryOrderContainer Members

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
    }
}
