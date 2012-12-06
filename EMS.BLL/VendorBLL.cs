using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.DAL;
using EMS.Common;
using EMS.Entity;
using EMS.Utilities;

namespace EMS.BLL
{
    public class VendorBLL
    {
        public int AddEditVndor(IVendor Vendor)
        {
            return VendorDAL.AddEditVndor(Vendor);
        }

        public static List<IVendor> GetVendor(SearchCriteria searchCriteria, int ID)
        {
            return VendorDAL.GetVendor(searchCriteria, ID);
        }

        public static IVendor GetVendor(int ID)
        {
            return VendorDAL.GetVendor(ID);
        }

        public static int DeleteVndor(int VendorId)
        {
            return VendorDAL.DeleteVndor(VendorId);
        }
    }
}
