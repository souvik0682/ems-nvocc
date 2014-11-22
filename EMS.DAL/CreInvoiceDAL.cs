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
     public sealed class CreInvoiceDAL
    {
         public static long SaveInvoice(ICreditorInvoice invoice, string misc)
         {
             string strExecution = "uspManageCreInv";
             long invoiceId = 0;

             using (DbQuery oDq = new DbQuery(strExecution))
             {
                 if (invoice.InvoiceID != 0)
                     oDq.AddBigIntegerParam("@pk_CinvoiceID", invoice.InvoiceID);

                 oDq.AddVarcharParam("@Mode", 1, "A");
                 
                 oDq.AddVarcharParam("@Misc", 20, misc);
                 oDq.AddIntegerParam("@CompanyID", invoice.CompanyID);
                 oDq.AddIntegerParam("@LocationID", invoice.LocationID);

                 if (invoice.InvoiceTypeID != 0)
                     oDq.AddIntegerParam("@InvoiceTypeID", invoice.InvoiceTypeID);
                 if (invoice.JobID != 0)
                     oDq.AddBigIntegerParam("@BookingID", invoice.JobID);

                 oDq.AddBigIntegerParam("@BLID", invoice.BLID);
                 oDq.AddIntegerParam("@JobID", invoice.JobID);
                 oDq.AddIntegerParam("@EstimateID", invoice.JobID);
                 oDq.AddDateTimeParam("@InvoiceDate", invoice.InvoiceDate);
                 oDq.AddDecimalParam("@GrossAmount", 12, 2, invoice.GrossAmount);
                 oDq.AddDecimalParam("@ServiceTax", 12, 2, invoice.ServiceTax);
                 oDq.AddDecimalParam("@ServiceTaxCess", 12, 2, invoice.ServiceTaxCess);
                 oDq.AddDecimalParam("@ServiceTaxACess", 12, 2, invoice.ServiceTaxACess);
                 oDq.AddDecimalParam("@Roff", 12, 2, invoice.Roff);
                 oDq.AddIntegerParam("@UserAdded", invoice.UserAdded);
                 oDq.AddIntegerParam("@UserLastEdited", invoice.UserLastEdited);
                 oDq.AddDateTimeParam("@AddedOn", invoice.AddedOn);
                 oDq.AddDateTimeParam("@EditedOn", invoice.EditedOn);
                 oDq.AddVarcharParam("@Charges", int.MaxValue, Utilities.GeneralFunctions.SerializeWithXmlTag(invoice.CreChargeRates).Replace("?<?xml version=\"1.0\" encoding=\"utf-16\"?>", ""));
                 
                 invoiceId = Convert.ToInt64(oDq.GetScalar());
             }

             return invoiceId;
         }
    }
}
