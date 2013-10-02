using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IDeliveryOrder
    {
        Int64 DeliveryOrderId { get; set; }
        Int64 BookingId { get; set; }
        Int32 LocationId { get; set; }
        String LocationName { get; set; }
        Int32 NVOCCId { get; set; }
        String NVOCCName { get; set; }
        String BookingNumber { get; set; }
        String DeliveryOrderNumber { get; set; }
        DateTime DeliveryOrderDate { get; set; }
        int EmptyYardId { get; set; }
        String Containers { get; set; }
    }
}
