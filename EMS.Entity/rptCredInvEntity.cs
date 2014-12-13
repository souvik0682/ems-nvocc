using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Entity
{
    public class rptCredInvEntity
    {
        public string JobNo { get; set; }
        public string Creditor { get; set; }
        public DateTime JobDate { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal BillValue { get; set; }
        public decimal ServiceTax { get; set; }
        public decimal PayableAmount { get; set; }
        public decimal PaymentAmount { get; set; }
    }
}
