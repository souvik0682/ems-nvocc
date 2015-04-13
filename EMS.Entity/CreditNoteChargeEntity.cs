using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using System.Data;

namespace EMS.Entity
{
    [Serializable]
    public class CreditNoteChargeEntity : ICreditNoteCharge
    {
        public long CRNID
        {
            get;
            set;
        }

        public long CRNChargeID
        {
            get;
            set;
        }

        public string ChargeName { get; set; }

        public decimal CRNAmount
        {
            get;
            set;
        }

        public decimal CRBUSD
        {
            get;
            set;
        }

        public decimal GrossCRNAmount
        {
            get;
            set;
        }

        public decimal ServiceTaxAmount
        {
            get;
            set;
        }

        public long TerminalID
        {
            get;
            set;
        }

        public decimal ServiceTaxCessAmount
        {
            get;
            set;
        }

        public decimal ServiceTaxACess
        {
            get;
            set;
        }

        public decimal TotalServiceTax { get; set; }
        public decimal STaxPer { get; set; }

        public decimal ChargeAmount { get; set; }
        public decimal ChargeServiceTax { get; set; }

        public long ChargeId { get; set; }


        public CreditNoteChargeEntity()
        {
        }

        public CreditNoteChargeEntity(DataTableReader reader)
        {
            if (ColumnExists(reader, "ChargeID"))
                if (reader["ChargeID"] != DBNull.Value)
                    ChargeId = Convert.ToInt64(reader["ChargeID"]);

            if (ColumnExists(reader, "ChargeName"))
                if (reader["ChargeName"] != DBNull.Value)
                    ChargeName = Convert.ToString(reader["ChargeName"]);

            if (ColumnExists(reader, "CRNID"))
                if (reader["CRNID"] != DBNull.Value)
                    CRNID = Convert.ToInt64(reader["CRNID"]);

            if (ColumnExists(reader, "GrossAmount"))
                if (reader["GrossAmount"] != DBNull.Value)
                    GrossCRNAmount = Convert.ToDecimal(reader["GrossAmount"]);

            if (ColumnExists(reader, "ServiceTax"))
                if (reader["ServiceTax"] != DBNull.Value)
                    ServiceTaxAmount = Convert.ToDecimal(reader["ServiceTax"]);

            if (ColumnExists(reader, "ChargeInvoice"))
                if (reader["ChargeInvoice"] != DBNull.Value)
                    ChargeAmount = Convert.ToDecimal(reader["ChargeInvoice"]);

            if (ColumnExists(reader, "InvoiceServiceTax"))
                if (reader["InvoiceServiceTax"] != DBNull.Value)
                    ChargeServiceTax = Convert.ToDecimal(reader["InvoiceServiceTax"]);

            if (ColumnExists(reader, "ServTaxPer"))
                if (reader["ServTaxPer"] != DBNull.Value)
                    STaxPer = Convert.ToDecimal(reader["ServTaxPer"]);

        }

        public bool ColumnExists(IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).ToUpper() == columnName.ToUpper())
                {
                    return true;
                }
            }

            return false;
        }
    }
}
