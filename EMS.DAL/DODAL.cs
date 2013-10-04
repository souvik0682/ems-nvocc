﻿using System;
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
        public static List<IDeliveryOrder> GetDeliveryOrder(SearchCriteria searchCriteria, int userId)
        {
            string strExecution = "[exp].[uspGetDOList]";
            List<IDeliveryOrder> lstDO = new List<IDeliveryOrder>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@UserId", userId);
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

        public static List<IDeliveryOrderContainer> GetDeliveryOrderContriner(Int64 bookingId, int emptyYardId)
        {
            string strExecution = "[exp].[uspGetContainerForDO]";
            List<IDeliveryOrderContainer> lstCntr = new List<IDeliveryOrderContainer>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@BookingId", bookingId);
                oDq.AddIntegerParam("@EmptyYardId", emptyYardId);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IDeliveryOrderContainer container = new DeliveryOrderContainerEntity(reader);
                    lstCntr.Add(container);
                }

                reader.Close();
            }

            return lstCntr;
        }

        public static int SaveDeliveryOrder(IDeliveryOrder deliveryOrder, string xmlDoc, int modifiedBy)
        {
            int result = 0;
            Int64 newDOId = 0;
            string newDONo = string.Empty;
            string strExecution = "[exp].[uspSaveDeliveryOrder]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@DOId", deliveryOrder.DeliveryOrderId);
                oDq.AddBigIntegerParam("@BookingId", deliveryOrder.BookingId);
                oDq.AddIntegerParam("@EmptyYardId", deliveryOrder.EmptyYardId);
                oDq.AddDateTimeParam("@DeliveryOrderDate", deliveryOrder.DeliveryOrderDate);
                oDq.AddVarcharParam("@XmlDoc", 1000, xmlDoc);
                oDq.AddIntegerParam("@ModifiedBy", modifiedBy);
                oDq.AddBigIntegerParam("@NewDOId", newDOId, QueryParameterDirection.Output);
                oDq.AddVarcharParam("@DONumber", 50, newDONo, QueryParameterDirection.Output);
                result = oDq.RunActionQuery();
                deliveryOrder.DeliveryOrderId = newDOId;
                deliveryOrder.DeliveryOrderNumber = newDONo;
            }

            return result;
        }

        public static DataTable GetEmptyYard(int userId)
        {
            string strExecution = "[exp].[uspGetEmptyYardForDO]";
            DataTable myDataTable;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@UserId", userId);
                myDataTable = oDq.GetTable();
            }

            return myDataTable;
        }

        public static DataTable GetBookingList(int userId)
        {
            string strExecution = "[exp].[uspGetBookingListForDO]";
            DataTable myDataTable;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@UserId", userId);
                myDataTable = oDq.GetTable();
            }

            return myDataTable;
        }
    }
}