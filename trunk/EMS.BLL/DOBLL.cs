using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.DAL;
using EMS.Entity;

namespace EMS.BLL
{
    public class DOBLL
    {
        public static List<IDeliveryOrder> GetDeliveryOrder(SearchCriteria searchCriteria)
        {
            return DODAL.GetDeliveryOrder(searchCriteria);
        }

        public static int DeleteDeliveryOrder(Int64 doId)
        {
            return DODAL.DeleteDeliveryOrder(doId);
        }
    }
}
