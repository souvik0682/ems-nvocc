using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web.UI.WebControls;
using EMS.Common;
using EMS.DAL;
using EMS.Entity;
using EMS.Utilities;
using EMS.Utilities.Cryptography;
using EMS.Utilities.ResourceManager;

namespace EMS.BLL
{
    public class InvoiceBLL
    {
        #region Invoice Type
        public DataTable GetInvoiceType()
        {
            return InvoiceDAL.GetInvoiceType();
        }

        public DataTable GetExpInvoiceType()
        {
            return InvoiceDAL.GetExpInvoiceType();
        }
        #endregion

        #region Location
        public DataTable GetLocation()
        {
            return InvoiceDAL.GetLocation();
        }
        #endregion

        #region BL No
        public DataTable GetExpBLno(long NvoccId, long LocationId)
        {
            return InvoiceDAL.GetExpBLno(NvoccId, LocationId);
        }
        public DataTable GetBLno(long NvoccId, long LocationId)
        {
            return InvoiceDAL.GetBLno(NvoccId, LocationId);
        }
        #endregion

        #region Gross Weight
        public DataTable GrossWeight(string BLno)
        {
            return InvoiceDAL.GrossWeight(BLno);
        }

        public DataTable ExpGrossWeight(string BLno)
        {
            return InvoiceDAL.ExpGrossWeight(BLno);
        }
        #endregion

        public DataTable TEU(string BLno)
        {
            return InvoiceDAL.TEU(BLno);
        }

        public DataTable FEU(string BLno)
        {
            return InvoiceDAL.FEU(BLno);
        }

        public DataTable Volume(string BLno)
        {
            return InvoiceDAL.Volume(BLno);
        }

        public DataTable ExpBLContainers(string BLno)
        {
            return InvoiceDAL.ExpBLContainers(BLno);
        }

        public DataTable BLdate(string BLno)
        {
            return InvoiceDAL.BLdate(BLno);
        }

        public DataTable GetCHAId()
        {
            return InvoiceDAL.GetCHAId();
        }

        public  decimal GetExchangeRate(long BlId)
        {
            return InvoiceDAL.GetExchangeRate(BlId);
        }

        public decimal GetExchangeRateByDate(DateTime Dt, int Line)
        {
            return InvoiceDAL.GetExchangeRateByDate(Dt, Line);
        }

        public List<ICharge> GetAllCharges(int docTypeId)
        {
            return InvoiceDAL.GetAllCharges(docTypeId);
        }
        public List<ICharge> GetAllExpCharges(int docTypeId, int LocationID, int LineID, string BookingNo)
        {
            return InvoiceDAL.GetAllExpCharges(docTypeId, LocationID, LineID, BookingNo);
        }

        public DataTable GetTerminals(long LocationId)
        {
            return InvoiceDAL.GetTerminals(LocationId);
        }

        public List<IChargeRate> GetAllChargeRate(int ChargesID, long LocationID, int TerminalID) //, int WashingType
        {
            return InvoiceDAL.GetAllChargeRate(ChargesID, LocationID, TerminalID); //, WashingType
        }

        public DataTable GetServiceTax(DateTime InvoiceDate)
        {
            return InvoiceDAL.GetServiceTax(InvoiceDate);
        }

        public int GetNumberOfContainer(int BlId)
        {
            return InvoiceDAL.GetNumberOfContainer(BlId);
        }

        public decimal GetDetentionAmount(int BlId)
        {
            return InvoiceDAL.GetDetentionAmount(BlId);
        }

        public long SaveInvoice(IInvoice invoice, string misc)
        {
            long invoiceId = 0;
            int invoiceChargeId = 0;

            invoiceId = InvoiceDAL.SaveInvoice(invoice, misc);

            if (invoiceId > 0)
            {
                if (!ReferenceEquals(invoice.ChargeRates, null))
                {
                    foreach (IChargeRate cRate in invoice.ChargeRates)
                    {
                        cRate.InvoiceId = invoiceId;
                        invoiceChargeId = InvoiceDAL.SaveInvoiceCharges(cRate);
                    }
                }
            }

            return invoiceId;
        }
        public long SaveInvoiceExp(IInvoice invoice, string misc, List<IChargeRate> expInvoiceCharge, int isedit)
        {
            long invoiceId = 0;
            int invoiceChargeId = 0;

            invoiceId = InvoiceDAL.SaveExpInvoice(invoice, misc, isedit);

            if (invoiceId > 0)
            {
                if (!ReferenceEquals(expInvoiceCharge, null))
                {
                    foreach (IChargeRate cRate in expInvoiceCharge)
                    {
                        cRate.InvoiceId = invoiceId;
                        invoiceChargeId = InvoiceDAL.SaveInvoiceChargesExp(cRate);
                    }
                }
            }

            return invoiceId;
        }
        public IInvoice GetInvoiceById(long InvoiceId)
        {
            return InvoiceDAL.GetInvoiceById(InvoiceId);
        }

        public IInvoice GetExpInvoiceById(long InvoiceId)
        {
            return InvoiceDAL.GetExpInvoiceById(InvoiceId);
        }

        public string GetInvoiceNoById(long InvoiceId)
        {
            return InvoiceDAL.GetInvoiceNoById(InvoiceId);
        }

        public List<IChargeRate> GetInvoiceChargesById(long InvoiceId)
        {
            return InvoiceDAL.GetInvoiceChargesById(InvoiceId);
        }

        public long GetDefaultTerminal(long BlId)
        {
            return InvoiceDAL.GetDefaultTerminal(BlId);
        }

        public long GetExpDefaultTerminal(long BlId)
        {
            return InvoiceDAL.GetExpDefaultTerminal(BlId);
        }

        public int DeleteInvoiceCharge(int InvoiceChargeId)
        {
            return InvoiceDAL.DeleteInvoiceCharge(InvoiceChargeId);
        }

        public List<IInvoice> GetInvoice(SearchCriteria searchCriteria)
        {
            return InvoiceDAL.GetInvoice(searchCriteria);
        }

        public DataTable GetLineLocation(string BLNo)
        {
            return InvoiceDAL.GetLineLocation(BLNo);
        }
        public DataTable GetExpLineLocation(string BLNo)
        {
            return InvoiceDAL.GetExpLineLocation(BLNo);
        }

        public List<IChargeRate> GetInvoiceCharges_New(long BlId, int ChargesID, int TerminalID, decimal ExchangeRate, int DocTypeId, string Param3, DateTime InvoiceDate)
        {
            return InvoiceDAL.GetInvoiceCharges_New(BlId, ChargesID, TerminalID, ExchangeRate, DocTypeId, Param3, InvoiceDate);
        }
        public List<IChargeRate> GetExpInvoiceCharges(long BlId, int ChargesID, int TerminalID, int DocTypeId, DateTime InvoiceDate)
        {
            return InvoiceDAL.GetExpInvoiceCharges(BlId, ChargesID, TerminalID, DocTypeId, InvoiceDate);
        }
        public DataTable ChargeEditable(int ChargeId)
        {
            return InvoiceDAL.ChargeEditable(ChargeId);
        }

        public decimal GetExchangeRateForExport(long BlId)
        {
            return InvoiceDAL.GetExchangeRateForExport(BlId);
        }

        public List<IChargeRate> GetExpInvoiceChargesById(long InvoiceId)
        {
            return InvoiceDAL.GetExpInvoiceChargesById(InvoiceId);
        }
    }
}
