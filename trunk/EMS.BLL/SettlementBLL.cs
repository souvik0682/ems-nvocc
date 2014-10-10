using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.Entity;
using EMS.DAL;
using System.Data;

namespace EMS.BLL
{
    public class SettlementBLL
    {
        public List<ISettlement> GetSettlements(SearchCriteria searchCriteria, int locationId)
        {
            return SettlementDAL.GetAllSettlements(searchCriteria, locationId);
        }

        public DataSet GetSettlementWithBL(Int32 BlNo)
        {
            return SettlementDAL.GetSettlementWithBL(BlNo);
        }

        public DataSet GetSettlementWithSettlment(Int32 SettlementID)
        {
            return SettlementDAL.GetSettlementWithSettlment(SettlementID);
        }

        public long SaveSettlement(ISettlement settlement)
        {
            long Settlementid = 0;
            Settlementid = SettlementDAL.SaveSettlement(settlement);
            return Settlementid;
        }

        public static int DeleteSettlement(int SettlementId, int UserID)
        {
            return SettlementDAL.DeleteSettlement(SettlementId, UserID);
        }
    }
}
