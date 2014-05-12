using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.DAL;
using EMS.Common;
using EMS.Entity;
using EMS.Utilities.ResourceManager;
using EMS.Utilities;
using EMS.Utilities.Cryptography;
using System.Net.Mail;
using System.Data;
using System.Web.UI.WebControls;

namespace EMS.BLL
{
    public class ServiceBLL
    {
        
        public int AddEditService(IService Service, int CompanyID)
        {
            return ServiceDAL.AddEditService(Service, CompanyID);
        }

        public static List<IService> GetService(SearchCriteria searchCriteria, int ID)
        {
            return ServiceDAL.GetService(searchCriteria, ID);
        }

        public static IService GetService(int ID)
        {
            return ServiceDAL.GetService(ID);
        }

        public static int DeleteService(int ImportHaulageId)
        {
            return ServiceDAL.DeleteService(ImportHaulageId);
        }

        public static DataTable GetAllPort(string Initial)
        {
            return ImportHaulageDAL.GetAllPort(Initial);
        }

        public static List<IService> GetServiceWithLine(int ID)
        {
            return ServiceDAL.GetServiceWithLine(ID);
        }
    }
}
