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
    public partial class IGMCargoDesc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            if (!IsPostBack)
            {
                GeneralFunctions.PopulateDropDownList(ddlLine, EMS.BLL.EquipmentBLL.DDLGetLine());
                GeneralFunctions.PopulateDropDownList(ddlVessel, dbinteract.PopulateDDLDS("trnVessel", "pk_VesselID", "VesselName"), true);
                GeneralFunctions.PopulateDropDownList(ddlLoc, dbinteract.PopulateDDLDS("DSR.dbo.mstLocation", "pk_LocID", "LocName", true), true);
                GenerateReport();
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
            ReportBLL cls = new ReportBLL();

            LocalReportManager reportManager = new LocalReportManager(rptViewer, "IGMCargoDesc", ConfigurationManager.AppSettings["ReportNamespace"].ToString(), ConfigurationManager.AppSettings["ReportPath"].ToString());
            string rptName = "IGMCargoDesc.rdlc";
            int vesselId = Convert.ToInt32(ddlVessel.SelectedValue);
            int voyageId = ddlVoyage.SelectedIndex > 0 ? Convert.ToInt32(ddlVoyage.SelectedValue) : 0;

            DataSet ds = EMS.BLL.IGMReportBLL.GetRptCargoDesc(vesselId, voyageId);
            try
            {
                rptViewer.Reset();
                rptViewer.LocalReport.Dispose();
                rptViewer.LocalReport.DataSources.Clear();
                rptViewer.LocalReport.ReportPath = this.Server.MapPath(this.Request.ApplicationPath) +"/" +ConfigurationManager.AppSettings["ReportPath"].ToString() + "/" + rptName;

                rptViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds.Tables[0]));
                rptViewer.LocalReport.Refresh();
            }
            catch {
                
                
            }
               
               
           
        }
        private void GenerateDDLVoyage()
        {
            int vesselId = Convert.ToInt32(ddlVessel.SelectedValue);
            int LocationId = Convert.ToInt32(ddlLoc.SelectedValue);
            int nvoccID = Convert.ToInt32(ddlLine.SelectedValue);
            GeneralFunctions.PopulateDropDownList(ddlVoyage, EMS.BLL.VoyageBLL.GetVoyageAccVessel_Loc(LocationId, vesselId, nvoccID));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }

        protected void ddlLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GenerateDDLVoyage();
        }
    }
}