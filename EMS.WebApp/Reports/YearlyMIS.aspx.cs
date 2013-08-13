using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.BLL;
using EMS.Common;
using EMS.Entity.Report;
using EMS.Utilities;
using EMS.Utilities.ReportManager;
using Microsoft.Reporting.WebForms;

namespace EMS.WebApp.Reports
{
    public partial class YearlyMIS : System.Web.UI.Page
    {
        private IFormatProvider _culture = new CultureInfo(ConfigurationManager.AppSettings["Culture"].ToString());
        private int _userId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        private bool _LocationSpecific = true;
        private int _userLocation = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();

            if (!IsPostBack)
            {
                PopulateControls();
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                GenerateReport();
            }
            catch (Exception ex)
            {
                GeneralFunctions.RegisterErrorAlertScript(this, ex.Message);
            }
        }

        private void PopulateControls()
        {
            PopulateLocation();
            if (_LocationSpecific)
            {
                ddlLoc.SelectedValue = Convert.ToString(_userLocation);
                ddlLoc.Enabled = false;
            }
            PopulateLine();
            PopulateYear();
        }

        private void PopulateLocation()
        {
            //New Function Added By Souvik - 11-06-2013
            List<ILocation> lstLoc = new CommonBLL().GetActiveLocation();
            ddlLine.Enabled = true;
            ddlLoc.DataValueField = "Id";
            ddlLoc.DataTextField = "Name";
            ddlLoc.DataSource = lstLoc;
            ddlLoc.DataBind();
            ddlLoc.Items.Insert(0, new ListItem("All", "0"));


        }

        private void PopulateLine()
        {
            BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            DataSet ds = dbinteract.GetNVOCCLine(-1, string.Empty);
            ddlLine.DataValueField = "pk_NVOCCID";
            ddlLine.DataTextField = "NVOCCName";
            ddlLine.DataSource = ds;
            ddlLine.DataBind();
            ddlLine.Items.Insert(0, new ListItem("All", "0"));


        }

        private void PopulateYear()
        {
            for (int index = 2010; index < 2030; index++)
            {
                ddlYear.Items.Add(new ListItem(index.ToString(), index.ToString()));
            }

            ddlYear.SelectedValue = System.DateTime.Now.Year.ToString();
        }

        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();

            //Get user permission.
            UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);
            _LocationSpecific = UserBLL.GetUserLocationSpecific();
            _userLocation = UserBLL.GetUserLocation();

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
            //string stat = "";
            //if (ddlStatus.Text == "All")
            //    stat = "A";
            //else if (ddlStatus.Text == "Empty")
            //    stat = "E";
            //else
            //    stat = "L";

            //List<YearlyMISEntity> lstEntity = ReportBLL.GetYearlyMIS(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlLine.SelectedValue), Convert.ToInt32(ddlLoc.SelectedValue), ddlSize.Text, stat);



            LocalReportManager reportManager = new LocalReportManager(rptViewer, "YearlyMIS", ConfigurationManager.AppSettings["ReportNamespace"].ToString(), ConfigurationManager.AppSettings["ReportPath"].ToString());
            string rptName = "YearlyMIS.rdlc";

            DataSet ds = EMS.BLL.LogisticReportBLL.GetRptYearlyReport(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlLine.SelectedValue), Convert.ToInt32(ddlLoc.SelectedValue), ddlSize.Text, ddlStatus.Text);
            //BuildEntity(callDetail);
            //IEnumerable<ICallDetail> lst = cls.GetYearlyMisReportData(Convert.ToInt32(ddlYear.SelectedValue), callDetail, Convert.ToChar(ddlParam.SelectedValue), _userId);
            rptViewer.Reset();
            rptViewer.LocalReport.Dispose();
            rptViewer.LocalReport.DataSources.Clear();
            rptViewer.LocalReport.ReportPath = this.Server.MapPath(this.Request.ApplicationPath) + ConfigurationManager.AppSettings["ReportPath"].ToString() + "/" + rptName;
            //rptViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportDataSet", lst));
            rptViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportDataSet", ds.Tables[0]));
            rptViewer.LocalReport.SetParameters(new ReportParameter("CompanyName", Convert.ToString(ConfigurationManager.AppSettings["CompanyName"])));
            rptViewer.LocalReport.SetParameters(new ReportParameter("Location", ddlLoc.SelectedItem.Text));
            rptViewer.LocalReport.SetParameters(new ReportParameter("Prospect", ddlLine.SelectedItem.Text));
            rptViewer.LocalReport.SetParameters(new ReportParameter("RptYear", ddlYear.SelectedItem.Text));
            rptViewer.LocalReport.SetParameters(new ReportParameter("ReportType", ddlStatus.SelectedItem.Text));
            rptViewer.LocalReport.SetParameters(new ReportParameter("Size", ddlSize.SelectedItem.Text));

            rptViewer.LocalReport.Refresh();
        }

        //private void BuildEntity(ICallDetail detail)
        //{
        //    detail.LocationId = Convert.ToInt32(ddlLoc.SelectedValue);
        //    detail.ProspectId = Convert.ToInt32(ddlLine.SelectedValue);
        //}
    }
}