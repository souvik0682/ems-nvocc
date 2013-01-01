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
    public partial class ImportSummary : System.Web.UI.Page
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
            SetAttributes();

            if (!IsPostBack)
            {
                PopulateControls();
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                string message = string.Empty;

                if (ValidateData(out message))
                {
                    GenerateReport();
                }
                else
                {
                    GeneralFunctions.RegisterAlertScript(this, message);
                }
            }
            catch (Exception ex)
            {
                GeneralFunctions.RegisterErrorAlertScript(this, ex.Message);
            }
        }

        #endregion

        #region Private Methods

        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();

            //Get user permission.
            UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);
        }

        private void SetAttributes()
        {
            if (!IsPostBack)
            {
                cbeFromDt.Format = Convert.ToString(ConfigurationManager.AppSettings["DateFormat"]);
                cbeToDt.Format = Convert.ToString(ConfigurationManager.AppSettings["DateFormat"]);
            }
        }

        private void CheckUserAccess()
        {
            if (!_canView)
            {
                Response.Redirect("~/Unauthorized.aspx");
            }
        }

        private void PopulateControls()
        {
            PopulateLocation();
            PopulateLine();
        }

        private void PopulateLocation()
        {
            List<ILocation> lstLoc = new CommonBLL().GetActiveLocation();

            ddlLoc.DataValueField = "Id";
            ddlLoc.DataTextField = "Name";
            ddlLoc.DataSource = lstLoc;
            ddlLoc.DataBind();
            ddlLoc.Items.Insert(0, new ListItem(Constants.DROPDOWNLIST_DEFAULT_TEXT, Constants.DROPDOWNLIST_DEFAULT_VALUE));
        }

        private void PopulateLine()
        {
            BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            DataSet ds = dbinteract.GetNVOCCLine(-1, string.Empty);
            ddlLine.DataValueField = "pk_NVOCCID";
            ddlLine.DataTextField = "NVOCCName";
            ddlLine.DataSource = ds;
            ddlLine.DataBind();
            ddlLine.Items.Insert(0, new ListItem(Constants.DROPDOWNLIST_DEFAULT_TEXT, Constants.DROPDOWNLIST_DEFAULT_VALUE));
        }

        private bool ValidateData(out string message)
        {
            bool isValid = true;
            int slNo = 1;
            message = GeneralFunctions.FormatAlertMessage("Please correct the following errors:");

            if (string.IsNullOrEmpty(txtFromDt.Text))
            {
                isValid = false;
                message += GeneralFunctions.FormatAlertMessage(slNo, "Please enter from date");
                slNo++;
            }

            if (string.IsNullOrEmpty(txtToDt.Text))
            {
                isValid = false;
                message += GeneralFunctions.FormatAlertMessage(slNo, "Please enter to date");
                slNo++;
            }

            if (!isValid)
            {
                GeneralFunctions.RegisterAlertScript(this, message);
            }

            return isValid;
        }

        private void GenerateReport()
        {
            ReportBLL cls = new ReportBLL();
            DateTime dtFrom = Convert.ToDateTime(txtFromDt.Text, _culture);
            DateTime dtTo = Convert.ToDateTime(txtToDt.Text, _culture);
            List<ImpRegisterSummaryEntity> lstReg = ReportBLL.GetImportRegisterSummary(Convert.ToInt32(ddlLine.SelectedValue), Convert.ToInt32(ddlLoc.SelectedValue), dtFrom, dtTo);

            LocalReportManager reportManager = new LocalReportManager(rptViewer, "ImportSummary", ConfigurationManager.AppSettings["ReportNamespace"].ToString(), ConfigurationManager.AppSettings["ReportPath"].ToString());
            string rptName = "ImportSummary.rdlc";

            rptViewer.Reset();
            rptViewer.LocalReport.Dispose();
            rptViewer.LocalReport.DataSources.Clear();
            rptViewer.LocalReport.ReportPath = this.Server.MapPath(this.Request.ApplicationPath) + ConfigurationManager.AppSettings["ReportPath"].ToString() + "/" + rptName;

            //rptViewer.LocalReport.ReportPath = Server.MapPath("/" + ConfigurationManager.AppSettings["ReportPath"].ToString() + "/" + rptName);
            rptViewer.LocalReport.DataSources.Add(new ReportDataSource("ImpRegDataSet", lstReg));
            rptViewer.LocalReport.SetParameters(new ReportParameter("CompanyName", Convert.ToString(ConfigurationManager.AppSettings["CompanyName"])));
            rptViewer.LocalReport.SetParameters(new ReportParameter("Location", ddlLoc.SelectedItem.Text));
            rptViewer.LocalReport.SetParameters(new ReportParameter("Line", ddlLine.SelectedItem.Text));
            rptViewer.LocalReport.SetParameters(new ReportParameter("DischargeFrom", txtFromDt.Text));
            rptViewer.LocalReport.SetParameters(new ReportParameter("DischargeTo", txtToDt.Text));
            rptViewer.LocalReport.Refresh();
        }

        #endregion
    }
}