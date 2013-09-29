using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.DAL.DbManager;
using EMS.Entity;

namespace EMS.DAL
{
    public class DODAL
    {
        public static List<IDeliveryOrder> GetDeliveryOrder(SearchCriteria searchCriteria)
        {
            string strExecution = "[exp].[uspGetDOList]";
            List<IDeliveryOrder> lstDO = new List<IDeliveryOrder>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@SchBookingNo", 50, searchCriteria.BookingNo);
                oDq.AddVarcharParam("@SchDONo", 50, searchCriteria.DONumber);
                oDq.AddVarcharParam("@SchLocation", 50, searchCriteria.Location);
                oDq.AddVarcharParam("@SchLine", 50, searchCriteria.LineName);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IDeliveryOrder deliveryOrder = new DeliveryOrderEntity(reader);
                    lstDO.Add(deliveryOrder);
                }

                reader.Close();
            }

            return lstDO;
        }

        public static int DeleteDeliveryOrder(Int64 doId)
        {
            string strExecution = "[exp].[uspDeleteDO]";
            int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@DOId", doId);
                Result = oDq.RunActionQuery();
            }

            return Result;
        }
    }
}
