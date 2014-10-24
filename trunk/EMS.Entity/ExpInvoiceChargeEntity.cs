﻿// -----------------------------------------------------------------------
// <copyright file="ExpInvoiceCharge.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
namespace EMS.Entity
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>r
    [Serializable]
    public class ExpInvoiceChargeEntity : IExpInvoiceCharge
    {
        public long InvoiceId{get;set;}
		public long InvoiceChargeId{get;set;}
		public int ChargesId{get;set;}
		public string ChargeName{get;set;}
		public int TerminalId{get;set;}
		public string TerminalName{get;set;}
		public decimal RatePerBL{get;set;}
		public decimal RatePerTEU{get;set;}
		public decimal RatePerFEU{get;set;}
		public decimal RatePerCBM{get;set;}
		public decimal RatePerTon{get;set;}
		public int fk_CurrencyID{get;set;}
		public decimal GrossAmount{get;set;}
		public decimal ServiceTaxAmount{get;set;}
		public decimal ExchgRate{get;set;}
		public decimal ServiceTaxACess{get;set;}
		public decimal ServiceTaxCessAmount{get;set;}
		public decimal TotalAmount{get;set;}
        public int ChargesRateId { get; set; }
        //public decimal SlabHigh{get;set;}
        //public decimal SlabLow{get;set;}
        public int LocationId { get; set; }
		public bool RateActive{get;set;}
        //public decimal PRatePerBL{get;set;}
        //public decimal PRatePerFEU{get;set;}
        //public decimal PRatePerTEU{get;set;}
    
    }

}