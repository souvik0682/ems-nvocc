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
        #endregion

        #region Location
        public DataTable GetLocation()
        {
            return InvoiceDAL.GetLocation();
        }
        #endregion

        #region BL No
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

        public List<ICharge> GetAllCharges()
        {
            return InvoiceDAL.GetAllCharges();
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

        public long SaveInvoice(IInvoice invoice)
        {
            long invoiceId = 0;
            int invoiceChargeId = 0;

            invoiceId = InvoiceDAL.SaveInvoice(invoice);

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

        public IInvoice GetInvoiceById(long InvoiceId)
        {
            return InvoiceDAL.GetInvoiceById(InvoiceId);
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

        public int DeleteInvoiceCharge(int InvoiceChargeId)
        {
            return InvoiceDAL.DeleteInvoiceCharge(InvoiceChargeId);
        }

        public List<IInvoice> GetInvoice(SearchCriteria searchCriteria)
        {
            return InvoiceDAL.GetInvoice(searchCriteria);
        }
    }
}
