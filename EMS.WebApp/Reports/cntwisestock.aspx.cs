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
using EMS.Entity.Report;
using Microsoft.Reporting.WebForms;

namespace EMS.WebApp.Reports
{
    public partial class cntwisestock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                FillDropDown();
            }
        }

        void FillDropDown()
        {
            ListItem Li;
            Li = new ListItem("Select", "0");
            PopulateDropDown((int)Enums.DropDownPopulationFor.Location, ddlLoc, 0, 0);
            ddlLoc.Items.Insert(0, Li);

            Li = new ListItem("Select", "0");
            PopulateDropDown((int)Enums.DropDownPopulationFor.Line, ddlLine, 0, 0);
            ddlLine.Items.Insert(0, Li);
        }

        void PopulateDropDown(int Number, DropDownList ddl, int? Filter1, int? Filter2)
        {
            CommonBLL.PopulateDropdown(Number, ddl, Filter1, Filter2);
        }

        protected void LocationLine_Changed(object sender, EventArgs e)
        {
            autoComplete1.ContextKey = ddlLoc.SelectedValue + "|" + ddlLine.SelectedValue;            
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }

        private void GenerateReport()
        {
            ReportBLL cls = new ReportBLL();

            LocalReportManager reportManager = new LocalReportManager(rptViewer, "CntrWiseStock", ConfigurationManager.AppSettings["ReportNamespace"].ToString(), ConfigurationManager.AppSettings["ReportPath"].ToString());
            string rptName = "CntrWiseStock.rdlc";


            //DataSet ds = EMS.BLL.LogisticReportBLL.GetRptCntrStockDetails(ddlLine.SelectedValue, ddlLoc.SelectedValue, ddlStatus.SelectedValue, ddlContainerType.SelectedValue, txtdtStock.Text.Trim());
            IList<ContainerWiseStockEntity> ContainersStatus = EMS.BLL.LogisticReportBLL.GetRptContainerwiseStockSummery(txtContainerNo.Text, Convert.ToInt32(ddlLine.SelectedValue), Convert.ToInt32(ddlLoc.SelectedValue));
            try
            {
                string Size = string.Empty;
                string LStatus = string.Empty;
                string lLocation = string.Empty;

                var maxDate = ContainersStatus.Max(x => x.MovementDate);

                IEnumerable<ContainerWiseStockEntity> Items = from itm in ContainersStatus
                                                              where itm.MovementDate == maxDate
                                                              select itm;

                Size = Items.ToList()[0].Size;
                LStatus = Items.ToList()[0].MoveAbbr;
                lLocation = Items.ToList()[0].Locabbr;


                rptViewer.Reset();
                rptViewer.LocalReport.Dispose();
                rptViewer.LocalReport.DataSources.Clear();
                rptViewer.LocalReport.ReportPath = this.Server.MapPath(this.Request.ApplicationPath) + ConfigurationManager.AppSettings["ReportPath"].ToString() + "/" + rptName;
                rptViewer.LocalReport.SetParameters(new ReportParameter("CompanyName", Convert.ToString(ConfigurationManager.AppSettings["CompanyName"])));
                rptViewer.LocalReport.SetParameters(new ReportParameter("CntNo", txtContainerNo.Text));
                rptViewer.LocalReport.SetParameters(new ReportParameter("CntSize", Size));
                rptViewer.LocalReport.SetParameters(new ReportParameter("LastStatus", LStatus));
                rptViewer.LocalReport.SetParameters(new ReportParameter("LastLocation", lLocation));
                rptViewer.LocalReport.DataSources.Add(new ReportDataSource("ContainerWiseStock", ContainersStatus));
                rptViewer.LocalReport.Refresh();
            }
            catch(Exception ex)
            {
                string s = ex.Message;

            }



        }
    }
}