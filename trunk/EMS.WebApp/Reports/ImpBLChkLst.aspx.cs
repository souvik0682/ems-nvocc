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
using EMS.Entity.Report;
using System.Data;

namespace EMS.WebApp.Reports
{
    public partial class ImpBLChkLst : System.Web.UI.Page
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
            GenerateReport();
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
            ReportBLL cls = new ReportBLL();
            List<ImpBLChkLstEntity> lstEntity = ReportBLL.GetImportBLCheckList();
            LocalReportManager reportManager = new LocalReportManager(rptViewer, "ImpBLChkLst", ConfigurationManager.AppSettings["ReportNamespace"].ToString(), ConfigurationManager.AppSettings["ReportPath"].ToString());
            string rptName = "ImpBLChkLst.rdlc";

            rptViewer.Reset();
            rptViewer.LocalReport.Dispose();
            rptViewer.LocalReport.DataSources.Clear();
            rptViewer.LocalReport.ReportPath = this.Server.MapPath(this.Request.ApplicationPath) + ConfigurationManager.AppSettings["ReportPath"].ToString() + "/" + rptName;

            //rptViewer.LocalReport.ReportPath = Server.MapPath("/" + ConfigurationManager.AppSettings["ReportPath"].ToString() + "/" + rptName);
            rptViewer.LocalReport.DataSources.Add(new ReportDataSource("RptDataSet", lstEntity));
            rptViewer.LocalReport.SetParameters(new ReportParameter("CompanyName", Convert.ToString(ConfigurationManager.AppSettings["CompanyName"])));
            rptViewer.LocalReport.SetParameters(new ReportParameter("ReportDate", "test"));
            rptViewer.LocalReport.Refresh();
        }

        #endregion
    }
}