using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EMS.Entity.Report
{
    public class ImportInvoiceEntity
    {
        #region Public Properties

        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Billto { get; set; }
        public string BilltoDetail { get; set; }
        public string DocHeading { get; set; }
        public string BLNo { get; set; }
        public string LoadPort { get; set; }
        public decimal GrossWeight { get; set; }
        public string UnitCode { get; set; }
        public string MainLineVessel { get; set; }
        public string Berth { get; set; }
        public string ExVessel { get; set; }
        public decimal ExchRate { get; set; }
        public string ImportRotationNo { get; set; }
        public string VoyageNo { get; set; }
        public int Total20 { get; set; }
        public int Total40 { get; set; }
        public DateTime IGMDate { get; set; }
        public DateTime GeneralLandingDate { get; set; }
        public string LineNos { get; set; }
        public int DetetionFree { get; set; }
        public int PGRFree { get; set; }
        public string CompName { get; set; }
        public string CompAddress { get; set; }
        public string CompCity { get; set; }
        public string CompPIN { get; set; }
        public DateTime Datee { get; set; }
        public string DebitNoteNo { get; set; }
        public string Cha  { get; set; }
        public string BankDetail { get; set; }
        public string BankDetail1 { get; set; }
        public string BankDetail2 { get; set; }
        public string BankDetail3 { get; set; }
        public string BankDetail4 { get; set; }
        public string BankDetail5 { get; set; }
        public string BankDetail6 { get; set; }
        public string BankDetail7 { get; set; }
        public decimal taxper { get; set; }
        public decimal TaxCessper { get; set; }
        public decimal TaxAddCessper { get; set; }
        public decimal gross { get; set; }
        public decimal stax { get; set; }
        public decimal staxcess { get; set; }
        public decimal staxacess { get; set; }
        public int fk_LocationID { get; set; }
        public int Slno { get; set; }
        public string ChargeDescr { get; set; }
        public decimal GrossAmount { get; set; }
        public string RtUSD { get; set; }

        #endregion

        #region Constructors

        public ImportInvoiceEntity()
        {
            
        }

        public ImportInvoiceEntity(DataTableReader reader)
        {
            this.InvoiceNo = Convert.ToString(reader["InvoiceNo"]);
            if (reader["InvoiceDate"] != DBNull.Value) this.InvoiceDate = Convert.ToDateTime(reader["InvoiceDate"]);
            if (reader["Billto"] != DBNull.Value) this.Billto = Convert.ToString(reader["Billto"]);
            if (reader["BilltoDetail"] != DBNull.Value) this.Billto = Convert.ToString(reader["BilltoDetail"]);
            if (reader["DocHeading"] != DBNull.Value) this.DocHeading = Convert.ToString(reader["DocHeading"]);
            if (reader["BLNo"] != DBNull.Value) this.BLNo = Convert.ToString(reader["BLNo"]);
            if (reader["LoadPort"] != DBNull.Value) this.LoadPort = Convert.ToString(reader["LoadPort"]);
            if (reader["GrossWeight"] != DBNull.Value) this.GrossWeight = Convert.ToDecimal(reader["GrossWeight"]);
            if (reader["UnitCode"] != DBNull.Value) this.UnitCode = Convert.ToString(reader["UnitCode"]);
            if (reader["MainLineVessel"] != DBNull.Value) this.MainLineVessel = Convert.ToString(reader["MainLineVessel"]);
            if (reader["Berth"] != DBNull.Value) this.Berth = Convert.ToString(reader["Berth"]);
            if (reader["ExVessel"] != DBNull.Value) this.ExVessel = Convert.ToString(reader["ExVessel"]);
            if (reader["ImportRotationNo"] != DBNull.Value) this.ImportRotationNo = Convert.ToString(reader["ImportRotationNo"]);
            if (reader["VoyageNo"] != DBNull.Value) this.VoyageNo = Convert.ToString(reader["VoyageNo"]);
            if (reader["Berth"] != DBNull.Value) this.Berth = Convert.ToString(reader["Berth"]);
            if (reader["Total20"] != DBNull.Value) this.Total20 = Convert.ToInt32(reader["Total20"]);
            if (reader["Total40"] != DBNull.Value) this.Total40 = Convert.ToInt32(reader["Total40"]);

            if (reader["ExchRate"] != DBNull.Value) this.ExchRate = Convert.ToDecimal(reader["ExchRate"]);
            if (reader["IGMDate"] != DBNull.Value) this.IGMDate = Convert.ToDateTime(reader["IGMDate"]);
            if (reader["GeneralLandingDate"] != DBNull.Value) this.GeneralLandingDate = Convert.ToDateTime(reader["GeneralLandingDate"]);
            if (reader["LineNos"] != DBNull.Value) this.LineNos = Convert.ToString(reader["LineNos"]);
            if (reader["DetetionFree"] != DBNull.Value) this.DetetionFree = Convert.ToInt32(reader["DetetionFree"]);
            if (reader["PGRFree"] != DBNull.Value) this.PGRFree = Convert.ToInt32(reader["PGRFree"]);
            if (reader["CompName"] != DBNull.Value) this.CompName = Convert.ToString(reader["CompName"]);
            if (reader["CompAddress"] != DBNull.Value) this.CompAddress = Convert.ToString(reader["CompAddress"]);
            if (reader["CompCity"] != DBNull.Value) this.CompCity = Convert.ToString(reader["CompCity"]);
            if (reader["CompPIN"] != DBNull.Value) this.CompPIN = Convert.ToString(reader["CompPIN"]);
            if (reader["DebitNoteNo"] != DBNull.Value) this.DebitNoteNo = Convert.ToString(reader["DebitNoteNo"]);
            if (reader["Datee"] != DBNull.Value) this.Datee = Convert.ToDateTime(reader["Datee"]);
            if (reader["Cha"] != DBNull.Value) this.Cha = Convert.ToString(reader["Cha"]);
            if (reader["BankDetail"] != DBNull.Value) this.BankDetail = Convert.ToString(reader["BankDetail"]);
            if (reader["BankDetail1"] != DBNull.Value) this.BankDetail1 = Convert.ToString(reader["BankDetail1"]);
            if (reader["BankDetail2"] != DBNull.Value) this.BankDetail2 = Convert.ToString(reader["BankDetail2"]);
            if (reader["BankDetail3"] != DBNull.Value) this.BankDetail3 = Convert.ToString(reader["BankDetail3"]);
            if (reader["BankDetail4"] != DBNull.Value) this.BankDetail4 = Convert.ToString(reader["BankDetail4"]);
            if (reader["BankDetail5"] != DBNull.Value) this.BankDetail5 = Convert.ToString(reader["BankDetail5"]);
            if (reader["BankDetail6"] != DBNull.Value) this.BankDetail6 = Convert.ToString(reader["BankDetail6"]);
            if (reader["BankDetail7"] != DBNull.Value) this.BankDetail7 = Convert.ToString(reader["BankDetail7"]);
            if (reader["fk_LocationID"] != DBNull.Value) this.fk_LocationID = Convert.ToInt32(reader["fk_LocationID"]);
            if (reader["taxper"] != DBNull.Value) this.taxper = Convert.ToDecimal(reader["taxper"]);
            if (reader["TaxCessper"] != DBNull.Value) this.TaxCessper = Convert.ToDecimal(reader["TaxCessper"]);
            if (reader["TaxAddCessper"] != DBNull.Value) this.TaxAddCessper = Convert.ToDecimal(reader["TaxAddCessper"]);
            if (reader["gross"] != DBNull.Value) this.gross = Convert.ToDecimal(reader["gross"]);
            if (reader["stax"] != DBNull.Value) this.stax = Convert.ToDecimal(reader["stax"]);
            if (reader["staxacess"] != DBNull.Value) this.staxacess = Convert.ToDecimal(reader["staxacess"]);
            if (reader["staxcess"] != DBNull.Value) this.staxcess = Convert.ToDecimal(reader["staxcess"]);
            if (reader["Slno"] != DBNull.Value) this.Slno = Convert.ToInt32(reader["Slno"]);
            if (reader["ChargeDescr"] != DBNull.Value) this.ChargeDescr = Convert.ToString(reader["ChargeDescr"]);
            if (reader["GrossAmount"] != DBNull.Value) this.GrossAmount = Convert.ToDecimal(reader["GrossAmount"]);
            if (reader["RtUSD"] != DBNull.Value) this.RtUSD = Convert.ToString(reader["RtUSD"]);
        }

        #endregion

        #region Private Methods

        private bool HasColumn(DataTableReader reader, string columnName)
        {
            try
            {
                return reader.GetOrdinal(columnName) >= 0;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        #endregion
    }
}
