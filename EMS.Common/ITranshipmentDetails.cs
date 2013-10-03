using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface ITranshipmentDetails : ICommon
    {
        Int64 ActualTranshipmentId { get; set; }
        Int64 PortId { get; set; }
        Int64 ExportBLId { get; set; }
        Int64 ImportBLFooterId { get; set; }
        Int64 HireContainerId { get; set; }
        Int64 TranshipmentId { get; set; }
        DateTime ActualArrival { get; set; }
        DateTime ActualDeparture { get; set; }

        string PortName { get; set; }
        string PortCode { get; set; }
        string ExportBLNo { get; set; }
        DateTime ExportBLDate { get; set; }
        Int64 BookingId { get; set; }
        string BookingCode { get; set; }
        Int64 VesselId { get; set; }
        string VesselName { get; set; }
        Int64 VoyageId { get; set; }
        string VoyageNo { get; set; }
        string ContainerNo { get; set; }
        string Size { get; set; }
    }
}
