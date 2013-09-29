using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IDeliveryOrderContainer
    {
        Int32 ContainerTypeId { get; set; }
        String ContainerType { get; set; }
        String ContainerSize { get; set; }
        Int32 AvailableUnit { get; set; }
        Int32 RequiredUnit { get; set; }
    }
}
