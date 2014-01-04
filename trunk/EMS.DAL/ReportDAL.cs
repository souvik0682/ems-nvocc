using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EMS.DAL.DbManager;
using EMS.Entity.Report;

namespace EMS.DAL
{
    public sealed class ReportDAL
    {
        #region Public Methods

        public static List<ImpBLChkLstEntity> GetImportBLCheckList(int lineId, int locId, Int64 voyageId, Int64 vesselId)
        {
            string strExecution = "[report].[uspGetImportBLCheckList]";
            List<ImpBLChkLstEntity> lstEntity = new List<ImpBLChkLstEntity>();
            ImpBLChkLstEntity entity = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@NVOCCId", lineId);
                oDq.AddIntegerParam("@LocId", locId);
                oDq.AddBigIntegerParam("@VoyageId", voyageId);
                oDq.AddBigIntegerParam("@VesselId", vesselId);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    entity = new ImpBLChkLstEntity(reader);
                    lstEntity.Add(entity);
                }
            }


            return lstEntity;
        }

        public static List<ImpRegisterEntity> GetImportRegisterHeader(int lineId, int locId, Int64 voyageId, Int64 vesselId)
        {
            string strExecution = "[report].[uspGetImportRegisterHeader]";
            List<ImpRegisterEntity> lstEntity = new List<ImpRegisterEntity>();
            ImpRegisterEntity entity = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@NVOCCId", lineId);
                oDq.AddIntegerParam("@LocId", locId);
                oDq.AddBigIntegerParam("@VoyageId", voyageId);
                oDq.AddBigIntegerParam("@VesselId", vesselId);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    entity = new ImpRegisterEntity();

                    entity.Location = Convert.ToString(reader["Location"]);
                    entity.Line = Convert.ToString(reader["Line"]);
                    entity.ItemLineNo = Convert.ToString(reader["ItemLineNo"]);
                    entity.BLNo = Convert.ToString(reader["ImpLineBLNo"]);
                    entity.BLDate = Convert.ToDateTime(reader["ImpLineBLDate"]);

                    //if (reader["ImpLineBLDate"] != DBNull.Value) entity.BLDate = Convert.ToDateTime(reader["ImpLineBLDate"]);
                    if (reader["NoofTEU"] != DBNull.Value) entity.TEU = Convert.ToInt32(reader["NoofTEU"]);
                    if (reader["NoofFEU"] != DBNull.Value) entity.FEU = Convert.ToInt32(reader["NoofFEU"]);
                    entity.PortLoading = Convert.ToString(reader["PortLoading"]);
                    entity.PortDischarge = Convert.ToString(reader["PortDischarge"]);
                    entity.FinalDestination = Convert.ToString(reader["FinalDestination"]);
                    if (reader["DischargeDate"] != DBNull.Value) entity.DischargeDate = Convert.ToDateTime(reader["DischargeDate"]);
                    //entity.DischargeDate = Convert.ToString(reader["DischargeDate"]);
                    entity.IGMNo = Convert.ToString(reader["IGMNo"]);
                    if (reader["GrossWeight"] != DBNull.Value) entity.GrossWeight = Convert.ToDecimal(reader["GrossWeight"]);
                    entity.GoodsDescription = Convert.ToString(reader["GoodDescription"]);
                    if (reader["NumberPackage"] != DBNull.Value) entity.NumberPackage = Convert.ToInt64(reader["NumberPackage"]);
                    entity.PackageUnit = Convert.ToString(reader["PackageUnit"]);
                    if (reader["RsStatus"] != DBNull.Value) entity.Stat = Convert.ToString(reader["RsStatus"]);
                    if (reader["PGR_FreeDays"] != DBNull.Value) entity.PGRFreeDays = Convert.ToInt32(reader["PGR_FreeDays"]);
                    entity.AddressCFS = Convert.ToString(reader["AddressCFS"]);
                    //entity.ICD = Convert.ToString(reader["ICD"]);
                    entity.TPBondNo = Convert.ToString(reader["TPBondNo"]);
                    entity.CACode = Convert.ToString(reader["CACode"]);
                    entity.CargoMovement = Convert.ToString(reader["CargoMovement"]);
                    entity.ConsigneeInformation = Convert.ToString(reader["ConsigneeInformation"]);
                    entity.NotifyPartyInformation = Convert.ToString(reader["NotifyPartyInformation"]);
                    entity.MarksNumbers = Convert.ToString(reader["MarksNumbers"]);

                    lstEntity.Add(entity);
                }
            }


            return lstEntity;
        }

        public static List<ImpRegisterEntity> GetImportRegisterFooter(int lineId, int locId, Int64 voyageId, Int64 vesselId)
        {
            string strExecution = "[report].[uspGetImportRegisterFooter]";
            List<ImpRegisterEntity> lstEntity = new List<ImpRegisterEntity>();
            ImpRegisterEntity entity = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@NVOCCId", lineId);
                oDq.AddIntegerParam("@LocId", locId);
                oDq.AddBigIntegerParam("@VoyageId", voyageId);
                oDq.AddBigIntegerParam("@VesselId", vesselId);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    entity = new ImpRegisterEntity();

                    entity.Location = Convert.ToString(reader["Location"]);
                    entity.Line = Convert.ToString(reader["Line"]);
                    entity.BLNo = Convert.ToString(reader["ImpLineBLNo"]);
                    entity.ContainerNo = Convert.ToString(reader["CntrNo"]);
                    entity.ContainerSize = Convert.ToString(reader["CntrSize"]);
                    entity.ContainerType = Convert.ToString(reader["ContainerType"]);
                    entity.SealNo = Convert.ToString(reader["SealNo"]);
                    if (reader["PayLoad"] != DBNull.Value) entity.PayLoad = Convert.ToDecimal(reader["PayLoad"]);
                    if (reader["NumberPackage"] != DBNull.Value) entity.NumberPackage = Convert.ToInt64(reader["NumberPackage"]);
                    entity.PackageUnit = Convert.ToString(reader["PackageUnit"]);
                    if (reader["Stat"] != DBNull.Value) entity.Stat = Convert.ToString(reader["Stat"]);
                    entity.GoodsDescription = Convert.ToString(reader["GoodDescription"]);
                    entity.PortLoading = Convert.ToString(reader["PortLoading"]);
                    entity.PortDischarge = Convert.ToString(reader["PortDischarge"]);
                    entity.FinalDestination = Convert.ToString(reader["FinalDestination"]);

                    lstEntity.Add(entity);
                }
            }


            return lstEntity;
        }

        public static List<ImpRegisterSummaryEntity> GetImportRegisterSummary(int lineId, int locId, DateTime dischargeFrom, DateTime dischargeTo)
        {
            string strExecution = "[report].[uspGetImportSummary]";
            List<ImpRegisterSummaryEntity> lstEntity = new List<ImpRegisterSummaryEntity>();
            ImpRegisterSummaryEntity entity = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@NVOCCId", lineId);
                oDq.AddIntegerParam("@LocId", locId);
                oDq.AddDateTimeParam("@DischargeFrom", dischargeFrom);
                oDq.AddDateTimeParam("@DischargeTo", dischargeTo);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    entity = new ImpRegisterSummaryEntity();

                    entity.Location = Convert.ToString(reader["Location"]);
                    entity.Line = Convert.ToString(reader["Line"]);
                    entity.BLNo = Convert.ToString(reader["ImpLineBLNo"]);
                    entity.VesselName = Convert.ToString(reader["VesselName"]);
                    entity.VoyageNo = Convert.ToString(reader["VoyageNo"]);
                    entity.DischargeDate = Convert.ToDateTime(reader["DischargeDate"]);
                    entity.BLNo = Convert.ToString(reader["BLNo"]);
                    entity.TEU = Convert.ToInt32(reader["TEU"]);
                    entity.FEU = Convert.ToInt32(reader["FEU"]);
                    entity.TotalTEU = Convert.ToInt32(reader["TotalTEU"]);
                    entity.InvoiceReference = Convert.ToString(reader["InvoiceReference"]);
                    if (reader["DO"] != DBNull.Value) entity.DO = Convert.ToDecimal(reader["DO"]);
                    if (reader["Docs"] != DBNull.Value) entity.Docs = Convert.ToDecimal(reader["Docs"]);
                    if (reader["Survey"] != DBNull.Value) entity.Survey = Convert.ToDecimal(reader["Survey"]);
                    if (reader["Washing"] != DBNull.Value) entity.Washing = Convert.ToDecimal(reader["Washing"]);
                    if (reader["Others"] != DBNull.Value) entity.Others = Convert.ToDecimal(reader["Others"]);
                    entity.SecurityInvoice = Convert.ToString(reader["SecurityInvoice"]);
                    if (reader["SecurityDeposit"] != DBNull.Value) entity.SecurityDeposit = Convert.ToDecimal(reader["SecurityDeposit"]);
                    entity.LastInvoice = Convert.ToString(reader["LastInvoice"]);
                    if (reader["DetentionUSD"] != DBNull.Value) entity.DetentionUSD = Convert.ToDecimal(reader["DetentionUSD"]);
                    if (reader["DetentionINR"] != DBNull.Value) entity.DetentionINR = Convert.ToDecimal(reader["DetentionINR"]);
                    if (reader["Repairing"] != DBNull.Value) entity.Repairing = Convert.ToDecimal(reader["Repairing"]);
                    if (reader["LastInvoiceOtherCharge"] != DBNull.Value) entity.LastInvoiceOtherCharge = Convert.ToDecimal(reader["LastInvoiceOtherCharge"]);
                    if (reader["TotalCharge"] != DBNull.Value) entity.TotalCharge = Convert.ToDecimal(reader["TotalCharge"]);
                    if (reader["TotalReceipt"] != DBNull.Value) entity.TotalReceipt = Convert.ToDecimal(reader["TotalReceipt"]);
                    if (reader["Refund"] != DBNull.Value) entity.Refund = Convert.ToDecimal(reader["Refund"]);
                    entity.Remarks = Convert.ToString(reader["Remarks"]);

                    lstEntity.Add(entity);
                }
            }


            return lstEntity;
        }

        public static DataSet GetRptCargoDesc(int vesselId, int voyageId)
        {
            DataSet ds = new DataSet();
            using (DbQuery dq = new DbQuery("prcRptCargoDesc"))
            {
                dq.AddIntegerParam("@vesselId", vesselId);
                dq.AddIntegerParam("@voyageId", voyageId);
                ds = dq.GetTables();
            }
            return ds;
        }

        public static List<ImpInvRegisterEntity> GetImportInvoiceRegister(int lineId, int locId, int billType, DateTime dtFrom, DateTime dtTo)
        {
            string strExecution = "[report].[uspGetImportInvoiceRegister]";
            List<ImpInvRegisterEntity> lstEntity = new List<ImpInvRegisterEntity>();
            ImpInvRegisterEntity entity = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@LocationID", locId);
                oDq.AddIntegerParam("@LineID", lineId);
                oDq.AddIntegerParam("@BillType", billType);
                oDq.AddDateTimeParam("@StartDate", dtFrom);
                oDq.AddDateTimeParam("@EndDate", dtTo);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    entity = new ImpInvRegisterEntity(reader);
                    lstEntity.Add(entity);
                }
            }


            return lstEntity;
        }

        public static List<ImpInvRegisterEntity> GetExportInvoiceRegister(int lineId, int locId, int billType, DateTime dtFrom, DateTime dtTo)
        {
            string strExecution = "[exp].[uspGetExportInvoiceRegister]";
            List<ImpInvRegisterEntity> lstEntity = new List<ImpInvRegisterEntity>();
            ImpInvRegisterEntity entity = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@LocationID", locId);
                oDq.AddIntegerParam("@LineID", lineId);
                oDq.AddIntegerParam("@BillType", billType);
                oDq.AddDateTimeParam("@StartDate", dtFrom);
                oDq.AddDateTimeParam("@EndDate", dtTo);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    entity = new ImpInvRegisterEntity(reader);
                    lstEntity.Add(entity);
                }
            }


            return lstEntity;
        }

        //public static List<ImportInvoiceEntity> GetImportInvoicePrint(int lineId, int locId, int billType, DateTime dtFrom, DateTime dtTo)
        //{
        //    string strExecution = "[report].[uspRptInvoiceDateRange]";
        //    List<ImportInvoiceEntity> lstEntity = new List<ImportInvoiceEntity>();
        //    ImportInvoiceEntity entity = null;

        //    using (DbQuery oDq = new DbQuery(strExecution))
        //    {
        //        oDq.AddIntegerParam("@LocationID", locId);
        //        oDq.AddIntegerParam("@LineID", lineId);
        //        oDq.AddIntegerParam("@BillType", billType);
        //        oDq.AddDateTimeParam("@StartDate", dtFrom);
        //        oDq.AddDateTimeParam("@EndDate", dtTo);

        //        DataTableReader reader = oDq.GetTableReader();

        //        while (reader.Read())
        //        {
        //            entity = new ImportInvoiceEntity(reader);
        //            lstEntity.Add(entity);
        //        }
        //    }


        //    return lstEntity;
        //}

        public static DataTable GetTypeWiseStockSummary(string LineId, string LocationId, DateTime StockDate)
        {
            string strExecution = "prcRptTypeWiseStockSummary";
            DataTable dt = new DataTable();


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@Line", 100, LineId);
                oDq.AddVarcharParam("@Loc", 100, LocationId);
                oDq.AddDateTimeParam("@StockDate", StockDate);
                dt = oDq.GetTable();
            }

            return dt;
        }

        public static DataTable GetContainerStockDetail(string Line, string Loc, string Stat, string CntrType, string StockDate, int EmptyYard)
        {

            DataTable dt = new DataTable();
            DateTime dt1 = string.IsNullOrEmpty(StockDate) ? DateTime.Now : Convert.ToDateTime(StockDate);
            string strExecution = "prcRptStockDetail";
            //DateTime dt1 = string.IsNullOrEmpty(StockDate) ? DateTime.Now : Convert.ToDateTime(StockDate);
            //DataSet ds = new DataSet();
            using (DbQuery oDq = new DbQuery(strExecution))

            //using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@Line", 80, Line);
                oDq.AddVarcharParam("@Loc", 100, Loc);
                oDq.AddVarcharParam("@Stat", 100, Stat);
                oDq.AddVarcharParam("@CntrType", 100, CntrType);
                oDq.AddDateTimeParam("@StockDate", dt1);
                oDq.AddIntegerParam("@EmptyYard", EmptyYard);
                dt = oDq.GetTable();
            }

            return dt;
        }



        public static DataTable GetDetentionReport(string vord, DateTime StartDate, DateTime EndDate, string VoyageID, string VesselID, string LineId, string LocationId)
        {
            string strExecution = "uspRptDetention";
            DataTable dt = new DataTable();


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@VoyageOrDate", 1, vord);
                oDq.AddDateTimeParam("@StartDate", StartDate);
                oDq.AddDateTimeParam("@EndDate", EndDate);
                oDq.AddVarcharParam("@fk_VoyageID", 60, VoyageID);
                oDq.AddVarcharParam("@fk_VesselID", 60, VesselID);
                oDq.AddVarcharParam("@fk_LineID", 60, LineId);
                oDq.AddVarcharParam("@fk_LocationID", 60, LocationId);
                dt = oDq.GetTable();
            }

            return dt;
        }

        public static DataTable GetMonthlyImportStatement(DateTime StartDate, DateTime EndDate, string LineId, string LocationId)
        {
            string strExecution = "prcRptMonthlyImport";
            DataTable dt = new DataTable();


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddDateTimeParam("@StartDate", StartDate);
                oDq.AddDateTimeParam("@EndDate", EndDate);
                oDq.AddVarcharParam("@fk_LineID", 60, LineId);
                oDq.AddVarcharParam("@fk_LocationID", 60, LocationId);
                dt = oDq.GetTable();
            }

            return dt;
        }

        #endregion

        #region Private Methods

        private static bool HasColumn(DataTableReader reader, string columnName)
        {
            try
            {
                return reader.GetOrdinal(columnName) >= 0;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        #endregion

        #region Manifest
        public static DataSet GetRptFrieghtManifest_TFS(string blNo)
        {
            DataSet ds = new DataSet();
            using (DbQuery dq = new DbQuery("[exp].[rptFrieghtManifest_TFS]"))
            {
                dq.AddVarcharParam("@EXPBLID", 60, blNo);
                ds = dq.GetTables();
            }
            return ds;
        }
        public static DataSet GetRptFrieghtManifest_CTRS(string blNo)
        {
            DataSet ds = new DataSet();
            using (DbQuery dq = new DbQuery("[exp].[rptFrieghtManifest_CTRS]"))
            {
                dq.AddVarcharParam("@EXPBLID", 60, blNo);
                ds = dq.GetTables();
            }
            return ds;
        }
        public static DataSet GetRptFrieghtManifest(string blNo)
        {
            DataSet ds = new DataSet();
            using (DbQuery dq = new DbQuery("[exp].[rptFrieghtManifest]"))
            {
                dq.AddVarcharParam("@EXPBLID", 60, blNo);
                ds = dq.GetTables();
            }
            return ds;
        }
        #endregion

        public static DataSet GetBooking(string Initial)
        {

            string ProcName = "[exp].[prcGetBooking]";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            dquery.AddVarcharParam("@initial", 250, Initial);
            return dquery.GetTables();

        }

        #region Delivery Order Print

        public static List<DOPrintEntity> GetDeliveryOrder(Int64 doId)
        {
            string strExecution = "[exp].[uspRptDO]";
            List<DOPrintEntity> lstDO = new List<DOPrintEntity>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@DOID", doId);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    DOPrintEntity container = new DOPrintEntity(reader);
                    lstDO.Add(container);
                }

                reader.Close();
            }

            return lstDO;
        }

        public static List<DOPrintEntity> GetDeliveryOrderContainer(Int64 doId)
        {
            string strExecution = "[exp].[uspRptDOContainer]";
            List<DOPrintEntity> lstCntr = new List<DOPrintEntity>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@DOID", doId);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    DOPrintEntity container = new DOPrintEntity();
                    container.CntrNos = Convert.ToInt32(reader["CntrNos"]);
                    container.CntrType = Convert.ToString(reader["CntrType"]);
                    container.CntrSize = Convert.ToString(reader["CntrSize"]);
                    lstCntr.Add(container);
                }

                reader.Close();
            }

            return lstCntr;
        }


        #endregion

        #region Export EDI

        public static List<ExportEDIEntity> GetExportEdiHeader(Int64 vesselId, Int64 voyageId, Int32 polId, Int32 locId)
        {
            string strExecution = "[exp].[rptExportEDI]";
            List<ExportEDIEntity> lstDO = new List<ExportEDIEntity>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@VesselID", vesselId);
                oDq.AddBigIntegerParam("@VoyageID", voyageId);
                oDq.AddBigIntegerParam("@POLID", polId);
                oDq.AddBigIntegerParam("@LocationID", locId);
                oDq.AddBigIntegerParam("@cf", 1);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    ExportEDIEntity shpbl = new ExportEDIEntity(reader, true);
                    lstDO.Add(shpbl);
                }

                reader.Close();
            }

            return lstDO;
        }

        public static List<ExportEDIEntity> GetExportEdi(Int64 vesselId, Int64 voyageId, Int32 polId, Int32 locId)
        {
            string strExecution = "[exp].[rptExportEDI]";
            List<ExportEDIEntity> lstDO = new List<ExportEDIEntity>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@VesselID", vesselId);
                oDq.AddBigIntegerParam("@VoyageID", voyageId);
                oDq.AddBigIntegerParam("@POLID", polId);
                oDq.AddBigIntegerParam("@LocationID", locId);
                oDq.AddBigIntegerParam("@cf", 2);
                DataTableReader reader = oDq.GetTableReader();

                //while (reader.Read())
                //{
                //    //Do nothing
                //}

                //reader.NextResult();

                while (reader.Read())
                {
                    ExportEDIEntity container = new ExportEDIEntity(reader, false);
                    lstDO.Add(container);
                }

                reader.Close();
            }

            return lstDO;
        }
        #endregion
        #region Export Manifest
        public static DataSet GetBlNumberFromVoyageID(Int32 VoyageID, Int32 VesselID, Int32 POD, Int32 LineID, int LocID)
        {

            DataSet ds = new DataSet();
            using (DbQuery dq = new DbQuery("[exp].[uspGetBlNumberFromVoyageID]"))
            {
                dq.AddBigIntegerParam("@VoyageID", VoyageID);
                dq.AddBigIntegerParam("@VesselID", VesselID);
                dq.AddBigIntegerParam("@fk_POD", POD);
                dq.AddBigIntegerParam("@LineID", LineID);
                dq.AddBigIntegerParam("@LocID", LocID);

                ds = dq.GetTables();
            }
            return ds;


            //string strExecution = "prcRptTypeWiseStockSummary";
            //DataTable dt = new DataTable();


            //using (DbQuery oDq = new DbQuery(strExecution))
            //{
            //    oDq.AddBigIntegerParam("@VoyageNo", VoyageID);
            //    dt = oDq.GetTable();
            //}

            //return dt;
        }
 

        public static DataSet GetPOD(Int32 VesselID, Int32 VoyageID, Int32 LineID)
        {
            string ProcName = "[exp].[PODWithVesselVoyage]";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);

            dquery.AddIntegerParam("@VesselId", VesselID);
            dquery.AddIntegerParam("@VoyageId", VoyageID);
            dquery.AddIntegerParam("@LineId", LineID);

            return dquery.GetTables();
        }
        #endregion
    }
}
