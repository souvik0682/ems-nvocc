﻿using System;
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

                    if (reader["ImpLineBLDate"] != DBNull.Value) entity.BLDate = Convert.ToDateTime(reader["ImpLineBLDate"]);
                    if (reader["NoofTEU"] != DBNull.Value) entity.TEU = Convert.ToInt32(reader["NoofTEU"]);
                    if (reader["NoofFEU"] != DBNull.Value) entity.FEU = Convert.ToInt32(reader["NoofFEU"]);
                    entity.PortLoading = Convert.ToString(reader["PortLoading"]);
                    entity.PortDischarge = Convert.ToString(reader["PortDischarge"]);
                    entity.FinalDestination = Convert.ToString(reader["FinalDestination"]);
                    //entity.DischargeDate = Convert.ToString(reader["DischargeDate"]);
                    entity.IGMBLNo = Convert.ToString(reader["IGMBLNumber"]);
                    if (reader["GrossWeight"] != DBNull.Value) entity.GrossWeight = Convert.ToDecimal(reader["GrossWeight"]);
                    entity.GoodsDescription = Convert.ToString(reader["GoodDescription"]);
                    if (reader["NumberPackage"] != DBNull.Value) entity.NumberPackage = Convert.ToInt64(reader["NumberPackage"]);
                    entity.PackageUnit = Convert.ToString(reader["PackageUnit"]);
                    if (reader["RsStatus"] != DBNull.Value) entity.RsStatus = Convert.ToBoolean(reader["RsStatus"]);
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
                    if (reader["RsStatus"] != DBNull.Value) entity.RsStatus = Convert.ToBoolean(reader["RsStatus"]);
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
    }
}