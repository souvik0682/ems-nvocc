using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.DAL;
using System.Data;

namespace EMS.BLL
{
    public class CreditNoteBLL
    {
        public ICreditNote GetHeaderInformation(int LineId, int LocationId, int InvoiceId)
        {
            return CreditNoteDAL.GetHeaderInformation(LineId, LocationId, InvoiceId);
        }

        public DataTable GetAllCharges(string InvoiceNo)
        {
            return CreditNoteDAL.GetAllCharges(InvoiceNo);
        }

        public ICreditNoteCharge GetChargeDetails(int ChargeId, string InvoiceNo)
        {
            return CreditNoteDAL.GeChargeDetails(ChargeId, InvoiceNo);
        }

        public long SaveCreditNote(ICreditNote CreditNote)
        {
            long creditNoteId = 0;
            long creditNoteChargeId = 0;

            creditNoteId = CreditNoteDAL.SaveCreditNoteHeader(CreditNote);

            if (creditNoteId > 0)
            {
                if (!ReferenceEquals(CreditNote.CreditNoteCharges, null))
                {
                    foreach (ICreditNoteCharge cRate in CreditNote.CreditNoteCharges)
                    {
                        cRate.CRNID = creditNoteId;
                        creditNoteChargeId = CreditNoteDAL.SaveCreditNoteFooter(cRate);
                    }
                }
            }

            return creditNoteId;
        }

        public ICreditNote GetCreditNoteForView(long CreditNoteId)
        {
            ICreditNote creditNote = CreditNoteDAL.GetCreditNoteHeaderForView(CreditNoteId);
            creditNote.CreditNoteCharges = CreditNoteDAL.GetCreditNoteFooterForView(CreditNoteId);

            return creditNote;
        }
    }
}
