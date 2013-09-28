using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EMS.Common;

namespace EMS.Entity
{
    public class DeliveryOrderEntity
    {        
        #region Public Properties

        public Int32 LocationId
        {
            get;
            set;
        }

        public string LocationName
        {
            get;
            set;
        }

        public Int32 NVOCCId
        {
            get;
            set;
        }

        public string NVOCCName
        {
            get;
            set;
        }

        public string BookingNumber
        {
            get;
            set;
        }

        public string DeliveryOrderNumber
        {
            get;
            set;
        }

        public DateTime DeliveryOrderDate
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public DeliveryOrderEntity()
        {
            
        }

        public DeliveryOrderEntity(DataTableReader reader)
        {

        }       

        #endregion
    }
}
