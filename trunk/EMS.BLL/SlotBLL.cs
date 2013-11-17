using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.DAL;
using EMS.Common;
using EMS.Entity;
using EMS.Utilities;
using System.Data;

namespace EMS.BLL
{
    public class SlotBLL
    {
        #region SlotAddedit
        public int AddEditSlot(ISlot Slot)
        {
            return SlotDAL.AddEditSlot(Slot);
        }
        #endregion

        public ICharge GetSlotDetails(int ChargesID)
        {
            return ChargeDAL.GetChargeDetails(ChargesID);
        }

        public List<IChargeRate> GetChargeRates(int ChargesID)
        {
            return ChargeDAL.GetChargeRates(ChargesID);
        }

        public static int DeleteSlot(int SlotId)
        {
            return SlotDAL.DeleteSlot(SlotId);
        }

        public static DataTable GetAllSlots(SearchCriteria searchCriteria, int CompanyId)
        {
            return SlotDAL.GetAllSlots(searchCriteria, CompanyId);
        }

        public DataTable GetSlot(SearchCriteria searchCriteria)
        {
            return EMS.DAL.SlotDAL.GetSlot(searchCriteria);
        }

        public int SaveSlot(EMS.Common.ISlot slot, int IsEdit)
        {

            return EMS.DAL.SlotDAL.SaveSlot(slot, IsEdit);
        }
    }

}
