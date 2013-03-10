using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using System.Data;

namespace EMS.Entity
{
    public class InvoiceEntity : IInvoice
    {
        public long InvoiceID { get; set; }
        public int CompanyID { get; set; }
        public int LocationID { get; set; }
        public int NVOCCID { get; set; }
        public int InvoiceTypeID { get; set; }
        public long BookingID { get; set; }
        public long BLID { get; set; }
        public string ExportImport { get; set; }
        public decimal XchangeRate { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int CHAID { get; set; }
        public string Account { get; set; }
        public bool AllInFreight { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal ServiceTax { get; set; }
        public decimal ServiceTaxCess { get; set; }
        public decimal ServiceTaxACess { get; set; }
        public int UserAdded { get; set; }
        public int UserLastEdited { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime EditedOn { get; set; }
        public string BLNo { get; set; }
        public DateTime BLDate { get; set; }
        public decimal BillAmount { get; set; }

        public List<IChargeRate> ChargeRates { get; set; }

        public InvoiceEntity()
        {
        }
        public InvoiceEntity(DataTableReader reader)
        {
            if (ColumnExists(reader, "InvoiceID"))
                if (reader["InvoiceID"] != DBNull.Value)
                    InvoiceID = Convert.ToInt64(reader["InvoiceID"]);

            if (ColumnExists(reader, "CompanyID"))
                if (reader["CompanyID"] != DBNull.Value)
                    CompanyID = Convert.ToInt32(reader["CompanyID"]);

            if (ColumnExists(reader, "LocationID"))
                if (reader["LocationID"] != DBNull.Value)
                    LocationID = Convert.ToInt32(reader["LocationID"]);

            if (ColumnExists(reader, "NVOCCID"))
                if (reader["NVOCCID"] != DBNull.Value)
                    NVOCCID = Convert.ToInt32(reader["NVOCCID"]);

            if (ColumnExists(reader, "InvoiceTypeID"))
                if (reader["InvoiceTypeID"] != DBNull.Value)
                    InvoiceTypeID = Convert.ToInt32(reader["InvoiceTypeID"]);

            if (ColumnExists(reader, "BookingID"))
                if (reader["BookingID"] != DBNull.Value)
                    BookingID = Convert.ToInt32(reader["BookingID"]);

            if (ColumnExists(reader, "BLID"))
                if (reader["BLID"] != DBNull.Value)
                    BLID = Convert.ToInt64(reader["BLID"]);

            if (ColumnExists(reader, "ExportImport"))
                if (reader["ExportImport"] != DBNull.Value)
                    ExportImport = Convert.ToString(reader["ExportImport"]);

            if (ColumnExists(reader, "XchangeRate"))
                if (reader["XchangeRate"] != DBNull.Value)
                    XchangeRate = Convert.ToDecimal(reader["XchangeRate"]);

            if (ColumnExists(reader, "InvoiceNo"))
                if (reader["InvoiceNo"] != DBNull.Value)
                    InvoiceNo = Convert.ToString(reader["InvoiceNo"]);

            if (ColumnExists(reader, "InvoiceDate"))
                if (reader["InvoiceDate"] != DBNull.Value)
                    InvoiceDate = Convert.ToDateTime(reader["InvoiceDate"]);

            if (ColumnExists(reader, "CHAID"))
                if (reader["CHAID"] != DBNull.Value)
                    CHAID = Convert.ToInt32(reader["CHAID"]);

            if (ColumnExists(reader, "ServiceTaxAmount"))
                if (reader["ServiceTaxAmount"] != DBNull.Value)
                    Account = Convert.ToString(reader["Account"]);

            if (ColumnExists(reader, "Account"))
                if (reader["Account"] != DBNull.Value)
                    AllInFreight = Convert.ToBoolean(reader["AllInFreight"]);

            if (ColumnExists(reader, "GrossAmount"))
                if (reader["GrossAmount"] != DBNull.Value)
                    GrossAmount = Convert.ToDecimal(reader["GrossAmount"]);

            if (ColumnExists(reader, "ServiceTax"))
                if (reader["ServiceTax"] != DBNull.Value)
                    ServiceTax = Convert.ToDecimal(reader["ServiceTax"]);

            if (ColumnExists(reader, "ServiceTaxCess"))
                if (reader["ServiceTaxCess"] != DBNull.Value)
                    ServiceTaxCess = Convert.ToDecimal(reader["ServiceTaxCess"]);

            if (ColumnExists(reader, "ServiceTaxACess"))
                if (reader["ServiceTaxACess"] != DBNull.Value)
                    ServiceTaxACess = Convert.ToDecimal(reader["ServiceTaxACess"]);

            if (ColumnExists(reader, "UserAdded"))
                if (reader["UserAdded"] != DBNull.Value)
                    UserAdded = Convert.ToInt32(reader["UserAdded"]);

            if (ColumnExists(reader, "UserLastEdited"))
                if (reader["UserLastEdited"] != DBNull.Value)
                    UserLastEdited = Convert.ToInt32(reader["UserLastEdited"]);

            if (ColumnExists(reader, "AddedOn"))
                if (reader["AddedOn"] != DBNull.Value)
                    AddedOn = Convert.ToDateTime(reader["AddedOn"]);

            if (ColumnExists(reader, "EditedOn"))
                if (reader["EditedOn"] != DBNull.Value)
                    EditedOn = Convert.ToDateTime(reader["EditedOn"]);

            if (ColumnExists(reader, "BLNo"))
                if (reader["BLNo"] != DBNull.Value)
                    BLNo = Convert.ToString(reader["BLNo"]);

            if (ColumnExists(reader, "BLDate"))
                if (reader["BLDate"] != DBNull.Value)
                    BLDate = Convert.ToDateTime(reader["BLDate"]);

            if (ColumnExists(reader, "BillAmount"))
                if (reader["BillAmount"] != DBNull.Value)
                    BillAmount = Convert.ToDecimal(reader["BillAmount"]);
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
