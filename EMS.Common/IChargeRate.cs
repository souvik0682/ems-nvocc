using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IChargeRate
    {
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

	    string ChargeName { get; set; }
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
        int fk_CurrencyID { get; set; }
        decimal ExchgRate { get; set; }
        decimal Units { get; set; }
        int UnitTypeID { get; set; }
        string UnitType { get; set; }

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
                
        
    }
}
