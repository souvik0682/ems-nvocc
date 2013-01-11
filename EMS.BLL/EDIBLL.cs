using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.DAL;

namespace EMS.BLL
{
  public  class EDIBLL
    {
       public static List<IVesselVoyageEDI> VesselVoyageEDI(int VesselID,int VoyageID)
        {
            return EDIDAL.GetVesselVoyageFrEDI(VesselID,VoyageID);
        }

       public static System.Data.DataSet  GetEDICargoInfo(int VesselID, int VoyageID)
       {
           return EDIDAL.GetEDICargoInfo(VesselID, VoyageID);
       }

       public static System.Data.DataSet GetEDIContainerInfo(int VesselID, int VoyageID)
       {
           return EDIDAL.GetEDIContainerInfo(VesselID, VoyageID);
       }

      
    }

}
