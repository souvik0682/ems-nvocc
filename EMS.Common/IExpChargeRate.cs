using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IExpChargeRate
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
        //int SlabHigh { get; set; }
        //int SlabLow { get; set; }
        int LocationId { get; set; }
        bool RateActive { get; set; }
        int ChargesRateId { get; set; }
       
        /*



        int SlNo { get; set; }
        int ChargesRateID { get; set; }
        int ChargesID { get; set; }
        int LocationId { get; set; }
        int TerminalId { get; set; }
        int WashingType { get; set; }
        int High { get; set; }
        int Low { get; set; }
        decimal RatePerBL { get; set; }
        decimal RatePerTEU { get; set; }
        decimal RatePerFEU { get; set; }
        decimal RatePerTON { get; set; }
        decimal RatePerCBM { get; set; }
        decimal SharingBL { get; set; }
        decimal SharingTEU { get; set; }
        decimal SharingFEU { get; set; }
        bool RateActive { get; set; }
        decimal ServiceTax { get; set; }

        string ChangeName { get; set; }
        string TerminalName { get; set; }
        string WashingTypeName { get; set; }
        decimal Usd { get; set; }
        decimal GrossAmount { get; set; }
        decimal STax { get; set; }
        decimal ServiceTaxCessAmount { get; set; }
        decimal ServiceTaxACess { get; set; }
        decimal TotalAmount { get; set; }
        int InvoiceChargeId { get; set; }
        long InvoiceId { get; set; }

        int Type
        {
            get;
            set;
        }
        string Size
        {
            get;
            set;
        }
        decimal RatePerUnit
        {
            get;
            set;
        }
        decimal RatePerDoc
        {
            get;
            set;
        }
        */
    }
}
