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
        string AcNo { get; set; }
        string AcType { get; set; }
        string PAN { get; set; }
        string TANo { get; set; }
        string BIN { get; set; }
        string Mobile { get; set; }
        string EmailID { get; set; }
        string BankName { get; set; }
        string IEC { get; set; }
        string CP { get; set; }

    }
}
