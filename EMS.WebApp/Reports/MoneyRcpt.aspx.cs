using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.BLL;
using EMS.Utilities.ReportManager;
using System.Data;
using System.Configuration;
using Microsoft.Reporting.WebForms;
using EMS.Utilities;
using EMS.Common;

namespace EMS.WebApp.Reports
{
    public partial class MoneyRcpt : System.Web.UI.Page
    {
        private Int64 _MoneyRecptNo = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserAccess();
            if (!Page.IsPostBack)
            {
                RetriveParameters();
                GenerateReport();
            }
        }

        private void CheckUserAccess()
        {
            if (!ReferenceEquals(Session[Constants.SESSION_USER_INFO], null))
            {
                IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

                if (ReferenceEquals(user, null) || user.Id == 0)
                {
                    Response.Redirect("~/Login.aspx");
                }

                if (user.UserRole.Id != (int)UserRole.Admin)
                {
                    Response.Redirect("~/Unauthorized.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        private void RetriveParameters()
        {
            if (!ReferenceEquals(Request.QueryString["mrid"], null))
            {
                Int64.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["mrid"].ToString()), out _MoneyRecptNo);
            }
        }

        private void GenerateReport()
        {
            ReportBLL cls = new ReportBLL();

            LocalReportManager reportManager = new LocalReportManager(rptViewer, "MoneyReciept", ConfigurationManager.AppSettings["ReportNamespace"].ToString(), ConfigurationManager.AppSettings["ReportPath"].ToString());
            string rptName = "MoneyReciept.rdlc";

            //string invoiceNo = ((TextBox)AutoCompletepInvoice1.FindControl("txtInvoice")).Text;
            DataSet ds = EMS.BLL.BLLReport.GetMoneyRcptDetails(_MoneyRecptNo);
            try
            {
                rptViewer.Reset();
                rptViewer.LocalReport.Dispose();
                rptViewer.LocalReport.DataSources.Clear();
                rptViewer.LocalReport.ReportPath = this.Server.MapPath(this.Request.ApplicationPath) + ConfigurationManager.AppSettings["ReportPath"].ToString() + "/" + rptName;
                rptViewer.LocalReport.SetParameters(new ReportParameter("CompanyName", Convert.ToString(ConfigurationManager.AppSettings["CompanyName"])));
                rptViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", ds.Tables[0]));
                rptViewer.LocalReport.Refresh();
            }
            catch
            {


            }



        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            //GenerateReport();
        }

        protected void btnGen_Click(object sender, EventArgs e)
        {
           // string invoiceNo = ((TextBox)AutoCompletepInvoice1.FindControl("txtInvoice")).Text;
           //EMS.Utilities.GeneralFunctions.PopulateDropDownList(ddlMnyRcpt,  BLLReport.FillDDLMoneyRcpt(invoiceNo));
        }
    }
}