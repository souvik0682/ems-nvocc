using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface ICreditNote
    {
        long CRNID { get; set; }
        string CrnNo { get; set; }
        DateTime CrnDate { get; set; }
        long InvoiceID { get; set; }
        int CompanyID { get; set; }
        int LocationID { get; set; }
        int NVOCCID { get; set; }
        int InvoiceTypeID { get; set; }
        int BookingID { get; set; }
        string ExportImport { get; set; }
        bool AllInFreight { get; set; }
        int UserAdded { get; set; }
        string LocationName
        {
            get;
            set;
        }
        string NVOCCName
        {
            get;
            set;
        }
        string InvoiceTypeName
        {
            get;
            set;
        }
        long BLID
        {
            get;
            set;
        }

        string BLNumber
        {
            get;
            set;
        }

        string InvoiceNumber
        {
            get;
            set;
        }

        DateTime InvoiceDate
        {
            get;
            set;
        }

        string Containers
        {
            get;
            set;
        }

        List<ICreditNoteCharge> CreditNoteCharges { get; set; }
    }
}
