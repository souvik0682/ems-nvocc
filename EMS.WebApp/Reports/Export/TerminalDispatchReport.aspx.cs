using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EMS.Utilities;
using EMS.BLL;
using EMS.Common;
using EMS.Entity;


namespace EMS.WebApp.Reports.Export
{
    public partial class TerminalDispatchReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateLine();
                PopulateLocation();
                LoadData();
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
            ddlLoc.Items.Insert(0, new ListItem(Constants.DROPDOWNLIST_DEFAULT_TEXT, Constants.DROPDOWNLIST_DEFAULT_VALUE));
        }
        private void GenerateReport()
        {
            TerminalDispatchBLL cls = new TerminalDispatchBLL();
            try
            {               

                DataTable dtExcel = new DataTable();   
                
                //dtExcel = cls.GetTerminalDispatchStatement(Convert.ToInt32(txtVessel.Text), Convert.ToInt64(txtPol.Text), 
                //    Convert.ToInt32(ddlLine.SelectedValue), Convert.ToInt32(ddlLoc.SelectedValue));
               
                
                dtExcel = cls.GetTerminalDispatchStatement(Convert.ToInt32(hdnVessel.Value), Convert.ToInt64(hdnPOL.Value),
                   Convert.ToInt32(ddlLine.SelectedValue), Convert.ToInt32(ddlLoc.SelectedValue));
                dtExcel.Columns.Remove("fk_ExpVesselID");
                dtExcel.Columns.Remove("fk_expVoyageID");
                dtExcel.Columns.Remove("fk_NVOCCID");
                dtExcel.Columns.Remove("fk_MainLineVesselID");
                dtExcel.Columns.Remove("fk_MainLineVoyageID");
                dtExcel.Columns.Remove("fk_SlotId");
                dtExcel.Columns.Remove("LocationName");
                dtExcel.Columns.Remove("LineName");
                dtExcel.Columns.Remove("ServiceName");
                dtExcel.Columns.Remove("ETANextPort");
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
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Terminal_Dispatch_Report.xls");
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
        protected void btnShow_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }
        private void LoadData()
        {
           // ImportHaulageEntity oImportHaulage = (ImportHaulageEntity)ImportHaulageBLL.GetImportHaulage(Convert.ToInt32(hdnVessel.Value));
            //TerminalDispatchEntity oterminaldispatch=TerminalDispatchBLL.GetTerminalDispatchStatement(
           // ImportBLDAL.GetVesselId(hdnVessel.Value);
           // hdnVessel.Value = oImportHaulage.LocationFrom.Substring(oImportHaulage.LocationFrom.IndexOf('|') + 1);
            //txtVessel.Text = oImportHaulage.LocationFrom.Substring(0, oImportHaulage.LocationFrom.IndexOf('|'));
            //ddlFromLocation.SelectedIndex = Convert.ToInt32(ddlFromLocation.Items.IndexOf(ddlFromLocation.Items.FindByValue(oImportHaulage.LocationFrom)));
           // hdnPOL.Value = oImportHaulage.LocationFrom.Substring(oImportHaulage.LocationFrom.IndexOf('|') + 1);
            //txtPol.Text = oImportHaulage.LocationFrom.Substring(0, oImportHaulage.LocationFrom.IndexOf('|'));
            //ddlToLocation.SelectedIndex = Convert.ToInt32(ddlToLocation.Items.IndexOf(ddlToLocation.Items.FindByValue(oImportHaulage.LocationTo)));

            //hdnToLocation.Value = oImportHaulage.LocationTo.Substring(oImportHaulage.LocationTo.IndexOf('|') + 1);
            //txtToLocation.Text = oImportHaulage.LocationTo.Substring(0, oImportHaulage.LocationTo.IndexOf('|'));
            // ddlLine.SelectedValue = oImportHaulage.LinerID.ToString();

            //ddlContainerSize.SelectedIndex = Convert.ToInt32(ddlContainerSize.Items.IndexOf(ddlContainerSize.Items.FindByText(oImportHaulage.ContainerSize)));


            //txtWFrom.Text = Convert.ToString(oImportHaulage.WeightFrom);
            //txtWTo.Text = Convert.ToString(oImportHaulage.WeightTo);
            //txtRate.Text = Convert.ToString(oImportHaulage.HaulageRate);

            //if (oImportHaulage.EffectDate >= Convert.ToDateTime("01/01/1950"))
            //    txtEffectDate.Text = oImportHaulage.EffectDate.ToString("dd/MM/yyyy");
        }
    }
}