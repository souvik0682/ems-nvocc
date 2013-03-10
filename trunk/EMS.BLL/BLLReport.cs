using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.BLL
{
   public class BLLReport
    {

        public static System.Data.DataSet GetMoneyRcptDetails(string invNo)
        {
            return EMS.DAL.DALReport.GetMoneyRcptDetails(invNo);
        }

        public static System.Data.DataSet FillDDLMoneyRcpt(string invNo)
        {
            return EMS.DAL.DALReport.FillDDLMoneyRcpt(invNo);
        }
    }
}
