using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EMS.DAL.DbManager;

namespace EMS.DAL
{
   public class IGMReportDAL
    {
       public static DataSet GetRptCargoDesc(int vesselId, int voyageId,int pod)
       {
           DataSet ds = new DataSet();
           using (DbQuery dq = new DbQuery("prcRptCargoDesc"))
           {
               dq.AddIntegerParam("@vesselId", vesselId);
               dq.AddIntegerParam("@voyageId", voyageId);
               dq.AddIntegerParam("@pod", pod);
               ds = dq.GetTables();
           }
           return ds;
       }
    }
}
