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

       public static int VoyageLandingDateEntry(int vesselId, int voyageId,int Pod, DateTime? LandingDate, DateTime? OldLandingDate, int UserId)
       {
           return EMS.DAL.VoyageDAL.VoyageLandingDateEntry(vesselId, voyageId,Pod, LandingDate, OldLandingDate, UserId);
       }



       public static string CheckVoyageEntryAbilty(int vesselId, string voyageNo, int fk_pod, DateTime? LandingDate, bool isedit)
       {
           return EMS.DAL.VoyageDAL.CheckVoyageEntryAbilty(vesselId, voyageNo,fk_pod, LandingDate, isedit);
       }

       public static int AddEditMLVoyage(string voyageId, string vesselId, string MLvoyageNo, string dtActivity, int fk_PortID, int _userId,bool isEdit)
       {
           return EMS.DAL.VoyageDAL.AddEditMLVoyage(voyageId,vesselId, MLvoyageNo, dtActivity, fk_PortID, _userId, isEdit);
       }

       public static DataSet GetVessleByNVOCC(int nvoccId)
       {
           return EMS.DAL.VoyageDAL.GetVessleByNVOCC(nvoccId);

       }
    }
}
