using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.DAL.DbManager;
using EMS.Entity;
using System.Data;
namespace EMS.DAL
{
    public sealed class ServiceDAL
    {
        private ServiceDAL()
        { }

        public static int AddEditService(IService Service, int CompanyId)
        {
            string strExecution = "[exp].[prcAddEditServices]";
            int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@userID", Service.CreatedBy);
                oDq.AddIntegerParam("@fk_CompanyID", CompanyId);
                oDq.AddBooleanParam("@isedit", Service.Action);
                oDq.AddIntegerParam("@pk_ServiceID", Service.ServiceID);
                oDq.AddIntegerParam("@fk_LineID", Service.LinerID);
                oDq.AddIntegerParam("@fk_FPOD", Convert.ToInt32(Service.FPODID));
                oDq.AddIntegerParam("@ServiceNameID", Service.ServiceNameID);
                oDq.AddIntegerParam("@RESULT", Result, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                Result = Convert.ToInt32(oDq.GetParaValue("@RESULT"));
            }
            return Result;
        }

        public static List<IService> GetService(SearchCriteria searchCriteria, int ID)
        {
            string strExecution = "[exp].[prcGetServicesList]";
            List<IService> lstService = new List<IService>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@ServiceID", ID);
                oDq.AddVarcharParam("@SchServiceName", 100, searchCriteria.ServiceName);
                oDq.AddVarcharParam("@SchLineName", 100, searchCriteria.LineName);
                oDq.AddVarcharParam("@SchFPOD", 100, searchCriteria.POD);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();


                while (reader.Read())
                {
                    IService oIH = new ServiceEntity(reader);
                    lstService.Add(oIH);
                }
                reader.Close();
            }
            return lstService;
        }

        public static IService GetService(int ID)
        {
            string strExecution = "[exp].[prcGetServicesList]";
            IService oIH = null;
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@ServiceID", ID);
                oDq.AddVarcharParam("@SortExpression", 30, "");
                oDq.AddVarcharParam("@SortDirection", 4, "");
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    oIH = new ServiceEntity(reader);
                }
                reader.Close();
            }
            return oIH;
        }

        public static int DeleteService(int ServiceId)
        {
            string strExecution = "[exp].[prcDeleteServices]";
            int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@pk_ServiceID", ServiceId);
                Result = oDq.RunActionQuery();
            }
            return Result;
        }

    }
}
