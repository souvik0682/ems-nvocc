using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface ICreChargeRate
    {
        int SlNo { get; set; }
        int ChargesRateID { get; set; }
        int ChargesID { get; set; }
        int LocationId { get; set; }
        decimal RatePerBL { get; set; }
        bool RateActive { get; set; }
        decimal ServiceTax { get; set; }
        string ChargeName { get; set; }
        decimal Usd { get; set; }
        decimal GrossAmount { get; set; }
        decimal STax { get; set; }
        decimal ServiceTaxCessAmount { get; set; }
        decimal ServiceTaxACess { get; set; }
        decimal TotalAmount { get; set; }
        int InvoiceChargeId { get; set; }
        long InvoiceId { get; set; }
        int fk_CurrencyID { get; set; }
        decimal ExchgRate { get; set; }

        int Type
        {
            get;
            set;
        }
 
    }
}
