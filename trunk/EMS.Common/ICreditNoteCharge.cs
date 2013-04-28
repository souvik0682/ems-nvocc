using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface ICreditNoteCharge
    {
        long CRNID { get; set; }
        long CRNChargeID { get; set; }
        string ChargeName { get; set; }
        decimal CRNAmount { get; set; }
        decimal CRBUSD { get; set; }
        decimal GrossCRNAmount { get; set; }
        decimal ServiceTaxAmount { get; set; }
        long TerminalID { get; set; }
        decimal ServiceTaxCessAmount { get; set; }
        decimal ServiceTaxACess { get; set; }
        decimal TotalServiceTax { get; set; }

        decimal ChargeAmount { get; set; }
        decimal ChargeServiceTax { get; set; }

        long ChargeId { get; set; }
    }
}
