using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IEstimate : ICommon
    {
        long EstimateID { get; set; }
        bool EstimateStatus { get; set; }
        string TransactionType { get; set; }
        DateTime EstimateDate { get; set; }
        string EstimateNo { get; set; }
        string PorR { get; set; }
        int fk_JobID { get; set; }
        int fk_BillFromID { get; set; }
        int CompanyID { get; set; }
        int CreditDays { get; set; }
        decimal Charges { get; set; }
    }
}
