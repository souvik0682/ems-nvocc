using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IExchangeRate : ICommon
    {
        int ExchangeRateID { get; set; }
        int CompanyID { get; set; }
        DateTime ExchangeDate { get; set; }
        decimal USDExchangeRate { get; set; }
        int FreeDays { get; set; }
    }
}
