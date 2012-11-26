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

namespace EMS.BLL
{
    public class CommonBLL
    {
        #region Common

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
        /// <createddate>08/07/2012</createddate>
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

        #endregion
    }
}
