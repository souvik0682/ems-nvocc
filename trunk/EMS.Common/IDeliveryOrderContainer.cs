using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IDeliveryOrderContainer
    {
        Int64 BookingContainerId { get; set; }
        Int32 ContainerTypeId { get; set; }
        String ContainerType { get; set; }
        String ContainerSize { get; set; }
        int BookingUnit { get; set; }
        int AvailableUnit { get; set; }
        int RequiredUnit { get; set; }
    }
}
