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
    public class JobBLL
    {
        public static List<IJob> GetJobs(SearchCriteria searchCriteria, int ID, string JobType) 
        {
            return JobDAL.GetJobs(searchCriteria, ID, JobType);
        }

        public static int AddEditJob(IJob Jobs, int CompanyId)
        {
            return JobDAL.AddEditJob(Jobs, CompanyId);
        }

        public static int DeleteJob(int JobID, int UserID)
        {
            return JobDAL.DeleteJob(JobID, UserID);
        }
    }
}
