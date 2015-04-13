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
    public partial class UnitSummary : System.Web.UI.Page
    {
        private IFormatProvider _culture = new CultureInfo(ConfigurationManager.AppSettings["Culture"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            DateTime theDate = Convert.ToDateTime(txtFromDt.Text);
            DateTime yearInTheFuture = Convert.ToDateTime(theDate.AddYears(1)).AddDays(-1);
            if (yearInTheFuture < Convert.ToDateTime(txtToDt.Text))
            {
                lblError.Text = "Invalid Date Range";
                return;
            }
            rptViewer.LocalReport.SetParameters(new ReportParameter("CompanyName", Convert.ToString(ConfigurationManager.AppSettings["CompanyName"])));
            rptViewer.LocalReport.SetParameters(new ReportParameter("FromDate", txtFromDt.Text.Trim()));
            rptViewer.LocalReport.SetParameters(new ReportParameter("ToDate", txtToDt.Text.Trim()));
            rptViewer.LocalReport.SetParameters(new ReportParameter("Line", ddlLine.SelectedValue.ToString()));
            rptViewer.LocalReport.SetParameters(new ReportParameter("Location", ddlLine.SelectedValue.ToString()));
            rptViewer.LocalReport.Refresh();
        }

        protected void odsRevSummary_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["StartDate"] = Convert.ToDateTime(txtFromDt.Text, _culture).ToShortDateString();
            e.InputParameters["EndDate"] = Convert.ToDateTime(txtToDt.Text, _culture).ToShortDateString();
            e.InputParameters["Line"] = Convert.ToInt32(ddlLine.SelectedValue);
            e.InputParameters["Location"] = Convert.ToInt32(ddlLocation.SelectedValue);
        }
    }
}