using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface ISettlement : ICommon
    {
        int CompanyID { get; set; }
        int LocationID { get; set; }
        Int64 BLID { get; set; }
        int NVOCCID { get; set; }
        int pk_SettlementID { get; set; }
        string BLNo { get; set; }
        string SettlementNo { get; set; }
        DateTime SettlementDate { get; set; }
        decimal SettlementAmount { get; set; }
        decimal OutstandingAmount { get; set; }
        string PorR { get; set; }
        string LocName { get; set; }
        string Line { get; set; }
        string PayRcvd { get; set; }
        string ChequeDetail { get; set; }
        string BankName { get; set; }

    }
}
