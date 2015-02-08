using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Entity
{
    [Serializable]
    public class AdjustmentModel
    {
        public string AdjustmentNo { get; set; }
        public int AdjustmentPk { get; set; }
        public DateTime AdjustmentDate { get; set; }
        public string JobNo { get; set; }
        public string InvoiceOrAdvNo { get; set; }
        public string DOrC { get; set; }
        public int CompanyID { get; set; }
        public int UserID { get; set; }
        public string DebtorOrCreditorName { get; set; }
        public bool Status { get; set; }
        public List<InvoiceJobAdjustment> LstInvoiceJobAdjustment { get; set; }
        public string PartyType { get; set; }
        public string PartyName { get; set; }
        public int AdvanceID { get; set; }
        public string AdvanceNo { get; set; }
    }

    [Serializable]
    public class InvoiceJobAdjustment
    {
        public long InvoiceJobAdjustmentPk { get; set; }
        public string InvoiceOrAdvNo { get; set; }
        public int invOrAdvID { get; set; }
        public string InvOrAdv { get; set; }
        public DateTime InvoiceOrAdvDate { get; set; }
        public Double DrAmount{ get; set; }
        public Double CrAmount { get; set; }
        public bool Status { get; set; }
    }
}
