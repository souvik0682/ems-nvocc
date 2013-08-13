using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EMS.DAL.DbManager;
using EMS.Entity.Report;

namespace EMS.DAL
{
    public class LogisticReportDAL
    {
        public static System.Data.DataSet GetRptCargoDesc(string Line, string Loc, string Stat, string CntrType, string StockDate)
        {
            DateTime dt=string.IsNullOrEmpty(StockDate)?DateTime.Now:Convert.ToDateTime(StockDate);
            DataSet ds = new DataSet();
            using (DbQuery dq = new DbQuery("prcRptStockDetail"))
            {
                dq.AddVarcharParam("@Line",80, Line);
                dq.AddVarcharParam("@Loc",100, Loc);
                dq.AddVarcharParam("@Stat", 100, Stat);
                dq.AddVarcharParam("@CntrType", 100, CntrType);
                dq.AddDateTimeParam("@StockDate", dt);
                ds = dq.GetTables();
            }
            return ds;

        }

        public static DataSet GetRptCntrStockSummery(string Line, string Loc, string StockDate)
        {
            DateTime dt = string.IsNullOrEmpty(StockDate) ? DateTime.Now : Convert.ToDateTime(StockDate);
            DataSet ds = new DataSet();
            using (DbQuery dq = new DbQuery("prcRptStockSummery"))
            {
                dq.AddVarcharParam("@Line", 80, Line);
                dq.AddVarcharParam("@Loc", 100, Loc);
                dq.AddDateTimeParam("@StockDate", dt);
                ds = dq.GetTables();
            }
            return ds;
        }

        public static DataSet GetRptYearlyReport(int Yr, int lineId, int locId, string sizes, string Stat)
       
        {
            DataSet ds = new DataSet();
            using (DbQuery dq = new DbQuery("[report].[uspGetYearlyMisReportData]"))
            {
                dq.AddIntegerParam("@LineId", lineId);
                dq.AddIntegerParam("@LocId", locId);
                dq.AddBigIntegerParam("@RptYear", Yr);
                dq.AddVarcharParam("@Sizes", 3, sizes);
                dq.AddVarcharParam("@LoadEmpty", 1, Stat);
                ds = dq.GetTables();
            }
            return ds;
        }


        public static IList<ContainerWiseStockEntity> GetRptContainerwiseStockSummery(string ContainerNo, int LineID, int LocationID)
        {
           /*
            DataTable dt = new DataTable();
            using (DbQuery dq = new DbQuery("prcRptContainerWiseStatus"))
            {
                dq.AddIntegerParam("@Location", LocationID);
                dq.AddIntegerParam("@Line", LineID);
                dq.AddNVarcharParam("@CntrNo", 15,ContainerNo);
                //dt = dq.GetTable();
                
            }
            return dt;
            * */

            ContainerWiseStockEntity entity;
            List<ContainerWiseStockEntity> lstEntity = new List<ContainerWiseStockEntity>();
            using (DbQuery oDq = new DbQuery("prcRptContainerWiseStatus"))
            {
                oDq.AddIntegerParam("@Location", LocationID);
                oDq.AddIntegerParam("@Line", LineID);
                oDq.AddNVarcharParam("@CntrNo", 15, ContainerNo);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    entity = new ContainerWiseStockEntity();

                    entity.cfs = Convert.ToString(reader["cfs"]);
                    entity.ety = Convert.ToString(reader["ety"]);
                    entity.GroupID = Convert.ToString(reader["GroupID"]);
                    entity.Locabbr = Convert.ToString(reader["Locabbr"]);
                    entity.MoveAbbr = Convert.ToString(reader["MoveAbbr"]);
                    entity.MovementDate = Convert.ToDateTime(reader["MovementDate"]);
                    entity.NVOCC = Convert.ToString(reader["NVOCC"]);                   
                    entity.Size = Convert.ToString(reader["Size"]);
                    entity.vesselvoyage = Convert.ToString(reader["vesselvoyage"]);
                    entity.HireRef = Convert.ToString(reader["hireref"]);

                    lstEntity.Add(entity);
                }
            }


            return lstEntity;
        }

        /*
         * 
         * 
         * 
         * */


    }
}
