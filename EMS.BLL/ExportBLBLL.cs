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

        public static List<IExportBLContainer> GetExportBLContainersForAdd(long BookingId)
        {
            return ExportBLDAL.GetExportBLContainersForAdd(BookingId);
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

        public static List<IExportBL> GetExportBLForListing(SearchCriteria searchCriteria)
        {
            return ExportBLDAL.GetExportBLForListing(searchCriteria);
        }

        public static DataTable GetUnitsForExportBlContainer()
        {
            return ExportBLDAL.GetUnitsForExportBlContainer();
        }
    }
}
