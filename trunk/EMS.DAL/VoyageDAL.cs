using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EMS.DAL.DbManager;

namespace EMS.DAL
{
   public class VoyageDAL
    {
       public static DataSet DDLGetVoyageAccVessel_Loc(int locationId,int vesselId,int nvoccId)
       {
           DataSet ds = new DataSet();
           using (DbQuery dq = new DbQuery("prcGetVoyageAccVessel_Loc"))
           {
               dq.AddIntegerParam("@LocationId", locationId);
               dq.AddIntegerParam("@vesselId", vesselId);
               dq.AddIntegerParam("@nvoccID", nvoccId);
               ds = dq.GetTables();
           }
           return ds;
       }

    
    }
}
