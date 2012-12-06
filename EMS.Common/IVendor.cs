using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IVendor : ICommon
    {
        int VendorId { get; set; }
        string VendorType { get; set; }
        int LocationID { get; set; }
        string LocationName { get; set; }
        int VendorSalutation { get; set; }
        string VendorName { get; set; }
        string VendorAddress { get; set; }
        string CFSCode { get; set; }
        int Terminalid { get; set; }
        int CompanyID { get; set; }
        bool VendorActive { get; set; }
    }
}
