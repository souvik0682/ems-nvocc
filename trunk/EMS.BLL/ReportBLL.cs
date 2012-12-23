using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.DAL;
using EMS.Entity.Report;
using System.Data;

namespace EMS.BLL
{
    public class ReportBLL
    {
        public static List<ImpBLChkLstEntity> GetImportBLCheckList()
        {
            return ReportDAL.GetImportBLCheckList();
        }

        //public static DataTable GetImportBLCheckList()
        //{
        //    return ReportDAL.GetImportBLCheckList();
        //}
    }
}
