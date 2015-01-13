using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EMS.DAL;
using EMS.Common;
using EMS.Entity;

namespace EMS.BLL
{
    public class CreditorInvoiceBLL
    {
        public  DataSet GetCreditor(ISearchCriteria searchCriteria) {

            return CreditorInvoiceDAL.GetCreditor(searchCriteria);
        }
        public  DataSet GetJobForCreInv(ISearchCriteria searchCriteria)
        {

            return CreditorInvoiceDAL.GetJobForCreInv(searchCriteria);
        }
        public List<CreditorInvoice> GetCreditorInvoice(ISearchCriteria searchCriteria)
        {
            return CreditorInvoiceDAL.GetCreditorInvoice(searchCriteria);
        }
        public int SaveCreditorInvoice(CreditorInvoice creditorInvoice, string Mode)
        {
            return CreditorInvoiceDAL.SaveCreditorInvoice(creditorInvoice, Mode);
        }

    }
}
