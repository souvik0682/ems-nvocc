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

namespace EMS.WebApp.Reports
{
    public partial class ExcRateList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            if (!IsPostBack)
            {
                //GeneralFunctions.PopulateDropDownList(ddlLine, EMS.BLL.EquipmentBLL.DDLGetLine());
                //GeneralFunctions.PopulateDropDownList(ddlVessel, dbinteract.PopulateDDLDS("trnVessel", "pk_VesselID", "VesselName"), true);
                //GeneralFunctions.PopulateDropDownList(ddlLoc, dbinteract.PopulateDDLDS("DSR.dbo.mstLocation", "pk_LocID", "LocName", true), true);
                //GenerateReport();
            }

        }

        //protected void ddlVoyage_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}

        //protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    GenerateDDLVoyage();
        //}

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
                dtExcel = cls.GetExchangeRate(dt1, dt2);

                ExporttoExcel(dtExcel);
            }
            catch (System.Threading.ThreadAbortException lException)
            {

                // do nothing

            }

            //LocalReportManager reportManager = new LocalReportManager(rptViewer, "IGMCargoDesc", ConfigurationManager.AppSettings["ReportNamespace"].ToString(), ConfigurationManager.AppSettings["ReportPath"].ToString());
            //string rptName = "IGMCargoDesc.rdlc";
            //int vesselId = Convert.ToInt32(ddlVessel.SelectedValue);
            //int voyageId = ddlVoyage.SelectedIndex > 0 ? Convert.ToInt32(ddlVoyage.SelectedValue) : 0;

            //DataSet ds = EMS.BLL.IGMReportBLL.GetRptCargoDesc(vesselId, voyageId, 0);
            //try
            //{
            //    rptViewer.Reset();
            //    rptViewer.LocalReport.Dispose();
            //    rptViewer.LocalReport.DataSources.Clear();
            //    rptViewer.LocalReport.ReportPath = this.Server.MapPath(this.Request.ApplicationPath) +"/" +ConfigurationManager.AppSettings["ReportPath"].ToString() + "/" + rptName;

            //    rptViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds.Tables[0]));
            //    rptViewer.LocalReport.Refresh();
            //}
            //catch {
                
                
            //}
               
               
           
        }
        //private void GenerateDDLVoyage()
        //{
        //    int vesselId = Convert.ToInt32(ddlVessel.SelectedValue);
        //    int LocationId = Convert.ToInt32(ddlLoc.SelectedValue);
        //    int nvoccID = Convert.ToInt32(ddlLine.SelectedValue);
        //    GeneralFunctions.PopulateDropDownList(ddlVoyage, EMS.BLL.VoyageBLL.GetVoyageAccVessel_Loc(LocationId, vesselId, nvoccID));
        //}
        private void ExporttoExcel(DataTable table)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=ExchangeRates.xls");
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
    }
}