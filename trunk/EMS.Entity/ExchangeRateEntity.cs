using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EMS.Common;

namespace EMS.Entity
{
    public class ExchangeRateEntity : IExchangeRate, ICommon
    {
        #region IExchangeRate Members

        public int ExchangeRateID
        {
            get;
            set;
        }

        public int CompanyID
        {
            get;
            set;
        }

        public DateTime ExchangeDate
        {
            get;
            set;
        }

        public decimal USDExchangeRate
        {
            get;
            set;
        }

        public int FreeDays
        {
            get;
            set;
        }

        #endregion

        #region ICommon Members

        public int CreatedBy
        {
            get;
            set;
        }

        public DateTime CreatedOn
        {
            get;
            set;
        }

        public int ModifiedBy
        {
            get;
            set;
        }

        public DateTime ModifiedOn
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public ExchangeRateEntity()
        {

        }

        public ExchangeRateEntity(DataTableReader reader)
        {
            this.ExchangeRateID = Convert.ToInt32(reader["ExchRateID"]);
            this.CompanyID = Convert.ToInt32(reader["CompanyID"]);
            this.ExchangeDate = Convert.ToDateTime(reader["ExchDate"]);
            this.USDExchangeRate = Convert.ToDecimal(reader["USDXchRate"]);
            //this.FreeDays = Convert.ToInt32(reader["FreeDays"]);
        }

        #endregion
    }
}
