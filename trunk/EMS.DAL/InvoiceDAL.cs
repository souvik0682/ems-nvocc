using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.DAL.DbManager;
using EMS.Entity;
using EMS.Utilities;
namespace EMS.DAL
{
    public sealed class InvoiceDAL
    {
        public static DataTable GetInvoiceType()
        {
            string strExecution = "uspGetInvoiceType";
            DataTable dt = new DataTable();


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                dt = oDq.GetTable();
            }

            return dt;
        }

        public static DataTable GetExpInvoiceType()
        {
            string strExecution = "exp.uspGetInvoiceType";
            DataTable dt = new DataTable();


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                dt = oDq.GetTable();
            }

            return dt;
        }

        public static DataTable GetLocation()
        {
            string strExecution = "usp_Invoice_GetLocation";
            DataTable dt = new DataTable();


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                dt = oDq.GetTable();
            }

            return dt;
        }
        public static DataTable GetExpBLno(long NvoccId, long LocationId)
        {
            string strExecution = "[exp].[uspExp_Invoice_GetBLno]";
            DataTable dt = new DataTable();


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@NvoccID", NvoccId);
                oDq.AddBigIntegerParam("@LocationID", LocationId);
                dt = oDq.GetTable();
            }

            return dt;
        }
        public static DataTable GetBLno(long NvoccId, long LocationId)
        {
            string strExecution = "usp_Invoice_GetBLno";
            DataTable dt = new DataTable();


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@NvoccID", NvoccId);
                oDq.AddBigIntegerParam("@LocationID", LocationId);
                dt = oDq.GetTable();
            }

            return dt;
        }

        public static DataTable GrossWeight(string BLno)
        {
            string strExecution = "usp_Invoice_GetGrossWeight";
            DataTable dt = new DataTable();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@BLno", 60, BLno);
                dt = oDq.GetTable();
            }
            return dt;
        }

        public static DataTable ExpGrossWeight(string BLno)
        {
            string strExecution = "[exp].[usp_Invoice_GetGrossWeight]";
            DataTable dt = new DataTable();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@BLno", 60, BLno);
                dt = oDq.GetTable();
            }
            return dt;
        }

        public static DataTable TEU(string BLno)
        {
            string strExecution = "usp_Invoice_GetTEU";
            DataTable dt = new DataTable();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@BLno", 60, BLno);
                dt = oDq.GetTable();
            }
            return dt;
        }

        public static DataTable FEU(string BLno)
        {
            string strExecution = "usp_Invoice_GetFEU";
            DataTable dt = new DataTable();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@BLno", 60, BLno);
                dt = oDq.GetTable();
            }
            return dt;
        }

        public static DataTable Volume(string BLno)
        {
            string strExecution = "usp_Invoice_GetVolume";
            DataTable dt = new DataTable();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@BLno", 60, BLno);
                dt = oDq.GetTable();
            }
            return dt;
        }

        public static DataTable ExpBLContainers(string BLno)
        {
            string strExecution = "[exp].[usp_Invoice_GetContainers]";
            DataTable dt = new DataTable();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@BLno", 60, BLno);
                dt = oDq.GetTable();
            }
            return dt;
        }

        public static DataTable BLdate(string BLno)
        {
            string strExecution = "usp_Invoice_GetBLdate";
            DataTable dt = new DataTable();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@BLno", 60, BLno);
                dt = oDq.GetTable();
            }
            return dt;
        }

        public static DataTable GetCHAId()
        {
            string strExecution = "uspGetCHAId";
            DataTable dt = new DataTable();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                dt = oDq.GetTable();
            }
            return dt;
        }

        public static decimal GetExchangeRate(long BlId)
        {
            string strExecution = "usp_Invoice_GetExchangeRate";
            decimal ExchangeRate = 0;


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@BlID", BlId);

                ExchangeRate = Convert.ToDecimal(oDq.GetScalar());
            }

            return ExchangeRate;
        }

        public static decimal GetExchangeRateByDate(DateTime Dt)
        {
            string strExecution = "[exp].[usp_Invoice_GetExchangeRateByDate]";
            decimal ExchangeRate = 0;


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddDateTimeParam("@Dt", Dt);

                ExchangeRate = Convert.ToDecimal(oDq.GetScalar());
            }

            return ExchangeRate;
        }

        public static List<ICharge> GetAllExpCharges(int docTypeId, int LocationID, int LineID, string BookingNo)
        {
            string strExecution = "[exp].[uspExp_Invoice_GetAllCharges]";

            List<ICharge> lstCharges = new List<ICharge>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@DocTypeId", docTypeId);
                oDq.AddBigIntegerParam("@LineId", LineID);
                oDq.AddBigIntegerParam("@LocationId", LocationID);
                oDq.AddVarcharParam("@BookingNo", 40, BookingNo);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    ICharge charge = new ChargeEntity(reader);
                    lstCharges.Add(charge);
                }
                reader.Close();
            }
            return lstCharges;
        }
        public static List<ICharge> GetAllCharges(int docTypeId)
        {
            string strExecution = "usp_Invoice_GetAllCharges";

            List<ICharge> lstCharges = new List<ICharge>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@DocTypeId", docTypeId);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    ICharge charge = new ChargeEntity(reader);
                    lstCharges.Add(charge);
                }
                reader.Close();
            }
            return lstCharges;
        }

        public static DataTable GetTerminals(long LocationId)
        {
            string strExecution = "usp_Invoice_GetTerminals";
            DataTable dt = new DataTable();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@LocationID", LocationId);
                dt = oDq.GetTable();
            }
            return dt;
        }

        public static List<IChargeRate> GetAllChargeRate(int ChargesID, long LocationID, int TerminalID) //, int WashingType
        {
            string strExecution = "usp_Invoice_GetCharges";

            List<IChargeRate> lstChargeRates = new List<IChargeRate>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@ChargesID", ChargesID);
                oDq.AddBigIntegerParam("@LocationID", LocationID);
                oDq.AddIntegerParam("@TerminalID", TerminalID);
                //oDq.AddIntegerParam("@WashingType", WashingType);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IChargeRate chargeRate = new ChargeRateEntity(reader);
                    lstChargeRates.Add(chargeRate);
                }
                reader.Close();
            }
            return lstChargeRates;
        }

        public static DataTable GetServiceTax(DateTime InvoiceDate)
        {
            string strExecution = "usp_Invoice_GetServiceTax";
            DataTable dt = new DataTable();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddDateTimeParam("@StartDate", InvoiceDate);
                dt = oDq.GetTable();
            }
            return dt;
        }

        public static int GetNumberOfContainer(int BlId)
        {
            string strExecution = "usp_Invoice_GetNumberOfContainer";
            int NumberOfContainer = 0;


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@BlID", BlId);

                NumberOfContainer = Convert.ToInt32(oDq.GetScalar());
            }

            return NumberOfContainer;
        }

        public static decimal GetDetentionAmount(int BlId)
        {
            string strExecution = "usp_Invoice_GetDetentionAmount";
            decimal detentionAmount = 0;


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@BlId", BlId);

                detentionAmount = Convert.ToDecimal(oDq.GetScalar());
            }

            return detentionAmount;
        }

        public static long SaveInvoice(IInvoice invoice, string misc)
        {
            string strExecution = "usp_Invoice_Save";
            long invoiceId = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                if (invoice.InvoiceID != 0)
                    oDq.AddBigIntegerParam("@InvoiceID", invoice.InvoiceID);

                oDq.AddVarcharParam("@Misc", 20, misc);
                oDq.AddIntegerParam("@CompanyID", invoice.CompanyID);
                oDq.AddIntegerParam("@LocationID", invoice.LocationID);
                oDq.AddIntegerParam("@NVOCCID", invoice.NVOCCID);

                if (invoice.InvoiceTypeID != 0)
                    oDq.AddIntegerParam("@InvoiceTypeID", invoice.InvoiceTypeID);
                if (invoice.BookingID != 0)
                    oDq.AddBigIntegerParam("@BookingID", invoice.BookingID);

                oDq.AddBigIntegerParam("@BLID", invoice.BLID);
                oDq.AddVarcharParam("@ExportImport", 1, invoice.ExportImport);
                oDq.AddDecimalParam("@XchangeRate", 12, 2, invoice.XchangeRate);
                oDq.AddDateTimeParam("@InvoiceDate", invoice.InvoiceDate);
                oDq.AddIntegerParam("@CHAID", invoice.CHAID);
                oDq.AddVarcharParam("@Account", 300, invoice.Account);
                oDq.AddBooleanParam("@AllInFreight", invoice.AllInFreight);
                oDq.AddDecimalParam("@GrossAmount", 12, 2, invoice.GrossAmount);
                oDq.AddDecimalParam("@ServiceTax", 12, 2, invoice.ServiceTax);
                oDq.AddDecimalParam("@ServiceTaxCess", 12, 2, invoice.ServiceTaxCess);
                oDq.AddDecimalParam("@ServiceTaxACess", 12, 2, invoice.ServiceTaxACess);
                oDq.AddDecimalParam("@Roff", 12, 2, invoice.Roff);
                oDq.AddIntegerParam("@UserAdded", invoice.UserAdded);
                oDq.AddIntegerParam("@UserLastEdited", invoice.UserLastEdited);
                oDq.AddDateTimeParam("@AddedOn", invoice.AddedOn);
                oDq.AddDateTimeParam("@EditedOn", invoice.EditedOn);

                invoiceId = Convert.ToInt64(oDq.GetScalar());
            }

            return invoiceId;
        }


        public static long SaveExpInvoice(IInvoice invoice, string misc)
        {
            string strExecution = "[exp].[usp_Exp_Invoice_Save]";
            long invoiceId = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                if (invoice.InvoiceID != 0)
                    oDq.AddBigIntegerParam("@InvoiceID", invoice.InvoiceID);

                oDq.AddVarcharParam("@Misc", 20, misc);
                oDq.AddIntegerParam("@CompanyID", invoice.CompanyID);
                oDq.AddIntegerParam("@LocationID", invoice.LocationID);
                oDq.AddIntegerParam("@NVOCCID", invoice.NVOCCID);

                if (invoice.InvoiceTypeID != 0)
                    oDq.AddIntegerParam("@InvoiceTypeID", invoice.InvoiceTypeID);
                if (invoice.BookingID != 0)
                    oDq.AddBigIntegerParam("@BookingID", invoice.BookingID);

                oDq.AddBigIntegerParam("@BLID", invoice.BLID);
                oDq.AddVarcharParam("@ExportImport", 1, invoice.ExportImport);
                oDq.AddDecimalParam("@XchangeRate", 12, 2, invoice.XchangeRate);
                oDq.AddDateTimeParam("@InvoiceDate", invoice.InvoiceDate);
                oDq.AddIntegerParam("@CHAID", invoice.CHAID);
                oDq.AddVarcharParam("@Account", 300, invoice.Account);
                oDq.AddBooleanParam("@AllInFreight", invoice.AllInFreight);
                oDq.AddDecimalParam("@GrossAmount", 12, 2, invoice.GrossAmount);
                oDq.AddDecimalParam("@ServiceTax", 12, 2, invoice.ServiceTax);
                oDq.AddDecimalParam("@ServiceTaxCess", 12, 2, invoice.ServiceTaxCess);
                oDq.AddDecimalParam("@ServiceTaxACess", 12, 2, invoice.ServiceTaxACess);
                oDq.AddDecimalParam("@Roff", 12, 2, invoice.Roff);
                oDq.AddIntegerParam("@UserAdded", invoice.UserAdded);
                oDq.AddIntegerParam("@UserLastEdited", invoice.UserLastEdited);
                oDq.AddDateTimeParam("@AddedOn", invoice.AddedOn);
                oDq.AddDateTimeParam("@EditedOn", invoice.EditedOn);

                invoiceId = Convert.ToInt64(oDq.GetScalar());
            }

            return invoiceId;
        }


        public static int SaveInvoiceCharges(IChargeRate cRate)
        {
            string strExecution = "usp_Invoice_Charges_Save";
            int invoiceChargeId = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@InvoiceID", cRate.InvoiceId);

                if (cRate.InvoiceChargeId > 0)
                    oDq.AddBigIntegerParam("@InvoiceChargeID", cRate.InvoiceChargeId);

                oDq.AddIntegerParam("@ChargeID", cRate.ChargesID);

                oDq.AddDecimalParam("@RatePerUnit", 12, 2, cRate.RatePerBL);
                oDq.AddDecimalParam("@RatePerTEU", 12, 2, cRate.RatePerTEU);
                oDq.AddDecimalParam("@RatePerFEU", 12, 2, cRate.RatePerFEU);
                oDq.AddDecimalParam("@RatePerCBM", 12, 2, cRate.RatePerCBM);
                oDq.AddDecimalParam("@RatePerTon", 12, 2, cRate.RatePerTON);
                oDq.AddDecimalParam("@RateUSD", 12, 2, cRate.Usd);
                oDq.AddDecimalParam("@GrossAmount", 12, 2, cRate.GrossAmount);
                oDq.AddDecimalParam("@ServiceTaxAmount", 12, 2, cRate.STax);
                oDq.AddDecimalParam("@ServiceTaxCessAmount", 12, 2, cRate.ServiceTaxCessAmount);
                oDq.AddDecimalParam("@ServiceTaxACess", 12, 2, cRate.ServiceTaxACess);
                oDq.AddBigIntegerParam("@TerminalId", cRate.TerminalId);
                oDq.AddDecimalParam("@SharingBL", 12, 2, cRate.SharingBL);
                oDq.AddDecimalParam("@SharingTEU", 12, 2, cRate.SharingTEU);
                oDq.AddDecimalParam("@SharingFEU", 12, 2, cRate.SharingFEU);

                invoiceChargeId = Convert.ToInt32(oDq.GetScalar());
            }

            return invoiceChargeId;
        }

        public static int SaveInvoiceChargesExp(IChargeRate cRate)

        {
            string strExecution = "[exp].[uspExp_Invoice_Charges_Save]";
            int invoiceChargeId = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@InvoiceID", cRate.InvoiceId);

                if (cRate.InvoiceChargeId > 0)
                    oDq.AddBigIntegerParam("@InvoiceChargeID", cRate.InvoiceChargeId);

                oDq.AddIntegerParam("@ChargeID", cRate.ChargesID);

                oDq.AddDecimalParam("@RatePerUnit", 12, 2, cRate.RatePerBL);
                oDq.AddDecimalParam("@RatePerTEU", 12, 2, cRate.RatePerTEU);
                oDq.AddDecimalParam("@RatePerFEU", 12, 2, cRate.RatePerFEU);
                oDq.AddDecimalParam("@RatePerCBM", 12, 2, cRate.RatePerCBM);
                oDq.AddDecimalParam("@RatePerTon", 12, 2, cRate.RatePerTON);
                oDq.AddDecimalParam("@RateUSD", 12, 2, 0);
                oDq.AddDecimalParam("@GrossAmount", 12, 2, cRate.GrossAmount);
                oDq.AddDecimalParam("@ServiceTaxAmount", 12, 2, cRate.ServiceTax);
                oDq.AddDecimalParam("@ServiceTaxCessAmount", 12, 2, cRate.ServiceTaxCessAmount);
                oDq.AddDecimalParam("@ServiceTaxACess", 12, 2, cRate.ServiceTaxACess);
                oDq.AddBigIntegerParam("@TerminalId", cRate.TerminalId);
                oDq.AddDecimalParam("@SharingBL", 12, 2, 0);
                oDq.AddDecimalParam("@SharingTEU", 12, 2,0);
                oDq.AddDecimalParam("@SharingFEU", 12, 2, 0);
                oDq.AddIntegerParam("@CurId", cRate.fk_CurrencyID);
                oDq.AddDecimalParam("@ExchgRt", 12, 2, cRate.ExchgRate);
                invoiceChargeId = Convert.ToInt32(oDq.GetScalar());
            }

            return invoiceChargeId;
        }
        public static IInvoice GetInvoiceById(long InvoiceId)
        {
            string strExecution = "usp_Invoice_GetInvoiceById";
            IInvoice invoice = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@InvoiceID", InvoiceId);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    invoice = new InvoiceEntity(reader);
                }

                reader.Close();
            }

            return invoice;
        }

        public static List<IChargeRate> GetInvoiceChargesById(long InvoiceId)
        {
            string strExecution = "usp_Invoice_GetChargesById";
            List<IChargeRate> lstRates = new List<IChargeRate>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@InvoiceID", InvoiceId);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IChargeRate footer = new ChargeRateEntity(reader);
                    lstRates.Add(footer);
                }

                reader.Close();
            }

            return lstRates;
        }

        public static string GetInvoiceNoById(long InvoiceId)
        {
            string strExecution = "usp_Invoice_GetInvoiveNoById";
            string invoiceNo = string.Empty;


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@InvoiceId", InvoiceId);

                invoiceNo = Convert.ToString(oDq.GetScalar());
            }

            return invoiceNo;
        }

        public static long GetDefaultTerminal(long BlId)
        {
            string strExecution = "usp_Invoice_GetDefaultTerminal";
            long terminalId = 0;


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@BlID", BlId);

                terminalId = Convert.ToInt64(oDq.GetScalar());
            }

            return terminalId;
        }

        public static long GetExpDefaultTerminal(long BlId)
        {
            string strExecution = "[exp].[usp_Invoice_GetDefaultTerminal]";
            long terminalId = 0;


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@BlID", BlId);

                terminalId = Convert.ToInt64(oDq.GetScalar());
            }

            return terminalId;
        }

        public static int DeleteInvoiceCharge(int InvoiceChargeId)
        {
            string strExecution = "usp_Invoice_RemoveInvoiceCharge";
            int ret = 0;


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@InvoiceChargeId", InvoiceChargeId);

                ret = Convert.ToInt32(oDq.GetScalar());
            }

            return ret;
        }

        public static List<IInvoice> GetInvoice(SearchCriteria searchCriteria)
        {
            string strExecution = "usp_Invoice_GetAllInvoice";
            List<IInvoice> lstInvoice = new List<IInvoice>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@InvoiceNo", 250, searchCriteria.InvoiceNo);
                oDq.AddVarcharParam("@BLNo", 250, searchCriteria.BLNo);
                oDq.AddVarcharParam("@ImportExport", 250, searchCriteria.ImportExport);
                oDq.AddVarcharParam("@BookingNo", 250, searchCriteria.BookingNo);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IInvoice invoice= new InvoiceEntity(reader);
                    lstInvoice.Add(invoice);
                }

                reader.Close();
            }

            return lstInvoice;
        }

        public static DataTable GetExpLineLocation(string BLNo)
        {
            string strExecution = "[exp].[uspExp_Invoice_GetLineLocation]";
            DataTable dt = new DataTable();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@BLNo", 60,BLNo);
                dt = oDq.GetTable();
            }
            return dt;
        }
        public static DataTable GetLineLocation(string BLNo)
        {
            string strExecution = "usp_Invoice_GetLineLocation";
            DataTable dt = new DataTable();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@BLNo", 60,BLNo);
                dt = oDq.GetTable();
            }
            return dt;
        }

        //public static List<IExpChargeRate> GetExpInvoiceCharges(long BlId, int ChargeID, int TerminalID, int DocTypeId, DateTime InvoiceDate)
        //{
        //    string strExecution;
        //    if (DocTypeId == 1)
        //        strExecution = "[exp].[usp_ExpInvFrt_Charge]";
        //    else
        //        strExecution = "[exp].[usp_ExpInvoice_Charge]";

        //    List<IExpChargeRate> lstRates = new List<IExpChargeRate>();

        //    using (DbQuery oDq = new DbQuery(strExecution))
        //    {
        //        oDq.AddBigIntegerParam("@BLID", BlId);
        //        oDq.AddIntegerParam("@ChargeID", ChargeID);
        //        oDq.AddIntegerParam("@TerminalID", TerminalID);
        //        oDq.AddIntegerParam("@DoctypeID", DocTypeId);
        //        oDq.AddDateTimeParam("@InvoiceDate", InvoiceDate);

        //        DataTableReader reader = oDq.GetTableReader();

        //        while (reader.Read())
        //        {
        //            IExpChargeRate chargeRate = new ExpChargeRateEntity(reader);
        //            lstRates.Add(chargeRate);
        //        }

        //        reader.Close();
        //    }

        //    return lstRates;
        //}



        public static List<IChargeRate> GetExpInvoiceCharges(long BlId, int ChargeID, int TerminalID, int DocTypeId, DateTime InvoiceDate)
        {
            string strExecution;
            if (DocTypeId == 1)
                strExecution = "[exp].[usp_ExpInvFrt_Charge]";
            else
                strExecution = "[exp].[usp_ExpInvoice_Charge]";

            List<IChargeRate> lstRates = new List<IChargeRate>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@BLID", BlId);
                oDq.AddIntegerParam("@ChargeID", ChargeID);
                oDq.AddIntegerParam("@TerminalID", TerminalID);
                oDq.AddIntegerParam("@DoctypeID", DocTypeId);
                oDq.AddDateTimeParam("@InvoiceDate", InvoiceDate);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IChargeRate chargeRate = new ChargeRateEntity(reader);
                    lstRates.Add(chargeRate);
                }

                reader.Close();
            }

            return lstRates;
        }

      
      
        public static List<IChargeRate> GetInvoiceCharges_New(long BlId, int ChargesID, int TerminalID, decimal ExchangeRate, int DocTypeId, string Param3, DateTime InvoiceDate)
        {
            string strExecution = "usp_Invoice_CalculateCharge";
            List<IChargeRate> lstRates = new List<IChargeRate>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@BLID", BlId);
                oDq.AddIntegerParam("@ChargeID", ChargesID);
                oDq.AddIntegerParam("@TerminalID", TerminalID);
                oDq.AddDecimalParam("@ExRate", 12, 2, ExchangeRate);
                oDq.AddIntegerParam("@DoctypeID", DocTypeId);
                oDq.AddVarcharParam("@Param3", 500, Param3);
                oDq.AddDateTimeParam("@InvoiceDate", InvoiceDate); 
                
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IChargeRate chargeRate = new ChargeRateEntity(reader);
                    lstRates.Add(chargeRate);
                }

                reader.Close();
            }

            return lstRates;
        }

        public static DataTable ChargeEditable(int ChargeId)
        {
            string strExecution = "usp_Invoice_IsChargeEditable";
            DataTable charge = null;


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@ChargeId", ChargeId);

                charge = oDq.GetTable();
            }

            return charge;
        }

        public static decimal GetExchangeRateForExport(long BlId)
        {
            string strExecution = "[exp].[usp_GetExchangeRate]";
            decimal exchangeRate = 0;
            
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@BLID", BlId);
                exchangeRate = Convert.ToInt64(oDq.GetScalar());
            }
            return exchangeRate;
        }

        public static List<IChargeRate> GetExpInvoiceChargesById(long InvoiceId)
        {
            string strExecution = "[exp].[usp_Invoice_GetChargesById]";
            List<IChargeRate> lstRates = new List<IChargeRate>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@InvoiceID", InvoiceId);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IChargeRate footer = new ChargeRateEntity(reader);
                    lstRates.Add(footer);
                }

                reader.Close();
            }

            return lstRates;
        }

    }
}
