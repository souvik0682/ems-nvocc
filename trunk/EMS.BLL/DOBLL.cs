using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.DAL;
using EMS.Entity;
using EMS.Utilities.ResourceManager;
using System.Data;

namespace EMS.BLL
{
    public class DOBLL
    {
        public static List<IDeliveryOrder> GetDeliveryOrder(SearchCriteria searchCriteria)
        {
            return DODAL.GetDeliveryOrder(searchCriteria);
        }

        public static string DeleteDeliveryOrder(Int64 doId)
        {
            int result = 0;
            string message = string.Empty;
            result = DODAL.DeleteDeliveryOrder(doId);

            switch (result)
            {
                case -3: //Transaction data exists.
                    message = "Transaction data exists";
                    break;
                case 0: //No records deleted.
                    message = "No records deleted";
                    break;
                default: //Successfully deleted
                    message = ResourceManager.GetStringWithoutName("ERR00010");
                    break;
            }

            return message;
        }

        public static DataTable GetEmptyYard(int userId)
        {
            return DODAL.GetEmptyYard(userId);
        }

        public static DataTable GetBookingList(int userId)
        {
            return DODAL.GetBookingList(userId);
        }
    }
}
