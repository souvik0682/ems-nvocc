using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IBookingCharges : ICommon
    {
        long BookingChargeId { get; set; }
        long BookingId { get; set; }
        int ChargeId { get; set; }
        string ChargeName { get; set; }
        int ChargeRateId { get; set; }
        int CurrencyId { get; set; }
        string CurrencyName { get; set; }
        bool ChargeApplicable { get; set; }
        string Size { get; set; }
        int ContainerTypeId { get; set; }
        string ContainerType { get; set; }
        int DocumentTypeID {get; set;}
        decimal WtInCBM { get; set; }
        decimal WtInTon { get; set; }
        decimal ActualRate { get; set; }
        decimal ManifestRate { get; set; }
        decimal RefundAmount { get; set; }
        decimal BrokerageBasic { get; set; }
        bool ChargeStatus { get; set; }
        int ChgExist { get; set; }
        int Unit { get; set; }
        bool ChargedEditable { get; set; }
        bool RefundEditable { get; set; }
        bool BrokerageEditable { get; set; }
        bool ManifestEditabe { get; set; }
    }
}
