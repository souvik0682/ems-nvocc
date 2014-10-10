using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.DAL.DbManager;
using EMS.Entity;
using System.Data;

namespace EMS.DAL
{
    public sealed class TranshipmentDetailsDAL
    {
        private TranshipmentDetailsDAL(){ }

        public static DataSet GetTranshipmentHeader(int ExpBLId, int CountID)
        {
            string ProcName = "[exp].[prcGetTranshipmentDetailsByBLNo]";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);

            dquery.AddIntegerParam("@ExpBLId", ExpBLId);
            dquery.AddIntegerParam("@CountID", CountID);

            return dquery.GetTables();
        }

        public static DateTime TranshipmentStatus(int ExpBLId, ref bool VoyageStatus, ref int CountID)
        {
          
            DateTime Result;
            string TempString;
            bool ClsVoyage = false;
            int Count = 0;
            string ProcName = "[exp].[prcTranshipmentStatus]";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);

            dquery.AddIntegerParam("@ExpBLId", ExpBLId);
            dquery.AddDateTimeParam("@RESULT", null, QueryParameterDirection.Output);
            dquery.AddBooleanParam("@closeVoyage", ClsVoyage, QueryParameterDirection.Output);
            dquery.AddIntegerParam("@CountID", Count, QueryParameterDirection.Output);

            dquery.RunActionQuery();
            TempString = Convert.ToString(dquery.GetParaValue("@Result"));
            if (TempString == null || TempString == "")
                Result = default(DateTime);
            else
                Result = Convert.ToDateTime(dquery.GetParaValue("@Result"));
            //Result = Convert.ToDateTime(dquery.GetParaValue("@SailDate"));
            VoyageStatus = Convert.ToBoolean(dquery.GetParaValue("@CloseVoyage"));
            CountID = Convert.ToInt32(dquery.GetParaValue("@CountID"));
            return Result;
        }

        public static DataSet GetBookingTranshipment(int BookingId)
        {
            string ProcName = "[exp].[prcGetBookingTranshipmentByBookingId]";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);

            dquery.AddIntegerParam("@BookingId", BookingId);

            return dquery.GetTables();
        }

        public static DataSet GetContainerTranshipment(int PortId,int ExpBLId,int BookingId)
        {
            string ProcName = "[exp].[prcGetcontainerTranshipment]";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);

            dquery.AddIntegerParam("@PortId", PortId);
            dquery.AddIntegerParam("@ExpBLId", ExpBLId);
            dquery.AddIntegerParam("@BookingId", BookingId);

            return dquery.GetTables();
        }

        public static void SaveContainerTranshipment(TranshipmentDetails obj)
        {
            string strExecution = "[exp].[prcSaveContainerTranshipment]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@PortId", Convert.ToInt32(obj.PortId));
                oDq.AddIntegerParam("@ExpBLId", Convert.ToInt32(obj.ExportBLId));

                if(obj.ImportBLFooterId != 0)
                    oDq.AddIntegerParam("@ImpBLFooterId", Convert.ToInt32(obj.ImportBLFooterId));

                if(obj.HireContainerId != 0)
                    oDq.AddIntegerParam("@HireContainerId", Convert.ToInt32(obj.HireContainerId));

                if(obj.TranshipmentId != 0)
                    oDq.AddIntegerParam("@TranshipmentId", Convert.ToInt32(obj.TranshipmentId));
                oDq.AddDateTimeParam("@ActualArrival", Convert.ToDateTime(obj.ActualArrival));
                oDq.AddDateTimeParam("@ActualDeparture", Convert.ToDateTime(obj.ActualDeparture));
                oDq.AddIntegerParam("@ExpBLContainerId", Convert.ToInt32(obj.ExpBLContainerID));
                oDq.RunActionQuery();

            }
        }

        public static DataSet GetTranshipmentList(SearchCriteria searchCriteria)
        {
            string ProcName = "[exp].[prcTranshipmentList]";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);

            //if(searchCriteria.tra != 0)
            //    dquery.AddIntegerParam("@pk_TranShipID", TranshipmentId);

            if(!string.IsNullOrEmpty(searchCriteria.ContainerNo))
                dquery.AddVarcharParam("@SchContainerNo", 11, searchCriteria.ContainerNo);

            if (!string.IsNullOrEmpty(searchCriteria.BookingNo))
                dquery.AddVarcharParam("@SchBookingNo", 10, searchCriteria.BookingNo);

            if (!string.IsNullOrEmpty(searchCriteria.BLNo))
                dquery.AddVarcharParam("@SchEDGEBLNo", 30, searchCriteria.BLNo);

            if (!string.IsNullOrEmpty(searchCriteria.StringOption1))
                dquery.AddVarcharParam("@SchRefBookingNo", 30, searchCriteria.StringOption1);

            dquery.AddVarcharParam("@SortExpression", 30, searchCriteria.SortExpression);
            dquery.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);

            return dquery.GetTables();
        }

        public static void DeleteTranshipmentList(int TranshipmentId)
        {
            string strExecution = "[exp].[prcDeleteTranshipment]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@pk_TranShipID", TranshipmentId);

                oDq.RunActionQuery();

            }
        }

        public static DataSet GetExportVoyages(int Vessel)
        {
            string ProcName = "[exp].[spGetTranVoyageByVesselID]";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);

            dquery.AddIntegerParam("@Vessel", Vessel);
        
            return dquery.GetTables();
        }

        public static DataSet GetExportMainlineVoyages(int Vessel)
        {
            string ProcName = "[exp].[spGetMainlineVoyageByVesselID]";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);

            dquery.AddIntegerParam("@Vessel", Vessel);

            return dquery.GetTables();
        }
    }
}
