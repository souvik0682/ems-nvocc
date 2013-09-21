using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EMS.Entity.Report;

namespace EMS.BLL
{
    public class LogisticReportBLL
    {


        public static DataSet GetRptCntrStockDetails(string Line, string Loc, string Stat, string CntrType, string StockDate, int EmptyYard)
        {
            return EMS.DAL.LogisticReportDAL.GetRptCargoDesc(Line, Loc, Stat, CntrType, StockDate, EmptyYard);
        }

        public static DataSet GetRptCntrStockSummery(string Line, string Loc, string StockDate)
        {
            return EMS.DAL.LogisticReportDAL.GetRptCntrStockSummery(Line, Loc, StockDate);
        }
        public static DataSet GetRptYearlyReport(int Yr, int lineId, int locId, string sizes, string Stat)
        {
            return EMS.DAL.LogisticReportDAL.GetRptYearlyReport(Yr, lineId, locId, sizes, Stat);
        }

        public static IList<ContainerWiseStockEntity> GetRptContainerwiseStockSummery(string ContainerNo, int LineID, int LocationID)
        {
            
            return EMS.DAL.LogisticReportDAL.GetRptContainerwiseStockSummery(ContainerNo, LineID, LocationID);
        }
    }
}
