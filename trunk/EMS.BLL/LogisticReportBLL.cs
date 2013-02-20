using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EMS.BLL
{
  public  class LogisticReportBLL
    {
     

      public static DataSet GetRptCntrStockDetails(string Line, string Loc, string StockDate)
      {
          return EMS.DAL.LogisticReportDAL.GetRptCargoDesc(Line, Loc, StockDate);
      }
    }
}
