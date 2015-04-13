using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Entity
{
    public class rptFwdCollEntity
    {
        public String JobNo { get; set; }
        public DateTime JobDate { get; set; }
        public string MRNo { get; set; }
        public DateTime MRDate { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal CashPayment { get; set; }
        public decimal ChequePayment { get; set; }
        public decimal TDSDeducted { get; set; }
        public string PartyName { get; set; }
        public string ChequeBank { get; set; }
        public string ChequeNo { get; set; }
        public DateTime ChequeDate { get; set; }
        public string LocAbbr { get; set; }
    }
}
