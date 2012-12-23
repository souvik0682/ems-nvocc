using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Utilities.ReportManager;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using EMS.BLL;

namespace EMS.WebApp.Reports
{
    public partial class IGMForm2 : System.Web.UI.Page
    {
        #region Private Member Variables

        private IFormatProvider _culture = new CultureInfo(ConfigurationManager.AppSettings["Culture"].ToString());
        private int _userId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;

        #endregion

        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            CheckUserAccess();
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Private Methods

        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();

            //Get user permission.
            UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);
        }

        private void CheckUserAccess()
        {
            if (!_canView)
            {
                Response.Redirect("~/Unauthorized.aspx");
            }
        }

        private void GenerateReport()
        {
            //ReportBLL cls = new ReportBLL();
            //ICallDetail callDetail = new CallDetailEntity();
            //LocalReportManager reportManager = new LocalReportManager(rptViewer, "DailyCallRpt", ConfigurationManager.AppSettings["ReportNamespace"].ToString(), ConfigurationManager.AppSettings["ReportPath"].ToString());
            //string rptName = "DailyCallRpt.Rdlc";
            //DateTime fromDate;
            //DateTime toDate;
            //fromDate = Convert.ToDateTime(txtFromDt.Text, _culture);
            //toDate = Convert.ToDateTime(txtToDt.Text, _culture);

            //BuildEntity(callDetail);
            //IEnumerable<ICallDetail> lst = cls.GetDailyCallData(fromDate, toDate, callDetail, _userId);

            //rptViewer.Reset();
            //rptViewer.LocalReport.Dispose();
            //rptViewer.LocalReport.DataSources.Clear();
            //rptViewer.LocalReport.ReportPath = this.Server.MapPath(this.Request.ApplicationPath) + ConfigurationManager.AppSettings["ReportPath"].ToString() + "/" + rptName;

            ////rptViewer.LocalReport.ReportPath = Server.MapPath("/" + ConfigurationManager.AppSettings["ReportPath"].ToString() + "/" + rptName);
            //rptViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportDataSet", lst));
            //rptViewer.LocalReport.SetParameters(new ReportParameter("CompanyName", Convert.ToString(ConfigurationManager.AppSettings["CompanyName"])));
            //rptViewer.LocalReport.SetParameters(new ReportParameter("FromDate", txtFromDt.Text));
            //rptViewer.LocalReport.SetParameters(new ReportParameter("ToDate", txtToDt.Text));
            //rptViewer.LocalReport.SetParameters(new ReportParameter("Location", ddlLoc.SelectedItem.Text));
            //rptViewer.LocalReport.SetParameters(new ReportParameter("SalesPerson", ddlSales.SelectedItem.Text));
            //rptViewer.LocalReport.SetParameters(new ReportParameter("Prospect", ddlPros.SelectedItem.Text));
            //rptViewer.LocalReport.SetParameters(new ReportParameter("CallType", ddlType.SelectedItem.Text));
            //rptViewer.LocalReport.Refresh();
        }

        #endregion
    }
}