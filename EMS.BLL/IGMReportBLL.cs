using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EMS.BLL
{
   public class IGMReportBLL
    {
       public static DataSet GetRptCargoDesc(int vesselId, int voyageId)
       {
           return EMS.DAL.IGMReportDAL.GetRptCargoDesc(vesselId, voyageId);
       }
    }
}
