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
    public static class ExportBLBLL
    {
        public static IExportBL GetExportBLHeaderInfoForAdd(string BookingNumber)
        {
            return ExportBLDAL.GetExportBLHeaderInfoForAdd(BookingNumber);
        }

        public static IExportBL GetExportBLHeaderInfoForEdit(string BLNumber)
        {
            return ExportBLDAL.GetExportBLHeaderInfoForEdit(BLNumber);
        }

        public static List<IExportBLContainer> GetExportBLContainersForAdd(long BookingId, int Status)
        {
            return ExportBLDAL.GetExportBLContainersForAdd(BookingId, Status);
        }

        public static List<IExportBLContainer> GetExportBLContainersForEdit(long BLId)
        {
            return ExportBLDAL.GetExportBLContainersForEdit(BLId);
        }

        public static DataTable GetShipmentModes()
        {
            return ExportBLDAL.GetShipmentModes();
        }

        public static DataTable GetDeliveryAgents(int fk_fpod)
        {
            return ExportBLDAL.GetDeliveryAgents(fk_fpod);
        }

        public static void InsertBLContainers(List<IExportBLContainer> lstData)
        {
            lstData = lstData.Where(d => d.IsDeleted == false).ToList();
            ExportBLDAL.InsertBLContainers(lstData);
        }

        public static void UpdateBLContainers(List<IExportBLContainer> lstData)
        {
            ExportBLDAL.UpdateBLContainers(lstData);
        }

        public static long SaveExportBLHeader(IExportBL objBL)
        {
            return ExportBLDAL.SaveExportBLHeader(objBL);
        }

        public static void CloseOpenBL(long Blid, int Userid, string Action)
        {
            ExportBLDAL.CloseOpenBL(Blid, Userid, Action);
        }

        public static List<IExportBL> GetExportBLForListing(SearchCriteria searchCriteria)
        {
            return ExportBLDAL.GetExportBLForListing(searchCriteria);
        }

        public static DataTable GetUnitsForExportBlContainer()
        {
            return ExportBLDAL.GetUnitsForExportBlContainer();
        }

        public static void ChangeBLStatus(string BLNumber, bool IsActive)
        {
            ExportBLDAL.ChangeBLStatus(BLNumber, IsActive);
        }

        public static bool CheckBookingLocation(string BookingNo, int Loc)
        {
            return ExportBLDAL.CheckBookingLocation(BookingNo, Loc);
        }

        public static bool CheckBookingBLContainer(long BookingNo, int Status)
        {
            return ExportBLDAL.CheckBookingBLContainer(BookingNo, Status);
        }

        public static int CheckExpBLExistance(string BookingNo)
        {
            return ExportBLDAL.CheckExpBLExistance(BookingNo);
        }
    }
}
