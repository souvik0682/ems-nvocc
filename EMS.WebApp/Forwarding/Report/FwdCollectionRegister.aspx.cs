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
    public partial class FwdCollectionRegister : System.Web.UI.Page
    {
        private IFormatProvider _culture = new CultureInfo(ConfigurationManager.AppSettings["Culture"].ToString());

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            rptViewer.LocalReport.SetParameters(new ReportParameter("CompanyName", Convert.ToString(ConfigurationManager.AppSettings["CompanyName"])));
            rptViewer.LocalReport.SetParameters(new ReportParameter("FromDate", txtFromDt.Text.Trim()));
            rptViewer.LocalReport.SetParameters(new ReportParameter("ToDate", txtToDt.Text.Trim()));
            rptViewer.LocalReport.SetParameters(new ReportParameter("LocationName", ddlLocation.SelectedItem.Text));

            rptViewer.LocalReport.Refresh();
        }

        protected void odsCollReg_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["StartDate"] = Convert.ToDateTime(txtFromDt.Text, _culture).ToShortDateString();
            e.InputParameters["EndDate"] = Convert.ToDateTime(txtToDt.Text, _culture).ToShortDateString();
            e.InputParameters["LocationID"] = ddlLocation.SelectedValue;
        }
    }
}