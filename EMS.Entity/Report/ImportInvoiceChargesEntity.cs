using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EMS.Entity.Report
{
    class ImportInvoiceChargesEntity
    {
         #region Public Properties

        public int pk_BLID { get; set; }
        public int slno { get; set; }
        public string ChargeDescr { get; set; }
        public decimal GrossAmount  { get; set; }
        public string RtUSD { get; set; }
 
        #endregion

        #region Constructors

        public ImportInvoiceChargesEntity()
        {
            
        }

        public ImportInvoiceChargesEntity(DataTableReader reader)
        {
            this.slno = Convert.ToInt32(reader["Slno"]);
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
