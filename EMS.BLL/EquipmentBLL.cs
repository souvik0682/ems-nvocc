using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.DAL;
using System.Data;

namespace EMS.BLL
{
   public class EquipmentBLL
    {
       public static DataTable GetEqpRepair(int UserId, IEqpRepairing ieqp)//List<IEqpRepairing>
       {
           return EquipmentDAL.GetEqpRepair(UserId, ieqp);
       }

       public static DataSet DDLGetLine()
       {
           return EquipmentDAL.DDLGetLine();
       }

       public static DataSet DDLGetStatus()
       {
           return EquipmentDAL.DDLGetStatus();
       }

       public static DataSet DDLGetContainerType()
       {
           return EquipmentDAL.DDLGetContainerType();
       }

       public static DataSet DDLGetEmptyYard(int loc)
       {
           return EquipmentDAL.DDLGetEmptyYard(loc);
       }

       public static int AddEditEquipEstimate(int userId,bool isEdit,IEqpRepairing ieqp)
       {
           return EquipmentDAL.AddEditEquipEstimate(userId, isEdit, ieqp);
       }

       public static DataSet CheckContainerStatus(string CntrNo)
       {
           return EquipmentDAL.CheckContainerStatus(CntrNo);
       }

       public DataTable GetContainerList(int LocId, string Initial)
       {
           return EquipmentDAL.GetContainerList(LocId,0, Initial);
       }


       public DataTable GetContainerList(int LocId, int LineId, string Initial)
       {
           return EquipmentDAL.GetContainerList(LocId, LineId, Initial);
       }

       public static DataSet GetOMHinformation(int LocId, int LineId, int VesselID, int VoyageID, int POD )
       {
           return EquipmentDAL.GetOMHinformation(LocId, LineId, VesselID, VoyageID, POD);
       }

       public static DataSet GetCOPRARContainerInfo(int VesselID, int VoyageID, int POD)
       {
           return EquipmentDAL.GetCOPRARContainerInfo(VesselID, VoyageID, POD);
       }

       public static DataSet GetExportBLHeader(string Initial)
       {
           return EquipmentDAL.GetExportBLHeader(Initial);
       }
    }
}
