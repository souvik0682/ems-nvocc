using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Utilities;
using EMS.BLL;
using EMS.Utilities.ReportManager;
using System.Configuration;
using System.Data;
using EMS.Common;
using Microsoft.Reporting.WebForms;

namespace EMS.WebApp.Reports
{
    public partial class GrRentLOLOReport : System.Web.UI.Page
    {
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        private int _userId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            CheckUserAccess();
            BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            if (!IsPostBack)
            {
                GeneralFunctions.PopulateDropDownList(ddlLine, EMS.BLL.EquipmentBLL.DDLGetLine());
                GeneralFunctions.PopulateDropDownList(ddlLoc, dbinteract.PopulateDDLDS("DSR.dbo.mstLocation", "pk_LocID", "LocAbbr", true), true);
                //GeneralFunctions.PopulateDropDownList(ddlStatus, EMS.BLL.EquipmentBLL.DDLGetStatus());
                //GeneralFunctions.PopulateDropDownList(ddlContainerType, EMS.BLL.EquipmentBLL.DDLGetContainerType());
                ddlEmptyYard.Enabled = false;
                //GeneralFunctions.PopulateDropDownList(ddlEmptyYard, EMS.BLL.EquipmentBLL.DDLGetEmptyYard(ddlLoc.SelectedValue.ToInt()));

                //GenerateReport();
            }

        }
        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();

            //Get user permission.
            UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);
        }

        private void CheckUserAccess()
        {
            if (!_canView)
            {
                Response.Redirect("~/Unauthorized.aspx");
            }
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            ReportBLL cls = new ReportBLL();

            try
            {
                //DateTime dt = Convert.ToDateTime(txtdtStock.Text.Trim());
                DataTable dtExcel = new DataTable();
                dtExcel = cls.GetGroundRentLOLOStatement(ddlLine.SelectedValue, ddlLoc.SelectedValue, ddlStatus.SelectedValue, txtStartDate.Text.Trim(), txtdtStock.Text.Trim(), ddlEmptyYard.SelectedValue.ToInt());
                ExporttoExcel(dtExcel);
            }
            catch (Exception ex)
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
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=ContainerStockDetail.xls");
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

        protected void ddlLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GeneralFunctions.PopulateDropDownList(ddlEmptyYard, EMS.BLL.EquipmentBLL.DDLGetEmptyYard(ddlLoc.SelectedValue.ToInt()));
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlEmptyYard.Enabled = true;
            GeneralFunctions.PopulateDropDownList(ddlEmptyYard, EMS.BLL.EquipmentBLL.DDLGetEmptyYard(ddlLoc.SelectedValue.ToInt()));
            
        }

        protected void ddlLine_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlEmptyYard_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //protected void btnSave_Click(object sender, EventArgs e)
        //{

        //}
    }
}