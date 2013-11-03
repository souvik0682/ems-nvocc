using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EMS.DAL;

namespace EMS.BLL
{
   public class ExportEDIBLL
   {
       public DataSet GetExportEDI(int locationid, long veselid, long voyageid, int loadingport)
       {
            return ExportEDIDAL.GetExportEDI(locationid, veselid, voyageid, loadingport);
       }

       public DataTable GetLoadPort(int VoyageId)
       {
           return ExportEDIDAL.GetLoadPort(VoyageId);
       }

   }
}
