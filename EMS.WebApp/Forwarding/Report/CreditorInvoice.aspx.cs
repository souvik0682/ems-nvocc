using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Common;
using EMS.Entity.Report;
using System.Data;
using EMS.Entity;
using Microsoft.Reporting.WebForms;
using System.Globalization;
using System.Configuration;
using EMS.BLL;
using EMS.Utilities.ReportManager;

namespace EMS.WebApp.Forwarding.Report
{
    public partial class CreditorInvoice : System.Web.UI.Page
    {
        private IFormatProvider _culture = new CultureInfo(ConfigurationManager.AppSettings["Culture"].ToString());

        protected void Page_Load(object sender, EventArgs e)
        {
            txtFromDt.Text = "2014-01-01";
            txtToDt.Text = "2014-12-13"; 
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            //ReportBLL cls = new ReportBLL();
            //DateTime dtFrom = Convert.ToDateTime(txtFromDt.Text, _culture);
            //DateTime dtTo = Convert.ToDateTime(txtToDt.Text, _culture);
            //List<rptCredInvEntity> lstReg = ReportBLL.GetCredInvoice(Convert.ToInt32(ddlCreditor.SelectedValue), dtFrom, dtTo);

            //LocalReportManager reportManager = new LocalReportManager(rptViewer, "ExportInvRegister", ConfigurationManager.AppSettings["ReportNamespace"].ToString(), ConfigurationManager.AppSettings["ReportPath"].ToString());
            //string rptName = "CredInvoice.rdlc";

            //rptViewer.Reset();
            //rptViewer.LocalReport.Dispose();
            //rptViewer.LocalReport.DataSources.Clear();
            //rptViewer.LocalReport.ReportPath = this.Server.MapPath(this.Request.ApplicationPath) + ConfigurationManager.AppSettings["ReportPath"].ToString() + "/" + rptName;

            //rptViewer.LocalReport.DataSources.Add(new ReportDataSource("CredInvDS", lstReg));
            
            //rptViewer.LocalReport.SetParameters(new ReportParameter("CompanyName", Convert.ToString(ConfigurationManager.AppSettings["CompanyName"])));
            //rptViewer.LocalReport.SetParameters(new ReportParameter("FromDate", txtFromDt.Text.Trim()));
            //rptViewer.LocalReport.SetParameters(new ReportParameter("ToDate", txtToDt.Text.Trim()));
            //rptViewer.LocalReport.SetParameters(new ReportParameter("Creditor", ddlCreditor.SelectedItem.Text));

            rptViewer.LocalReport.Refresh();
        }
    }
}