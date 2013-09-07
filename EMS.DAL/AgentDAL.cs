﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.DAL.DbManager;
using EMS.Entity;
using System.Data;

namespace EMS.DAL
{
    public sealed class AgentDAL
    {
        private AgentDAL()
        { }

        public static int AddEditAgent(IAgent Agent, int CompanyId)
        {
            string strExecution = "[exp].[prcAddEditAgent]";
            int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@userID", Agent.CreatedBy);
                oDq.AddIntegerParam("@fk_CompanyID", CompanyId);
                oDq.AddBooleanParam("@isedit", Agent.Action);
                oDq.AddIntegerParam("@pk_AgentID", Agent.AgentID);
                oDq.AddIntegerParam("@fk_LineID", Agent.LinerID);
                oDq.AddIntegerParam("@fk_FPOD", Convert.ToInt32(Agent.FPODID));
                oDq.AddVarcharParam("@AgentName", 50, Agent.AgentName);
                oDq.AddVarcharParam("@ContactPerson", 100, Agent.ContactPerson);
                oDq.AddVarcharParam("@AgentAddress", 300, Agent.AgentAddress);
                oDq.AddVarcharParam("@Fax", 100, Agent.FAX);
                oDq.AddVarcharParam("@Email", 100, Agent.email);

                oDq.AddVarcharParam("@Phone", 100, Agent.Phone);
                //oDq.AddDecimalParam("@WeightTo", 12, 3, Convert.ToDecimal(ImportHaulage.WeightTo));
                //oDq.AddDecimalParam("@HaulageRate", 12, 2, Convert.ToDecimal(ImportHaulage.HaulageRate));
                //oDq.AddBooleanParam("@HaulageStatus", ImportHaulage.HaulageStatus);

                oDq.AddIntegerParam("@RESULT", Result, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                Result = Convert.ToInt32(oDq.GetParaValue("@RESULT"));
            }
            return Result;
        }

        public static List<IAgent> GetAgent(SearchCriteria searchCriteria, int ID)
        {
            string strExecution = "[exp].[prcGetAgentList]";
            List<IAgent> lstAgent = new List<IAgent>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@AgentID", ID);
                oDq.AddVarcharParam("@SchAgentName", 100, searchCriteria.AgentName);
                oDq.AddVarcharParam("@SchLineName", 100, searchCriteria.LineName);
                oDq.AddVarcharParam("@SchFPOD", 100, searchCriteria.POD);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();


                while (reader.Read())
                {
                    IAgent oIH = new AgentEntity(reader);
                    lstAgent.Add(oIH);
                }
                reader.Close();
            }
            return lstAgent;
        }

        public static IAgent GetAgent(int ID)
        {
            string strExecution = "[exp].[prcGetAgentList]";
            IAgent oIH = null;
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@AgentID", ID);
                oDq.AddVarcharParam("@SortExpression", 30, "");
                oDq.AddVarcharParam("@SortDirection", 4, "");
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    oIH = new AgentEntity(reader);
                }
                reader.Close();
            }
            return oIH;
        }

        public static int DeleteAgent(int AgentId)
        {
            string strExecution = "[exp].[prcDeleteAgent]";
            int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@pk_AgentID", AgentId);
                Result = oDq.RunActionQuery();
            }
            return Result;
        }
    }
}
