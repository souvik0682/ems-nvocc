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
    public class JobBLDAL
    {
        public static IJobBL GetExportBLHeaderInfoForAdd(string JobNo)
        {
            string strExecution = "[fwd].[usp_GetJobBLForAdd]";
            IJobBL objHeader = new JobBLEntity();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@JobNo", 50, JobNo);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    objHeader = new JobBLEntity(reader);
                }

                reader.Close();
            }

            return objHeader;
        }

        public static IJobBL GetExportBLHeaderInfoForEdit(string BLNo)
        {
            string strExecution = "[fwd].[usp_GetJobBLForEdit]";
            IJobBL objHeader = new JobBLEntity();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@BLNumber", 50, BLNo);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    objHeader = new JobBLEntity(reader);
                }

                reader.Close();
            }

            return objHeader;
        }

        public static List<IJobBLContainer> GetExportBLContainersForAdd(long BookingId, int Status)
        {
            string strExecution = "[fwd].[usp_GetJobBLContainerForAdd]";
            List<IJobBLContainer> lstContainer = new List<IJobBLContainer>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@JobId", BookingId);
                oDq.AddIntegerParam("@Status", Status);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IJobBLContainer container = new JobBLContainerEntity(reader);
                    lstContainer.Add(container);
                }

                reader.Close();
            }

            return lstContainer;
        }

        public static List<IJobBLContainer> GetExportBLContainersForEdit(long BLId)
        {
            string strExecution = "[fwd].[usp_GetJobBLCorntainerForEdit]";
            List<IJobBLContainer> lstContainer = new List<IJobBLContainer>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@BLId", BLId);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IJobBLContainer container = new JobBLContainerEntity(reader);
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

        public static void InsertBLContainers(List<IJobBLContainer> lstData)
        {
            if (!ReferenceEquals(lstData, null))
            {
                foreach (IJobBLContainer container in lstData)
                {
                    string strExecution = "[fwd].[usp_JobBLContainers_Insert]";
                    long exportBLContainerId = 0;

                    using (DbQuery oDq = new DbQuery(strExecution))
                    {
                        //oDq.AddBigIntegerParam("@ContainerId", container.ContainerId);
                        oDq.AddBigIntegerParam("@JobId", container.BookingId);
                        oDq.AddBigIntegerParam("@BLId", container.BLId);
                        oDq.AddVarcharParam("@CntrNo", 11, container.HireContainerNumber);
                        //oDq.AddBigIntegerParam("@ImportBLFooterId", container.ImportBLFooterId);
                        //oDq.AddBigIntegerParam("@HireContainerId", container.HireContainerId);
                        //oDq.AddIntegerParam("@VesselId", container.VesselId);
                        //oDq.AddIntegerParam("@VoyageId", container.VoyageId);
                        oDq.AddVarcharParam("@ContainerSize", 2, container.ContainerSize);
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

        public static void UpdateBLContainers(List<IJobBLContainer> lstData)
        {
            if (!ReferenceEquals(lstData, null))
            {
                foreach (IJobBLContainer container in lstData)
                {
                    string strExecution = "[fwd].[usp_JobBLContainers_Update]";
                    long exportBLContainerId = 0;

                    using (DbQuery oDq = new DbQuery(strExecution))
                    {
                        oDq.AddBigIntegerParam("@ContainerId", container.ContainerId);
                        oDq.AddBigIntegerParam("@JobId", container.BookingId);
                        oDq.AddBigIntegerParam("@BLId", container.BLId);
                        //oDq.AddBigIntegerParam("@ImportBLFooterId", container.ImportBLFooterId);
                        //oDq.AddBigIntegerParam("@HireContainerId", container.HireContainerId);
                        //oDq.AddIntegerParam("@VesselId", container.VesselId);
                        //oDq.AddIntegerParam("@VoyageId", container.VoyageId);
                        oDq.AddVarcharParam("@ContainerSize", 2, container.ContainerSize);
                        oDq.AddIntegerParam("@ContainerTypeId", container.ContainerTypeId);
                        oDq.AddVarcharParam("@SealNumber", 15, container.SealNumber);
                        oDq.AddDecimalParam("@GrossWeight", 12, 3, container.GrossWeight);
                        oDq.AddDecimalParam("@TareWeight", 12, 3, container.TareWeight);
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

        public static long SaveExportBLHeader(IJobBL objBL)
        {
            long exportBLId = 0;
            string strExecution = "[fwd].[usp_fwdBLHeader_Save]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@BLId", objBL.BLId);
                oDq.AddIntegerParam("@LocationId", objBL.LocationId);
                oDq.AddBigIntegerParam("@NvoccId", objBL.NvoccId);
                oDq.AddBigIntegerParam("@JobId", objBL.BookingId);
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
                oDq.AddVarcharParam("@GoodDesc", 2000, objBL.GoodDesc);
                oDq.AddVarcharParam("@MarksNumnbers", 300, objBL.MarksNumnbers);
                oDq.AddIntegerParam("@ShipmentMode", objBL.ShipmentMode);
                oDq.AddIntegerParam("@AgentId", objBL.AgentId);
                oDq.AddIntegerParam("@CreatedBy", objBL.CreatedBy);
                oDq.AddDateTimeParam("@CreatedOn", objBL.CreatedOn);
                oDq.AddIntegerParam("@ModifiedBy", objBL.ModifiedBy);
                oDq.AddDateTimeParam("@ModifiedOn", objBL.ModifiedOn);
                oDq.AddVarcharParam("@BLClause", 20, objBL.BLClause);
                oDq.AddVarcharParam("@BLType", 20, objBL.BLType);
                oDq.AddIntegerParam("@NoOfBL", objBL.NoOfBL);
                oDq.AddDecimalParam("@NetWeight", 12, 3, objBL.NetWeight);
                oDq.AddDecimalParam("@GrossWeight", 12, 3, objBL.GrossWeight);
                oDq.AddDateTimeParam("@BLReleaseDate", objBL.BLReleaseDate);
                oDq.AddBooleanParam("@BLThruEdge", objBL.BLthruEdge);

                exportBLId = Convert.ToInt64(oDq.GetScalar());
                return exportBLId;
            }
        }

        public static void CloseOpenBL(long BLID, int UserID, string Action)
        {

            string strExecution = "[fwd].[CloseRFSBL]";
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@ExpBLId", BLID);
                oDq.AddIntegerParam("@fk_UserID", UserID);
                oDq.AddVarcharParam("@Action", 10, Action);
                oDq.RunActionQuery();
                //exportBLId = Convert.ToInt64(oDq.GetScalar());
                //return exportBLId;
            }
        }

        public static List<IJobBL> GetExportBLForListing(SearchCriteria searchCriteria)
        {
            string strExecution = "[fwd].[usp_GetFwdBLForListing]";
            List<IJobBL> lstBL = new List<IJobBL>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@JobNo", 100, searchCriteria.JobNo);
                oDq.AddVarcharParam("@BLNo", 100, searchCriteria.EdgeBLNumber);
                oDq.AddVarcharParam("@POL", 100, searchCriteria.POL);
                oDq.AddVarcharParam("@Line", 100, searchCriteria.LineName);
                oDq.AddVarcharParam("@Location", 100, searchCriteria.Location);
                oDq.AddIntegerParam("@Status", searchCriteria.IntegerOption1);
                oDq.AddVarcharParam("@SortExpression", 100, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 100, searchCriteria.SortDirection);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IJobBL bl = new JobBLEntity(reader);
                    lstBL.Add(bl);
                }

                reader.Close();
            }

            return lstBL;
        }

        public static DataTable GetUnitsForExportBlContainer()
        {
            string ProcName = "[fwd].[usp_GetUnitsForFwdBlContainer]";
            DbQuery dquery = new DbQuery(ProcName);

            return dquery.GetTable();
        }

        public static DataTable GetContainerType()
        {
            string ProcName = "[fwd].[usp_GetContainerType]";
            DbQuery dquery = new DbQuery(ProcName);

            return dquery.GetTable();
        }

        public static void ChangeBLStatus(string BLNumber, bool IsActive)
        {
            string strExecution = "[fwd].[usp_ChangeBLStatus]";
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@BLNumber", 50, BLNumber);
                oDq.AddBooleanParam("@IsActive", IsActive);
                oDq.RunActionQuery();
            }
        }

        public static bool CheckBookingLocation(string BookingNo, int Loc)
        {
            string strExecution = "[fwd].[usp_CheckJobLocation]";
            
            using (DbQuery oDq = new DbQuery(strExecution))
            {

                oDq.AddNVarcharParam("@JobNo", 50, BookingNo);
                oDq.AddIntegerParam("@Loc", Loc);
                oDq.AddBooleanParam("@Result", false, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                return Convert.ToBoolean(oDq.GetParaValue("@Result"));
            }
        }

        public static string GetTareWeight(int ContainerTypeId, decimal Size)
        {
            string strExecution = "[fwd].[usp_GetTareWeight]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {

                oDq.AddDecimalParam("@Size", 12, 3, Size);
                oDq.AddIntegerParam("@ContainerTypeId", ContainerTypeId);
                oDq.AddDecimalParam("@Result", 12, 3, 0, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                return Convert.ToString(oDq.GetParaValue("@Result"));
            }
        }

        public static bool CheckBookingBLContainer(long BookingID, int Status)
        {
            string strExecution = "[fwd].[usp_GetExpBLContainerForAddCount]";


            using (DbQuery oDq = new DbQuery(strExecution))
            {

                oDq.AddBigIntegerParam("@BookingId", BookingID);
                oDq.AddIntegerParam("@Status", Status);
                oDq.AddBooleanParam("@Result", false, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                return Convert.ToBoolean(oDq.GetParaValue("@Result"));
            }
        }

        public static int CheckExpBLExistance(string BookingNo)
        {
            string strExecution = "[fwd].[usp_CheckFwdBLExistance]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddNVarcharParam("@JobNo", 50, BookingNo);
                oDq.AddIntegerParam("@Result", 0, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                return Convert.ToInt32(oDq.GetParaValue("@Result"));
            }
        }
    }
}
