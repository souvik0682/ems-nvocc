using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.DAL;
using System.Data;

namespace EMS.BLL
{
  public  class EDIBLL
    {
       public static List<IVesselVoyageEDI> VesselVoyageEDI(int VesselID,int VoyageID,int pod)
        {
            return EDIDAL.GetVesselVoyageFrEDI(VesselID,VoyageID,pod);
        }

       public static System.Data.DataSet  GetEDICargoInfo(int VesselID, int VoyageID,int pod)
       {
           return EDIDAL.GetEDICargoInfo(VesselID, VoyageID,pod);
       }

       public static System.Data.DataSet GetEDIContainerInfo(int VesselID, int VoyageID)
       {
           return EDIDAL.GetEDIContainerInfo(VesselID, VoyageID);
       }
      
       public static System.Data.DataSet GetCustomHouse()
       {
           return EDIDAL.GetCustomHouse();
       }
      
       public static System.Data.DataSet GetTerminalOperator(int VoyageID, int VesselID, int POD)
       {
           return EDIDAL.GetTerminalOperator(VoyageID, VesselID, POD);
       }

       public static DataTable GetItemNoForEDI(int VoyageID, int VesselID, int POD)
       {
           return EDIDAL.GetItemNoForEDI(VoyageID,VesselID,POD);
       }

       public static void SaveEDINo(int VoyageID, int VesselID, int POD, int NvoccID, int StartNo,int SurveyorID)
       {
           EDIDAL.SaveEDINo(VoyageID, VesselID, POD, NvoccID, StartNo,SurveyorID);
       }

       public static DataSet GetLoadingPort(Int64 VoyageId)
       {
           return EDIDAL.GetLoadingPort(VoyageId);
       }
    }
}
