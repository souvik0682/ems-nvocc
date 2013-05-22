using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EMS.Entity.Report
{
    public class ImpInvRegisterEntity
    {
        #region Public Properties

        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public Decimal NetAmt { get; set; }
        public string ImpLineBLNo { get; set; }
        public string IGMBLNumber { get; set; }
        public string POL { get; set; }
        public string POD { get; set; }
        public string FPOD { get; set; }
        public int NoofTEU { get; set; }
        public int NoofFEU { get; set; }
        public Decimal Mrtotal { get; set; }
        public Decimal Crntotal { get; set; }
        public string ConsigneeName { get; set; }
        public string AddrName { get; set; }
        public string VesselDetail { get; set; }

        #endregion

        #region Constructors

        public ImpInvRegisterEntity()
        {

        }

        public ImpInvRegisterEntity(DataTableReader reader)
        {
            this.InvoiceNo = Convert.ToString(reader["InvoiceNo"]);
            if (reader["InvoiceDate"] != DBNull.Value) this.InvoiceDate = Convert.ToDateTime(reader["InvoiceDate"]);
            if (reader["netamt"] != DBNull.Value) this.NetAmt = Convert.ToDecimal(reader["netamt"]);
            this.ImpLineBLNo = Convert.ToString(reader["ImpLineBLNo"]);
            this.IGMBLNumber = Convert.ToString(reader["IGMBLNumber"]);
            this.POL = Convert.ToString(reader["pol"]);
            this.POD = Convert.ToString(reader["pod"]);
            this.FPOD = Convert.ToString(reader["fpod"]);
            if (reader["NoofTEU"] != DBNull.Value) this.NoofTEU = Convert.ToInt32(reader["NoofTEU"]);
            if (reader["NoofFEU"] != DBNull.Value) this.NoofFEU = Convert.ToInt32(reader["NoofFEU"]);
            if (reader["mrtotal"] != DBNull.Value) this.Mrtotal = Convert.ToDecimal(reader["mrtotal"]);
            if (reader["crntotal"] != DBNull.Value) this.Crntotal = Convert.ToDecimal(reader["crntotal"]);
            this.ConsigneeName = Convert.ToString(reader["ConsigneeName"]);
            this.AddrName = Convert.ToString(reader["AddrName"]);
            this.VesselDetail = Convert.ToString(reader["VesselDetail"]);
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
