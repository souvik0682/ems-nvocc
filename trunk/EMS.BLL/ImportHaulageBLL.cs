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
    public class ImportHaulageBLL
    {
        public int AddEditImportHAulageChrg(IImportHaulage IHChrg)
        {
            return ImportHaulageDAL.AddEditImportHAulageChrg(IHChrg);
        }

        public static List<IImportHaulage> GetImportHaulage(SearchCriteria searchCriteria, int ID)
        {
            return ImportHaulageDAL.GetImportHaulage(searchCriteria, ID);
        }

        public static IImportHaulage GetImportHaulage(int ID)
        {
            return ImportHaulageDAL.GetImportHaulage(ID);
        }

        public static int DeleteVndor(int ImportHaulageId)
        {
            return ImportHaulageDAL.DeleteImportHaulage(ImportHaulageId);
        }

        public static DataTable GetAllPort(string Initial)
        {
            return ImportHaulageDAL.GetAllPort(Initial);
        }
    }
}
