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


        public static DataSet GetRptCntrStockDetails(string Line, string Loc, string Stat, string CntrType, string StockDate)
        {
            return EMS.DAL.LogisticReportDAL.GetRptCargoDesc(Line, Loc, Stat, CntrType, StockDate);
        }

        public static DataSet GetRptCntrStockSummery(string Line, string Loc, string StockDate)
        {
            return EMS.DAL.LogisticReportDAL.GetRptCntrStockSummery(Line, Loc, StockDate);
        }

        public static IList<ContainerWiseStockEntity> GetRptContainerwiseStockSummery(string ContainerNo, int LineID, int LocationID)
        {
            
            return EMS.DAL.LogisticReportDAL.GetRptContainerwiseStockSummery(ContainerNo, LineID, LocationID);
        }
    }
}
