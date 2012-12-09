using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface ICharge : ICommon
    {
        int ChargesID { get; set; }
        int CompanyID { get; set; }
        string ChargeDescr { get; set; }
        char ChargeType { get; set; }
        char IEC { get; set; }
        int NVOCCID { get; set; }
        int Sequence { get; set; }
        bool RateChangeable { get; set; }
        bool ChargeActive { get; set; }
    }
}
