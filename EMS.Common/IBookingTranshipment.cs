using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IBookingTranshipment
    {
        Int32 BookingID { get; set; }
        Int32 BookingTranshipmentID { get; set; }
        int PortId { get; set; }
        DateTime ArrivalDate { get; set; }
        DateTime DepartureDate { get; set; }
        int OrderNo { get; set; }
        bool BkTransStatus { get; set; }
        string PortName { get; set; }
    }
}
