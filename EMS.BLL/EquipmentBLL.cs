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

       public static int AddEditEquipEstimate(int userId,bool isEdit,IEqpRepairing ieqp)
       {
           return EquipmentDAL.AddEditEquipEstimate(userId, isEdit, ieqp);
       }

       public static DataSet CheckContainerStatus(string CntrNo)
       {
           return EquipmentDAL.CheckContainerStatus(CntrNo);
       }
    }
}
