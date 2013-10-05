using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IExportBLContainer
    {
         long ContainerId { get; set; }
         string HireContainerNumber { get; set; }
         long HireContainerId { get; set; }
         long BookingId { get; set; }
         long BLId { get; set; }
         int VesselId { get; set; }
         int VoyageId { get; set; }
         string ContainerSize { get; set; }
         int ContainerTypeId { get; set; }
         string ContainerType { get; set; }
         string SealNumber { get; set; }
         decimal GrossWeight { get; set; }
         decimal TareWeight { get; set; }
         int Package { get; set; }
         bool Part { get; set; }
         string ShippingBillNumber { get; set; }
         DateTime? ShippingBillDate { get; set; }
         long ImportBLFooterId { get; set; }
         bool IsDeleted { get; set; }
    }
}
