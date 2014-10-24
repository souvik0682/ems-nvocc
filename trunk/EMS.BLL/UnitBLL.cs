using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.DAL;
using EMS.Entity;

namespace EMS.BLL
{
    public class UnitBLL
    {
        public  List<IUnit> GetJobs(SearchCriteria searchCriteria, int ID, int CompanyID)
        {
            return UnitDAL.GetJobs(searchCriteria, ID, CompanyID);
        }

        public  int AddEditJob(IUnit Units, int CompanyId,string Mode)
        {
            return UnitDAL.AddEditJob(Units, CompanyId, Mode);

        }

        public  int DeleteJob(int UnitTypeID, int UserID)
        {
            return UnitDAL.DeleteJob(UnitTypeID, UserID);
        }
    }
}
