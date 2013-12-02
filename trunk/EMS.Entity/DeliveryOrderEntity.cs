﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EMS.Common;

namespace EMS.Entity
{
    public class DeliveryOrderEntity : IDeliveryOrder
    {
        #region IDeliveryOrder Members

        public Int64 DeliveryOrderId
        {
            get;
            set;
        }

        public Int64 BookingId
        {
            get;
            set;
        }

        public int LocationId
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

        public int EmptyYardId
        {
            get;
            set;
        }

        public string Containers
        {
            get;
            set;
        }

        public string BookingRef
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
            this.DeliveryOrderId = Convert.ToInt64(reader["DOId"]);
            this.BookingId = Convert.ToInt64(reader["BookingId"]);
            this.LocationName = Convert.ToString(reader["LocationName"]);
            this.NVOCCName = Convert.ToString(reader["NVOCCName"]);
            this.BookingNumber = Convert.ToString(reader["BookingNo"]);
            this.DeliveryOrderNumber = Convert.ToString(reader["DONo"]);
            this.DeliveryOrderDate = Convert.ToDateTime(reader["DODate"]);
            this.EmptyYardId = Convert.ToInt32(reader["EmptyYardId"]);
            this.Containers = Convert.ToString(reader["Containers"]);
            this.BookingRef = Convert.ToString(reader["RefBookingNo"]);
        }

        #endregion
    }
}
