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
    }
}
