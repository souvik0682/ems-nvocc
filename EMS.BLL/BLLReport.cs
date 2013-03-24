using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.BLL
{
   public class BLLReport
    {

        //public static System.Data.DataSet GetMoneyRcptDetails(string invNo)
       public static System.Data.DataSet GetMoneyRcptDetails(Int64 moneyRecptNo)
        {
            return EMS.DAL.DALReport.GetMoneyRcptDetails(moneyRecptNo);
        }

        public static System.Data.DataSet FillDDLMoneyRcpt(string invNo)
        {
            return EMS.DAL.DALReport.FillDDLMoneyRcpt(invNo);
        }

        public static System.Data.DataSet GetImpBill(string BLRefNo, string dt)
        {
            return EMS.DAL.DALReport.GetImpBill(BLRefNo, dt);
        }


        public static string GetAddByCompName(string compname)
        {
            return EMS.DAL.DALReport.GetAddByCompName(compname);
        }

        public static string GetNumToWords(long num)
        {
            return EMS.DAL.DALReport.GetNumToWords(num);
        }


    }
}
