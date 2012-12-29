using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace EMS.Entity.Report
{
    public class ImpRegisterSummaryEntity
    {
        #region Public Properties

        public string Location { get; set; }

        public string Line { get; set; }

        public string VesselName { get; set; }

        public string VoyageNo { get; set; }

        public DateTime DischargeDate { get; set; }

        public string BLNo { get; set; }

        public int? TEU { get; set; }

        public int? FEU { get; set; }

        public int? TotalTEU { get; set; }

        public string InvoiceReference { get; set; }

        public decimal? DO { get; set; }

        public decimal? Docs { get; set; }

        public decimal? Survey { get; set; }

        public decimal? Washing { get; set; }

        public decimal? Others { get; set; }

        public string SecurityInvoice { get; set; }

        public decimal? SecurityDeposit { get; set; }

        public string LastInvoice { get; set; }

        public decimal? DetentionUSD { get; set; }

        public decimal? DetentionINR { get; set; }

        public decimal? Repairing { get; set; }

        public decimal? LastInvoiceOtherCharge { get; set; }

        public decimal? TotalCharge { get; set; }

        public decimal? TotalReceipt { get; set; }

        public decimal? Refund { get; set; }

        public string Remarks { get; set; }

        #endregion
    }
}