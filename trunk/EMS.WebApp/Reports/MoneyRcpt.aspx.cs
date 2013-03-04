using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.BLL;
using EMS.Utilities.ReportManager;
using System.Data;
using System.Configuration;
using Microsoft.Reporting.WebForms;

namespace EMS.WebApp.Reports
{
    public partial class MoneyRcpt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }

        private void GenerateReport()
        {
            ReportBLL cls = new ReportBLL();

            LocalReportManager reportManager = new LocalReportManager(rptViewer, "MoneyReciept", ConfigurationManager.AppSettings["ReportNamespace"].ToString(), ConfigurationManager.AppSettings["ReportPath"].ToString());
            string rptName = "MoneyReciept.rdlc";


            DataSet ds = EMS.BLL.BLLReport.GetMoneyRcptDetails(txtInvoiceNo.Text.Trim());
            try
            {
                rptViewer.Reset();
                rptViewer.LocalReport.Dispose();
                rptViewer.LocalReport.DataSources.Clear();
                rptViewer.LocalReport.ReportPath = this.Server.MapPath(this.Request.ApplicationPath) + ConfigurationManager.AppSettings["ReportPath"].ToString() + "/" + rptName;
                rptViewer.LocalReport.SetParameters(new ReportParameter("CompanyName", Convert.ToString(ConfigurationManager.AppSettings["CompanyName"])));
                rptViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", ds.Tables[0]));
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