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



       public static DataTable IfExistInBL(int vesselId, int voyageId)
       {

           DataTable ds = new DataTable();
           using (DbQuery dq = new DbQuery("select * from ImpBLHeader where fk_ImpVesselID="+vesselId+" and fk_ImpVoyageID="+@voyageId,true))
           {
               //dq.AddIntegerParam("@vesselId", vesselId);
               //dq.AddIntegerParam("@voyageId", voyageId);
            
               ds = dq.GetTable();
               
              
           }
           return ds;
       }

       public static  int VoyageLandingDateEntry(int vesselId ,int voyageId,DateTime? LandingDate, int UserId)
        {
           string ProcName = "prcVoyageLandingDateEntry";
           DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
           
           dquery.AddIntegerParam("@UserId", UserId);
           dquery.AddIntegerParam("@vesselId", vesselId);
           dquery.AddIntegerParam("@voyageId", voyageId);
           dquery.AddDateTimeParam("@LandingDate", LandingDate);
           
           return dquery.RunActionQuery();

        }
       
    }
}
