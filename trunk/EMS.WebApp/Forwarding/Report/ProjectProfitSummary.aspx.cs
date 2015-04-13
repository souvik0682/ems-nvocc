using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EMS.Common;
using EMS.Utilities;
using EMS.BLL;
using EMS.Entity;
using EMS.Utilities.ResourceManager;

namespace EMS.WebApp.Forwarding.Report
{
    public partial class ProjectProfitSummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FillDebtors();
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
                dt2 = Convert.ToDateTime(txtEndDt.Text.Trim());
                dtExcel = cls.GetProjectProfitSummary(dt1, dt2, ddlLocation.SelectedValue.ToInt(), ddlJobStatus.SelectedValue.ToString());
                //dtExcel = cls.GetProjectProfitSummary(dt1, dt2, 1, "O");

                ExporttoExcel(dtExcel);
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
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=ProjectProfitSummary.xls");
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

        void FillDebtors()
        {
            DataTable principal = new CommonBLL().GetfwdPartyByType("D");
            ddlCustomer.DataSource = principal;
            ddlCustomer.DataTextField = "LineName";
            ddlCustomer.DataValueField = "LineID";
            ddlCustomer.DataBind();
            ddlCustomer.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }
}