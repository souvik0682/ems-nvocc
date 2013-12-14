using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using System.Data;

namespace EMS.Entity
{
    public class CreditNoteEntity : ICreditNote
    {
        public long CRNID
        {
            get;
            set;
        }

        public string CrnNo
        {
            get;
            set;
        }

        public DateTime CrnDate
        {
            get;
            set;
        }

        public long InvoiceID
        {
            get;
            set;
        }

        public int CompanyID
        {
            get;
            set;
        }

        public int LocationID
        {
            get;
            set;
        }

        public int NVOCCID
        {
            get;
            set;
        }

        public int InvoiceTypeID
        {
            get;
            set;
        }

        public int BookingID
        {
            get;
            set;
        }

        public string ExportImport
        {
            get;
            set;
        }

        public bool AllInFreight
        {
            get;
            set;
        }

        public int UserAdded
        {
            get;
            set;
        }

        public string LocationName
        {
            get;
            set;
        }

        public string NVOCCName
        {
            get;
            set;
        }

        public string InvoiceTypeName
        {
            get;
            set;
        }

        public long BLID
        {
            get;
            set;
        }

        public string BLNumber
        {
            get;
            set;
        }

        public string InvoiceNumber
        {
            get;
            set;
        }

        public DateTime InvoiceDate
        {
            get;
            set;
        }

        public string Containers
        {
            get;
            set;
        }

        public List<ICreditNoteCharge> CreditNoteCharges
        {
            get;
            set;
        }

        public CreditNoteEntity() { }

        public CreditNoteEntity(DataTableReader reader)
        {
            if (ColumnExists(reader, "InvoiceID"))
                if (reader["InvoiceID"] != DBNull.Value)
                    InvoiceID = Convert.ToInt64(reader["InvoiceID"]);

            if (ColumnExists(reader, "LocationID"))
                if (reader["LocationID"] != DBNull.Value)
                    LocationID = Convert.ToInt32(reader["LocationID"]);

            if (ColumnExists(reader, "LocName"))
                if (reader["LocName"] != DBNull.Value)
                    LocationName = Convert.ToString(reader["LocName"]);

            if (ColumnExists(reader, "NVOCCID"))
                if (reader["NVOCCID"] != DBNull.Value)
                    NVOCCID = Convert.ToInt32(reader["NVOCCID"]);

            if (ColumnExists(reader, "ProspectName"))
                if (reader["ProspectName"] != DBNull.Value)
                    NVOCCName = Convert.ToString(reader["ProspectName"]);

            if (ColumnExists(reader, "ExportImport"))
                if (reader["ExportImport"] != DBNull.Value)
                    ExportImport = Convert.ToString(reader["ExportImport"]);

            if (ColumnExists(reader, "InvoiceTypeID"))
                if (reader["InvoiceTypeID"] != DBNull.Value)
                    InvoiceTypeID = Convert.ToInt32(reader["InvoiceTypeID"]);

            if (ColumnExists(reader, "InvoiceTypeName"))
                if (reader["InvoiceTypeName"] != DBNull.Value)
                    InvoiceTypeName = Convert.ToString(reader["InvoiceTypeName"]);

            if (ColumnExists(reader, "BLID"))
                if (reader["BLID"] != DBNull.Value)
                    BLID = Convert.ToInt64(reader["BLID"]);

            if (ColumnExists(reader, "BLNo"))
                if (reader["BLNo"] != DBNull.Value)
                    BLNumber = Convert.ToString(reader["BLNo"]);

            if (ColumnExists(reader, "InvoiceNo"))
                if (reader["InvoiceNo"] != DBNull.Value)
                    InvoiceNumber = Convert.ToString(reader["InvoiceNo"]);

            if (ColumnExists(reader, "InvoiceDate"))
                if (reader["InvoiceDate"] != DBNull.Value)
                    InvoiceDate = Convert.ToDateTime(reader["InvoiceDate"]);

            if (ColumnExists(reader, "Containers"))
                if (reader["Containers"] != DBNull.Value)
                    Containers = Convert.ToString(reader["Containers"]);

            if (ColumnExists(reader, "CRNID"))
                if (reader["CRNID"] != DBNull.Value)
                    CRNID = Convert.ToInt64(reader["CRNID"]);

            if (ColumnExists(reader, "CrnNo"))
                if (reader["CrnNo"] != DBNull.Value)
                    CrnNo = Convert.ToString(reader["CrnNo"]);

            if (ColumnExists(reader, "CrnDate"))
                if (reader["CrnDate"] != DBNull.Value)
                    CrnDate = Convert.ToDateTime(reader["CrnDate"]);
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
