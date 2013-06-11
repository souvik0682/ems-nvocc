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
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        private int _userId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            CheckUserAccess();
            BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            if (!IsPostBack)
            {
                GeneralFunctions.PopulateDropDownList(ddlLine, EMS.BLL.EquipmentBLL.DDLGetLine());
                GeneralFunctions.PopulateDropDownList(ddlLoc, dbinteract.PopulateDDLDS("DSR.dbo.mstLocation", "pk_LocID", "LocAbbr", true), true);
                GeneralFunctions.PopulateDropDownList(ddlStatus, EMS.BLL.EquipmentBLL.DDLGetStatus());
                GeneralFunctions.PopulateDropDownList(ddlContainerType, EMS.BLL.EquipmentBLL.DDLGetContainerType());
               //GenerateReport();
            }
        }

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

            LocalReportManager reportManager = new LocalReportManager(rptViewer, "CntrStockDetail", ConfigurationManager.AppSettings["ReportNamespace"].ToString(), ConfigurationManager.AppSettings["ReportPath"].ToString());
            string rptName = "CntrStockDetail.rdlc";
         
           
            DataSet ds = EMS.BLL.LogisticReportBLL.GetRptCntrStockDetails(ddlLine.SelectedValue,ddlLoc.SelectedValue, ddlStatus.SelectedValue, ddlContainerType.SelectedValue, txtdtStock.Text.Trim());
            try
            {
                rptViewer.Reset();
                rptViewer.LocalReport.Dispose();
                rptViewer.LocalReport.DataSources.Clear();
                rptViewer.LocalReport.ReportPath = this.Server.MapPath(this.Request.ApplicationPath) + ConfigurationManager.AppSettings["ReportPath"].ToString() + "/" + rptName;
                rptViewer.LocalReport.SetParameters(new ReportParameter("CompanyName", Convert.ToString(ConfigurationManager.AppSettings["CompanyName"])));
                rptViewer.LocalReport.SetParameters(new ReportParameter("StockDate", txtdtStock.Text ));
                rptViewer.LocalReport.SetParameters(new ReportParameter("loc", ddlLoc.SelectedItem.Text));
                rptViewer.LocalReport.SetParameters(new ReportParameter("line", ddlLine.SelectedItem.Text));
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