using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.DAL.DbManager;
using System.Data;
using EMS.Entity;

namespace EMS.DAL
{
    public class ExportBLDAL
    {
        public static IExportBL GetExportBLHeaderInfoForAdd(string BookingNumber)
        {
            string strExecution = "[exp].[usp_GetExpBLForAdd]";
            IExportBL objHeader = new ExportBLEntity();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@BookingNumber", 50, BookingNumber);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    objHeader = new ExportBLEntity(reader);
                }

                reader.Close();
            }

            return objHeader;
        }

        public static IExportBL GetExportBLHeaderInfoForEdit(string BLNumber)
        {
            string strExecution = "[exp].[usp_GetExpBLForEdit]";
            IExportBL objHeader = new ExportBLEntity();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@BLNumber", 50, BLNumber);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    objHeader = new ExportBLEntity(reader);
                }

                reader.Close();
            }

            return objHeader;
        }

        public static List<IExportBLContainer> GetExportBLContainersForAdd(long BookingId)
        {
            string strExecution = "[exp].[usp_GetExpBLCorntainerForAdd]";
            List<IExportBLContainer> lstContainer = new List<IExportBLContainer>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@BookingId", BookingId);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IExportBLContainer container = new ExportBLContainerEntity(reader);
                    lstContainer.Add(container);
                }

                reader.Close();
            }

            return lstContainer;
        }

        public static List<IExportBLContainer> GetExportBLContainersForEdit(long BLId)
        {
            string strExecution = "[exp].[usp_GetExpBLCorntainerForEdit]";
            List<IExportBLContainer> lstContainer = new List<IExportBLContainer>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@BLId", BLId);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IExportBLContainer container = new ExportBLContainerEntity(reader);
                    lstContainer.Add(container);
                }

                reader.Close();
            }

            return lstContainer;
        }

        public static DataTable GetShipmentModes()
        {
            string ProcName = "[exp].[usp_GetShipmentModes]";
            DbQuery dquery = new DbQuery(ProcName);

            return dquery.GetTable();
        }

        public static DataTable GetDeliveryAgents()
        {
            string ProcName = "[exp].[usp_GetDeliveryAgents]";
            DbQuery dquery = new DbQuery(ProcName);

            return dquery.GetTable();
        }

        public static void InsertBLContainers(List<IExportBLContainer> lstData)
        {
            if (!ReferenceEquals(lstData, null))
            {
                foreach (IExportBLContainer container in lstData)
                {
                    string strExecution = "[exp].[usp_ExportBLContainers_Insert]";
                    long exportBLContainerId = 0;

                    using (DbQuery oDq = new DbQuery(strExecution))
                    {
                        //oDq.AddBigIntegerParam("@ContainerId", container.ContainerId);
                        oDq.AddBigIntegerParam("@BookingId", container.BookingId);
                        oDq.AddBigIntegerParam("@BLId", container.BLId);
                        oDq.AddBigIntegerParam("@ImportBLFooterId", container.ImportBLFooterId);
                        oDq.AddBigIntegerParam("@HireContainerId", container.HireContainerId);
                        oDq.AddIntegerParam("@VesselId", container.VesselId);
                        oDq.AddIntegerParam("@VoyageId", container.VoyageId);
                        oDq.AddVarcharParam ("@ContainerSize", 2, container.ContainerSize);
                        oDq.AddIntegerParam("@ContainerTypeId", container.ContainerTypeId);
                        oDq.AddVarcharParam("@SealNumber", 15, container.SealNumber);
                        oDq.AddDecimalParam("@GrossWeight", 12, 2, container.GrossWeight);
                        oDq.AddDecimalParam("@TareWeight", 12, 2, container.TareWeight);
                        oDq.AddIntegerParam("@Package", container.Package);
                        oDq.AddBooleanParam("@Part", container.Part);
                        oDq.AddVarcharParam("@ShippingBillNumber", 50, container.ShippingBillNumber);
                        oDq.AddDateTimeParam("@ShippingBillDate", container.ShippingBillDate);

                        exportBLContainerId = Convert.ToInt64(oDq.GetScalar());
                    }
                }
            }
        }

        public static void UpdateBLContainers(List<IExportBLContainer> lstData)
        {
            if (!ReferenceEquals(lstData, null))
            {
                foreach (IExportBLContainer container in lstData)
                {
                    string strExecution = "[exp].[usp_ExportBLContainers_Update]";
                    long exportBLContainerId = 0;

                    using (DbQuery oDq = new DbQuery(strExecution))
                    {
                        oDq.AddBigIntegerParam("@ContainerId", container.ContainerId);
                        oDq.AddBigIntegerParam("@BookingId", container.BookingId);
                        oDq.AddBigIntegerParam("@BLId", container.BLId);
                        oDq.AddBigIntegerParam("@ImportBLFooterId", container.ImportBLFooterId);
                        oDq.AddBigIntegerParam("@HireContainerId", container.HireContainerId);
                        oDq.AddIntegerParam("@VesselId", container.VesselId);
                        oDq.AddIntegerParam("@VoyageId", container.VoyageId);
                        oDq.AddVarcharParam("@ContainerSize", 2, container.ContainerSize);
                        oDq.AddIntegerParam("@ContainerTypeId", container.ContainerTypeId);
                        oDq.AddVarcharParam("@SealNumber", 15, container.SealNumber);
                        oDq.AddDecimalParam("@GrossWeight", 12, 2, container.GrossWeight);
                        oDq.AddDecimalParam("@TareWeight", 12, 2, container.TareWeight);
                        oDq.AddIntegerParam("@Package", container.Package);
                        oDq.AddBooleanParam("@Part", container.Part);
                        oDq.AddVarcharParam("@ShippingBillNumber", 50, container.ShippingBillNumber);
                        oDq.AddDateTimeParam("@ShippingBillDate", container.ShippingBillDate);
                        oDq.AddBooleanParam("@IsDeleted", container.IsDeleted);

                        exportBLContainerId = Convert.ToInt64(oDq.GetScalar());
                    }
                }
            }
        }

        public static long SaveExportBLHeader(IExportBL objBL)
        {
            long exportBLId = 0;
            string strExecution = "[exp].[usp_ExportBLHeader_Save]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@BLId", objBL.BLId);
                oDq.AddIntegerParam("@LocationId", objBL.LocationId);
                oDq.AddBigIntegerParam("@NvoccId", objBL.NvoccId);
                oDq.AddBigIntegerParam("@BookingId", objBL.BookingId);
                oDq.AddIntegerParam("@BLIssuePlaceId", objBL.BLIssuePlaceId);
                oDq.AddVarcharParam("@BLNumber", 60, objBL.BLNumber);
                oDq.AddDateTimeParam("@BLDate", objBL.BLDate);
                oDq.AddVarcharParam("@PORDesc", 25, objBL.PORDesc);
                oDq.AddVarcharParam("@POLDesc", 25, objBL.POLDesc);
                oDq.AddVarcharParam("@PODDesc", 25, objBL.PODDesc);
                oDq.AddVarcharParam("@FPODDesc", 25, objBL.FPODDesc);
                oDq.AddVarcharParam("@ShipperName", 100, objBL.ShipperName);
                oDq.AddVarcharParam("@Shipper", 300, objBL.Shipper);
                oDq.AddVarcharParam("@ConsigneeName", 100, objBL.ConsigneeName);
                oDq.AddVarcharParam("@Consignee", 300, objBL.Consignee);
                oDq.AddVarcharParam("@NotifyPartyName", 20, objBL.NotifyPartyName);
                oDq.AddVarcharParam("@NotifyParty", 20, objBL.NotifyParty);
                oDq.AddVarcharParam("@GoodDesc", 20, objBL.GoodDesc);
                oDq.AddVarcharParam("@MarksNumnbers", 20, objBL.MarksNumnbers);
                oDq.AddIntegerParam("@ShipmentMode", objBL.ShipmentMode);
                oDq.AddIntegerParam("@AgentId", objBL.AgentId);
                //oDq.AddVarcharParam("@RateType", 20, objBL.); // BL Status
                //oDq.AddVarcharParam("@RateType", 20, objBL.); // Export BL Status
                oDq.AddIntegerParam("@CreatedBy", objBL.CreatedBy);
                oDq.AddDateTimeParam("@CreatedOn", objBL.CreatedOn);
                oDq.AddIntegerParam("@ModifiedBy", objBL.ModifiedBy);
                oDq.AddDateTimeParam("@ModifiedOn", objBL.ModifiedOn);
                oDq.AddVarcharParam("@BLClause", 20, objBL.BLClause);
                oDq.AddVarcharParam("@BLType", 20, objBL.BLType);
                oDq.AddIntegerParam("@NoOfBL", objBL.NoOfBL);
                oDq.AddDecimalParam("@NetWeight", 12, 2, objBL.NetWeight);
                oDq.AddDateTimeParam("@BLReleaseDate", objBL.BLReleaseDate);

                exportBLId = Convert.ToInt64(oDq.GetScalar());
                return exportBLId;
            }
        }
    }
}
