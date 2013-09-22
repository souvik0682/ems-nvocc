using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.DAL;
using EMS.Common;
using EMS.Entity;
using EMS.Utilities;
using System.Data;

namespace EMS.BLL
{
   public class ExportBLQueryBLL
    {
       public DataSet GetBLQuery(string ImpBLNo, int ActivityType)
       {
           return ExportBLQueryDAL.GetBLQuery(ImpBLNo, ActivityType);
       }
       public DataTable GetAllInvoice(Int64 BLId)
       {
           return ExportBLQueryDAL.GetAllInvoice(BLId);
       }
    }
}
