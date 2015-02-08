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
    public sealed class CreditNoteDAL
    {
        public static ICreditNote GetHeaderInformation(int LineId, int LocationId, int InvoiceId)
        {
            string strExecution = "usp_CN_GetHeaderInfo";
            ICreditNote creditNote = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@LocationId", LocationId);
                oDq.AddIntegerParam("@LineId", LineId);
                oDq.AddIntegerParam("@InvoiceId", InvoiceId);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    creditNote = new CreditNoteEntity(reader);
                }

                reader.Close();
            }

            return creditNote;
        }

        public static ICreditNote GetfwdCrnHeaderInformation(int InvoiceId)
        {
            string strExecution = "[fwd].[usp_CN_GetHeaderInfo]";
            ICreditNote creditNote = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@InvoiceId", InvoiceId);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    creditNote = new CreditNoteEntity(reader);
                }

                reader.Close();
            }

            return creditNote;
        }

        public static DataTable GetAllCharges(string InvoiceNo)
        {
            string strExecution = "usp_CN_GetChargesForInvoice";
            DataTable dt = new DataTable();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@InvoiceNo", 30, InvoiceNo);
                dt = oDq.GetTable();
            }
            return dt;
        }

        public static DataTable GetAllfwdCharges(string InvoiceNo)
        {
            string strExecution = "[fwd].[usp_CN_GetChargesForInvoice]";
            DataTable dt = new DataTable();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@InvoiceNo", 30, InvoiceNo);
                dt = oDq.GetTable();
            }
            return dt;
        }

        public static ICreditNoteCharge GeChargeDetails(int ChargeId, string InvoiceNo)
        {
            string strExecution = "usp_CN_GetChargeDetail";
            ICreditNoteCharge creditNoteCharge = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@ChargeId", ChargeId);
                oDq.AddVarcharParam("@InvoiceNo", 30, InvoiceNo);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    creditNoteCharge = new CreditNoteChargeEntity(reader);
                }

                reader.Close();
            }

            return creditNoteCharge;
        }

        public static long SaveCreditNoteHeader(ICreditNote creditNote)
        {
            string strExecution = "usp_CN_SaveCreditNoteHeader";
            long creditNoteId = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                if (creditNote.CRNID != 0)
                    oDq.AddBigIntegerParam("@CRNID", creditNote.CRNID);

                //oDq.AddVarcharParam("@ExportImport", 1, creditNote.ExportImport);
                oDq.AddDateTimeParam("@CrnDate", creditNote.CrnDate);
                oDq.AddBigIntegerParam("@InvoiceID", creditNote.InvoiceID);
                oDq.AddIntegerParam("@LocationID", creditNote.LocationID);
                oDq.AddIntegerParam("@NVOCCID", creditNote.NVOCCID);
                oDq.AddIntegerParam("@InvoiceTypeID", creditNote.InvoiceTypeID);
                oDq.AddIntegerParam("@UserId", creditNote.UserAdded);

                //if (creditNote.CrnNo != string.Empty)
                //    oDq.AddVarcharParam("@CrnNumber", 40, creditNote.CrnNo);

                creditNoteId = Convert.ToInt64(oDq.GetScalar());
            }

            return creditNoteId;
        }

        public static long SaveFwdCreditNoteHeader(ICreditNote creditNote)
        {
            string strExecution = "usp_CN_SaveCreditNoteHeader";
            long creditNoteId = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                if (creditNote.CRNID != 0)
                    oDq.AddBigIntegerParam("@CRNID", creditNote.CRNID);

                //oDq.AddVarcharParam("@ExportImport", 1, creditNote.ExportImport);
                oDq.AddDateTimeParam("@CrnDate", creditNote.CrnDate);
                oDq.AddBigIntegerParam("@InvoiceID", creditNote.InvoiceID);
                oDq.AddIntegerParam("@LocationID", creditNote.LocationID);
                oDq.AddIntegerParam("@NVOCCID", creditNote.NVOCCID);
                oDq.AddIntegerParam("@InvoiceTypeID", creditNote.InvoiceTypeID);
                oDq.AddIntegerParam("@UserId", creditNote.UserAdded);

                //if (creditNote.CrnNo != string.Empty)
                //    oDq.AddVarcharParam("@CrnNumber", 40, creditNote.CrnNo);

                creditNoteId = Convert.ToInt64(oDq.GetScalar());
            }

            return creditNoteId;
        }

        public static long SaveCreditNoteFooter(ICreditNoteCharge creditNoteCharge)
        {
            string strExecution = "usp_CN_SaveCrediNoteFooter";
            long creditNoteChargeId = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                if (creditNoteCharge.CRNChargeID != 0)
                    oDq.AddBigIntegerParam("@CRNChargeID", creditNoteCharge.CRNChargeID);

                oDq.AddBigIntegerParam("@CRNID", creditNoteCharge.CRNID);

                if (creditNoteCharge.TerminalID != 0)
                    oDq.AddBigIntegerParam("@TerminalID", creditNoteCharge.TerminalID);

                oDq.AddDecimalParam("@CRNAmount", 12, 2, creditNoteCharge.CRNAmount);
                oDq.AddDecimalParam("@CRBUSD", 12, 2, creditNoteCharge.CRBUSD);
                oDq.AddDecimalParam("@GrossCRNAmount", 12, 2, creditNoteCharge.GrossCRNAmount);
                oDq.AddDecimalParam("@ServiceTaxAmount", 12, 2, creditNoteCharge.ServiceTaxAmount);
                oDq.AddDecimalParam("@ServiceTaxCessAmount", 12, 2, creditNoteCharge.ServiceTaxCessAmount);
                oDq.AddDecimalParam("@ServiceTaxACess", 12, 2, creditNoteCharge.ServiceTaxACess);
                oDq.AddBigIntegerParam("@ChargeId", creditNoteCharge.ChargeId);

                creditNoteChargeId = Convert.ToInt64(oDq.GetScalar());
            }

            return creditNoteChargeId;
        }

        public static long UpdateCRN(long CRNID)
        {
            string strExecution = "usp_CN_UpdCreditNoteHeader";
            //long creditNoteId = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@CRNID", CRNID);

                //oDq.AddVarcharParam("@ExportImport", 1, creditNote.ExportImport);
                //oDq.AddDateTimeParam("@CrnDate", creditNote.CrnDate);
                //oDq.AddBigIntegerParam("@InvoiceID", creditNote.InvoiceID);
                //oDq.AddIntegerParam("@LocationID", creditNote.LocationID);
                //oDq.AddIntegerParam("@NVOCCID", creditNote.NVOCCID);
                //oDq.AddIntegerParam("@InvoiceTypeID", creditNote.InvoiceTypeID);
                //oDq.AddIntegerParam("@UserId", creditNote.UserAdded);

                //if (creditNote.CrnNo != string.Empty)
                //    oDq.AddVarcharParam("@CrnNumber", 40, creditNote.CrnNo);

                return oDq.RunActionQuery(); ;
            }

        }


        public static ICreditNote GetCreditNoteHeaderForView(long CreditNoteId)
        {
            string strExecution = "usp_CN_GetHeaderForView";
            ICreditNote creditNote = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@CreditNoteId", CreditNoteId);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    creditNote = new CreditNoteEntity(reader);
                }

                reader.Close();
            }

            return creditNote;
        }

        public static List<ICreditNoteCharge> GetCreditNoteFooterForView(long CreditNoteId)
        {
            string strExecution = "usp_CN_GetFooterForView";

            List<ICreditNoteCharge> lstCnCharges = new List<ICreditNoteCharge>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@CreditNoteId", CreditNoteId);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    ICreditNoteCharge cnCharge = new CreditNoteChargeEntity(reader);
                    lstCnCharges.Add(cnCharge);
                }
                reader.Close();
            }
            return lstCnCharges;
        }
    }
}
