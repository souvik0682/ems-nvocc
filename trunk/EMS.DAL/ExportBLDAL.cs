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

        public static List<IExportBLContainer> GetExportBLContainersForAdd(long BookingId, int Status)
        {
            string strExecution = "[exp].[usp_GetExpBLContainerForAdd]";
            List<IExportBLContainer> lstContainer = new List<IExportBLContainer>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@BookingId", BookingId);
                oDq.AddIntegerParam("@Status", Status);

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

        public static DataTable GetDeliveryAgents(int fk_FPOD)
        {
            string ProcName = "[exp].[usp_GetDeliveryAgents]";
            DbQuery dquery = new DbQuery(ProcName);
            dquery.AddIntegerParam("@FPOD", fk_FPOD);
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
                        oDq.AddVarcharParam("@UnitId", 10, container.Unit);

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
                        oDq.AddVarcharParam("@UnitId", 10, container.Unit);

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
                oDq.AddVarcharParam("@NotifyPartyName", 100, objBL.NotifyPartyName);
                oDq.AddVarcharParam("@NotifyParty", 300, objBL.NotifyParty);
                oDq.AddVarcharParam("@GoodDesc", 1000, objBL.GoodDesc);
                oDq.AddVarcharParam("@MarksNumnbers", 300, objBL.MarksNumnbers);
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
                oDq.AddDecimalParam("@NetWeight", 12, 3, objBL.NetWeight);
                oDq.AddDateTimeParam("@BLReleaseDate", objBL.BLReleaseDate);
                oDq.AddBooleanParam("@BLThruEdge", objBL.BLthruEdge);

                exportBLId = Convert.ToInt64(oDq.GetScalar());
                return exportBLId;
            }
        }

        public static List<IExportBL> GetExportBLForListing(SearchCriteria searchCriteria)
        {
            string strExecution = "[exp].[usp_GetExportBLForListing]";
            List<IExportBL> lstBL = new List<IExportBL>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@BookingNumber", 100, searchCriteria.BookingNo);
                oDq.AddVarcharParam("@EdgeBLNo", 100, searchCriteria.EdgeBLNumber);
                oDq.AddVarcharParam("@RefBLNo", 100, searchCriteria.RefBLNumber);
                oDq.AddVarcharParam("@POL", 100, searchCriteria.POL);
                oDq.AddVarcharParam("@Line", 100, searchCriteria.LineName);
                oDq.AddVarcharParam("@Location", 100, searchCriteria.Location);
                oDq.AddVarcharParam("@SortExpression", 100, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 100, searchCriteria.SortDirection);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IExportBL bl = new ExportBLEntity(reader);
                    lstBL.Add(bl);
                }

                reader.Close();
            }

            return lstBL;
        }

        public static DataTable GetUnitsForExportBlContainer()
        {
            string ProcName = "[exp].[usp_GetUnitsForExportBlContainer]";
            DbQuery dquery = new DbQuery(ProcName);

            return dquery.GetTable();
        }

        public static void ChangeBLStatus(string BLNumber, bool IsActive)
        {
            string strExecution = "[exp].[usp_ChangeBLStatus]";
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@BLNumber", 50, BLNumber);
                oDq.AddBooleanParam("@IsActive", IsActive);
                oDq.RunActionQuery();
            }
        }

        public static bool CheckBookingLocation(string BookingNo, int Loc)
        {
            string strExecution = "[exp].[usp_CheckBookingLocation]";


            using (DbQuery oDq = new DbQuery(strExecution))
            {

                oDq.AddNVarcharParam("@BookingNo", 50, BookingNo);
                oDq.AddIntegerParam("@Loc", Loc);
                oDq.AddBooleanParam("@Result", false, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                return Convert.ToBoolean(oDq.GetParaValue("@Result"));
            }
        }

        public static int CheckExpBLExistance(string BookingNo)
        {
            string strExecution = "[exp].[usp_CheckExpBLExistance]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddNVarcharParam("@BookingNo", 50, BookingNo);
                oDq.AddIntegerParam("@Result", 0, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                return Convert.ToInt32(oDq.GetParaValue("@Result"));
            }
        }

    }
}
