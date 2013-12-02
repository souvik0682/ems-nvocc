using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.DAL;
using EMS.Entity.Report;
using System.Data;

namespace EMS.BLL
{
    public class ReportBLL
    {
        public static List<ImpBLChkLstEntity> GetImportBLCheckList(int lineId, int locId, Int64 voyageId, Int64 vesselId)
        {
            return ReportDAL.GetImportBLCheckList(lineId, locId, voyageId, vesselId);
        }

        public static List<ImpRegisterEntity> GetImportRegisterHeader(int lineId, int locId, Int64 voyageId, Int64 vesselId)
        {
            return ReportDAL.GetImportRegisterHeader(lineId, locId, voyageId, vesselId);
        }

        public static List<ImpRegisterEntity> GetImportRegisterFooter(int lineId, int locId, Int64 voyageId, Int64 vesselId)
        {
            return ReportDAL.GetImportRegisterFooter(lineId, locId, voyageId, vesselId);
        }

        public static List<ImpRegisterSummaryEntity> GetImportRegisterSummary(int lineId, int locId, DateTime dischargeFrom, DateTime dischargeTo)
        {
            return ReportDAL.GetImportRegisterSummary(lineId, locId, dischargeFrom, dischargeTo);
        }

        public static List<ImpInvRegisterEntity> GetImportInvoiceRegister(int lineId, int locId, int billType, DateTime dtFrom, DateTime dtTo)
        {
            return ReportDAL.GetImportInvoiceRegister(lineId, locId, billType, dtFrom, dtTo);
        }

        //public static List<ImportInvoiceEntity> GetImportInvoicePrint(int lineId, int locId, int billType, DateTime dtFrom, DateTime dtTo)
        //{
        //    return ReportDAL.GetImportInvoicePrint(lineId, locId, billType, dtFrom, dtTo);
        //}

        public DataTable GetTypeWiseStockSummary(string LineId, string LocationId, DateTime StockDate)
        {
            return ReportDAL.GetTypeWiseStockSummary(LineId, LocationId, StockDate);
        }

        public DataTable GetDetentionReport(string vord, DateTime StartDate, DateTime EndDate, string VoyageID, string VesselID, string LineId, string LocationId)
        {
            return ReportDAL.GetDetentionReport(vord, StartDate, EndDate, VoyageID, VesselID, LineId, LocationId);
        }

        public DataTable GetMonthlyImportStatement(DateTime StartDate, DateTime EndDate, string LineId, string LocationId)
        {
            return ReportDAL.GetMonthlyImportStatement(StartDate, EndDate, LineId, LocationId);
        }

        public DataTable GetContainerStockDetail(string Line, string Loc, string Stat, string CntrType, string StockDate, int EmptyYard)
        {
            return ReportDAL.GetContainerStockDetail(Line, Loc, Stat, CntrType, StockDate, EmptyYard);
        }

        public static DataSet GetBooking(string Initial)
        {
            return ReportDAL.GetBooking(Initial);
        }

        #region Manifest
        public static DataSet GetRptFrieghtManifest_TFS(string blNo) { return ReportDAL.GetRptFrieghtManifest_TFS(blNo); }
        public static DataSet GetRptFrieghtManifest(string blNo) { return ReportDAL.GetRptFrieghtManifest(blNo); }
        public static DataSet GetRptFrieghtManifest_CTRS(string blNo) { return ReportDAL.GetRptFrieghtManifest_CTRS(blNo); }
        #endregion

        #region Delivery Order Print

        public static List<DOPrintEntity> GetDeliveryOrder(Int64 doId)
        {
            return ReportDAL.GetDeliveryOrder(doId);
        }

        public static List<DOPrintEntity> GetDeliveryOrderContainer(Int64 doId)
        {
            return ReportDAL.GetDeliveryOrderContainer(doId);
        }

        #endregion

        #region Export EDI

        public static List<ExportEDIEntity> GetExportEdi(Int64 vesselId, Int64 voyageId, Int32 polId, Int32 locId)
        {
            return ReportDAL.GetExportEdiHeader(vesselId, voyageId, polId, locId);
        }

        public static List<ExportEDIEntity> GetExportEdiCntr(Int64 vesselId, Int64 voyageId, Int32 polId, Int32 locId)
        {
            return ReportDAL.GetExportEdi(vesselId, voyageId, polId, locId);
        }

        public static DataSet GetBlNumberFromVoyageID(Int32 Voyage, Int32 Vessel, Int32 POD)
        {
            return ReportDAL.GetBlNumberFromVoyageID(Voyage, Vessel, POD);
        }
        
        public DataSet GetPOD(Int32 VesselID, Int32 VoyageID)
        {
            return ReportDAL.GetPOD(VesselID, VoyageID);
        }
        #endregion
    }
}
