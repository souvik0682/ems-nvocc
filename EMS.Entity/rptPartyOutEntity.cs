using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Entity
{
    public class rptPartyOutEntity
    {
        public String JobNo { get; set; }
        public DateTime JobDate { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public String LocName { get; set; }
        public decimal InvValue { get; set; }
        public decimal Receipts { get; set; }
        public decimal CreditNotes { get; set; }
        public decimal Adjustments { get; set; }
        public decimal Outstanding { get; set; }
        public decimal Out30 { get; set; }
        public decimal Out60 { get; set; }
        public decimal Out90 { get; set; }
        public decimal Out180 { get; set; }
        public decimal Out180p { get; set; }
    }
}
