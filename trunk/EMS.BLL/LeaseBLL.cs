using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.DAL;
using EMS.Common;
using EMS.Entity;
using EMS.Utilities;
using System.Data;

namespace EMS.BLL
{
    public class LeaseBLL
    {
        public static List<ILease> GetLease(SearchCriteria searchCriteria, int ID)
        {
            return LeaseDAL.GetLease(searchCriteria, ID);
        }

        public static ILease GetLease(int ID)
        {
            return LeaseDAL.GetLease(ID);
        }

        public int AddEditLease(ILease Lease, int CompanyID, ref int BookingId)
        {
            return LeaseDAL.AddEditLease(Lease, CompanyID, ref BookingId);
        }

        public void DeactivateAllContainersAgainstLeaseId(int LeaseId)
        {
            LeaseDAL.DeactivateAllContainersAgainstLeaseId(LeaseId);
        }

        public int AddEditLeaseContainer(IBookingContainer Containers)
        {
            return LeaseDAL.AddEditLeaseContainer(Containers);
        }

        public List<IBookingContainer> GetLeaseContainers(int ChargesID)
        {
            return LeaseDAL.GetLeaseContainers(ChargesID);
        }

        public int CheckForDuplicateLease(string LeaseNo)
        {
            return LeaseDAL.CheckForDuplicateLease(LeaseNo);
        }

        public static int DeleteLease(int LeaseId)
        {
            return LeaseDAL.DeleteLease(LeaseId);
        }
    }
}
