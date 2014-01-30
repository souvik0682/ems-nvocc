using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.BLL;
using EMS.Common;
using EMS.Entity.Report;
using EMS.Utilities;
using EMS.Utilities.ReportManager;
using Microsoft.Reporting.WebForms;

namespace EMS.WebApp.Transaction
{
    public partial class Export_Monthly_Report_Brokerage_Excel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateLine();
                PopulateLocation();
                dropdownReport();
                //LoadData();
            }
        }
        public void dropdownReport()
        {           
            ddlReport.Items.Insert(0, new ListItem(Constants.DROPDOWNLIST_DEFAULT_TEXT, Constants.DROPDOWNLIST_DEFAULT_VALUE));
            ddlReport.Items.Insert(1, new ListItem("Brokerage", "Brokerage"));
            ddlReport.Items.Insert(2, new ListItem("Commission", "Commission"));
            ddlReport.Items.Insert(3, new ListItem("Refund", "Refund"));
            ddlReport.Items.Insert(4, new ListItem("Service", "Service"));          
        }
        private void PopulateLine()
        {
            BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            DataSet ds = dbinteract.GetNVOCCLine(-1, string.Empty);
            ddlLine.DataValueField = "pk_NVOCCID";
            ddlLine.DataTextField = "NVOCCName";
            ddlLine.DataSource = ds;
            ddlLine.DataBind();
            ddlLine.Items.Insert(0, new ListItem(Constants.DROPDOWNLIST_DEFAULT_TEXT, Constants.DROPDOWNLIST_DEFAULT_VALUE));
        }
        private void PopulateLocation()
        {
            List<ILocation> lstLoc = new CommonBLL().GetActiveLocation();

            ddlLoc.DataValueField = "Id";
            ddlLoc.DataTextField = "Name";
            ddlLoc.DataSource = lstLoc;
            ddlLoc.DataBind();
            ddlLoc.Items.Insert(0, new ListItem(Constants.DROPDOWNLIST_DEFAULT_TEXT, Constants.DROPDOWNLIST_DEFAULT_VALUE));
        }
        private void GenerateReport()
        {
            ExportMonthlyReportBLL cls = new ExportMonthlyReportBLL();
            try
            {
                DateTime dt1 = DateTime.Today;
                DateTime dt2 = DateTime.Today;
                DataTable dtExcel = new DataTable();
                dt1 = Convert.ToDateTime(txtStartDt.Text.Trim());
                dt2 = Convert.ToDateTime(txtEndDt.Text.Trim());
                if (hdnVessel.Value == "")
                    hdnVessel.Value = "0";

                int val=0;
                string strFileName = string.Empty;
                if (ddlReport.SelectedValue.ToString() == "Brokerage")
                {
                     val = 1;
                     strFileName = "Export_Monthly_Reports_Brokerage.xls";
                }
                else if (ddlReport.SelectedValue.ToString() == "Commission")
                {
                     val = 2;
                     strFileName = "Export_Monthly_Reports_Comm.xls";
                }
                else if (ddlReport.SelectedValue.ToString() == "Refund")
                {
                     val = 3;
                     strFileName = "Export_Monthly_Reports_Refunds.xls";
                }
                else if (ddlReport.SelectedValue.ToString() == "Service")
                {
                     val = 4;
                     strFileName = "Export_Monthly_Reports_STax.xls";
                }
                dtExcel = cls.GetExportMonthlyReport(Convert.ToInt32(ddlLoc.SelectedValue), Convert.ToInt32(ddlLine.SelectedValue),
                Convert.ToInt32(hdnVessel.Value), Convert.ToInt64(ddlVoyage.SelectedValue), dt1, dt2, val);
                removerows(dtExcel, val);
                #region alldata
                //dtExcel.Columns.Remove("fk_LocationID");
                //dtExcel.Columns.Remove("LOC");
                //dtExcel.Columns.Remove("Line");
                //dtExcel.Columns.Remove("EDGEBkg");
                //dtExcel.Columns.Remove("LineBkg");
                //dtExcel.Columns.Remove("VesselName");
                //dtExcel.Columns.Remove("VoyageNo");
                //dtExcel.Columns.Remove("BLNO");
                //dtExcel.Columns.Remove("ExpBLDate");
                //dtExcel.Columns.Remove("BookingParty");
                //dtExcel.Columns.Remove("AddrName");
                //dtExcel.Columns.Remove("LDVSL");
                //dtExcel.Columns.Remove("LDVOY");
                //dtExcel.Columns.Remove("POR");
                //dtExcel.Columns.Remove("POL");
                //dtExcel.Columns.Remove("POD");
                //dtExcel.Columns.Remove("FPOD");
                //dtExcel.Columns.Remove("TTL20");
                //dtExcel.Columns.Remove("TTL40");
                //dtExcel.Columns.Remove("TEUS");
                //dtExcel.Columns.Remove("actual");
                //dtExcel.Columns.Remove("Manifest");
                //dtExcel.Columns.Remove("Diff");
                //dtExcel.Columns.Remove("BasicFreight");
                //dtExcel.Columns.Remove("BrokeragePercent");
                //dtExcel.Columns.Remove("BrkAmt");
                //dtExcel.Columns.Remove("Shipper");
                //dtExcel.Columns.Remove("Brokerage");
                //dtExcel.Columns.Remove("Commodity");
                //dtExcel.Columns.Remove("Gross");
                //dtExcel.Columns.Remove("Stax");
                //dtExcel.Columns.Remove("InvoiceNo");
                //dtExcel.Columns.Remove("InvoiceDate");
                //dtExcel.Columns.Remove("ROE");
                //dtExcel.Columns.Remove("TotalPayment");
                //dtExcel.Columns.Remove("RefAmt");
                //dtExcel.Columns.Remove("AddrName");
                #endregion
                ExporttoExcel(dtExcel, strFileName);
            }
            catch (Exception)
            {
                //Response.Write(ex.Message.ToString());
            }

        }
        private void ExporttoExcel(DataTable table,string strFilename)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");           
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename="+ strFilename +"");
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
            try
            {
                HttpContext.Current.Response.Flush();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                HttpContext.Current.Response.End();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void removerows(DataTable dtExcel,int val)
        {
            if (val != 0)
            {
                if (val == 1)
                {
                    dtExcel.Columns.Remove("fk_LocationID");
                    dtExcel.Columns.Remove("VesselName");
                    dtExcel.Columns.Remove("VoyageNo");
                    dtExcel.Columns.Remove("ExpBLDate");
                    //dtExcel.Columns.Remove("AddrName");
                    //dtExcel.Columns.Remove("Charged");
                    dtExcel.Columns.Remove("Manifest");
                    dtExcel.Columns.Remove("Diff");
                    //dtExcel.Columns.Remove("Commper");
                    dtExcel.Columns.Remove("Shipper");
                    dtExcel.Columns.Remove("Commodity");
                    dtExcel.Columns.Remove("Gross");
                    dtExcel.Columns.Remove("Stax");
                    dtExcel.Columns.Remove("InvoiceNo");
                    dtExcel.Columns.Remove("InvoiceDate");
                    dtExcel.Columns.Remove("RefAmt");
                    dtExcel.Columns.Remove("RefundTo");
                    dtExcel.Columns.Remove("invCur");
                    dtExcel.Columns.Remove("Brokerage");
                    dtExcel.Columns.Remove("ExpCommPer");
                    dtExcel.Columns.Remove("ExpComm");
                }
                else if (val == 2)
                {
                    dtExcel.Columns.Remove("fk_LocationID");
                    dtExcel.Columns.Remove("VesselName");
                    dtExcel.Columns.Remove("VoyageNo");
                    dtExcel.Columns.Remove("ExpBLDate");
                    dtExcel.Columns.Remove("Brok");
                    //dtExcel.Columns.Remove("actual");
                    dtExcel.Columns.Remove("BasicFreight");
                    //dtExcel.Columns.Remove("BrkgPer");
                    //dtExcel.Columns.Remove("BrkgAmt");
                    dtExcel.Columns.Remove("Brokerage");
                    dtExcel.Columns.Remove("Gross");
                    dtExcel.Columns.Remove("Stax");
                    //dtExcel.Columns.Remove("InvoiceNo");
                    //dtExcel.Columns.Remove("InvoiceDate");
                    dtExcel.Columns.Remove("RefAmt");
                    dtExcel.Columns.Remove("Commodity");
                    dtExcel.Columns.Remove("RefundTo");
                    dtExcel.Columns.Remove("CommPer");
                    dtExcel.Columns.Remove("CommAmt");
                    //dtExcel.Columns.Remove("ReceiptAmt");
                }
                else if (val == 3)
                {
                    dtExcel.Columns.Remove("fk_LocationID");
                    dtExcel.Columns.Remove("VesselName");
                    dtExcel.Columns.Remove("VoyageNo");
                    dtExcel.Columns.Remove("ExpBLDate");
                    dtExcel.Columns.Remove("Brok");
                    //dtExcel.Columns.Remove("Charged");
                    dtExcel.Columns.Remove("BasicFreight");
                    dtExcel.Columns.Remove("CommPer");
                    dtExcel.Columns.Remove("CommAmt");
                    dtExcel.Columns.Remove("Brokerage");
                    dtExcel.Columns.Remove("Shipper");
                    dtExcel.Columns.Remove("Commodity");
                    dtExcel.Columns.Remove("Gross");
                    dtExcel.Columns.Remove("Stax");
                    dtExcel.Columns.Remove("InvoiceNo");
                    dtExcel.Columns.Remove("InvoiceDate");
                    //dtExcel.Columns.Remove("ReceiptAmt");
                    dtExcel.Columns.Remove("ExpCommPer");
                    dtExcel.Columns.Remove("ExpComm");
                    //dtExcel.Columns.Remove("RefundTo");
                }
                else if (val == 4)
                {
                    dtExcel.Columns.Remove("fk_LocationID");
                    dtExcel.Columns.Remove("VesselName");
                    dtExcel.Columns.Remove("VoyageNo");
                    dtExcel.Columns.Remove("ExpBLDate");
                    dtExcel.Columns.Remove("Brok");
                    dtExcel.Columns.Remove("Charged");
                    dtExcel.Columns.Remove("Manifest");
                    dtExcel.Columns.Remove("Diff");
                    dtExcel.Columns.Remove("BasicFreight");
                    dtExcel.Columns.Remove("CommPer");
                    dtExcel.Columns.Remove("CommAmt");
                    dtExcel.Columns.Remove("Shipper");
                    dtExcel.Columns.Remove("Brokerage");
                    dtExcel.Columns.Remove("Commodity");
                    dtExcel.Columns.Remove("ROE");
                    dtExcel.Columns.Remove("RefAmt");
                    dtExcel.Columns.Remove("RefundTo");
                    dtExcel.Columns.Remove("ExpCommPer");
                    dtExcel.Columns.Remove("ExpComm");
                }
            }
        }
        protected void btnShow_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }

        protected void txtVessel_TextChanged(object sender, EventArgs e)
        {
            Filler.FillData(ddlVoyage, CommonBLL.GetExpVoyages(hdnVessel.Value.ToString(), ddlLoc.SelectedValue.ToString()), "VoyageNo", "VoyageID", "Voyage");
        }
    }
}