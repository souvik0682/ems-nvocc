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
            string strExecution = "[report].[uspGetImportRegisterSummary]";
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

                    lstEntity.Add(entity);
                }
            }


            return lstEntity;
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
