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
namespace EMS.WebApp.Reports
{
    public partial class ExpManifestEdge : System.Web.UI.Page
    {
     
        protected void Page_Load(object sender, EventArgs e)
        {

         
            if (!IsPostBack)
            {
                Filler.FillData<ILocation>(ddlLine, new CommonBLL().GetActiveLocation(), "Name", "Id", "Location");
                //tr1.Visible = false;
                tr2.Visible = false;
               // GenerateReport();
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
               Filler.FillData(ddlBlNo, CommonBLL.GetExpBLHeaderByBLNo(lng), "ExpBLNo", "ExpBLNo", "Bl. No.");
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }
        private void GenerateReport()
        {
            
            LocalReportManager reportManager = new LocalReportManager(rptViewer, "ExpManifestEdge", ConfigurationManager.AppSettings["ReportNamespace"].ToString(), ConfigurationManager.AppSettings["ReportPath"].ToString());
            string rptName = "ExpManifestEdge.rdlc";
            //("MUM/UA/13-14/000001");// (ddlBlNo.SelectedValue);//
            var main = ReportBLL.GetRptFrieghtManifest(ddlBlNo.SelectedValue);
            var TFS = ReportBLL.GetRptFrieghtManifest_TFS(ddlBlNo.SelectedValue);
            var CTRS = ReportBLL.GetRptFrieghtManifest_CTRS(ddlBlNo.SelectedValue);


            rptViewer.Reset();
            rptViewer.LocalReport.Dispose();
            rptViewer.LocalReport.DataSources.Clear();
            rptViewer.LocalReport.ReportPath = this.Server.MapPath(this.Request.ApplicationPath) + ConfigurationManager.AppSettings["ReportPath"].ToString() + "/" + rptName;
            rptViewer.LocalReport.DataSources.Add(new ReportDataSource("dsLin", main.Tables[0]));
            rptViewer.LocalReport.DataSources.Add(new ReportDataSource("dsTFS", TFS.Tables[0]));
            rptViewer.LocalReport.DataSources.Add(new ReportDataSource("dsCTRS", CTRS.Tables[0]));
            rptViewer.LocalReport.SetParameters(new ReportParameter("EXPBLID", ddlBlNo.SelectedValue));
            rptViewer.LocalReport.SetParameters(new ReportParameter("ReportName", "FREIGHT MANIFEST - ("+ddlLocation.SelectedItem+")"));
            rptViewer.LocalReport.Refresh();
           
        }

        protected void ddlBLMan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBLMan.SelectedIndex > 0) {
                if (ddlBLMan.SelectedIndex == 1)
                {
                   
                    //tr1.Visible = true;
                    tr2.Visible = false;

                }
                else if (ddlBLMan.SelectedIndex == 2)
                {
                    //tr1.Visible = false;
                    tr2.Visible = true;
                }
            }

        }
    }
}