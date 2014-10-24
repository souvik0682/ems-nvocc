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
    public class fwLineBLL
    {
        public static List<IFwLine> GetfwLine(SearchCriteria searchCriteria)
        {
            return fwLineDAL.GetfwLine(searchCriteria);
        }

        public int SaveFwLine(IFwLine Line, int fk_CompanyID, string mode)
        {
            return fwLineDAL.AddEditFwLine(Line, fk_CompanyID, mode);
        }

        public static int DeletFwLine(int LineID, int UserID)
        {
            return fwLineDAL.DeleteFwLine(LineID, UserID);
        }

        public static IFwLine GetFLine(int ID)
        {
            return fwLineDAL.GetFLine(ID);
        }
    }
}
