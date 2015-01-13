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
    public class fwdAgentBLL
    {
        public int AddEditAgent(IfwdAgent Agent, int CompanyID)
        {
            return fwdAgentDAL.AddEditAgent(Agent, CompanyID);
        }

        public static List<IfwdAgent> GetAgent(SearchCriteria searchCriteria, int ID)
        {
              return fwdAgentDAL.GetAgent(searchCriteria, ID);
        }

        public static IfwdAgent GetAgent(int ID)
        {
            return fwdAgentDAL.GetAgent(ID);
        }

        public static int DeleteAgent(int ImportHaulageId)
        {
            return fwdAgentDAL.DeleteAgent(ImportHaulageId);
        }

        public static DataTable GetAllPort(string Initial)
        {
            return ImportHaulageDAL.GetAllPort(Initial);
        }
    }
}
