using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface ICreditorInvoice
    {
        long InvoiceID { get; set; }
        int CompanyID { get; set; }
        int LocationID { get; set; }
        int InvoiceTypeID { get; set; }
        long BLID { get; set; }
        string ExportImport { get; set; }
        string InvoiceNo { get; set; }
        DateTime InvoiceDate { get; set; }
        string PartyName { get; set; }
        int PartyID { get; set; }
        decimal GrossAmount { get; set; }
        decimal ServiceTax { get; set; }
        decimal ServiceTaxCess { get; set; }
        decimal ServiceTaxACess { get; set; }
        decimal Roff { get; set; }
        DateTime BLDate { get; set; }
        string JobNo { get; set; }
        int JobID { get; set; }
        int EstimateID { get; set; }
        DateTime JobDate { get; set; }
        string EstimateNo { get; set; }
        int UserAdded { get; set; }
        int UserLastEdited { get; set; }
        DateTime AddedOn { get; set; }
        DateTime EditedOn { get; set; }
        List<ICreChargeRate> CreChargeRates { get; set; }
    }
}
