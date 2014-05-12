using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EMS.Common;
using EMS.Entity;
using EMS.Utilities;
using EMS.BLL;

namespace EMS.WebApp.Transaction
{
    public partial class ExportPerformanceReport : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateLine();
                PopulateLocation();                
            }
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
            ddlLoc.Items.Insert(0, new ListItem(Constants.DROPDOWNLIST_ALL_TEXT, Constants.DROPDOWNLIST_DEFAULT_VALUE));
        }
        private void PopulateServices(int LineID)
        {
            //List<IService> lstService = new ServiceBLL()

            //ServiceEntity oService = (ServiceEntity)ServiceBLL.GetServiceWithLine(LineID);
            BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            DataSet ds = dbinteract.GetServiceForLine(LineID);

            ddlServices.DataValueField = "fk_ServiceNameID";
            ddlServices.DataTextField = "Servicename";
            ddlServices.DataSource = ds;
            ddlServices.DataBind();
            ddlServices.Items.Insert(0, new ListItem(Constants.DROPDOWNLIST_ALL_TEXT, Constants.DROPDOWNLIST_DEFAULT_VALUE));
        }
        private void GenerateReport()
        {
            ExportPerformanceBLL cls = new ExportPerformanceBLL();
            try
            {
                DateTime dt1 = DateTime.Today;
                DateTime dt2 = DateTime.Today;
                DataTable dtExcel = new DataTable();
                dt1 = Convert.ToDateTime(txtStartDt.Text.Trim());
                dt2 = Convert.ToDateTime(txtEndDt.Text.Trim());



                dtExcel = cls.GetExportPerformanceStatement(Convert.ToInt32(ddlLine.SelectedValue), Convert.ToInt32(ddlLoc.SelectedValue), dt1, dt2, Convert.ToInt32(ddlServices.SelectedValue));

                dtExcel.Columns.Remove("fk_NVOCCID");
                dtExcel.Columns.Remove("fk_MainLineVesselID");
                //dtExcel.Columns.Remove("CustName");
                dtExcel.Columns.Remove("fk_MainLineVoyageID");
                dtExcel.Columns.Remove("ContainerAbbr");
                //dtExcel.Columns.Remove("Nos");
                dtExcel.Columns.Remove("Commodity");
                //dtExcel.Columns.Remove("LineName");
                //dtExcel.Columns.Remove("ServiceName");
                //dtExcel.Columns.Remove("ETANextPort");
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
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Export_Performance_Report.xls");
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
                throw ;
                
            }
        }
        protected void btnShow_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }

        protected void ddlLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateServices(ddlLine.SelectedValue.ToInt());
        }
    }
}