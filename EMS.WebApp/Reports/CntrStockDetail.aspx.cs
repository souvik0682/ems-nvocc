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
    public partial class CntrStockDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            if (!IsPostBack)
            {
                GeneralFunctions.PopulateDropDownList(ddlLine, EMS.BLL.EquipmentBLL.DDLGetLine());
                GeneralFunctions.PopulateDropDownList(ddlLoc, dbinteract.PopulateDDLDS("DSR.dbo.mstLocation", "pk_LocID", "LocAbbr", true), true);
                GenerateReport();
            }
        }

        private void GenerateReport()
        {
            ReportBLL cls = new ReportBLL();

            LocalReportManager reportManager = new LocalReportManager(rptViewer, "CntrStockDetail", ConfigurationManager.AppSettings["ReportNamespace"].ToString(), ConfigurationManager.AppSettings["ReportPath"].ToString());
            string rptName = "CntrStockDetail.rdlc";
         

            DataSet ds = EMS.BLL.LogisticReportBLL.GetRptCntrStockDetails(ddlLine.SelectedValue,ddlLoc.SelectedValue,txtdtStock.Text.Trim());
            try
            {
                rptViewer.Reset();
                rptViewer.LocalReport.Dispose();
                rptViewer.LocalReport.DataSources.Clear();
                rptViewer.LocalReport.ReportPath = this.Server.MapPath(this.Request.ApplicationPath) + ConfigurationManager.AppSettings["ReportPath"].ToString() + "/" + rptName;

                rptViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", ds.Tables[0]));
                rptViewer.LocalReport.Refresh();
            }
            catch
            {


            }



        }
       

        protected void btnSave_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }
    }
}