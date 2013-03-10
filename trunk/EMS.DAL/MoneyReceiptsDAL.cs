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
    public sealed class MoneyReceiptDAL
    {
        private MoneyReceiptDAL()
        {
        }

        #region User
        public static List<MoneyReceiptEntity> GetMoneyReceiptsRearranged(SearchCriteria searchCriteria)
        {
            List<MoneyReceiptEntity> lstMoneyReceipts = GetMoneyReceipts(searchCriteria);
            if (lstMoneyReceipts != null)
            {
                lstMoneyReceipts.GroupBy(lmr => lmr.MRNo);

            }

            return lstMoneyReceipts;


        }

        public static List<MoneyReceiptEntity> GetMoneyReceipts(SearchCriteria searchCriteria)
        {
            string strExecution = "[dbo].[uspGetMoneyReceipts]";
            List<MoneyReceiptEntity> lstMoneyReceipts = new List<MoneyReceiptEntity>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {

                oDq.AddVarcharParam("@InvoiceNo", 20, searchCriteria.StringOption1);
                oDq.AddVarcharParam("@BLNo", 60, searchCriteria.StringOption2);
                oDq.AddVarcharParam("@MRNo", 40, searchCriteria.StringOption3);
                oDq.AddIntegerParam("@IsExport", searchCriteria.IntegerOption1);
                oDq.AddVarcharParam("@SortExpression", 20, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    MoneyReceiptEntity moneyReceipt = new MoneyReceiptEntity(reader);

                    lstMoneyReceipts.Add(moneyReceipt);

                }

                reader.Close();
            }

            return lstMoneyReceipts;
        }

        public static BLInformations GetBLInformation(string blNo)
        {
            string strExecution = "[dbo].[uspGetBLInformation]";
            BLInformations blHeader = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@BLNo", 60, blNo);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    blHeader = new BLInformations(reader);
                }

                reader.Close();
            }

            return blHeader;
        }

        public static List<InvoiceTypeEntity> GetInvoiceTypes()
        {
            string strExecution = "[dbo].[uspGetInvoiceTypes]";
            List<InvoiceTypeEntity> invoiceTypes = new List<InvoiceTypeEntity>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    InvoiceTypeEntity invoiceType = new InvoiceTypeEntity();
                    invoiceType.InvoiceTypeId = Convert.ToInt32(reader["DocTypeId"]);
                    invoiceType.InvoiceTypeName = reader["DocName"].ToString();
                    invoiceTypes.Add(invoiceType);
                }

                reader.Close();
            }

            return invoiceTypes;
        }

        public static List<InvoiceDetailsEntity> GetInvoiceDetails(int invoiceTypeId)
        {
            string strExecution = "[dbo].[uspGetInvoiceDetails]";
            List<InvoiceDetailsEntity> invoiceDetails = new List<InvoiceDetailsEntity>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@InvoiceTypeId", invoiceTypeId);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    InvoiceDetailsEntity invoiceDetail = new InvoiceDetailsEntity();
                    invoiceDetail.InvoiceId = long.Parse(reader["InvoiceId"].ToString());
                    invoiceDetail.InvoiceTypeId = Convert.ToInt32(reader["InvoiceTypeId"]);
                    invoiceDetail.InvoiceNo = reader["InvoiceNo"].ToString();
                    invoiceDetail.InvoiceDate = Convert.ToDateTime(reader["InvoiceDate"]);
                    invoiceDetail.InvoiceAmount = Convert.ToDecimal(reader["GrossAmount"]);
                    invoiceDetails.Add(invoiceDetail);
                }

                reader.Close();
            }

            return invoiceDetails;
        }

        public static int SaveMoneyReceipts(List<MoneyReceiptEntity> moneyReceipts)
        {
            int result = 0;
            int count = 0;
            if (moneyReceipts != null)
            {
                foreach (MoneyReceiptEntity mrEntity in moneyReceipts)
                {
                    result = SaveMoneyReceipt(moneyReceipts[count]);
                }
            }
            return result;
        }

        public static int SaveMoneyReceipt(MoneyReceiptEntity moneyReceipt)
        {
            string strExecution = "[dbo].[prcAddEditMoneyReceipt]";
            int result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                if (moneyReceipt.IsAdded == 1)
                {
                    oDq.AddBigIntegerParam("@InvoiceID", moneyReceipt.InvoiceId);
                    oDq.AddBigIntegerParam("@MoneyRcptID", 0); //Just to avoid exception. Not actually in use.
                    oDq.AddVarcharParam("@MRNo", 30, moneyReceipt.MRNo);
                    oDq.AddDateTimeParam("@MRDate", moneyReceipt.MRDate);
                    oDq.AddDecimalParam("@CashPayment", 14, 2, moneyReceipt.CashPayment);
                    oDq.AddDecimalParam("@ChequePayment", 14, 2, moneyReceipt.ChequePayment);
                    oDq.AddVarcharParam("@ChequeDetails", 50, moneyReceipt.ChequeBank);
                    oDq.AddDecimalParam("@TDSDeducted", 14, 2, moneyReceipt.TdsDeducted);
                    oDq.AddIntegerParam("@UserAdded", moneyReceipt.UserAddedId);
                    oDq.AddIntegerParam("@UserLastEdited", moneyReceipt.UserEditedId);
                    oDq.AddIntegerParam("@IsAdded", moneyReceipt.IsAdded);
                    oDq.AddDateTimeParam("@AddedOn", DateTime.Now);
                    oDq.AddIntegerParam("@Result", result, QueryParameterDirection.Output);
                    oDq.RunActionQuery();
                    result = Convert.ToInt32(oDq.GetParaValue("@Result"));
                }
                else
                {
                    oDq.AddBigIntegerParam("@InvoiceID", moneyReceipt.InvoiceId);
                    oDq.AddBigIntegerParam("@MoneyRcptID", moneyReceipt.MoneyReceiptId);
                    oDq.AddVarcharParam("@MRNo", 30, moneyReceipt.MRNo);
                    oDq.AddDateTimeParam("@MRDate", moneyReceipt.MRDate);
                    oDq.AddDecimalParam("@CashPayment", 14, 2, moneyReceipt.CashPayment);
                    oDq.AddDecimalParam("@ChequePayment", 14, 2, moneyReceipt.ChequePayment);
                    oDq.AddVarcharParam("@ChequeDetails", 50, moneyReceipt.ChequeBank);
                    oDq.AddDecimalParam("@TDSDeducted", 14, 2, moneyReceipt.TdsDeducted);
                    oDq.AddIntegerParam("@UserAdded", moneyReceipt.UserAddedId);
                    oDq.AddIntegerParam("@UserLastEdited", moneyReceipt.UserEditedId);
                    oDq.AddIntegerParam("@IsAdded", moneyReceipt.IsAdded);
                    oDq.AddDateTimeParam("@AddedOn", DateTime.Now);
                    oDq.AddIntegerParam("@Result", result, QueryParameterDirection.Output);
                    oDq.RunActionQuery();
                    result = Convert.ToInt32(oDq.GetParaValue("@Result"));

                }
            }

            return result;
        }

        public static void DeleteMoneyReceipts(string mrNo)
        {
            string strExecution = "[dbo].[prcDeleteMoneyReceipts]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@MRNo", 40, mrNo);
                oDq.RunActionQuery();
            }
        }

        #endregion


    }
}
