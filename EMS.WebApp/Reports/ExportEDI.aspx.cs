using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.BLL;
using EMS.Utilities;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using EMS.Utilities.ReportManager;
using EMS.Entity.Report;
using EMS.Common;
using System.Data;

namespace EMS.WebApp.Reports
{
    public partial class ExportEDI : System.Web.UI.Page
    {
        #region Private Member Variables

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
            if (!IsPostBack)
            {
                RetriveParameters();
                PopulateControls();
            }
            else
            {
                if (Convert.ToInt64(ddlVoyage.SelectedValue) == 0)
                {
                    //Int64 vesselId = GetSelectedVesselId();

                    int vesselId = Convert.ToInt32(hdnVessel.Value);
                    PopulateVoyage(vesselId);
                    //Filler.FillData(ddlVoyage, CommonBLL.GetVoyages(vesselId.ToString()), "VoyageNo", "VoyageID", "Voyage");
                }
            }
        }

        protected void txtVessel_TextChanged(object sender, EventArgs e)
        {
            int vesselId = Convert.ToInt32(hdnVessel.Value);
            PopulateVoyage(vesselId);
            //ddlVoyage.SelectedValue = "0";

        }

        protected void ddlVoyage_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateLoadingPort();
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

        private void PopulateVoyage(int vesselID)
        {
            //BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            DataSet ds = BookingBLL.GetExportVoyages(vesselID, ddlLoc.SelectedValue.ToInt());
            ddlVoyage.DataValueField = "VoyageID";
            ddlVoyage.DataTextField = "VoyageNo";
            ddlVoyage.DataSource = ds;
            ddlVoyage.DataBind();
            ddlVoyage.Items.Insert(0, new ListItem(Constants.DROPDOWNLIST_DEFAULT_TEXT, Constants.DROPDOWNLIST_DEFAULT_VALUE));
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

        private void PopulateControls()
        {
            PopulateLocation();

            if (_LocationSpecific)
            {
                ddlLoc.SelectedValue = Convert.ToString(_userLocation);
                ddlLoc.Enabled = false;
            }
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

        private void PopulateLoadingPort()
        {
            DataSet ds = EDIBLL.GetLoadingPort(Convert.ToInt64(ddlVoyage.SelectedValue));

            if (!ReferenceEquals(ds, null))
            {
                ddlPort.DataValueField = "PortId";
                ddlPort.DataTextField = "PortName";
                ddlPort.DataSource = ds;
                ddlPort.DataBind();
            }

            ddlPort.Items.Insert(0, new ListItem(Constants.DROPDOWNLIST_DEFAULT_TEXT, Constants.DROPDOWNLIST_DEFAULT_VALUE));
        }

        private bool ValidateData(out string message)
        {
            bool isValid = true;
            int slNo = 1;
            message = GeneralFunctions.FormatAlertMessage("Please correct the following errors:");

            if (ddlLoc.SelectedValue == "0")
            {
                isValid = false;
                message += GeneralFunctions.FormatAlertMessage(slNo, "Please select valid location");
                slNo++;
            }

            if (string.IsNullOrEmpty(txtVessel.Text))
            {
                isValid = false;
                message += GeneralFunctions.FormatAlertMessage(slNo, "Please select vessel name");
                slNo++;
            }

            //Validate selected voyage name and vessel no.
            Int64 voyageId = Convert.ToInt64(ddlVoyage.SelectedValue);
            //Int64 vesselId = GetSelectedVesselId();
            Int64 vesselId = 0;

            if (hdnVessel.Value.Trim() != string.Empty)
                Int64.TryParse(hdnVessel.Value, out vesselId);

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

            if (ddlPort.SelectedValue == "0")
            {
                isValid = false;
                message += GeneralFunctions.FormatAlertMessage(slNo, "Please select port of loading");
                slNo++;
            }

            if (txtMLO.Text.Trim() == string.Empty)
            {
                isValid = false;
                message += GeneralFunctions.FormatAlertMessage(slNo, "Please enter main line operator");
                slNo++;
            }

            if (!isValid)
            {
                GeneralFunctions.RegisterAlertScript(this, message);
            }

            return isValid;
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
            string agentName = string.Empty;
            int vesselId = Convert.ToInt32(hdnVessel.Value);
            //Int64 vesselId = GetSelectedVesselId();
            Int64 voyageId = Convert.ToInt64(ddlVoyage.SelectedValue);
            List<ExportEDIEntity> lstHeader = ReportBLL.GetExportEdi(vesselId, voyageId, Convert.ToInt32(ddlPort.SelectedValue), Convert.ToInt32(ddlLoc.SelectedValue));
            List<ExportEDIEntity> lstData = ReportBLL.GetExportEdiCntr(vesselId, voyageId, Convert.ToInt32(ddlPort.SelectedValue), Convert.ToInt32(ddlLoc.SelectedValue));

            DataSet dsCompany = CommonBLL.GetCompanyDetails(1);

            if (!ReferenceEquals(dsCompany, null) && dsCompany.Tables.Count > 0 && dsCompany.Tables[0].Rows.Count > 0)
            {
                agentName = Convert.ToString(dsCompany.Tables[0].Rows[0]["CompName"]);
            }

            LocalReportManager reportManager = new LocalReportManager(rptViewer, "ExportEDI", ConfigurationManager.AppSettings["ReportNamespace"].ToString(), ConfigurationManager.AppSettings["ReportPath"].ToString());
            reportManager.HasSubReport = true;
            reportManager.AddParameter("VesselVoyage", txtVessel.Text.Trim() + " V." + ddlVoyage.SelectedItem.Text);
            reportManager.AddParameter("AgentName", agentName);
            reportManager.AddParameter("MainLineOperator", txtMLO.Text.Trim());
            reportManager.AddDataSource(new ReportDataSource("DataSetHeader", lstHeader));
            reportManager.AddSubReportDataSource(new ReportDataSource("DataSetContainer", lstData));
            reportManager.Show();
        }

        #endregion
    }
}