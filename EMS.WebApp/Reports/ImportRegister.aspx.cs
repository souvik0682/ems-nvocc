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
    public partial class ImportRegister : System.Web.UI.Page
    {
        #region Private Member Variables

        private IFormatProvider _culture = new CultureInfo(ConfigurationManager.AppSettings["Culture"].ToString());
        private int _userId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        private bool _LocationSpecific = true;
        private int _userLocation = 0;

        #endregion

        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            //CheckUserAccess();

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

        private void PopulateControls()
        {
            PopulateLocation();
            if (_LocationSpecific)
            {
                ddlLoc.SelectedValue = Convert.ToString(_userLocation);
                ddlLoc.Enabled = false;
            }
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

            if (string.IsNullOrEmpty(txtVessel.Text))
            {
                isValid = false;
                message += GeneralFunctions.FormatAlertMessage(slNo, "Please select vessel name");
                slNo++;
            }

            if (string.IsNullOrEmpty(txtVoyage.Text))
            {
                isValid = false;
                message += GeneralFunctions.FormatAlertMessage(slNo, "Please select voyage no");
                slNo++;
            }

            //Validate selected voyage name and vessel no.
            Int64 voyageId = GetSelectedVoyageId();
            Int64 vesselId = GetSelectedVesselId();

            if (vesselId == 0)
            {
                isValid = false;
                message += GeneralFunctions.FormatAlertMessage(slNo, "Please select valid vessel name");
                slNo++;
            }

            if (voyageId == 0)
            {
                isValid = false;
                message += GeneralFunctions.FormatAlertMessage(slNo, "Please select valid voyage no");
                slNo++;
            }

            if (!isValid)
            {
                GeneralFunctions.RegisterAlertScript(this, message);
            }

            return isValid;
        }

        private Int64 GetSelectedVoyageId()
        {
            string voyage = txtVoyage.Text.Trim();
            int startIndex = voyage.IndexOf('(');
            int endIndex = voyage.IndexOf(')');
            Int64 voyageId = 0;

            if (startIndex > 0 && endIndex > 0 && endIndex > startIndex)
            {
                Int64.TryParse(voyage.Substring(startIndex + 1, endIndex - startIndex - 1), out voyageId);
            }

            return voyageId;
        }

        private Int64 GetSelectedVesselId()
        {
            string vessel = txtVessel.Text.Trim();
            int startIndex = vessel.IndexOf('(');
            int endIndex = vessel.IndexOf(')');
            Int64 vesselId = 0;

            if (startIndex > 0 && endIndex > 0 && endIndex > startIndex)
            {
                Int64.TryParse(vessel.Substring(startIndex + 1, endIndex - startIndex - 1), out vesselId);
            }

            return vesselId;
        }

        private void GenerateReport()
        {
            ReportBLL cls = new ReportBLL();
            Int64 vesselId = GetSelectedVesselId();
            Int64 voyageId = GetSelectedVoyageId();
            List<ImpRegisterEntity> lstHeader = ReportBLL.GetImportRegisterHeader(Convert.ToInt32(ddlLine.SelectedValue), Convert.ToInt32(ddlLoc.SelectedValue), voyageId, vesselId);
            List<ImpRegisterEntity> lstFooter = ReportBLL.GetImportRegisterFooter(Convert.ToInt32(ddlLine.SelectedValue), Convert.ToInt32(ddlLoc.SelectedValue), voyageId, vesselId);

            LocalReportManager reportManager = new LocalReportManager(rptViewer, "ImportRegister", ConfigurationManager.AppSettings["ReportNamespace"].ToString(), ConfigurationManager.AppSettings["ReportPath"].ToString());
            string rptName = "ImportRegister.rdlc";

            rptViewer.Reset();
            rptViewer.LocalReport.Dispose();
            rptViewer.LocalReport.DataSources.Clear();
            rptViewer.LocalReport.ReportPath = this.Server.MapPath(this.Request.ApplicationPath) + ConfigurationManager.AppSettings["ReportPath"].ToString() + "/" + rptName;

            //rptViewer.LocalReport.ReportPath = Server.MapPath("/" + ConfigurationManager.AppSettings["ReportPath"].ToString() + "/" + rptName);
            rptViewer.LocalReport.DataSources.Add(new ReportDataSource("HeaderDataSet", lstHeader));
            rptViewer.LocalReport.DataSources.Add(new ReportDataSource("FooterDataSet", lstFooter));
            rptViewer.LocalReport.SetParameters(new ReportParameter("CompanyName", Convert.ToString(ConfigurationManager.AppSettings["CompanyName"])));
            rptViewer.LocalReport.SetParameters(new ReportParameter("Location", ddlLoc.SelectedItem.Text));
            rptViewer.LocalReport.SetParameters(new ReportParameter("Line", ddlLine.SelectedItem.Text));
            rptViewer.LocalReport.SetParameters(new ReportParameter("Vessel", txtVessel.Text));
            rptViewer.LocalReport.SetParameters(new ReportParameter("Voyage", txtVoyage.Text));
            rptViewer.LocalReport.SetParameters(new ReportParameter("ReportType", string.Empty));
            rptViewer.LocalReport.Refresh();
        }

        #endregion
    }
}