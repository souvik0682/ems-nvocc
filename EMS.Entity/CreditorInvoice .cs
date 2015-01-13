using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Entity
{
    [Serializable]
    public class CreditorInvoice
    {
        public int CreditorInvoiceId { get; set; }
        public int CreditorId { get; set; }
        public string CreditorName { get; set; }
        public string JobNumber { get; set; }
        public string CreInvoiceNo { get; set; }
        public string OurInvoiceRef { get; set; }
        public string HouseBLNo { get; set; }
        public string Location { get; set; }
        public DateTime? CreInvoiceDate { get; set; }
        public DateTime? ReferenceDate { get; set; }
        public DateTime? HouseBLDate { get; set; }
        public double InvoiceAmount { get; set; }
        public double RoundingOff { get; set; }
        public double ROE { get; set; }
        public int PartyTypeID { get; set; }
        public List<CreditorInvoiceCharge> CreditorInvoiceCharges { get; set; }


        public int? CompanyID { get; set; }
        public int? UserID { get; set; }
    }
     [Serializable]
    public class CreditorInvoiceCharge
    {
        public int CreditorInvoiceChargeId { get; set; }
        public int ChargeId { get; set; }
        public string ChargeName { get; set; }

        public double Rate { get; set; }
        public double Unit { get; set; }
        public double Total { get; set; }
        public string CntrSize { get; set; }

        public int CurrencyId { get; set; }
        public string Currency { get; set; }
        public double ConvRate { get; set; }
        public string UnitType { get; set; }
        public int UnitTypeID { get; set; }

        public double Gross { get; set; }
        public double STaxPercentage { get; set; }
        public double STax { get; set; }
        public double STaxCess { get; set; }
        public double STaxACess { get; set; }

        public double GTotal { get; set; }
    }
}
