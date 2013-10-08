using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.DAL;
using EMS.Entity;
using EMS.Utilities.ResourceManager;
using System.Data;
using EMS.Utilities;

namespace EMS.BLL
{
    public class DOBLL
    {
        public static List<IDeliveryOrder> GetDeliveryOrder(SearchCriteria searchCriteria, int userId)
        {
            return DODAL.GetDeliveryOrder(searchCriteria, userId);
        }

        public static bool ValidateContainer(List<DeliveryOrderContainerEntity> lstCntr, out string message)
        {
            bool valid = true;
            int slNo = 1;
            message = "Please correct the following error(s):";

            for (int index = 0; index < lstCntr.Count; index++)
            {
                if (lstCntr[index].RequiredUnit > lstCntr[index].AvailableUnit)
                {
                    valid = false;
                    message += GeneralFunctions.FormatAlertMessage(slNo, "Required unit cannot be greater than available unit");
                    slNo++;
                }

                if (lstCntr[index].RequiredUnit > lstCntr[index].BookingUnit)
                {
                    valid = false;
                    message += GeneralFunctions.FormatAlertMessage(slNo, "Required unit cannot be greater than booking unit");
                    slNo++;
                }
            }

            return valid;
        }

        public static string SaveDeliveryOrder(IDeliveryOrder deliveryOrder, List<DeliveryOrderContainerEntity> lstCntr, int modifiedBy)
        {
            int result = 0;
            string message = string.Empty;
            string xmlDoc = GeneralFunctions.Serialize(lstCntr);
            result = DODAL.SaveDeliveryOrder(deliveryOrder, xmlDoc, modifiedBy);

            switch (result)
            {
                case -1://Transaction error occurs.
                    message = "Transaction error occurs";
                    break;
                case 0: //No records saved.
                    message = "No records saved";
                    break;
                default: //Successfully saved
                    message = ResourceManager.GetStringWithoutName("ERR00009");
                    break;
            }

            return message;
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

        public static List<IDeliveryOrderContainer> GetDeliveryOrderContriner(Int64 bookingId, int emptyYardId)
        {
            return DODAL.GetDeliveryOrderContriner(bookingId, emptyYardId);
        }

        public static DataTable GetEmptyYard(int BookingId)
        {
            return DODAL.GetEmptyYard(BookingId);
        }

        public static DataTable GetBookingList(int userId)
        {
            return DODAL.GetBookingList(userId);
        }
    }
}
