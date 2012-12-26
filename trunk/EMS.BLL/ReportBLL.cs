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
        public static List<ImpBLChkLstEntity> GetImportBLCheckList(int lineId, int locId, Int64 voyageId, Int64 vesselId)
        {
            return ReportDAL.GetImportBLCheckList(lineId, locId, voyageId, vesselId);
        }

        public static List<ImpRegisterEntity> GetImportRegister(int lineId, int locId, Int64 voyageId, Int64 vesselId)
        {
            return ReportDAL.GetImportRegister(lineId, locId, voyageId, vesselId);
        }
    }
}
