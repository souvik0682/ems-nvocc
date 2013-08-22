using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Utilities;
using EMS.BLL;
using EMS.Utilities.ReportManager;
using System.Configuration;
using System.Data;
using Microsoft.Reporting.WebForms;

namespace EMS.WebApp.Reports
{
    public partial class IGMForm2ICD : System.Web.UI.Page
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

        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            if (!IsPostBack)
            {
                GeneralFunctions.PopulateDropDownList(ddlLine, EMS.BLL.EquipmentBLL.DDLGetLine());
                GeneralFunctions.PopulateDropDownList(ddlVessel, dbinteract.PopulateDDLDS("trnVessel", "pk_VesselID", "VesselName"), true);
                GeneralFunctions.PopulateDropDownList(ddlLoc, dbinteract.PopulateDDLDS("DSR.dbo.mstLocation", "pk_LocID", "LocName", true), true);
                if (_LocationSpecific)
                {
                    ddlLoc.SelectedValue = Convert.ToString(_userLocation);
                    ddlLoc.Enabled = false;
                }
                // GenerateReport();
            }

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

        protected void ddlVoyage_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateDDLVoyage();
        }

        private void GenerateReport()
        {
            string pod = ((TextBox)AutoCompletepPort1.FindControl("txtPort")).Text;
            pod = pod.Contains(',') ? pod.Split(',')[1] : "";
            if (pod.Trim() == "")
            {
                lblError.Attributes.Add("Style", "display:block");
                return;
            }
            else
                lblError.Attributes.Add("Style", "display:none");

            ReportBLL cls = new ReportBLL();

            LocalReportManager reportManager = new LocalReportManager(rptViewer, "IGMCargoDesc", ConfigurationManager.AppSettings["ReportNamespace"].ToString(), ConfigurationManager.AppSettings["ReportPath"].ToString());
            string rptName = "IGMCargoDesc.rdlc";
            int vesselId = Convert.ToInt32(ddlVessel.SelectedValue);
            int voyageId = ddlVoyage.SelectedIndex > 0 ? Convert.ToInt32(ddlVoyage.SelectedValue) : 0;
            DBInteraction dbinteract = new DBInteraction();
            DataSet ds = EMS.BLL.IGMReportBLL.GetRptCargoDescICD(vesselId, voyageId, dbinteract.GetId("Port", pod));
            try
            {
                rptViewer.Reset();
                rptViewer.LocalReport.Dispose();
                rptViewer.LocalReport.DataSources.Clear();
                rptViewer.LocalReport.ReportPath = this.Server.MapPath(this.Request.ApplicationPath) + ConfigurationManager.AppSettings["ReportPath"].ToString() + "/" + rptName;
                rptViewer.LocalReport.SetParameters(new ReportParameter("ReportName", "C A R G O    D E C L A R A T I O N [I C D]"));
                rptViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds.Tables[0]));

                rptViewer.LocalReport.Refresh();
            }
            catch
            {

            }

        }
        private void GenerateDDLVoyage()
        {
            int vesselId = Convert.ToInt32(ddlVessel.SelectedValue);
            int LocationId = Convert.ToInt32(ddlLoc.SelectedValue);
            int nvoccID = Convert.ToInt32(ddlLine.SelectedValue);
            GeneralFunctions.PopulateDropDownList(ddlVoyage, EMS.BLL.VoyageBLL.GetVoyageAccVessel_Loc_ICD(LocationId, vesselId, nvoccID));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }

        protected void ddlLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GenerateDDLVessel();
            ddlLine.SelectedIndex = 0;
            ddlVessel.SelectedIndex = 0;
            if (ddlVoyage.Items.Count > 0)
            {
                ddlVoyage.SelectedIndex = 0;
            }
            
            ((TextBox)AutoCompletepPort1.FindControl("txtPort")).Text = "";
            
        }

        protected void ddlLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateDDLVessel();
        }

        private void GenerateDDLVessel()
        {

            //int LocationId = Convert.ToInt32(ddlLoc.SelectedValue);
            int nvoccID = Convert.ToInt32(ddlLine.SelectedValue);
            GeneralFunctions.PopulateDropDownList(ddlVessel, EMS.BLL.VoyageBLL.GetVessleByNVOCC(nvoccID), true);
        }


    }
}