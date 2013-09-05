using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.DAL.DbManager;
using System.Data;
using System.Data.SqlClient;
using EMS.Entity;

namespace EMS.BLL
{
  
    public static class ExpSlotCostBLL
    {
        public static DataTable GetSlotOperatorList(ISearchCriteria searchCriteria)
        {
            return EMS.DAL.DalExpSlotCost.GetSlotOperatorList(searchCriteria);
        }
        public static DataTable GetSlotCost(ISearchCriteria searchCriteria)
        {
            return EMS.DAL.DalExpSlotCost.GetSlotCost(searchCriteria);
        }
        public static bool DeleteSlotCost(long SlotId)
        {
            return EMS.DAL.DalExpSlotCost.DeleteSlotCost(SlotId);
        }
        public static int SaveSlotCost(SlotCostModel sLOTCOSTMODEL)
        {
            return EMS.DAL.DalExpSlotCost.SaveSlotCost(sLOTCOSTMODEL);
        }
        public static int UpdateSlotCost(SlotCostModel sLOTCOSTMODEL)
        {
            return EMS.DAL.DalExpSlotCost.UpdateSlotCost(sLOTCOSTMODEL);
        }
        public static DataTable GetMovementType()
        {
            return EMS.DAL.DalExpSlotCost.GetMovementType();
        }
        public static SlotCostModel GetSlotCost(long slotID) { return EMS.DAL.DalExpSlotCost.GetSlotCost(slotID); }
    }
}
