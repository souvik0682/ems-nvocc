using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IBookingContainer
    {
        Int32 BookingID { get; set; }
        Int32 BookingContainerID { get; set; }
        int SlNo { get; set; }
        int ContainerTypeID { get; set; }
        string CntrSize { get; set; }
        int NoofContainers { get; set; }
        decimal wtPerCntr { get; set; }
        bool BkCntrStatus { get; set; }
        string ContainerType { get; set; }

    }
}
