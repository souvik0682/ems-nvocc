// -----------------------------------------------------------------------
// <copyright file="ExpInvoiceCharge.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace EMS.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>r
    [Serializable]
    public class ExpInvoiceCharge
    {
        public long InvoiceID{get;set;}
		public long InvoiceChargeID{get;set;}
		public int ChargesID{get;set;}
		public string ChargeName{get;set;}
		public int TerminalID{get;set;}
		public string TerminalName{get;set;}
		public decimal RatePerBL{get;set;}
		public decimal RatePerTEU{get;set;}
		public decimal RateperFEU{get;set;}
		public decimal RatePerCBM{get;set;}
		public decimal RatePerTon{get;set;}
		public int fk_CurrencyID{get;set;}
		public decimal GrossAmount{get;set;}
		public decimal ServiceTaxAmount{get;set;}
		public decimal ExchgRate{get;set;}
		public decimal ServiceTaxACess{get;set;}
		public decimal ServiceTaxCessAmount{get;set;}
		public decimal TotalAmount{get;set;}
		public decimal SlabHigh{get;set;}
		public decimal SlabLow{get;set;}
        public int LocationID { get; set; }
		public decimal RateActive{get;set;}
		public decimal PRatePerBL{get;set;}
		public decimal PRatePerFEU{get;set;}
		public decimal PRatePerTEU{get;set;}
    
    }

}
