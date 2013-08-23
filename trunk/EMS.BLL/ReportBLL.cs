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

        public static List<ImportInvoiceEntity> GetImportInvoicePrint(int lineId, int locId, int billType, DateTime dtFrom, DateTime dtTo)
        {
            return ReportDAL.GetImportInvoicePrint(lineId, locId, billType, dtFrom, dtTo);
        }

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

        public DataTable GetContainerStockDetail(string Line, string Loc, string Stat, string CntrType, DateTime StockDate)
        {
            return ReportDAL.GetContainerStockDetail(Line, Loc, Stat, CntrType, StockDate);
        }
    }
}
