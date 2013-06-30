using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.Entity;
using EMS.DAL;
using System.Data;


namespace EMS.BLL
{
    public class TestBLL
    {
        public DataTable GetTypeWiseStockSummary(string LineId, string LocationId, DateTime StockDate)
        {
            return TestDAL.GetTypeWiseStockSummary(LineId, LocationId, StockDate);
        }
    }
}
