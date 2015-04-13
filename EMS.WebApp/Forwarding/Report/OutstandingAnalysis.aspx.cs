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
    public partial class OutstandingAnalysis : System.Web.UI.Page
    {
        private IFormatProvider _culture = new CultureInfo(ConfigurationManager.AppSettings["Culture"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPartyTypeDDl();
            }
        }

        private void GenerateReport()
        {
            ReportBLL cls = new ReportBLL();

            try
            {
                DateTime dt1 = DateTime.Today;
                DateTime dt2 = DateTime.Today;
                DataTable dtExcel = new DataTable();
                dt1 = Convert.ToDateTime(txtStartDt.Text.Trim());
                //dt2 = Convert.ToDateTime(txtEndDt.Text.Trim());
                dtExcel = cls.GetOutstandingList(dt1, Convert.ToInt32(ddlParty.SelectedValue));
                if (ddlReportType.SelectedValue == "S")
                    ExporttoExcel(dtExcel);
                else
                    GenerateOutDetails();
            }
            catch (System.Threading.ThreadAbortException lException)
            {
            }


        }

        private void ExporttoExcel(DataTable table)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=OutstandingList.xls");
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
              "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
              "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");

            for (int j = 0; j < table.Columns.Count; j++)
            {
                HttpContext.Current.Response.Write("<Td>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write(table.Columns[j].ColumnName);
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");
            }
            HttpContext.Current.Response.Write("</TR>");
            foreach (DataRow row in table.Rows)
            {
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            HttpContext.Current.Response.End();
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }

        private void LoadPartyTypeDDl()
        {
            var partyType = new EstimateBLL().GetBillingGroupMaster((ISearchCriteria)null);
           
            ddlPartyType.DataSource = partyType;

            ddlPartyType.DataTextField = "PartyType";
            ddlPartyType.DataValueField = "pk_PartyTypeID";
            ddlPartyType.DataBind();
            ddlPartyType.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        protected void ddlPartyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GetPartyValuesSetToDdl(ddlPartyType.SelectedValue.ToInt());
            GetPartyValuesSetToDdl(Convert.ToInt32(ddlPartyType.SelectedValue));
        }

        private void GetPartyValuesSetToDdl(int PartyType)
        {
            ddlParty.Items.Clear();

            DataSet dll = new DataSet();

            dll = new EstimateBLL().GetAllParty(PartyType);

            ddlParty.DataSource = dll;
            ddlParty.DataTextField = "PartyName";
            ddlParty.DataValueField = "pk_fwPartyID";
            ddlParty.DataBind();
            ddlParty.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void GenerateOutDetails()
        {
            {
                rptViewer.LocalReport.SetParameters(new ReportParameter("CompanyName", Convert.ToString(ConfigurationManager.AppSettings["CompanyName"])));
                rptViewer.LocalReport.SetParameters(new ReportParameter("FromDate", txtStartDt.Text.Trim()));
                rptViewer.LocalReport.SetParameters(new ReportParameter("Creditor", ddlParty.SelectedValue));
                rptViewer.LocalReport.SetParameters(new ReportParameter("PartyName", ddlParty.SelectedItem.ToString()));

                rptViewer.LocalReport.Refresh();
            }
        }

        protected void odsParty_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["FromDate"] = Convert.ToDateTime(txtStartDt.Text, _culture).ToShortDateString();
            //e.InputParameters["EndDate"] = Convert.ToDateTime(txtToDt.Text, _culture).ToShortDateString();
            e.InputParameters["Creditor"] = ddlParty.SelectedValue;
        }

    }
}