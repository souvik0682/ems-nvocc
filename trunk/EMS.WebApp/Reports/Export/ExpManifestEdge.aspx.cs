using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.Net;
using System.Security.Principal;
using EMS.Utilities;
using EMS.BLL;
using EMS.Common;
using EMS.Entity;
using System.Collections.Specialized;
using EMS.Utilities.Cryptography;
using System.Text;
using EMS.Utilities.ReportManager;
using System.Data;
namespace EMS.WebApp.Reports1
{
    public partial class ExpManifestEdge : System.Web.UI.Page
    {
        private string _reportName = string.Empty;
        protected void Page_Init(object sender, EventArgs e)
        {
            rptViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportEventHandler);
        }

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                Filler.FillData<ILocation>(ddlLine, new CommonBLL().GetActiveLocation(), "Name", "Id", "Location");
                tr1.Visible = false;
                tr2.Visible = false;
                tr3.Visible = false;
                //  GenerateReport();
            }
        }
        protected void ddlLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLine.SelectedIndex > 0)
            {

                Filler.FillData(ddlLocation, CommonBLL.GetExpLine(ddlLine.SelectedValue), "ProspectName", "ProspectID", "Line");
            }
        }
        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lng = ddlLine.SelectedValue.ToLong();
            if (lng > 0)
            {
                // Filler.FillData(ddlBlNo, CommonBLL.GetBLHeaderByBLNo(lng), "ImpLineBLNo", "ImpLineBLNo", "Bl. No.");
                Filler.FillData(ddlVessel, new expVoyageBLL().GetVessels(), "VesselName", "pk_VesselID", "Vessel");
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            if (ddlVoyBL.SelectedIndex == 1)
                GenerateReportM();
            else
                GenerateReport();

        }
      
        private void GenerateReportM()
        {

            LocalReportManager reportManager = new LocalReportManager(rptViewer, "MME", ConfigurationManager.AppSettings["ReportNamespace"].ToString(), ConfigurationManager.AppSettings["ReportPath"].ToString());
            string rptName = "MME.rdlc";
            string rptSelect="F";
            var main = ReportBLL.GetBlNumberFromVoyageID(Convert.ToInt32(ddlVoyage.SelectedValue), Convert.ToInt32(ddlVessel.SelectedValue), Convert.ToInt32(ddlPOD.SelectedValue));
            if (ddlCargoOrFreight.SelectedIndex == 2)
            {
                rptSelect = "C";
                _reportName = "CARGO MANIFEST - (" + ddlLocation.SelectedItem + ")";
            }
            else
                _reportName = "FREIGHT MANIFEST - (" + ddlLocation.SelectedItem + ")";

            rptViewer.Reset();
            rptViewer.LocalReport.Dispose();
            rptViewer.LocalReport.DataSources.Clear();
            rptViewer.LocalReport.ReportPath = this.Server.MapPath(this.Request.ApplicationPath) + ConfigurationManager.AppSettings["ReportPath"].ToString() + "/" + rptName;
            rptViewer.LocalReport.DataSources.Add(new ReportDataSource("dsVoyage", main.Tables[0]));
            rptViewer.LocalReport.SetParameters(new ReportParameter("ReportName", _reportName));
            rptViewer.LocalReport.SetParameters(new ReportParameter("FreightOrCargo", rptSelect));
            rptViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportEventHandler);
            rptViewer.LocalReport.Refresh();
           

            //LocalReportManager reportManager = new LocalReportManager(rptViewer, "MME", ConfigurationManager.AppSettings["ReportNamespace"].ToString(), ConfigurationManager.AppSettings["ReportPath"].ToString());
            //string rptName = "MME.rdlc";
            //var main = ReportBLL.GetBlNumberFromVoyageID(Convert.ToInt64(ddlVoyage.SelectedValue));

            //_reportName = "ReportName";
            //rptViewer.Reset();
            //rptViewer.LocalReport.Dispose();
            //rptViewer.LocalReport.DataSources.Clear();
            //rptViewer.LocalReport.ReportPath = this.Server.MapPath(this.Request.ApplicationPath) + ConfigurationManager.AppSettings["ReportPath"].ToString() + "/" + rptName;
            //rptViewer.LocalReport.DataSources.Add(new ReportDataSource("dsVoyage", main.Tables[0]));
            //rptViewer.LocalReport.SetParameters(new ReportParameter("ReportName", _reportName));
            ////rptViewer.LocalReport.SetParameters(new ReportParameter("RptHeader", "FREIGHT MANIFEST - (" + ddlLocation.SelectedItem + ")"));
            //rptViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportEventHandler);
            //rptViewer.LocalReport.Refresh();
           

        }
        
        private void GenerateReport()
        {
            LocalReportManager reportManager = new LocalReportManager(rptViewer, "ExpManifestEdge", ConfigurationManager.AppSettings["ReportNamespace"].ToString(), ConfigurationManager.AppSettings["ReportPath"].ToString());
            string rptName = "ExpManifestEdge.rdlc";
            string rptSelect = "F";
            if (ddlCargoOrFreight.SelectedIndex == 2)
            {
                rptSelect = "C";
                _reportName = "CARGO MANIFEST - (" + ddlLocation.SelectedItem + ")";
            }
            else
                _reportName = "FREIGHT MANIFEST - (" + ddlLocation.SelectedItem + ")";

           
            var main = ReportBLL.GetRptFrieghtManifest(ddlBLNo.SelectedValue);
            var TFS = ReportBLL.GetRptFrieghtManifest_TFS(ddlBLNo.SelectedValue);
            var CTRS = ReportBLL.GetRptFrieghtManifest_CTRS(ddlBLNo.SelectedValue);


            rptViewer.Reset();
            rptViewer.LocalReport.Dispose();
            rptViewer.LocalReport.DataSources.Clear();
            rptViewer.LocalReport.ReportPath = this.Server.MapPath(this.Request.ApplicationPath) + ConfigurationManager.AppSettings["ReportPath"].ToString() + "/" + rptName;
            rptViewer.LocalReport.DataSources.Add(new ReportDataSource("dsLin", main.Tables[0]));
            rptViewer.LocalReport.DataSources.Add(new ReportDataSource("dsTFS", TFS.Tables[0]));
            rptViewer.LocalReport.DataSources.Add(new ReportDataSource("dsCTRS", CTRS.Tables[0]));
            rptViewer.LocalReport.SetParameters(new ReportParameter("EXPBLID", ddlBLNo.SelectedValue));
            rptViewer.LocalReport.SetParameters(new ReportParameter("ReportName", _reportName));
            rptViewer.LocalReport.SetParameters(new ReportParameter("FreightOrCargo", rptSelect));
            rptViewer.LocalReport.Refresh();

        }
        protected void SubreportEventHandler(object sender, SubreportProcessingEventArgs e)
        {
            var BlNo = e.Parameters["EXPBLID"].Values[0].ToString();
            var main = ReportBLL.GetRptFrieghtManifest(BlNo);
            var TFS = ReportBLL.GetRptFrieghtManifest_TFS(BlNo);
            var CTRS = ReportBLL.GetRptFrieghtManifest_CTRS(BlNo);

            e.DataSources.Add(new ReportDataSource("dsLin", main.Tables[0]));
            e.DataSources.Add(new ReportDataSource("dsTFS", TFS.Tables[0]));
            e.DataSources.Add(new ReportDataSource("dsCTRS", CTRS.Tables[0]));


        }
        protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlVessel.SelectedIndex > 0)
            {
                //if (1 == 1)
                //{

                //    tr1.Visible = true;
                //    tr2.Visible = false;

                //}
                //else if (ddlVessel.SelectedIndex == 2)
                //{
                //    tr1.Visible = false;
                //    tr2.Visible = true;
                //}
                Filler.FillData(ddlVoyage, BookingBLL.GetExportVoyages(Convert.ToInt32(ddlVessel.SelectedValue)).Tables[0], "VoyageNo", "VoyageID", "Voyage No");
            }

        }

        protected void ddlVoyBL_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lng = ddlLine.SelectedValue.ToLong();
            if (lng > 0)
            {
                if (ddlVoyBL.SelectedIndex == 1)
                {
                    tr1.Visible = true;
                    tr3.Visible = true;
                    tr2.Visible = false;
                    Filler.FillData(ddlVessel, new expVoyageBLL().GetVessels(), "VesselName", "pk_VesselID", "Vessel");
                }
                else
                {
                    tr1.Visible = false;
                    tr3.Visible = false;
                    tr2.Visible = true;
                    Filler.FillData(ddlBLNo, CommonBLL.GetExpBLHeaderByBLNo(lng), "ExpBLNo", "ExpBLNo", "Bl. No.");
                }
            }
        }

        protected void ddlVoyage_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulatePort(Convert.ToInt32(ddlVessel.SelectedValue), Convert.ToInt32(ddlVoyage.SelectedValue));
        }

        private void PopulatePort(Int32 Vessel, Int32 Voyage)
        {
            ReportBLL objBLL = new ReportBLL();
            DataSet ds = objBLL.GetPOD(Vessel, Voyage);
            ddlPOD.DataValueField = "fk_PortID";
            ddlPOD.DataTextField = "PortName";
            ddlPOD.DataSource = ds;
            ddlPOD.DataBind();
            ddlPOD.Items.Insert(0, new ListItem("All", "0"));
        }

    }
}