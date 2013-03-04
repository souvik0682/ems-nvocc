using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EMS.BLL
{
   public class VoyageBLL
    {
       public static DataSet GetVoyageAccVessel_Loc(int locationId,int vesselId,int nvoccId)
       {
           return EMS.DAL.VoyageDAL.DDLGetVoyageAccVessel_Loc(locationId, vesselId, nvoccId);
          
       }

       public static DataTable IfExistInBL(int vesselId, int voyageId)
       {
           return EMS.DAL.VoyageDAL.IfExistInBL(vesselId, voyageId);

       }

       public static int VoyageLandingDateEntry(int vesselId, int voyageId,int Pod, DateTime? LandingDate, int UserId)
       {
           return EMS.DAL.VoyageDAL.VoyageLandingDateEntry(vesselId, voyageId,Pod, LandingDate, UserId);
       }



       public static string CheckVoyageEntryAbilty(int vesselId, string voyageNo, int fk_pod, DateTime? LandingDate, bool isedit)
       {
           return EMS.DAL.VoyageDAL.CheckVoyageEntryAbilty(vesselId, voyageNo,fk_pod, LandingDate, isedit);
       }
    }
}
