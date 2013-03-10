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

            string invoiceNo = ((TextBox)AutoCompletepInvoice1.FindControl("txtInvoice")).Text;
            DataSet ds = EMS.BLL.BLLReport.GetMoneyRcptDetails(invoiceNo.Trim());
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

        protected void btnGen_Click(object sender, EventArgs e)
        {
            string invoiceNo = ((TextBox)AutoCompletepInvoice1.FindControl("txtInvoice")).Text;
           EMS.Utilities.GeneralFunctions.PopulateDropDownList(ddlMnyRcpt,  BLLReport.FillDDLMoneyRcpt(invoiceNo));
        }
    }
}