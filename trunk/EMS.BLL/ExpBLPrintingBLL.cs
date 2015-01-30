using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.DAL.DbManager;
using System.Data;
using System.Data.SqlClient;
using EMS.Entity;

namespace EMS.BLL
{
    public static class  ExpBLPrintingBLL
    {
        public static BLPrintModel GetxpBLPrinting(ISearchCriteria searchCriteria)
        {
            return EMS.DAL.ExpBLPrintingDAL.GetxpBLPrinting(searchCriteria);
        }
        public static DataSet GetxpBLPrintingDS(ISearchCriteria searchCriteria)
        {
            return EMS.DAL.ExpBLPrintingDAL.GetxpBLPrintingDS(searchCriteria);
        }

        public static DataSet GetfwdBLPrintingDS(ISearchCriteria searchCriteria)
        {
            return EMS.DAL.ExpBLPrintingDAL.GetfwdBLPrintingDS(searchCriteria);
        }
        public static bool CheckDraftOrOriginal(string expBLNo)
        {
            return EMS.DAL.ExpBLPrintingDAL.CheckDraftOrOriginal(expBLNo);
        }
    }
}
