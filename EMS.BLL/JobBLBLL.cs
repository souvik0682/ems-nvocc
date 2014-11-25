using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.DAL;
using System.Data;
using EMS.Entity;

namespace EMS.BLL
{
    public static class JobBLBLL
    {
        public static IJobBL GetExportBLHeaderInfoForAdd(string BookingNumber)
        {
            return JobBLDAL.GetExportBLHeaderInfoForAdd(BookingNumber);
        }

        public static IJobBL GetExportBLHeaderInfoForEdit(string BLNumber)
        {
            return JobBLDAL.GetExportBLHeaderInfoForEdit(BLNumber);
        }

        public static List<IJobBLContainer> GetExportBLContainersForAdd(long BookingId, int Status)
        {
            return JobBLDAL.GetExportBLContainersForAdd(BookingId, Status);
        }

        public static List<IJobBLContainer> GetExportBLContainersForEdit(long BLId)
        {
            return JobBLDAL.GetExportBLContainersForEdit(BLId);
        }

        public static DataTable GetShipmentModes()
        {
            return JobBLDAL.GetShipmentModes();
        }

        public static DataTable GetDeliveryAgents(int fk_fpod)
        {
            return JobBLDAL.GetDeliveryAgents(fk_fpod);
        }

        public static void InsertBLContainers(List<IJobBLContainer> lstData)
        {
            lstData = lstData.Where(d => d.IsDeleted == false).ToList();
            JobBLDAL.InsertBLContainers(lstData);
        }

        public static void UpdateBLContainers(List<IJobBLContainer> lstData)
        {
            JobBLDAL.UpdateBLContainers(lstData);
        }

        public static long SaveExportBLHeader(IJobBL objBL)
        {
            return JobBLDAL.SaveExportBLHeader(objBL);
        }

        public static void CloseOpenBL(long Blid, int Userid, string Action)
        {
            JobBLDAL.CloseOpenBL(Blid, Userid, Action);
        }

        public static List<IJobBL> GetExportBLForListing(SearchCriteria searchCriteria)
        {
            return JobBLDAL.GetExportBLForListing(searchCriteria);
        }

        public static DataTable GetUnitsForExportBlContainer()
        {
            return JobBLDAL.GetUnitsForExportBlContainer();
        }

        public static DataTable GetContainerType()
        {
            return JobBLDAL.GetContainerType();
        }

        public static string GetTareWeight(int ContainerTypeId, decimal Size)
        {
            return JobBLDAL.GetTareWeight(ContainerTypeId, Size);
        }

        public static void ChangeBLStatus(string BLNumber, bool IsActive)
        {
            JobBLDAL.ChangeBLStatus(BLNumber, IsActive);
        }

        public static bool CheckBookingLocation(string BookingNo, int Loc)
        {
            return JobBLDAL.CheckBookingLocation(BookingNo, Loc);
        }

        public static bool CheckBookingBLContainer(long BookingNo, int Status)
        {
            return JobBLDAL.CheckBookingBLContainer(BookingNo, Status);
        }

        public static int CheckExpBLExistance(string BookingNo)
        {
            return JobBLDAL.CheckExpBLExistance(BookingNo);
        }
    }
}
