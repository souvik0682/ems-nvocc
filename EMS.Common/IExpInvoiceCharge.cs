using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IExpInvoiceCharge
    {
        long InvoiceId { get; set; }
        long InvoiceChargeId { get; set; }
        int ChargesId { get; set; }
        string ChargeName { get; set; }
        int TerminalId { get; set; }
        string TerminalName { get; set; }
        decimal RatePerBL { get; set; }
        decimal RatePerTEU { get; set; }
        decimal RatePerFEU { get; set; }
        decimal RatePerCBM { get; set; }
        decimal RatePerTon { get; set; }
        int fk_CurrencyID { get; set; }
        decimal GrossAmount { get; set; }
        decimal ServiceTaxAmount { get; set; }
        decimal ExchgRate { get; set; }
        decimal ServiceTaxACess { get; set; }
        decimal ServiceTaxCessAmount { get; set; }
        decimal TotalAmount { get; set; }
        int LocationId { get; set; }
        bool RateActive { get; set; }
        int ChargesRateId { get; set; }

    }
}

