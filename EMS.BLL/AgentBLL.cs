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
    public class AgentBLL
    {
        public int AddEditAgent(IAgent Agent, int CompanyID)
        {
            return AgentDAL.AddEditAgent(Agent, CompanyID);
        }

        public static List<IAgent> GetAgent(SearchCriteria searchCriteria, int ID)
        {
            return AgentDAL.GetAgent(searchCriteria, ID);
        }

        public static IAgent GetAgent(int ID)
        {
            return AgentDAL.GetAgent(ID);
        }

        public static int DeleteAgent(int ImportHaulageId)
        {
            return AgentDAL.DeleteAgent(ImportHaulageId);
        }

        public static DataTable GetAllPort(string Initial)
        {
            return ImportHaulageDAL.GetAllPort(Initial);
        }
    }
}
