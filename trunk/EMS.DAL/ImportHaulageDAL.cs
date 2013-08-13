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
    public sealed class ImportHaulageDAL
    {
        private ImportHaulageDAL()
        { }


        public static int AddEditImportHAulageChrg(IImportHaulage ImportHaulage)
        {
            string strExecution = "[mst].[spAddEditImportHaulageChrg]";
            int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@HaulageChgID", ImportHaulage.HaulageChgID);
                oDq.AddIntegerParam("@LineID", ImportHaulage.LinerID);
                oDq.AddIntegerParam("@LocationFrom", Convert.ToInt32(ImportHaulage.LocationFrom));
                oDq.AddIntegerParam("@LocationTo", Convert.ToInt32(ImportHaulage.LocationTo));
                oDq.AddVarcharParam("@ContainerSize", 2, ImportHaulage.ContainerSize);
                oDq.AddDecimalParam("@WeightFrom", 12, 3, Convert.ToDecimal(ImportHaulage.WeightFrom));
                oDq.AddDecimalParam("@WeightTo", 12, 3, Convert.ToDecimal(ImportHaulage.WeightTo));
                oDq.AddDecimalParam("@HaulageRate", 12, 2, Convert.ToDecimal(ImportHaulage.HaulageRate));
                oDq.AddBooleanParam("@HaulageStatus", ImportHaulage.HaulageStatus);
                oDq.AddDateTimeParam("@EffDate",ImportHaulage.EffectDate);
                if (ImportHaulage.HaulageChgID <= 0)
                {
                    oDq.AddIntegerParam("@CreatedBy", ImportHaulage.CreatedBy);
                    oDq.AddDateTimeParam("@CreatedOn", ImportHaulage.CreatedOn);
                }
                oDq.AddIntegerParam("@ModifiedBy", ImportHaulage.ModifiedBy);
                oDq.AddDateTimeParam("@ModifiedOn", ImportHaulage.ModifiedOn);
                oDq.AddIntegerParam("@RESULT", Result, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                Result = Convert.ToInt32(oDq.GetParaValue("@RESULT"));
            }
            return Result;
        }

        public static List<IImportHaulage> GetImportHaulage(SearchCriteria searchCriteria, int ID)
        {
            string strExecution = "[mst].[spGetImportHaulageCharge]";
            List<IImportHaulage> lstImportHAulage = new List<IImportHaulage>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@HaulageChgID", ID);
                oDq.AddVarcharParam("@SchLocFrom", 100, searchCriteria.LocationFrom);
                oDq.AddVarcharParam("@SchLocTo", 100, searchCriteria.LocationTo);
                oDq.AddVarcharParam("@SchContSize", 2, searchCriteria.ContainerSize);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();


                while (reader.Read())
                {
                    IImportHaulage oIH = new ImportHaulageEntity(reader);
                    lstImportHAulage.Add(oIH);
                }
                reader.Close();
            }
            return lstImportHAulage;
        }

        public static IImportHaulage GetImportHaulage(int ID)
        {
            string strExecution = "[mst].[spGetImportHaulageCharge]";
            IImportHaulage oIH = null;
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@HaulageChgID", ID);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    oIH = new ImportHaulageEntity(reader);
                }
                reader.Close();
            }
            return oIH;
        }

        public static int DeleteImportHaulage(int ImportHaulageId)
        {
            string strExecution = "[mst].[spDeleteImportHaulageChrg]";
            int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@HaulageChgID", ImportHaulageId);
                Result = oDq.RunActionQuery();
            }
            return Result;
        }

        public static DataTable GetAllPort(string Initial)
        {
            string strExecution = "[mst].[getPortList]";
            DataTable myDataTable;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@InitialChar", 250, Initial);
                myDataTable = oDq.GetTable();
            }
            return myDataTable;
        }
    }
}
