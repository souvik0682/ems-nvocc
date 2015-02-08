using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Entity
{
    [Serializable]
    public class Estimate
    {

        public int EstimateId { get; set; }
        public string PartyName { get; set; }
        public int PartyId { get; set; }
        public string PartyType { get; set; }
        public int PartyTypeId { get; set; }
        //public string UnitType { get; set; }
        public int UnitTypeId { get; set; }
        public string BillFrom { get; set; }
        public int BillFromId { get; set; }
        public string PaymentIn { get; set; }
        public int TotalUnit { get; set; }
        public double TotalCharges { get; set; }
       
        public int CompanyID { get; set; }
        public int UserID { get; set; }

        public List<Charge> Charges { get; set; }

        public DateTime? EstimateDate { get; set; }

        public string EstimateNo { get; set; }
        public decimal ROE { get; set; }
        public int JobID { get; set; }
        public string JobNo { get; set; }
        public string PorR { get; set; }

        public string TransactionType { get; set; }

        public int? CreditDays { get; set; }
        public string JobActive { get; set; }
    }

    [Serializable]
    public class Charge
    {
        public int ChargeId { get; set; }
        public int ChargeMasterId { get; set; }
        public string ChargeMasterName { get; set; }
        public string ChargeType { get; set; }
        public string CntrSize { get; set; }
        public string UnitType { get; set; }
        public double Unit { get; set; }
        public double Rate { get; set; }
        public string  Currency { get; set; }
        public int StaxExists { get; set; }
        public double STax { get; set; }
        public double STaxCess { get; set; }
        public double STaxACess { get; set; }
        public int CurrencyId { get; set; }
        public double ROE { get; set; }
        public int UnitId { get; set; }
        public double INR{ get; set; } //{ get { return Unit * Rate * ROE; } }
    }
}
