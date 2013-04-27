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
    public class CommonBLL
    {
        public static void SavePrintCount(string blNo)
        {
            CommonDAL.SavePrintCount(blNo);
        }

        #region Common

        public static DataTable GetVoyageList(string prefixText)
        {
            return CommonDAL.GetVoyageList(prefixText);
        }

        public static DataTable GetVesselList(string prefixText)
        {
            return CommonDAL.GetVesselList(prefixText);
        }

        #region Email

        public static bool SendMail(string from, string displayName, string mailTo, string cc, string subject, string body, string mailServerIP)
        {
            bool sent = true;

            try
            {
                if (mailTo != "")
                {
                    MailMessage MyMail = new MailMessage();
                    MyMail.To.Add(new MailAddress(mailTo));
                    MyMail.Priority = MailPriority.High;
                    MyMail.From = new MailAddress(from, displayName);

                    if (cc != "")
                    {
                        MailAddress ccAddr = new MailAddress(cc);
                        MyMail.CC.Add(ccAddr);
                    }

                    MyMail.Subject = subject;
                    MyMail.Body = GetMessageBody(body);
                    //MyMail.BodyEncoding = System.Text.Encoding.ASCII;
                    MyMail.IsBodyHtml = true;

                    SmtpClient client = new SmtpClient(mailServerIP);
                    client.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                    client.Send(MyMail);
                }
                else { sent = false; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sent;
        }

        public static bool SendMail(string from, string displayName, string mailTo, string cc, string subject, string body, string mailServerIP, string mailUserAccount, string mailUserPwd)
        {
            bool sent = true;

            try
            {
                if (mailTo != "")
                {
                    MailMessage MyMail = new MailMessage();
                    MyMail.To.Add(new MailAddress(mailTo));
                    MyMail.Priority = MailPriority.High;
                    MyMail.From = new MailAddress(from, displayName);

                    if (cc != "")
                    {
                        MailAddress ccAddr = new MailAddress(cc);
                        MyMail.CC.Add(ccAddr);
                    }

                    MyMail.Subject = subject;
                    MyMail.Body = GetMessageBody(body);
                    //MyMail.BodyEncoding = System.Text.Encoding.ASCII;
                    MyMail.IsBodyHtml = true;

                    SmtpClient client = new SmtpClient(mailServerIP, 25);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    System.Net.NetworkCredential credential = new System.Net.NetworkCredential(mailUserAccount, mailUserPwd);
                    client.Credentials = credential;
                    client.Send(MyMail);
                }
                else { sent = false; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sent;
        }

        public static string GetMessageBody(string strBodyContent)
        {
            try
            {
                StringBuilder sbMsgBody = new StringBuilder();
                //sbMsgBody.Append("<font face='Verdana, Arial, Helvetica, sans-serif' size='10' color='#8B4B0D'>Daily Sales Call</font>");
                //sbMsgBody.Append("<br />");
                //sbMsgBody.Append("<br /><br /><br />");
                sbMsgBody.Append("<html><body>");
                sbMsgBody.Append("<font face=verdana size=2>" + strBodyContent + "</font>");
                sbMsgBody.Append("</body></html>");

                return sbMsgBody.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        /// <summary>
        /// Handles the exception.
        /// </summary>
        /// <param name="ex">The <see cref="System.Exception"/> object.</param>
        /// <param name="logFilePath">The log file path.</param>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>02/12/2012</createddate>
        public static void HandleException(Exception ex, string logFilePath)
        {
            int userId = 0;
            string userDetail = string.Empty;
            string baseException = string.Empty;

            if (ex.GetType() != typeof(System.Threading.ThreadAbortException))
            {
                if (System.Web.HttpContext.Current.Session[Constants.SESSION_USER_INFO] != null)
                {
                    IUser user = (IUser)System.Web.HttpContext.Current.Session[Constants.SESSION_USER_INFO];

                    if (!ReferenceEquals(user, null))
                    {
                        userId = user.Id;
                        //userDetail = user.Id.ToString() + ", " + user.FirstName + " " + user.LastName;
                    }
                }

                if (ex.GetBaseException() != null)
                {
                    baseException = ex.GetBaseException().ToString();
                }
                else
                {
                    baseException = ex.StackTrace;
                }

                try
                {
                    CommonDAL.SaveErrorLog(userId, ex.Message, baseException);
                }
                catch
                {
                    //try
                    //{
                    //    string message = DateTime.UtcNow.ToShortDateString().ToString() + " "
                    //            + DateTime.UtcNow.ToLongTimeString().ToString() + " ==> " + "User Id: " + userDetail + "\r\n"
                    //            + ex.GetBaseException().ToString();

                    //    GeneralFunctions.WriteErrorLog(logFilePath + LogFileName, message);
                    //}
                    //catch
                    //{
                    //    // Consume the exception.
                    //}
                }
            }
        }

        /// <summary>
        /// Common method to populate all the dropdownlist throughout the application
        /// </summary>
        /// <param name="Number">Unique Number</param>
        /// <returns>DataTable</returns>
        /// <createdby>Rajen Saha</createdby>
        /// <createddate>01/12/2012</createddate>
        public static void PopulateDropdown(int Number, DropDownList ddl, int? Filter1, int? Filter2)
        {
            ddl.DataSource = CommonDAL.PopulateDropdown(Number, Filter1, Filter2);
            ddl.DataValueField = "Value";
            ddl.DataTextField = "Text";
            ddl.DataBind();
        }

        #endregion

        #region Location

        private void SetDefaultSearchCriteriaForLocation(SearchCriteria searchCriteria)
        {
            searchCriteria.SortExpression = "Location";
            searchCriteria.SortDirection = "ASC";
        }

        public List<ILocation> GetAllLocation(SearchCriteria searchCriteria)
        {
            return CommonDAL.GetLocation('N', searchCriteria);
        }

        public List<ILocation> GetActiveLocation()
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForLocation(searchCriteria);
            return CommonDAL.GetLocation('Y', searchCriteria);
        }

        public ILocation GetLocation(int locId)
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForLocation(searchCriteria);
            return CommonDAL.GetLocation(locId, 'N', searchCriteria);
        }

        public void SaveLocation(ILocation loc, int modifiedBy)
        {
            CommonDAL.SaveLocation(loc, modifiedBy);
        }

        //public void DeleteLocation(int locId, int modifiedBy)
        //{
        //    CommonDAL.DeleteLocation(locId, modifiedBy);
        //}

        public List<ILocation> GetLocationByUser(int userId)
        {
            return CommonDAL.GetLocationByUser(userId);
        }

        #endregion

        #region Area

        //private void SetDefaultSearchCriteriaForArea(SearchCriteria searchCriteria)
        //{
        //    searchCriteria.SortExpression = "Location";
        //    searchCriteria.SortDirection = "ASC";
        //}

        //public List<IArea> GetAllArea(SearchCriteria searchCriteria)
        //{
        //    return CommonDAL.GetArea('N', searchCriteria);
        //}

        //public List<IArea> GetActiveArea()
        //{
        //    SearchCriteria searchCriteria = new SearchCriteria();
        //    SetDefaultSearchCriteriaForArea(searchCriteria);
        //    return CommonDAL.GetArea('Y', searchCriteria);
        //}

        //public IArea GetArea(int areaId)
        //{
        //    SearchCriteria searchCriteria = new SearchCriteria();
        //    SetDefaultSearchCriteriaForArea(searchCriteria);
        //    return CommonDAL.GetArea(areaId, 'N', searchCriteria);
        //}

        //public string SaveArea(IArea area, int modifiedBy)
        //{
        //    int result = 0;
        //    string errMessage = string.Empty;
        //    result = CommonDAL.SaveArea(area, modifiedBy);

        //    switch (result)
        //    {
        //        case 1:
        //            errMessage = ResourceManager.GetStringWithoutName("ERR00013");
        //            break;
        //        default:
        //            break;
        //    }

        //    return errMessage;
        //}

        //public void DeleteArea(int areaId, int modifiedBy)
        //{
        //    CommonDAL.DeleteArea(areaId, modifiedBy);
        //}

        //public List<IArea> GetAreaByLocation(int locId)
        //{
        //    return CommonDAL.GetAreaByLocation(locId);
        //}

        public List<IArea> GetAreaByLocationAndPinCode(int locId, string pinCode)
        {
            return CommonDAL.GetAreaByLocationAndPinCode(locId, pinCode);
        }

        #endregion

        #region Group Company

        private void SetDefaultSearchCriteriaForGroupCompany(SearchCriteria searchCriteria)
        {
            searchCriteria.SortExpression = "GroupName";
            searchCriteria.SortDirection = "ASC";
        }

        public List<IGroupCompany> GetActiveGroupCompany()
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForGroupCompany(searchCriteria);
            return CommonDAL.GetGroupCompany('Y', searchCriteria);
        }

        #endregion

        #region Customer

        private void SetDefaultSearchCriteriaForCustomer(SearchCriteria searchCriteria)
        {
            searchCriteria.SortExpression = "CustName";
            searchCriteria.SortDirection = "ASC";
        }

        public List<ICustomer> GetAllCustomer(SearchCriteria searchCriteria)
        {
            return CommonDAL.GetCustomerList('N', searchCriteria);
        }

        public List<ICustomer> GetActiveCustomer(SearchCriteria searchCriteria)
        {
            return CommonDAL.GetCustomerList('Y', searchCriteria);
        }

        public List<ICustomer> GetAllCustomer()
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForCustomer(searchCriteria);
            return CommonDAL.GetCustomerList('N', searchCriteria);
        }

        public List<ICustomer> GetActiveCustomer()
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForCustomer(searchCriteria);

            return CommonDAL.GetCustomerList('N', searchCriteria);
        }

        public List<ICustomer> GetCustomerByUser(int userId)
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForCustomer(searchCriteria);
            searchCriteria.UserId = userId;
            return CommonDAL.GetCustomerList('N', searchCriteria);
        }

        public ICustomer GetCustomer(int customer)
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForCustomer(searchCriteria);
            return CommonDAL.GetCustomer(customer, 'N', searchCriteria);
        }

        //public string SaveCustomer(ICustomer customer, int modifiedBy)
        //{
        //    int result = 0;
        //    string errMessage = string.Empty;
        //    result = CommonDAL.SaveCustomer(customer, modifiedBy);

        //    switch (result)
        //    {
        //        case 1:
        //            errMessage = ResourceManager.GetStringWithoutName("ERR00013");
        //            break;
        //        default:
        //            break;
        //    }

        //    return errMessage;
        //}

        //public void DeleteCustomer(int customer, int modifiedBy)
        //{
        //    CommonDAL.DeleteCustomer(customer, modifiedBy);
        //}

        #endregion

        #region Customer Type

        //public List<ICustomerType> GetAllCustomerType()
        //{
        //    return CommonDAL.GetCustomerType('N');
        //}

        public List<ICustomerType> GetActiveCustomerType()
        {
            return CommonDAL.GetCustomerType('Y');
        }

        //public ICustomerType GetCustomerType(int custTypeId)
        //{
        //    return CommonDAL.GetCustomerType(custTypeId, 'N');
        //}

        #endregion

        #region User

        public List<IUser> GetSalesExecutiveNew(int userId)
        {
            return CommonDAL.GetSalesExecutiveNew(userId);
        }

        #endregion

        #region Container Type

        public static IList<IContainerType> GetContainerType()
        {
            return CommonDAL.GetContainerType();
        }

        #endregion

        #region BLHeader
        public static DataTable GetBLHeaderByBLNo(long LocationId)
        {
            return CommonDAL.GetBLHeaderByBLNo(LocationId);
        }
        #endregion

        #region Report
        public static string GetTerminalType(int VoyageID)
        { return CommonDAL.GetTerminalType(VoyageID); }

        public static bool GenerateExcel(string filename, string Location, string Vessel, string PortOfDischarge, string Line, string Voyage, string VIANo)
        { return CommonDAL.GenerateExcel(filename, Location, Vessel, PortOfDischarge, Line, Voyage, VIANo); }
       
        public static bool GenerateText(string filename, int Location, int Vessel, int PortOfDischarge, int Line, int Voyage, int VIANo)
        { return CommonDAL.GenerateTxt(filename, Location, Vessel, PortOfDischarge, Line, Voyage, VIANo); }

        public static DataTable GetLineForHire(string Location)
        {
            return CommonDAL.GetLineForHire(Location);
        }
        public static DataTable GetLine(string Location)
        {

            return CommonDAL.GetLine(Location);
        }
        public static DataTable GetVessels(string Line)
        {
            return CommonDAL.GetVessels(Line);
        }

        public static DataTable GetVoyages(string Vessel, string Line)
        {
            return CommonDAL.GetVoyages(Vessel, Line);
        }
        public static DataTable GetBLNo(string line, string Vessel, string Voyage)
        {
            return CommonDAL.GetBLNo(line, Vessel, Voyage);
        }
        public static DataTable GetInvoiceByBLNo(string BLNo)
        {
            return CommonDAL.GetInvoiceByBLNo(BLNo);
        }

        #endregion
    }
}
