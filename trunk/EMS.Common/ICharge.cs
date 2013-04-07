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
        int ChargeType { get; set; }
        char IEC { get; set; }
        int NVOCCID { get; set; }
        int Sequence { get; set; }
        bool RateChangeable { get; set; }
        bool ChargeActive { get; set; }
        bool IsFreightComponent { get; set; }
        DateTime EffectDt { get; set; }
        bool IsTerminal { get; set; }
        bool IsWashing { get; set; }
        bool PrincipleSharing { get; set; }
        int Currency { get; set; }
        bool ServiceTax { get; set; }
        List<IChargeRate> ChargeRates { get; set; }
        int Location { get; set; }
        bool IsSpecialRate { get; set; }
        char DeliveryMode { get; set; }
        int DocumentType { get; set; }

        string ConvertListToXML(List<IChargeRate> Items);
    }
}
