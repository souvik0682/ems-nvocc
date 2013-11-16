using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Utilities;
using EMS.BLL;
using System.Configuration;
using EMS.Utilities.ResourceManager;
using EMS.Entity;
using EMS.Common;
using System.Data;

namespace EMS.WebApp.Reports.Export
{
    public partial class ExpCntrStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void txtBookingNo_TextChanged(object sender, EventArgs e)
        {

            if (txtBookingNo.Text != string.Empty)
            {
                DataSet ds =  new DataSet();
                ds = ReportBLL.GetBooking(txtBookingNo.Text);
                hdnBookingNo.Value = Convert.ToString(ds.Tables[0].Rows[0]["pk_BookingID"]);
            }
            IBooking oIH = BookingBLL.GetBookingById(Convert.ToInt32(hdnBookingNo.Value.Trim()));

            lblBookingParty.Text = oIH.CustomerERAS;
            lblVessel.Text = oIH.VesselName;
            lblVoyage.Text = oIH.VoyageNo;
            lblPOD.Text = oIH.POD;
            lblFPOD.Text = oIH.FPOD;
        }
        private void GenerateReport()
        {
            ExportContainerBLL cls = new ExportContainerBLL();
            try
            {
                DataTable dtExcel = new DataTable();

                dtExcel = cls.GetExportPerformanceStatement(Convert.ToInt64(hdnBookingNo.Value.Trim()));

                //dtExcel.Columns.Remove("fk_NVOCCID");
                //dtExcel.Columns.Remove("fk_MainLineVesselID");
                ////dtExcel.Columns.Remove("CustName");
                //dtExcel.Columns.Remove("fk_MainLineVoyageID");
                //dtExcel.Columns.Remove("ContainerAbbr");
                ////dtExcel.Columns.Remove("Nos");
                //dtExcel.Columns.Remove("Commodity");
                ////dtExcel.Columns.Remove("LineName");
                ////dtExcel.Columns.Remove("ServiceName");
                ////dtExcel.Columns.Remove("ETANextPort");
                ExporttoExcel(dtExcel);
            }
            catch (Exception)
            {
                //Response.Write(ex.Message.ToString());
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
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=ContainerStatus_Report.xls");
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
            catch (Exception ex)
            {
                throw;

            }
        }
        protected void btnShow_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }
    }
}