using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Utilities;
using EMS.Common;
using EMS.BLL;
using System.IO;
using System.Configuration;
using System.Data;
using Microsoft.Reporting.WebForms;

namespace EMS.WebApp.Reports
{
    public partial class AdvanceContainerList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Filler.FillData<ILocation>(ddlLine, new CommonBLL().GetActiveLocation(), "Name", "Id", "Location");
            }
        }
        protected void ddlLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLine.SelectedIndex > 0)
            {
                Filler.FillData(ddlLocation, CommonBLL.GetLine(ddlLine.SelectedValue), "ProspectName", "ProspectID", "Line");
            }
        }


        protected void btnReport_Click(object sender, EventArgs e)
        {
            string fileName = string.Empty;
            FileUtil t = null;
            CommonBLL cls = new CommonBLL();
            DataTable dtExcel = new DataTable();
            dtExcel = cls.GenerateExcel(ddlLine.SelectedValue, ddlVessel.SelectedValue, hdnReturn.Value, ddlLocation.SelectedValue, ddlVoyage.SelectedValue, txtVIANo.Text);
            ExporttoExcel(dtExcel);



            //switch (CommonBLL.GetTerminalType(Convert.ToInt32(ddlVoyage.SelectedValue), Convert.ToInt32(ddlVessel.SelectedValue), Convert.ToInt32(hdnReturn.Value)))
            //{
            //    case "NSICT":
            //        {
            //            CommonBLL cls = new CommonBLL();

            //            try
            //            {
            //                //DateTime dt = Convert.ToDateTime(txtdtStock.Text.Trim());
            //                DataTable dtExcel = new DataTable();

            //                dtExcel = cls.GenerateExcel(ddlLine.SelectedValue, ddlVessel.SelectedValue, hdnReturn.Value, ddlLocation.SelectedValue, ddlVoyage.SelectedValue, txtVIANo.Text);
            //                ExporttoExcel(dtExcel);
            //            }
            //            catch (Exception ex)
            //            {
            //            }
            //        }
 
            //        break;

            //    case "JNPT":
            //         fileName = Server.MapPath("~/Download/ContainerList.txt");

            //        if (File.Exists(fileName))
            //            File.Delete(fileName);

            //        t = new FileUtil(Server.MapPath("~/FileTemplate/ContainerList.txt"), fileName);
            //        if (CommonBLL.GenerateText(fileName, Convert.ToInt32(ddlLine.SelectedValue), Convert.ToInt32(ddlVessel.SelectedValue), Convert.ToInt32(hdnReturn.Value), Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlVoyage.SelectedValue), Convert.ToInt32(txtVIANo.Text)))
            //        {                        
            //            t.Download(Response);
            //        }

            //        break;

            //    case "GTI":
            //        fileName = Server.MapPath("~/Download/ContainerList.txt");

            //        if (File.Exists(fileName))
            //            File.Delete(fileName);

            //        t = new FileUtil(Server.MapPath("~/FileTemplate/ContainerList.txt"), fileName);
            //        if (CommonBLL.GenerateText(fileName, Convert.ToInt32(ddlLine.SelectedValue), Convert.ToInt32(ddlVessel.SelectedValue), Convert.ToInt32(hdnReturn.Value), Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlVoyage.SelectedValue), Convert.ToInt32(txtVIANo.Text)))
            //        {                        
            //            t.Download(Response);
            //        }

            //        break;
            //    default:
            //        {
            //            CommonBLL cls = new CommonBLL();

            //            try
            //            {
            //                DataTable dtExcel = new DataTable();

            //                dtExcel = cls.GenerateExcel(ddlLine.SelectedValue, ddlVessel.SelectedValue, hdnReturn.Value, ddlLocation.SelectedValue, ddlVoyage.SelectedValue, txtVIANo.Text);
            //                ExporttoExcel(dtExcel);
            //            }
            //            catch (Exception ex)
            //            {
            //            }
            //        }
            //        break;
            //}
        }
        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLocation.SelectedIndex > 0)
            {
                Filler.FillData(ddlVessel, CommonBLL.GetVessels(ddlLocation.SelectedValue), "VesselName", "VesselID", "Vessel");
            }
        }

        protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlVessel.SelectedIndex > 0)
            {
                Filler.FillData(ddlVoyage, CommonBLL.GetVoyages(ddlVessel.SelectedValue, ddlLocation.SelectedValue), "VoyageNo", "VoyageID", "Voyage");
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
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=AdvanceContainerList.xls");
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

    }
}